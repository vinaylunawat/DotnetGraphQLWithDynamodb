using AutoMapper;
using Framework.Business.Manager;
using Geography.Business.Country.Models;
using Geography.DataAccess.Repository;
using Microsoft.Extensions.Logging;

namespace Geography.Business.Country.Manager
{
    public class CountryManager : Manager<Entity.Entities.Country, CountryModel>, ICountryManager
    {
        /// <summary>
        /// Defines the _countryRepository.
        /// </summary>
        private readonly ICountryRepository _countryRepository;

        public CountryManager(ICountryRepository countryRepository, ILogger<CountryManager> logger, IMapper mapper)
            : base(countryRepository, logger, mapper)
        {
            _countryRepository = countryRepository;
        }
    }
}
