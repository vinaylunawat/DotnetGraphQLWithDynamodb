namespace Geography.DataAccess.Repository
{
    using Amazon.DynamoDBv2.DataModel;
    using Entity.Entities;
    using Framework.DataAccess.Repository;
    using LinqKit;
    using System;
    using System.Collections.Generic;
    using System.Threading.Tasks;

    /// <summary>
    /// Defines the <see cref="CountryRepository" />.
    /// </summary>
    public class CountryRepository : GenericRepository<Country>, ICountryRepository
    {
        /// <summary>
        /// Initializes a new instance of the <see cref="CountryQueryRepository"/> class.
        /// </summary>
        /// <param name="dbContext">The dbContext<see cref="GeographyDbContext"/>.</param>
        public CountryRepository(IDynamoDBContext context) : base(context)
        {
        }

        public async Task RemoveCountryWithDependency(long countryId, IEnumerable<long> stateIds)
        {
            //var lstState = new List<State>();

            //Country country = new Country
            //{
            //    Id = countryId
            //};
            //stateIds.ForEach(stateId =>
            //{
            //    var state = new State()
            //    {
            //        Id = stateId,
            //        CountryId = countryId,
            //    };
            //    lstState.Add(state);
            //});

            //_dbContext.Set<Country>().Remove(country);
            //_dbContext.Set<State>().RemoveRange(lstState);
            //await _dbContext.SaveChangesAsync();

        }
    }
}
