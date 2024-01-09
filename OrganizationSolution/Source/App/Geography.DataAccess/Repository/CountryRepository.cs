namespace Geography.DataAccess.Repository
{
    using Amazon.DynamoDBv2;
    using Amazon.DynamoDBv2.DataModel;
    using Amazon.DynamoDBv2.Model;
    using Amazon.Util;
    using AutoMapper;
    using Entity.Entities;
    using Framework.DataAccess.Repository;
    using Microsoft.IdentityModel.Tokens;
    using System;
    using System.Collections.Generic;
    using System.Threading.Tasks;

    /// <summary>
    /// Defines the <see cref="CountryRepository" />.
    /// </summary>
    public class CountryRepository : GenericRepository<Country>, ICountryRepository
    {
        private readonly IAmazonDynamoDB _client;
        private readonly IMapper _mapper;
        public CountryRepository(IDynamoDBContext context, IAmazonDynamoDB client, IMapper mapper) : base(context, client)
        {
            _client = client;
            _mapper = mapper;
        }

        public async Task<bool> SaveTransactionData(Country country)
        {
            //Create Country
            string countryId = Convert.ToString(country.Id);
            var countryItem = new Dictionary<string, AttributeValue>
            {
                { "Id" , new AttributeValue(countryId) },
                { "Name" , new AttributeValue(country.Name) },
                { "IsoCode", country.IsoCode.IsNullOrEmpty() ? new AttributeValue { NULL = true } : new AttributeValue(country.IsoCode) },
                { "Continent", new AttributeValue(country.Continent) },
                { "UpdatedDate", new AttributeValue(DateTime.UtcNow.ToString(AWSSDKUtils.ISO8601DateFormat)) },
            };

            var createCountry = new Put()
            {
                TableName = "Country",
                Item = countryItem,
                ReturnValuesOnConditionCheckFailure = Amazon.DynamoDBv2.ReturnValuesOnConditionCheckFailure.ALL_OLD,
            };
          

            var actions = new List<TransactWriteItem>()
            {
                 new TransactWriteItem() {Put = createCountry },
            };

            var stateData = new List<Put>();

            foreach (var state in country.States)
            {
                stateData.Add(new Put()
                {
                    TableName = "State",
                    Item = new Dictionary<string, AttributeValue>
                    {
                        { "Id" , new AttributeValue{ S = Convert.ToString(Guid.NewGuid()) } },
                        { "Name" , new AttributeValue{ S = state.Name } },
                        { "CountryId", new AttributeValue{S = countryId } },
                    },
                    ReturnValuesOnConditionCheckFailure = Amazon.DynamoDBv2.ReturnValuesOnConditionCheckFailure.ALL_OLD,
                });
            }

            foreach (var put in stateData)
            {
                actions.Add(new TransactWriteItem() { Put = put });
            }

            var transaction = new TransactWriteItemsRequest()
            {
                TransactItems = actions,
                ReturnConsumedCapacity = ReturnConsumedCapacity.TOTAL,

            };

            var result = false;

            try
            {
                var response = await _client.TransactWriteItemsAsync(transaction);
                result = response?.HttpStatusCode == System.Net.HttpStatusCode.OK;
            }
            catch (ResourceNotFoundException rnf)
            {
                Console.Error.WriteLine("One of the table involved in the transaction is not found" + rnf.Message);
            }
            catch (InternalServerErrorException ise)
            {
                Console.Error.WriteLine("Internal Server Error" + ise.Message);
            }
            catch (TransactionCanceledException tce)
            {
                Console.Error.WriteLine("Transaction Canceled " + tce.Message);
            }

            return result;
        }

        public async Task<bool> UpdateTransactionData(Country country)
        {
            string countryId = country.Id.ToString();

            var countryKey = new Dictionary<string, AttributeValue>
            {
                { "Id", new AttributeValue(countryId) }
            };

            //var checkCountryValid = new ConditionCheck()
            //{
            //    TableName = "Country",
            //    Key = countryKey,
            //    ConditionExpression = "attribute_exists(Id)"
            //};

            //Update Country

            var expressionAttributeName = new Dictionary<string, string>
            {
                { "#name", "Name" },
                { "#isoCode", "IsoCode" },
                { "#continent", "Continent"}
            };

            var expressionAttributeValues = new Dictionary<string, AttributeValue>
            {
                { ":Name", new AttributeValue(country.Name) },
                { ":IsoCode", country.IsoCode.IsNullOrEmpty() ? new AttributeValue { NULL = true } : new AttributeValue(country.IsoCode) },
                { ":Continent", new AttributeValue(country.Continent) },
                 //{ ":expected_Id", new AttributeValue(countryId) }
            };

            var updateCountry = new Update()
            {
                TableName = "Country",
                Key = countryKey,
                UpdateExpression = "SET #name = :Name, #isoCode = :IsoCode, #continent = :Continent",
                ExpressionAttributeNames = expressionAttributeName,
                ExpressionAttributeValues = expressionAttributeValues,
                //ConditionExpression = "Id = :expected_Id",
                ReturnValuesOnConditionCheckFailure = Amazon.DynamoDBv2.ReturnValuesOnConditionCheckFailure.ALL_OLD,
            };


            var actions = new List<TransactWriteItem>()
            {
                 new TransactWriteItem() {Update = updateCountry },                 
            };

            //var stateData = new List<Put>();

            //foreach (var state in country.States)
            //{
            //    stateData.Add(new Put()
            //    {
            //        TableName = "State",
            //        Item = new Dictionary<string, AttributeValue>
            //        {
            //            { "Id" , new AttributeValue{ S = Convert.ToString(Guid.NewGuid()) } },
            //            { "Name" , new AttributeValue{ S = state.Name } },
            //            { "CountryId", new AttributeValue{S = countryId } },
            //        },
            //        ReturnValuesOnConditionCheckFailure = Amazon.DynamoDBv2.ReturnValuesOnConditionCheckFailure.ALL_OLD,
            //    });
            //}

            //foreach (var put in stateData)
            //{
            //    actions.Add(new TransactWriteItem() { Put = put });
            //}

            var transaction = new TransactWriteItemsRequest()
            {
                TransactItems = actions,
                ReturnConsumedCapacity = ReturnConsumedCapacity.TOTAL
            };

            var result = false;
            try
            {
                var response = await _client.TransactWriteItemsAsync(transaction);
                result = response?.HttpStatusCode == System.Net.HttpStatusCode.OK;
            }
            catch (ResourceNotFoundException rnf)
            {
                Console.Error.WriteLine("One of the table involved in the transaction is not found" + rnf.Message);
            }
            catch (InternalServerErrorException ise)
            {
                Console.Error.WriteLine("Internal Server Error" + ise.Message);
            }
            catch (TransactionCanceledException tce)
            {
                Console.Error.WriteLine("Transaction Canceled " + tce.Message);
            }
            return result;
        }


        //public async Task SaveTransactionData(Country country, IEnumerable<State> states)
        //{
        //    //Create Country
        //    string countryId = Convert.ToString(country.Id);
        //    var countryItem = new Dictionary<string, AttributeValue>
        //    {
        //        { "Id" , new AttributeValue(countryId) },
        //        { "Name" , new AttributeValue(country.Name) },
        //        { "IsoCode", new AttributeValue(country.IsoCode) },
        //    };

        //    var createCountry = new Put()
        //    {
        //        TableName = "Country",
        //        Item = countryItem,
        //        ReturnValuesOnConditionCheckFailure = Amazon.DynamoDBv2.ReturnValuesOnConditionCheckFailure.ALL_OLD,
        //        // ConditionExpression = "attribute_not_exists(Id)"
        //    };

        //    //Create state            
        //    var stateItem = new Dictionary<string, AttributeValue>
        //    {
        //        { "Id" , new AttributeValue(Convert.ToString(Guid.NewGuid())) },
        //        { "CountryId" , new AttributeValue(countryId) },
        //        { "Name", new AttributeValue(states.First().Name) },
        //    };

        //    var createstate = new Put()
        //    {
        //        TableName = "State",
        //        Item = stateItem,
        //        ReturnValuesOnConditionCheckFailure = Amazon.DynamoDBv2.ReturnValuesOnConditionCheckFailure.ALL_OLD,
        //        // ConditionExpression = "attribute_not_exists(EmployeeId)"
        //    };

        //    var actions = new List<TransactWriteItem>()
        //    {
        //         new TransactWriteItem() {Put = createCountry },
        //         new TransactWriteItem() {Put = createstate }
        //    };

        //    var placeOrderTransaction = new TransactWriteItemsRequest()
        //    {
        //        TransactItems = actions,
        //        ReturnConsumedCapacity = ReturnConsumedCapacity.TOTAL
        //    };
        //    try
        //    {
        //        _ = await _client.TransactWriteItemsAsync(placeOrderTransaction);
        //        Console.WriteLine("Transaction Successful");
        //    }
        //    catch (ResourceNotFoundException rnf)
        //    {
        //        Console.Error.WriteLine("One of the table involved in the transaction is not found" + rnf.Message);
        //    }
        //    catch (InternalServerErrorException ise)
        //    {
        //        Console.Error.WriteLine("Internal Server Error" + ise.Message);
        //    }
        //    catch (TransactionCanceledException tce)
        //    {
        //        Console.Error.WriteLine("Transaction Canceled " + tce.Message);
        //    }
        //}
    }
}
