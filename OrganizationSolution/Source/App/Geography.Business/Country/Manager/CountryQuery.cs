using AutoMapper;
using Framework.DataAccess.Model;
using Geography.Business.Country.Models;
using Geography.Business.Country.Types;
using Geography.Business.GraphQL;
using Geography.Business.State.Models;
using Geography.DataAccess.Repository;
using GraphQL;
using GraphQL.Types;
using System;
using System.Collections.Generic;
using System.Linq;
using System.Threading;
using System.Threading.Tasks;

namespace Geography.Business.Country.Manager
{
    public class CountryQuery : ITopLevelQuery
    {
        private readonly ICountryRepository _countryRepository;
        private readonly IStateRepository _stateRepository;
        private readonly IMapper _mapper;
        public CountryQuery(ICountryRepository countryRepository, IStateRepository stateRepository, IMapper mapper)
        {
            _countryRepository = countryRepository;
            _stateRepository = stateRepository;
            _mapper = mapper;
        }
        public void RegisterField(ObjectGraphType graphType)
        {
            graphType.Field<CountryPaginatedType>("pagedCountries")
            .Argument<IntGraphType>("pageSize", "limit of page records")
            .Argument<IdGraphType>("lastPageKey", "last page key")
            .ResolveAsync(async context => await ResolvePaginatedCountries(context).ConfigureAwait(false));

            graphType.Field<CountryPaginatedType>("pagedProjectedCountries")
            .Argument<IntGraphType>("pageSize", "limit of page records")
            .Argument<IdGraphType>("lastPageKey", "last page key")
            .ResolveAsync(async context => await ResolveProjectedPaginatedCountries(context).ConfigureAwait(false));

            graphType.Field<ListGraphType<CountryType>>("countries")
            .ResolveAsync(async context => await ResolveCountries().ConfigureAwait(false));

            graphType.Field<CountryType>("country")
            .Argument<NonNullGraphType<IdGraphType>>("countryId", "id of the country")
            .ResolveAsync(async context => await ResolveCountry(context).ConfigureAwait(false));
        }


        private async Task<IEnumerable<CountryReadModel>> ResolveCountries()
        {
            var dbCountry = await _countryRepository.GetAll(CancellationToken.None).ConfigureAwait(false);
            var dbState = await _stateRepository.GetAll(CancellationToken.None).ConfigureAwait(false);

            var resultCountry = _mapper.Map<IEnumerable<CountryReadModel>>(dbCountry);
            var resultState = _mapper.Map<IEnumerable<StateReadModel>>(dbState);
            foreach (var country in resultCountry)
            {
                foreach (var state in resultState.Where(x => x.CountryId == country.Id))
                {
                    country.States.Add(new StateReadModel() { Id = state.Id, Name = state.Name, CountryId = state.CountryId });
                }
            }
            return _mapper.Map<IEnumerable<CountryReadModel>>(dbCountry);
        }

        private async Task<PagedResultModel<CountryReadModel>> ResolvePaginatedCountries(IResolveFieldContext<object> context)
        {
            var lastEvaluatedKey = context.GetArgument<string>("lastPageKey");
            var pageSize = context.GetArgument<int?>("pageSize");

            var paginated = await _countryRepository.GetPaginatedScanItemsAsync(pageSize ?? 10 , lastEvaluatedKey).ConfigureAwait(false);
            var result = _mapper.Map<PagedResultModel<CountryReadModel>>(paginated);
            return result;
        }

        private async Task<PagedResultModel<CountryReadModel>> ResolveProjectedPaginatedCountries(IResolveFieldContext<object> context)
        {
            var lastEvaluatedKey = context.GetArgument<string>("lastPageKey");
            var pageSize = context.GetArgument<int?>("pageSize");

            var projectionFields = new List<string> { "Name", "Continent" };

            var paginated = await _countryRepository.GetPaginatedScanItemsAsync(pageSize ?? 10, lastEvaluatedKey, projectionFields).ConfigureAwait(false);
            var result = _mapper.Map<PagedResultModel<CountryReadModel>>(paginated);
            return result;
        }

        private async Task<CountryReadModel> ResolveCountry(IResolveFieldContext<object> context)
        {

            var key = context.GetArgument<Guid>("countryId");
            if (key != Guid.Empty)
            {
                var dbEntity = await _countryRepository.GetByKey(key, default).ConfigureAwait(false);
                return _mapper.Map<CountryReadModel>(dbEntity);
            }
            else
            {
                context.Errors.Add(new ExecutionError("Wrong value for id"));
                return null;
            }
        }
    }
}
