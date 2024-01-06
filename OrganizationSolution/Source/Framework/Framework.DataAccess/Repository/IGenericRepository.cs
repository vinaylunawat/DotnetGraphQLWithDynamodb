namespace Framework.DataAccess.Repository
{
    using System.Collections.Generic;
    using System.Threading;
    using System.Threading.Tasks;

    public interface IGenericRepository<TEntity>
    {
        Task<IEnumerable<TEntity>> GetAll(CancellationToken cancellationToken);

        Task<TEntity> GetByKey<TKey>(TKey Id, CancellationToken cancellationToken);

        Task<TEntity> GetByKey<TKey, TRangeKey>(TKey key, TRangeKey rangeKey, CancellationToken cancellationToken);

        Task<TEntity> CreateAsync(TEntity entity, CancellationToken cancellationToken);
        Task UpdateAsync(TEntity entity, CancellationToken cancellationToken);

        Task DeleteAsync<TKey>(TKey key, CancellationToken cancellationToken);
    }
}
