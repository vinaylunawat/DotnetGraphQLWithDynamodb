namespace Geography.DataAccess.Repository
{
    using Framework.Configuration.Models;
    using Framework.DataAccess.Repository;
    using Geography.Entity.Entities;
    using System.IO;
    using System.Threading.Tasks;
    using static Org.BouncyCastle.Math.EC.ECCurve;

    /// <summary>
    /// Defines the <see cref="ICountryRepository" />.
    /// </summary>
    public interface ICountryRepository : IGenericRepository<Country>
    {
        Task<bool> SaveTransactionData(Country country);

        Task<bool> UpdateTransactionData(Country country);

        Task<bool> GetDetailsByAttributeName(string attributeName, string attributeValue);

    }
}
