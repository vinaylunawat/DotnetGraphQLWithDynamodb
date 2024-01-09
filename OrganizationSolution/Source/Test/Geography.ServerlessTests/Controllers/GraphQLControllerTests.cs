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
        [MemberData(nameof(GraphQLModelDataForCreate))]
        public async Task Save_New_Country_With_Valid_Query(GraphQLModel GetCountry, GraphQLModel AddCountry, CancellationToken cancellationToken = default)
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
        [MemberData(nameof(GraphQLModelDataForDelete))]
        public async Task Delete_Country_With_Valid_Query(GraphQLModel GetCountry, GraphQLModel AddCountry, GraphQLModel DeleteCountry, CancellationToken cancellationToken = default)
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

            DeleteCountry.Variables["countryId"] = addCountryResult.createCountry.id;

            var controllerResult = await _graphQLController.HandleRequest(DeleteCountry, cancellationToken);
            Assert.NotNull(controllerResult);
            Assert.Equal(((ObjectResult)controllerResult).StatusCode, 200);
            var deleteCountryResult = JsonConvert.SerializeObject((((ObjectResult)controllerResult).Value));
            Assert.NotNull(deleteCountryResult);
            Assert.Contains("successfully deleted from db", deleteCountryResult);

            var getControllerAfterDeleteResult = await _graphQLController.HandleRequest(GetCountry, cancellationToken);
            Assert.NotNull(getControllerAfterDeleteResult);
            Assert.Equal(((ObjectResult)getControllerAfterDeleteResult).StatusCode, 200);
            var getCountryAfterDeleteResult = JsonConvert.DeserializeObject<ValidQueryResult>(JsonConvert.SerializeObject((((ObjectResult)getControllerAfterDeleteResult).Value)));
            Assert.NotNull(getCountryAfterDeleteResult);
            int countryCountAfterDelete = getCountryAfterDeleteResult.countries.Count();
            Assert.Equal(countryCountAfterDelete, countryCountBeforeAddingNewCountry);
        }

        [Theory]
        [MemberData(nameof(GraphQLModelDataForUpdate))]
        public async Task Update_Country_With_Valid_Query(GraphQLModel GetCountry, GraphQLModel AddCountry, GraphQLModel UpdateCountry, GraphQLModel DeleteCountry, CancellationToken cancellationToken = default)
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

            var variablesData = UpdateCountry.Variables["country"];
            ((Dictionary<string, string>)variablesData)["id"] = addCountryResult.createCountry.id;
            UpdateCountry.Variables["country"] = variablesData;
            var controllerUpdateResult = await _graphQLController.HandleRequest(UpdateCountry, cancellationToken);
            Assert.NotNull(controllerUpdateResult);
            Assert.Equal(((ObjectResult)controllerUpdateResult).StatusCode, 200);
            var updateCountryResult = JsonConvert.DeserializeObject<UpdateCountryResponse>(JsonConvert.SerializeObject((((ObjectResult)controllerUpdateResult).Value)));
            Assert.NotNull(updateCountryResult);
            Assert.NotNull(updateCountryResult.updateCountry);
            Assert.NotNull(updateCountryResult.updateCountry.id);
            var updateData = ((Dictionary<string, string>)variablesData);
            Assert.Equal(updateCountryResult.updateCountry.id, updateData["id"]);
            Assert.Equal(updateCountryResult.updateCountry.name, updateData["name"]);
            Assert.Equal(updateCountryResult.updateCountry.isoCode, updateData["isoCode"]);
            //Assert.Equal(updateCountryResult.updateCountry.continent, updateData["continent"]);


            DeleteCountry.Variables["countryId"] = addCountryResult.createCountry.id;
            var controllerResult = await _graphQLController.HandleRequest(DeleteCountry, cancellationToken);
            Assert.NotNull(controllerResult);
            Assert.Equal(((ObjectResult)controllerResult).StatusCode, 200);
            var deleteCountryResult = JsonConvert.SerializeObject((((ObjectResult)controllerResult).Value));
            Assert.NotNull(deleteCountryResult);
            Assert.Contains("successfully deleted from db", deleteCountryResult);

            var getControllerAfterDeleteResult = await _graphQLController.HandleRequest(GetCountry, cancellationToken);
            Assert.NotNull(getControllerAfterDeleteResult);
            Assert.Equal(((ObjectResult)getControllerAfterDeleteResult).StatusCode, 200);
            var getCountryAfterDeleteResult = JsonConvert.DeserializeObject<ValidQueryResult>(JsonConvert.SerializeObject((((ObjectResult)getControllerAfterDeleteResult).Value)));
            Assert.NotNull(getCountryAfterDeleteResult);
            int countryCountAfterDelete = getCountryAfterDeleteResult.countries.Count();
            Assert.Equal(countryCountAfterDelete, countryCountBeforeAddingNewCountry);
        }

        [Theory]
        [MemberData(nameof(GraphQLModelDeleteInvalidData))]
        public async Task Delete_Country_With_Invalid_Contry_Id(GraphQLModel DeleteCountry, CancellationToken cancellationToken = default)
        {
            DeleteCountry.Variables["countryId"] = Guid.NewGuid().ToString();
            var controllerResult = await _graphQLController.HandleRequest(DeleteCountry, cancellationToken);
            Assert.NotNull(controllerResult);
            Assert.Equal(((ObjectResult)controllerResult).StatusCode, 400);
            var deleteCountryResult = JsonConvert.DeserializeObject<ErrorResponse>(JsonConvert.SerializeObject((((ObjectResult)controllerResult).Value)));
            Assert.NotNull(deleteCountryResult);
            Assert.NotNull(deleteCountryResult.errors);
            Assert.True(deleteCountryResult.errors.Any());
            Assert.Equal("Couldn't find country in db.", deleteCountryResult.errors[0]);
        }

        //[Theory]
        //[MemberData(nameof(GraphQLModelInvalidUpdate))]
        //public async Task Update_Country_With_Invalid_Country_id(GraphQLModel UpdateCountry, CancellationToken cancellationToken = default)
        //{
        //    var controllerResult = await _graphQLController.HandleRequest(UpdateCountry, cancellationToken);
        //    Assert.NotNull(controllerResult);
        //    Assert.Equal(((ObjectResult)controllerResult).StatusCode, 400);
        //    var addCountryResult = JsonConvert.DeserializeObject<ErrorResponse>(JsonConvert.SerializeObject((((ObjectResult)controllerResult).Value)));
        //    Assert.NotNull(addCountryResult);
        //    Assert.NotNull(addCountryResult.errors);
        //    Assert.True(addCountryResult.errors.Any());
        //    Assert.Contains(addCountryResult.errors[0],"Couldn't find country in db.");
        //}
        //[Theory]
        //[MemberData(nameof(GraphQLModelDataWithIsoAlreadyExist))]
        //public async Task Save_New_Countries_With_ISOCode_Already_Exist(GraphQLModel AddCountry, CancellationToken cancellationToken = default)
        //{
        //    var addcontrollerResult = await _graphQLController.HandleRequest(AddCountry, cancellationToken);
        //    Assert.NotNull(addcontrollerResult);
        //    Assert.Equal(((ObjectResult)addcontrollerResult).StatusCode, 400);
        //    var addCountryResult = JsonConvert.DeserializeObject<ErrorResponse>(JsonConvert.SerializeObject((((ObjectResult)addcontrollerResult).Value)));
        //    Assert.NotNull(addCountryResult);
        //    Assert.NotNull(addCountryResult.errors);
        //    Assert.True(addCountryResult.errors.Any());
        //    Assert.Equal("Country already exists", addCountryResult.errors[0]);
        //}
    }
}