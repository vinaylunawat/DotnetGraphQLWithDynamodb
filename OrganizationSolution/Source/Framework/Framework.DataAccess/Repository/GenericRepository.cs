namespace Framework.DataAccess.Repository
{
    using Amazon.DynamoDBv2.DataModel;
    using Amazon.DynamoDBv2.DocumentModel;
    using System.Collections.Generic;
    using System.Threading;
    using System.Threading.Tasks;

    public abstract class GenericRepository<TEntity> : RepositoryBase<TEntity>, IGenericRepository<TEntity>
        where TEntity : class
    {
        public GenericRepository(IDynamoDBContext context) : base(context)
        {
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

        public async Task<TEntity> CreateAsync(TEntity entity, CancellationToken cancellationToken)
        {
            await _dynamoDBContext.SaveAsync(entity, cancellationToken);
            return entity;
        }

        public async Task DeleteAsync<TKey>(TKey key, CancellationToken cancellationToken)
        {
            await _dynamoDBContext.DeleteAsync<TEntity>(key, cancellationToken);
        }
        public async Task UpdateAsync(TEntity entity, CancellationToken cancellationToken)
        {
            await _dynamoDBContext.SaveAsync(entity, cancellationToken);
        }
    }
}
