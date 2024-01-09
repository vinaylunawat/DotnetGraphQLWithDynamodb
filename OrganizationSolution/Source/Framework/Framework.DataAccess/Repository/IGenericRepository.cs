namespace Framework.DataAccess.Repository
{
    using Framework.DataAccess.Model;
    using System.Collections.Generic;
    using System.Threading;
    using System.Threading.Tasks;

    public interface IGenericRepository<TEntity>
    {
        Task<IEnumerable<TEntity>> GetAll(CancellationToken cancellationToken);

        Task<TEntity> GetByKey<TKey>(TKey Id, CancellationToken cancellationToken);

        Task<TEntity> GetByKey<TKey, TRangeKey>(TKey key, TRangeKey rangeKey, CancellationToken cancellationToken);

        Task<PagedResultModel<TEntity>> GetPaginatedScanItemsAsync(int pageSize = 5, string lastEvaluatedKey = null, IEnumerable<string> projectionFields = default);

        Task<TEntity> CreateAsync(TEntity entity, CancellationToken cancellationToken);
        Task<TEntity> UpdateAsync(TEntity entity, CancellationToken cancellationToken);

        Task DeleteAsync<TKey>(TKey key, CancellationToken cancellationToken);

    }
}
