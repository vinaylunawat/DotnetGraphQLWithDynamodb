using AutoMapper;
using Geography.Business.Country.Models;
using Geography.Business.GraphQL;
using Geography.Business.State.Models;
using Geography.Business.State.Types;
using Geography.DataAccess.Repository;
using GraphQL;
using GraphQL.Types;
using System;
using System.Collections.Generic;
using System.Threading.Tasks;

namespace Geography.Business.State.Manager
{
    public class StateQuery : ITopLevelQuery
    {
        private readonly IStateRepository _stateRepository;
        private readonly IMapper _mapper;
        public StateQuery(IStateRepository stateRepository, IMapper mapper)
        {
            _stateRepository = stateRepository;
            _mapper = mapper;
        }
        public void RegisterField(ObjectGraphType graphType)
        {
            graphType.Field<ListGraphType<StateType>>("States")
            .ResolveAsync(async context => await ResolveStates().ConfigureAwait(false));

            graphType.Field<StateType>("state")
            .Argument<NonNullGraphType<IdGraphType>>("stateId", "id of the state")
            .ResolveAsync(async context => await ResolveState(context).ConfigureAwait(false));
        }

        private async Task<IEnumerable<StateReadModel>> ResolveStates()
        {
            var dbState = await _stateRepository.GetAll(default).ConfigureAwait(false);
            return _mapper.Map<IEnumerable<StateReadModel>>(dbState);
        }

        private async Task<StateReadModel> ResolveState(IResolveFieldContext<object> context)
        {
            var key = context.GetArgument<Guid>("stateId");
            if (key != Guid.Empty)
            {
                var dbState = await _stateRepository.GetByKey(key, default).ConfigureAwait(false);
                return _mapper.Map<StateReadModel>(dbState);
            }
            else
            {
                context.Errors.Add(new ExecutionError("Wrong value for id"));
                return null;
            }
        }

    }
}
