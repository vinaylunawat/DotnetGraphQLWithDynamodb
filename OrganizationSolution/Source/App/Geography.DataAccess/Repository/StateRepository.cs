namespace Geography.DataAccess.Repository
{
    using Amazon.DynamoDBv2;
    using Amazon.DynamoDBv2.DataModel;
    using Framework.DataAccess.Repository;
    using Geography.Entity.Entities;

    /// <summary>
    /// Defines the <see cref="StateRepository" />.
    /// </summary>
    public class StateRepository : GenericRepository<State>, IStateRepository
    {
        private readonly IAmazonDynamoDB _client;
        public StateRepository(IDynamoDBContext dbContext, IAmazonDynamoDB client) : base(dbContext, client)
        {
            _client = client;
        }                 
    }
}
