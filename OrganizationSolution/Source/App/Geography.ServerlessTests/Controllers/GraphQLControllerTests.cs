using Geography.Serverless.Model;
using Geography.ServerlessTests;
using Geography.ServerlessTests.Models;
using GraphQL;
using GraphQL.Types;
using Microsoft.AspNetCore.Mvc;
using Microsoft.Extensions.Logging;
using Newtonsoft.Json;
using Xunit;
using Assert = Xunit.Assert;

namespace Geography.Service.Tests
{
    /// <summary>
    /// class GraphQLControllerTests
    /// </summary>
    public class GraphQLControllerTests
    {
        private GraphQLController _graphQLController;
        private ISchema _schema;
        private ILogger<GraphQLController> _logger;
        private IDocumentExecuter _documentExecuter;
        /// <summary>
        /// Constuctor GraphQLControllerTests
        /// </summary>
        public GraphQLControllerTests()
        {
            string[] args = new string[0];
            // IDocumentExecuter documentExecuter, ILogger<GraphQLController> logger
            TestSetup<ISchema> testSetup = new TestSetup<ISchema>();
            _schema = testSetup.Main(args).GetAwaiter().GetResult();

            TestSetup<IDocumentExecuter> testSetup1 = new TestSetup<IDocumentExecuter>();
            _documentExecuter = testSetup1.Main(args).GetAwaiter().GetResult();

            TestSetup<ILogger<GraphQLController>> testSetup2 = new TestSetup<ILogger<GraphQLController>>();
            _logger = testSetup2.Main(args).GetAwaiter().GetResult();

            _graphQLController = new GraphQLController(_schema, _documentExecuter, _logger);

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
            Assert.True(result.countries.Any() );
        }
        public static IEnumerable<object[]> GraphQLModelData() =>
        new List<GraphQLModel[]>
            {
                    new GraphQLModel[]
                    {
                        new GraphQLModel{ Query="query countries { countries { id name isoCode } }",
                                          Variables=null
                        }
                    }

            };
    }
}