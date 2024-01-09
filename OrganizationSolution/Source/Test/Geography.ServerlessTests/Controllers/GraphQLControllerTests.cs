using Geography.Serverless.Model;
using Geography.ServerlessTests;
using Geography.ServerlessTests.Models;
using Geography.ServerlessTests.TestData;
using Microsoft.AspNetCore.Mvc;
using Newtonsoft.Json;
using Xunit;
using Assert = Xunit.Assert;
using ErrorResponse = Geography.ServerlessTests.Models.ErrorResponse;

namespace Geography.Service.Tests
{/// <summary>
 /// class GraphQLControllerTests
 /// </summary>
    public class GraphQLControllerTests : XunitMemberDataInput, IClassFixture<TestFixture>
    {
        private GraphQLController _graphQLController;
        private readonly TestFixture _fixture;
        /// <summary>
        /// Constuctor GraphQLControllerTests
        /// </summary>
        public GraphQLControllerTests(TestFixture testFixture)
        {
            _fixture = testFixture;
            _graphQLController = _fixture._graphQLController;

        }
        [Theory]
        [MemberData(nameof(GraphQLModelData))]
        public async Task Get_Countries_Valid_Query(GraphQLModel graphQLModel, CancellationToken cancellationToken = default)
        {
            var controllerResult = await _graphQLController.HandleRequest(graphQLModel, cancellationToken);
            Assert.NotNull(controllerResult);
            Assert.Equal(((ObjectResult)controllerResult).StatusCode, 200);
            var result = JsonConvert.DeserializeObject<ValidQueryResult>(JsonConvert.SerializeObject((((ObjectResult)controllerResult).Value)));
            Assert.NotNull(result);
            Assert.True(result.countries.Any() || result.countries.Count() == 0);
        }
        [Theory]
        [MemberData(nameof(GraphQLModelDatas))]
        public async Task Save_New_Countries_With_Valid_Query(GraphQLModel GetCountry, GraphQLModel AddCountry, CancellationToken cancellationToken = default)
        {
            var getControllerResult = await _graphQLController.HandleRequest(GetCountry, cancellationToken);
            Assert.NotNull(getControllerResult);
            Assert.Equal(((ObjectResult)getControllerResult).StatusCode, 200);
            var getCountryResult = JsonConvert.DeserializeObject<ValidQueryResult>(JsonConvert.SerializeObject((((ObjectResult)getControllerResult).Value)));
            Assert.NotNull(getCountryResult);
            int countryCountBeforeAddingNewCountry = getCountryResult.countries.Count();

            var addcontrollerResult = await _graphQLController.HandleRequest(AddCountry, cancellationToken);
            Assert.NotNull(addcontrollerResult);
            Assert.Equal(((ObjectResult)addcontrollerResult).StatusCode, 200);
            var addCountryResult = JsonConvert.DeserializeObject<CreateCountryResponse>(JsonConvert.SerializeObject((((ObjectResult)addcontrollerResult).Value)));
            Assert.NotNull(addCountryResult);
            Assert.NotNull(addCountryResult.createCountry);
            Assert.NotNull(addCountryResult.createCountry.id);

            var getControllerResultAfterSave = await _graphQLController.HandleRequest(GetCountry, cancellationToken);
            Assert.NotNull(getControllerResultAfterSave);
            Assert.Equal(((ObjectResult)getControllerResultAfterSave).StatusCode, 200);
            var getCountryResultAfterSave = JsonConvert.DeserializeObject<ValidQueryResult>(JsonConvert.SerializeObject((((ObjectResult)getControllerResultAfterSave).Value)));
            Assert.NotNull(getCountryResultAfterSave);
            int countryCountafterAddingNewCountry = getCountryResultAfterSave.countries.Count();

            Assert.True(countryCountafterAddingNewCountry > countryCountBeforeAddingNewCountry);
            Assert.Equal(countryCountafterAddingNewCountry, countryCountBeforeAddingNewCountry + 1);


        }

        [Theory]
        [MemberData(nameof(GraphQLModelDataWithIsoAlreadyExist))]
        public async Task Save_New_Countries_With_ISOCode_Already_Exist(GraphQLModel AddCountry, CancellationToken cancellationToken = default)
        {
            var addcontrollerResult = await _graphQLController.HandleRequest(AddCountry, cancellationToken);
            Assert.NotNull(addcontrollerResult);
            Assert.Equal(((ObjectResult)addcontrollerResult).StatusCode, 400);
            var addCountryResult = JsonConvert.DeserializeObject<ErrorResponse>(JsonConvert.SerializeObject((((ObjectResult)addcontrollerResult).Value)));
            Assert.NotNull(addCountryResult);
            Assert.NotNull(addCountryResult.errors);
            Assert.True(addCountryResult.errors.Any());
            Assert.Equal("Country already exists", addCountryResult.errors[0]);
        }

        [Theory]
        [MemberData(nameof(GraphQLModelDataForUpdate))]
        public async Task Update_Country_With_Valid_Details(GraphQLModel UpdateCountry, CancellationToken cancellationToken = default)
        {
            var controllerResult = await _graphQLController.HandleRequest(UpdateCountry, cancellationToken);
            Assert.NotNull(controllerResult);
            Assert.Equal(((ObjectResult)controllerResult).StatusCode, 200);
            var addCountryResult = JsonConvert.DeserializeObject<UpdateCountryResponse>(JsonConvert.SerializeObject((((ObjectResult)controllerResult).Value)));
            Assert.NotNull(addCountryResult);
            Assert.NotNull(addCountryResult.updateCountry);
            Assert.NotNull(addCountryResult.updateCountry.id);
        }

        [Theory]
        [MemberData(nameof(GraphQLModelDataForUpdate))]
        public async Task Update_Country_With_Invalid_Country_id(GraphQLModel UpdateCountry, CancellationToken cancellationToken = default)
        {
            var controllerResult = await _graphQLController.HandleRequest(UpdateCountry, cancellationToken);
            Assert.NotNull(controllerResult);
            Assert.Equal(((ObjectResult)controllerResult).StatusCode, 400);
            var addCountryResult = JsonConvert.DeserializeObject<ErrorResponse>(JsonConvert.SerializeObject((((ObjectResult)controllerResult).Value)));
            Assert.NotNull(addCountryResult);
            Assert.NotNull(addCountryResult.errors);
            Assert.True(addCountryResult.errors.Any());
            Assert.Equal("Couldn't find country in db.", addCountryResult.errors[0]);
        }

        [Theory]
        [MemberData(nameof(GraphQLModelDelete))]
        public async Task Delete_Country_With_Valid_Country_Id(GraphQLModel DeleteCountry, CancellationToken cancellationToken = default)
        {
            var controllerResult = await _graphQLController.HandleRequest(DeleteCountry, cancellationToken);
            Assert.NotNull(controllerResult);
            Assert.Equal(((ObjectResult)controllerResult).StatusCode, 200);
            var deleteCountryResult = JsonConvert.SerializeObject((((ObjectResult)controllerResult).Value));
            Assert.NotNull(deleteCountryResult);
            Assert.Contains("successfully deleted from db", deleteCountryResult);
        }


        [Theory]
        [MemberData(nameof(GraphQLModelDelete))]
        public async Task Delete_Country_With_Invalid_Contry_Id(GraphQLModel DeleteCountry, CancellationToken cancellationToken = default)
        {
            var controllerResult = await _graphQLController.HandleRequest(DeleteCountry, cancellationToken);
            Assert.NotNull(controllerResult);
            Assert.Equal(((ObjectResult)controllerResult).StatusCode, 400);
            var deleteCountryResult = JsonConvert.DeserializeObject<ErrorResponse>(JsonConvert.SerializeObject((((ObjectResult)controllerResult).Value)));
            Assert.NotNull(deleteCountryResult);
            Assert.NotNull(deleteCountryResult.errors);
            Assert.True(deleteCountryResult.errors.Any());
            Assert.Equal("Couldn't find country in db.", deleteCountryResult.errors[0]);
        } 
    }
}