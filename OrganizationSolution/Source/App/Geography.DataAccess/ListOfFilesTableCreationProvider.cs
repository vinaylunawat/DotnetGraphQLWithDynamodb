using Amazon.DynamoDBv2.Model;
using Amazon.DynamoDBv2;
using Framework.DataAccess.Repository;
using Geography.Entity.Entities;
using Microsoft.Extensions.Logging;
using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;

namespace Geography.DataAccess
{
    public class ListOfFilesTableCreationProvider : DynamoDBClientProviderBase

    {

        private readonly ILogger<DynamoDBClientProviderBase> _logger;

        private readonly IAmazonDynamoDB _client;

        private const string TableName = "ListOfFiles";

        public ListOfFilesTableCreationProvider(ILogger<DynamoDBClientProviderBase> logger, IAmazonDynamoDB amazonDynamoDBClient)

            : base(logger, amazonDynamoDBClient)

        {

            _logger = logger;

            _client = amazonDynamoDBClient;

        }

        public override async Task CreateTable()

        {

            ListOfFiles listOfFiles;

            var request = new CreateTableRequest

            {

                TableName = TableName,

                AttributeDefinitions = new List<AttributeDefinition>()

                {

                    new AttributeDefinition

                    {

                            AttributeName = nameof(listOfFiles.Id),

                            AttributeType = ScalarAttributeType.S

                    },

                },

                KeySchema = new List<KeySchemaElement>()

                    {

                         new KeySchemaElement

                         {

                                AttributeName = nameof(ListOfFiles.Id),

                                KeyType =KeyType.HASH

                            },
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