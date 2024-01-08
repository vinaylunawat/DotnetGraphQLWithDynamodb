using AutoMapper;
using Geography.Business.GraphQL;
using Geography.Business.State.Models;
using Geography.Business.State.Types;
using Geography.DataAccess.Repository;
using GraphQL.Types;
using GraphQL;
using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;
using Geography.Business.ProofOfIdentity.Types;
using Geography.Business.ProofOfIdentity.Models;

namespace Geography.Business.ProofOfIdentity.Manager
{
    public class ProofOfIdentityQuery : ITopLevelQuery
    {
        private readonly IProofOfIdentityRepository _proofOfIdentityRepository;
        private readonly IMapper _mapper;
        public ProofOfIdentityQuery(IProofOfIdentityRepository proofOfIdentityRepository, IMapper mapper)
        {
            _proofOfIdentityRepository = proofOfIdentityRepository;
            _mapper = mapper;
        }

        public void RegisterField(ObjectGraphType graphType)
        {
            graphType.Field<ListGraphType<ProofOfIdentityType>>("ProofOfIdentitys")
                     .ResolveAsync(async context => await ResolveProofOfIdentitys().ConfigureAwait(false));

            graphType.Field<ProofOfIdentityType>("ProofOfIdentity")
                     .Argument<NonNullGraphType<IdGraphType>>("proofOfIdentityId", "id of the ProofOfIdentity")
                     .ResolveAsync(async context => await ResolveProofOfIdentity(context).ConfigureAwait(false));
        }

        private async Task<IEnumerable<ProofOfIdentityReadModel>> ResolveProofOfIdentitys()
        {
            var dbState = await _proofOfIdentityRepository.GetAll(default).ConfigureAwait(false);
            return _mapper.Map<IEnumerable<ProofOfIdentityReadModel>>(dbState);
        }

        private async Task<ProofOfIdentityReadModel> ResolveProofOfIdentity(IResolveFieldContext<object> context)
        {
            var key = context.GetArgument<Guid>("proofOfIdentityId");
            if (key != Guid.Empty)
            {
                var dbState = await _proofOfIdentityRepository.GetByKey(key, default).ConfigureAwait(false);
                return _mapper.Map<ProofOfIdentityReadModel>(dbState);
            }
            else
            {
                context.Errors.Add(new ExecutionError("Value can not be empty for id"));
                return null;
            }
        }
    }
}
