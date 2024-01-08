namespace Geography.Business.ProofOfIdentity.Models
{
    using AutoMapper;
    using Geography.Entity.Entities;

    public class ProofOfIdentityMappingProfile : Profile
    {
        /// <summary>
        /// Initializes a new instance of the <see cref="ProofOfIdentityMappingProfile"/> class.
        /// </summary>
        public ProofOfIdentityMappingProfile()
        {
            CreateMap<ProofOfIdentity, ProofOfIdentityReadModel>();

            CreateMap<ProofOfIdentityCreateModel, ProofOfIdentity>()
                .ForMember(x => x.Id, opt => opt.Ignore());

            CreateMap<ProofOfIdentityUpdateModel, ProofOfIdentity>();
        }
    }
}
