namespace Framework.DataAccess.Repository
{
    using Amazon.DynamoDBv2;
    using Amazon.DynamoDBv2.DataModel;
    using Amazon.DynamoDBv2.DocumentModel;
    using Amazon.DynamoDBv2.Model;
    using Framework.DataAccess.Model;
    using LinqKit;
    using Microsoft.IdentityModel.Tokens;
    using System.Collections.Generic;
    using System.Linq;
    using System.Threading;
    using System.Threading.Tasks;

    public abstract class GenericRepository<TEntity> : RepositoryBase<TEntity>, IGenericRepository<TEntity>
        where TEntity : class
    {
        private readonly IAmazonDynamoDB _client;
        public GenericRepository(IDynamoDBContext context, IAmazonDynamoDB client) : base(context)
        {
            _client = client;
        }

        public async Task<IEnumerable<TEntity>> GetAll(CancellationToken cancellationToken)
        {
            return await _dynamoDBContext.ScanAsync<TEntity>(default).GetRemainingAsync(cancellationToken);
        }

        public async Task<TEntity> GetByKey<TKey>(TKey key, CancellationToken cancellationToken)
        {
            return await _dynamoDBContext.LoadAsync<TEntity>(key, cancellationToken);
        }

        public async Task<TEntity> GetByKey<TKey, TRangeKey>(TKey key, TRangeKey rangeKey, CancellationToken cancellationToken)
        {
            return await _dynamoDBContext.LoadAsync<TEntity>(key, rangeKey, cancellationToken);
        }

        public async Task<PagedResultModel<TEntity>> GetPaginatedScanItemsAsync(int pageSize = 5, string lastEvaluatedKey = null, IEnumerable<string> projectionFields = default) 
        {
            Dictionary<string, AttributeValue> lastKeyEvaluated = !lastEvaluatedKey.IsNullOrEmpty() ? new Dictionary<string, AttributeValue> { { "Id", new AttributeValue(lastEvaluatedKey) } } : null;
            
            var request = new ScanRequest
            {
                TableName = typeof(TEntity).Name,
                ExclusiveStartKey = lastKeyEvaluated,
                Limit = pageSize,
            };

            if(projectionFields != null && projectionFields.Any())
            {
                request.ProjectionExpression = string.Join(",", projectionFields.Select(a => $"#{a}"));
                var attrNames = new Dictionary<string, string>();
                projectionFields.ForEach(a => attrNames.Add($"#{a}", a));
                request.ExpressionAttributeNames = attrNames;
            }

            var response = await _client.ScanAsync(request);

            var items = response.Items.Select(a =>
            {
                var doc = Document.FromAttributeMap(a);
                return _dynamoDBContext.FromDocument<TEntity>(doc);
            }).ToList();


            var hasMorePages = response.LastEvaluatedKey != null;

            return new PagedResultModel<TEntity>(items, hasMorePages ? response.LastEvaluatedKey["Id"].S : null, hasMorePages);
        }

        public async Task<TEntity> CreateAsync(TEntity entity, CancellationToken cancellationToken)
        {
            await _dynamoDBContext.SaveAsync(entity, cancellationToken);
            return entity;
        }

        public async Task DeleteAsync<TKey>(TKey key, CancellationToken cancellationToken)
        {
            await _dynamoDBContext.DeleteAsync<TEntity>(key, cancellationToken);
        }
        public async Task<TEntity> UpdateAsync(TEntity entity, CancellationToken cancellationToken)
        {
            await _dynamoDBContext.SaveAsync(entity, cancellationToken);
            return entity;
        }
    }
}
