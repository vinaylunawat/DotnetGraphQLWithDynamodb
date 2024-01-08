using GraphQL.Types;

namespace Geography.Business.ProofOfIdentity.Types
{
    public class ProofOfIdentityCreateInputType : InputObjectGraphType
    {
        public ProofOfIdentityCreateInputType()
        {
            Name = "proofOfIdentityCreateInput";
            Field<NonNullGraphType<StringGraphType>>("passport");
            Field<NonNullGraphType<StringGraphType>>("passportExpiryDate");
            Field<NonNullGraphType<StringGraphType>>("voterIDCard");
            Field<NonNullGraphType<StringGraphType>>("panCard");
            Field<NonNullGraphType<StringGraphType>>("drivingLicense");
            Field<NonNullGraphType<StringGraphType>>("drivingLicenseExpiryDate");
            Field<NonNullGraphType<StringGraphType>>("nregaJOBCard");
            Field<NonNullGraphType<StringGraphType>>("Aadhar");
        }
    }

    public class ProofOfIdentityUpdateInputType : InputObjectGraphType
    {
        public ProofOfIdentityUpdateInputType()
        {
            Name = "proofOfIdentityUpdateInput";
            Field<IdGraphType>("Id");            
            Field<StringGraphType>("passport");
            Field<StringGraphType>("passportExpiryDate");
            Field<StringGraphType>("voterIDCard");
            Field<StringGraphType>("panCard");
            Field<StringGraphType>("drivingLicense");
            Field<StringGraphType>("drivingLicenseExpiryDate");
            Field<StringGraphType>("nregaJOBCard");
            Field<StringGraphType>("Aadhar");
        }
    }
}
