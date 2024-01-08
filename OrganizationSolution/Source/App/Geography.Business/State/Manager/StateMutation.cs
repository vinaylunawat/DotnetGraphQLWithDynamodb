using AutoMapper;
using FluentValidation;
using FluentValidation.Results;
using Framework.DataAccess.Repository;
using Geography.Business.Country.Models;
using Geography.Business.GraphQL;
using Geography.Business.GraphQL.Model;
using Geography.Business.State.Models;
using Geography.Business.State.Types;
using Geography.Business.State.Validator;
using Geography.DataAccess;
using Geography.DataAccess.Repository;
using Geography.Entity.Entities;
using GraphQL;
using GraphQL.Types;
using System;
using System.Linq;
using System.Threading.Tasks;
using static GraphQL.Validation.BasicVisitor;

namespace Geography.Business.State.Manager
{
    public class StateMutation : ITopLevelMutation
    {
        private readonly IStateRepository _stateRepository;
        private readonly StateCreateModelValidator _stateCreateValidator;
        private readonly StateUpdateModelValidator _stateUpdateValidator;
        private readonly IMapper _mapper;
        public StateMutation(IStateRepository stateRepository, StateCreateModelValidator stateCreateValidator, StateUpdateModelValidator stateUpdateValidator, IMapper mapper)
        {
            _stateRepository = stateRepository;
            _stateCreateValidator = stateCreateValidator;
            _stateUpdateValidator = stateUpdateValidator;
            _mapper = mapper;
        }
        public void RegisterField(ObjectGraphType graphType)
        {
            graphType.Field<StateType>("createState")
               .Argument<NonNullGraphType<StateCreateInputType>>("state", "object of state")
               .ResolveAsync(async context => await ResolveCreateState(context).ConfigureAwait(false));

            graphType.Field<StateType>("updateState")
                .Argument<NonNullGraphType<StateUpdateInputType>>("state", "object of state")
                .ResolveAsync(async context => await ResolveUpdateState(context).ConfigureAwait(false));

            graphType.Field<StringGraphType>("deleteState")
            .Argument<NonNullGraphType<IdGraphType>>("stateId", "id of state")
            .ResolveAsync(async context => await ResolveDeleteState(context).ConfigureAwait(false));

            
        }

        private async Task<StateReadModel> ResolveCreateState(IResolveFieldContext<object> context)
        {
            var state = context.GetArgument<StateCreateModel>("state");

            var validationResult = _stateCreateValidator.Validate(state);

            if (!validationResult.IsValid)
            {
                LoadErrors(context, validationResult);
                return null;
            }

            //if (dbData != null)
            //{
            //    context.Errors.Add(new ExecutionError("State already exists"));
            //    return null;
            //}

            var dbEntity = _mapper.Map<Entity.Entities.State>(state);
            dbEntity.Id = Guid.NewGuid();
            var addedState = await _stateRepository.CreateAsync(dbEntity, default).ConfigureAwait(false);
            var result = _mapper.Map<StateReadModel>(addedState);
            return result;

        }
        

        private async Task<StateReadModel> ResolveUpdateState(IResolveFieldContext<object> context)
        {

            var stateUpdateModel = context.GetArgument<StateUpdateModel>("state");

            var validationResult = _stateUpdateValidator.Validate(stateUpdateModel);

            if (!validationResult.IsValid)
            {
                LoadErrors(context, validationResult);
                return null;
            }

            var dbEntity = await _stateRepository.GetByKey(stateUpdateModel.Id, default).ConfigureAwait(false);
            if (dbEntity == null)
            {
                context.Errors.Add(new ExecutionError("Couldn't find state in db."));
                return null;
            }
            var dbState = _mapper.Map<Entity.Entities.State>(stateUpdateModel);
            var updatedState = await _stateRepository.UpdateAsync(dbState, default).ConfigureAwait(false);
            return _mapper.Map<StateReadModel>(updatedState);

        }

        private async Task<object> ResolveDeleteState(IResolveFieldContext<object> context)
        {

            var stateId = context.GetArgument<Guid>("stateId");

            var dbEntity = await _stateRepository.GetByKey(stateId, default).ConfigureAwait(false);

            if (dbEntity == null)
            {
                context.Errors.Add(new ExecutionError("Couldn't find state in db."));
                return null;
            }

            await _stateRepository.DeleteAsync(stateId, default).ConfigureAwait(false);

            var res = new MutationResponse()
            {
                Message = $"The state with the id: {stateId} has been successfully deleted from db."
            };
            return res.Message;
        }

        private static void LoadErrors(IResolveFieldContext<object> context, ValidationResult validationResult)
        {
            foreach (var error in validationResult.Errors)
            {
                context.Errors.Add(new ExecutionError(error.ErrorMessage));
            }
        }
    }
}
