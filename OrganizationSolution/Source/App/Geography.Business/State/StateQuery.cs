using Framework.DataAccess.Repository;
using Framework.Service.Utilities.Criteria;
using Geography.Business.GraphQL;
using Geography.Business.State;
using Geography.Business.State.Manager;
using Geography.Business.State.Models;
using Geography.DataAccess;
using GraphQL;
using GraphQL.Types;
using System;
using System.Collections.Generic;
using System.Threading.Tasks;

namespace Geography.Business.State
{
    public class StateQuery : ITopLevelQuery
    {
        private readonly IStateManager _stateManager;
        public StateQuery(IStateManager stateManager)
        {
            _stateManager = stateManager;
        }
        public void RegisterField(ObjectGraphType graphType)
        {
            graphType.Field<ListGraphType<StateType>>("States")
            .ResolveAsync(async context => await ResolveStates().ConfigureAwait(false));

            graphType.Field<StateType>("state")
            .Argument<NonNullGraphType<IdGraphType>>("stateId", "id of the state")
            .ResolveAsync(async context => await ResolveState(context).ConfigureAwait(false));
        }

        private async Task<object> ResolveState(IResolveFieldContext<object> context)
        {
            var key = context.GetArgument<long>("stateId");
            if (key > 0)
            {
                return await _stateManager.GetByKey(key, default).ConfigureAwait(false);
            }
            else
            {
                context.Errors.Add(new ExecutionError("Wrong value for id"));
                return null;
            }
        }

        private async Task<IEnumerable<StateModel>> ResolveStates()
        {
            return await _stateManager.GetAll(default).ConfigureAwait(false);
        }
       
    }
}
