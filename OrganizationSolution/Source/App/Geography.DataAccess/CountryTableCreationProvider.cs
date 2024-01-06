using Amazon.DynamoDBv2.Model;
using Amazon.DynamoDBv2;
using Framework.DataAccess.Repository;
using Microsoft.Extensions.Logging;
using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;
using Geography.Entity.Entities;

namespace Geography.DataAccess
{
    public class CountryTableCreationProvider : DynamoDBClientProviderBase
    {
        private readonly ILogger<DynamoDBClientProviderBase> _logger;
        private readonly IAmazonDynamoDB _client;
        private const string TableName = "Country";

        public CountryTableCreationProvider(ILogger<DynamoDBClientProviderBase> logger, IAmazonDynamoDB amazonDynamoDBClient)
            : base(logger, amazonDynamoDBClient)
        {
            _logger = logger;
            _client = amazonDynamoDBClient;
        }

        public override async Task CreateTable()
        {
            Country country;
            var request = new CreateTableRequest
            {
                TableName = TableName,
                AttributeDefinitions = new List<AttributeDefinition>()
                    {
                        new AttributeDefinition
                        {
                                AttributeName = nameof(country.Id),
                                AttributeType = ScalarAttributeType.S
                        },
                        //new AttributeDefinition
                        //{
                        //        AttributeName =nameof(employee.Name),
                        //        AttributeType =ScalarAttributeType.S
                        //}
                    },
                KeySchema = new List<KeySchemaElement>()
                        {
                             new KeySchemaElement
                             {
                                    AttributeName = nameof(country.Id),
                                    KeyType =KeyType.HASH
                                },
                             //new KeySchemaElement
                             //   {
                             //       AttributeName = nameof(employee.Name),
                             //       KeyType =KeyType.RANGE
                             //   },
                        },
                ProvisionedThroughput = new ProvisionedThroughput
                {
                    ReadCapacityUnits = 10,
                    WriteCapacityUnits = 5
                }
            };
            await _client.CreateTableAsync(request);
        }
    }
}
