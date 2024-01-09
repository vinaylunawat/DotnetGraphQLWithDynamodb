namespace Geography.DataAccess.Repository
{
    using Framework.DataAccess.Repository;
    using Geography.Entity.Entities;
    using System.Threading.Tasks;

    /// <summary>
    /// Defines the <see cref="ICountryRepository" />.
    /// </summary>
    public interface ICountryRepository : IGenericRepository<Country>
    {
        Task<bool> SaveTransactionData(Country country);

        Task<bool> UpdateTransactionData(Country country);
    }
}
