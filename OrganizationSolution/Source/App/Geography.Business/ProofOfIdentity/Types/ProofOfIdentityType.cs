using Geography.Business.ProofOfIdentity.Models;
using GraphQL.Types;

namespace Geography.Business.ProofOfIdentity.Types
{
    public class ProofOfIdentityType : ObjectGraphType<ProofOfIdentityReadModel>
    {
        public ProofOfIdentityType()
        {
            Field(x => x.Id, type: typeof(IdGraphType));

            Field(x => x.Passport, type: typeof(StringGraphType));

            Field(x => x.PassportExpiryDate, type: typeof(StringGraphType));

            Field(x => x.VoterIDCard, type: typeof(StringGraphType));

            Field(x => x.PANCard, type: typeof(StringGraphType));

            Field(x => x.DrivingLicense, type: typeof(StringGraphType));

            Field(x => x.DrivingLicenseExpiryDate, type: typeof(StringGraphType));

            Field(x => x.NREGAJOBCard, type: typeof(StringGraphType));

            Field(x => x.Aadhar, type: typeof(StringGraphType));
        }
    }
}
