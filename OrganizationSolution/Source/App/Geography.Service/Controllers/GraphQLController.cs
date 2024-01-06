﻿using GraphQL;
using GraphQL.Execution;
using GraphQL.Types;
using Microsoft.AspNetCore.Http;
using Microsoft.AspNetCore.Mvc;
using Microsoft.Extensions.Logging;
using System.Linq;
using System.Threading;
using System.Threading.Tasks;

namespace Geography.Service
{
    [Route("api/graphql")]
    [ApiController]
    //[Microsoft.AspNetCore.Authorization.Authorize(AccountConstants.AllUsers)]
    public class GraphQLController : ControllerBase
    {
        private readonly IDocumentExecuter _documentExecuter;
        private readonly ISchema _schema;
        private readonly ILogger<GraphQLController> _logger;

        public GraphQLController(ISchema schema, IDocumentExecuter documentExecuter, ILogger<GraphQLController> logger)
        {
            _schema = schema;
            _documentExecuter = documentExecuter;
            _logger = logger;
        }

        [HttpPost]
        [ProducesResponseType(typeof(ProblemDetails), StatusCodes.Status200OK)]
        [ProducesResponseType(typeof(ProblemDetails), StatusCodes.Status400BadRequest)]
        [ProducesResponseType(typeof(ProblemDetails), StatusCodes.Status500InternalServerError)]
        public async Task<IActionResult> HandleRequest([FromBody] GraphQLModel query, CancellationToken cancellationToken)
        {
            if (query == null)
            {
                _logger.LogWarning("Empty GraphQL Query");
                return BadRequest();
            }

            var timedToken = GetTimedToken(cancellationToken);
            var executionOptions = new ExecutionOptions
            {
                Variables = query.Variables?.ToObject<Inputs>(),
                Schema = _schema,
                Query = query.Query,
                CancellationToken = timedToken,
                EnableMetrics = false,
                //UserContext = new GraphQLRequestContext(User, Request),
                //ComplexityConfiguration = new ComplexityConfiguration
                //{
                //    MaxDepth = 3
                //}
            };

            var result = await _documentExecuter.ExecuteAsync(executionOptions);

            if (result.Errors?.Count > 0)
            {
                _logger.LogWarning("Error in GraphQL Query Excecution {@Data}", new { result.Errors });

                return BadRequest(new { errors = result.Errors.Select(j => j.Message) });
            }

            _logger.LogInformation("GraphQL Query Excection Successful {@Data}", new { query });
            return Ok(((ExecutionNode)result.Data).ToValue());
        }

        private static CancellationToken GetTimedToken(CancellationToken cancellationToken)
        {
            var timedToken = new CancellationTokenSource(5000);
            var joinedToken = CancellationTokenSource.CreateLinkedTokenSource(cancellationToken, timedToken.Token);
            return timedToken.Token;
        }
    }
}
