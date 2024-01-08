using AutoMapper;
using FluentValidation.Results;
using Geography.Business.GraphQL;
using Geography.Business.GraphQL.Model;
using Geography.Business.ProofOfIdentity.Models;
using Geography.Business.ProofOfIdentity.Types;
using Geography.Business.ProofOfIdentity.Validatior;
using Geography.DataAccess.Repository;
using GraphQL;
using GraphQL.Types;
using System;
using System.Threading.Tasks;

namespace Geography.Business.ProofOfIdentity.Manager
{
    public class ProofOfIdentityMutation : ITopLevelMutation
    {
        private readonly IProofOfIdentityRepository _proofOfIdentityRepository;
        private readonly ProofOfIdentityCreateModelValidator _proofOfIdentityCreateValidator;
        private readonly ProofOfIdentityUpdateModelValidator _proofOfIdentityUpdateValidator;
        private readonly IMapper _mapper;
        public ProofOfIdentityMutation(IProofOfIdentityRepository proofOfIdentityRepository, ProofOfIdentityCreateModelValidator proofOfIdentityCreateValidator, ProofOfIdentityUpdateModelValidator proofOfIdentityUpdateValidator, IMapper mapper)
        {
            _proofOfIdentityRepository = proofOfIdentityRepository;
            _proofOfIdentityCreateValidator = proofOfIdentityCreateValidator;
            _proofOfIdentityUpdateValidator = proofOfIdentityUpdateValidator;
            _mapper = mapper;
        }
        public void RegisterField(ObjectGraphType graphType)
        {
            graphType.Field<ProofOfIdentityType>("createProofOfIdentity")
                     .Argument<NonNullGraphType<ProofOfIdentityCreateInputType>>("proofOfIdentity", "object of proofOfIdentity")
                     .ResolveAsync(async context => await ResolveCreateProofOfIdentity(context).ConfigureAwait(false));

            graphType.Field<ProofOfIdentityType>("updateProofOfIdentity")
                     .Argument<NonNullGraphType<ProofOfIdentityUpdateInputType>>("proofOfIdentity", "object of proofOfIdentity")
                     .ResolveAsync(async context => await ResolveUpdateProofOfIdentity(context).ConfigureAwait(false));

            graphType.Field<StringGraphType>("deleteProofOfIdentity")
                     .Argument<NonNullGraphType<IdGraphType>>("proofOfIdentityId", "id of proofOfIdentity")
                     .ResolveAsync(async context => await ResolveDeleteProofOfIdentity(context).ConfigureAwait(false));
        }

        private async Task<ProofOfIdentityReadModel> ResolveCreateProofOfIdentity(IResolveFieldContext<object> context)
        {
            var proofOfIdentity = context.GetArgument<ProofOfIdentityCreateModel>("proofOfIdentity");
            var validationResult = _proofOfIdentityCreateValidator.Validate(proofOfIdentity);
            if (!validationResult.IsValid)
            {
                LoadErrors(context, validationResult);
                return null;
            }
            var dbEntity = _mapper.Map<Entity.Entities.ProofOfIdentity>(proofOfIdentity);
            dbEntity.Id = Guid.NewGuid();
            var addedproofOfIdentity = await _proofOfIdentityRepository.CreateAsync(dbEntity, default).ConfigureAwait(false);
            var result = _mapper.Map<ProofOfIdentityReadModel>(addedproofOfIdentity);
            return result;
        }

        private async Task<ProofOfIdentityReadModel> ResolveUpdateProofOfIdentity(IResolveFieldContext<object> context)
        {
            var proofOfIdentityUpdateModel = context.GetArgument<ProofOfIdentityUpdateModel>("proofOfIdentity");
            var validationResult = _proofOfIdentityUpdateValidator.Validate(proofOfIdentityUpdateModel);
            if (!validationResult.IsValid)
            {
                LoadErrors(context, validationResult);
                return null;
            }
            var dbEntity = await _proofOfIdentityRepository.GetByKey(proofOfIdentityUpdateModel.Id, default).ConfigureAwait(false);
            if (dbEntity == null)
            {
                context.Errors.Add(new ExecutionError("Couldn't find proofOfIdentity in db."));
                return null;
            }
            var dbState = _mapper.Map<Entity.Entities.ProofOfIdentity>(proofOfIdentityUpdateModel);
            var updatedProofOfIdentity = await _proofOfIdentityRepository.UpdateAsync(dbState, default).ConfigureAwait(false);
            return _mapper.Map<ProofOfIdentityReadModel>(updatedProofOfIdentity);
        }

        private async Task<object> ResolveDeleteProofOfIdentity(IResolveFieldContext<object> context)
        {

            var proofOfIdentityId = context.GetArgument<Guid>("proofOfIdentityId");
            var dbEntity = await _proofOfIdentityRepository.GetByKey(proofOfIdentityId, default).ConfigureAwait(false);
            if (dbEntity == null)
            {
                context.Errors.Add(new ExecutionError("Couldn't find proofOfIdentity in db."));
                return null;
            }
            await _proofOfIdentityRepository.DeleteAsync(proofOfIdentityId, default).ConfigureAwait(false);
            var res = new MutationResponse()
            {
                Message = $"The proofOfIdentity with the id: {proofOfIdentityId} has been successfully deleted from db."
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
