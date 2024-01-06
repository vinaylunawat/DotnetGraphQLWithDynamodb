using AutoMapper;
using Geography.Business.Country.Models;
using Geography.Business.GraphQL;
using Geography.DataAccess.Repository;
using GraphQL;
using GraphQL.Types;
using System;
using System.Collections.Generic;
using System.Threading;
using System.Threading.Tasks;

namespace Geography.Business.Country
{
    public class CountryQuery : ITopLevelQuery
    {
        //private readonly ICountryManager _countryManager;
        private readonly ICountryRepository _countryRepository;
        private readonly IMapper _mapper;
        public CountryQuery(ICountryRepository countryRepository, IMapper mapper)
        {
            _countryRepository = countryRepository;
            _mapper = mapper;
        }
        public void RegisterField(ObjectGraphType graphType)
        {
            graphType.Field<ListGraphType<CountryType>>("countries")
            .ResolveAsync(async context => await ResolveCountries().ConfigureAwait(false));

            graphType.Field<CountryType>("country")
            .Argument<NonNullGraphType<IdGraphType>>("countryId", "id of the country")
            .ResolveAsync(async context => await ResolveCountry(context).ConfigureAwait(false));
        }

        private async Task<IEnumerable<CountryModel>> ResolveCountries()
        {
            var dbCountry = await _countryRepository.GetAll(CancellationToken.None).ConfigureAwait(false);
            return _mapper.Map<IEnumerable<CountryModel>>(dbCountry);
        }

        private async Task<CountryModel> ResolveCountry(IResolveFieldContext<object> context)
        {

            var key = context.GetArgument<Guid>("countryId");
            if (key != Guid.Empty)
            {
                var dbEntity = await _countryRepository.GetByKey<Guid>(key, default).ConfigureAwait(false);
                var result = _mapper.Map<CountryModel>(dbEntity);
                return result;
            }
            else
            {
                context.Errors.Add(new ExecutionError("Wrong value for id"));
                return null;
            }
        }
    }
}
