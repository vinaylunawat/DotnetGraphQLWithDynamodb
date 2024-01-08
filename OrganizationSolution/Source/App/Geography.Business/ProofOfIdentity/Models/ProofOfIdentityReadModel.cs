using System;

namespace Geography.Business.ProofOfIdentity.Models
{
    /// <summary>
    /// Defines the <see cref="ProofOfIdentityReadModel"/>
    /// </summary>
    public class ProofOfIdentityReadModel
    {
        public Guid Id { get; set; }

        public string Passport { get; set; }

        public string PassportExpiryDate { get; set; }

        public string VoterIDCard { get; set; }

        public string PANCard { get; set; }

        public string DrivingLicense { get; set; }

        public string DrivingLicenseExpiryDate { get; set; }

        public string NREGAJOBCard { get; set; }

        public string Aadhar { get; set; }
    }
}
