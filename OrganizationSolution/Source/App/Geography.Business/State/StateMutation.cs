using AutoMapper;
using Framework.DataAccess.Repository;
using Geography.Business.Country.Models;
using Geography.Business.GraphQL;
using Geography.Business.GraphQL.Model;
using Geography.Business.State.Models;
using Geography.DataAccess;
using GraphQL;
using GraphQL.Types;
using System;
using System.Linq;
using System.Threading.Tasks;

namespace Geography.Business.State
{
    public class StateMutation 
        //: ITopLevelMutation
    {
        //private readonly IStateRepository _stateRepository;
        //private readonly ICountryRepository _countryRepository;
        //private readonly IMapper _mapper;
        //public StateMutation(ICountryRepository countryRepository, IStateRepository stateRepository, IMapper mapper)
        //{
        //    _countryRepository = countryRepository;
        //    _stateRepository = stateRepository;
        //    _mapper = mapper;
        //}
        //public void RegisterField(ObjectGraphType graphType)
        //{
        //    graphType.Field<StateType>("createState")
        //       .Argument<NonNullGraphType<StateCreateInputType>>("state", "object of state")
        //       .ResolveAsync(ResolveCreateState());

        //    graphType.Field<StateType>("updateState")
        //        .Argument<NonNullGraphType<StateUpdateInputType>>("state", "object of state")
        //        .Argument<NonNullGraphType<IdGraphType>>("stateId", "id of state")
        //        .ResolveAsync(ResolveUpdateState(_stateRepository)
        //        );

        //    graphType.Field<StringGraphType>("deleteState")
        //    .Argument<NonNullGraphType<IdGraphType>>("stateId", "id of state")
        //    .ResolveAsync(ResolveDeleteState(_stateRepository)
        //    );

        //    Func<IResolveFieldContext<object>, Task<object>> ResolveUpdateState(IStateRepository repository)
        //    {
        //        return async context =>
        //        {
        //            var state = context.GetArgument<StateCreateModel>("state");
        //            var stateId = context.GetArgument<long>("stateId");
        //            var dbState = await repository.FetchByIdAsync(stateId);
        //            if (dbState == null)
        //            {
        //                context.Errors.Add(new ExecutionError("Couldn't find state in db."));
        //                return null;
        //            }
        //            _mapper.Map(state, dbState);
        //            return await repository.Update(dbState);
        //        };
        //    }
        //}

        //private static Func<IResolveFieldContext<object>, Task<object>> ResolveDeleteState(IStateRepository repository)
        //{
        //    return async context =>
        //    {
        //        var stateId = context.GetArgument<long>("stateId");
        //        var state = await repository.FetchByIdAsync(stateId);
        //        if (state == null)
        //        {
        //            context.Errors.Add(new ExecutionError("Couldn't find state in db."));
        //            return null;
        //        }

        //        await repository.Delete(state);
        //        var res = new MutationResponse()
        //        {
        //            Message = $"The state with the id: {stateId} has been successfully deleted from db."
        //        };
        //        return res.Message;
        //    };
        //}

        //private Func<IResolveFieldContext<object>, Task<object>> ResolveCreateState()
        //{
        //    return async context =>
        //    {
        //        var state = context.GetArgument<StateCreateModel>("state");
        //        var dbData = await _countryRepository.FetchByAsync(item => item.Id == state.CountryId, data => data.Id);
        //        if (!dbData.Any())
        //        {
        //            context.Errors.Add(new ExecutionError("Couldn't find country in db."));
        //            return null;
        //        }
        //        var stateEntity = _mapper.Map<Entity.Entities.State>(state);
        //        try
        //        {
        //            return await _stateRepository.Insert(stateEntity);
        //        }
        //        catch(Exception ex)
        //        {
        //            return null;
        //        }
        //    };
        //}
    }
}
