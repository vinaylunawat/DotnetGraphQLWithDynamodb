namespace Geography.DataAccess.Repository
{
    using Framework.DataAccess.Repository;
    using Geography.Entity.Entities;
    using System.Collections.Generic;
    using System.Threading.Tasks;

    /// <summary>
    /// Defines the <see cref="ICountryRepository" />.
    /// </summary>
    public interface ICountryRepository : IGenericRepository<Country>
    {
        Task RemoveCountryWithDependency(long countryId, IEnumerable<long> stateIds);
    }
}
