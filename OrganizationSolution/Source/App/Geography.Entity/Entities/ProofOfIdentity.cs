using Amazon.DynamoDBv2.DataModel;
using Framework.Entity.Entities;
using System;
using System.Collections.Generic;
using System.ComponentModel;
using System.Linq;
using System.Text;
using System.Threading.Tasks;

namespace Geography.Entity.Entities
{
    [DynamoDBTable("ProofOfIdentity")]
    public class ProofOfIdentity : EntityWithId<Guid>
    {
        [DynamoDBProperty("Passport")]
        public string Passport { get; set; }

        [DynamoDBProperty("PassportExpiryDate")]
        public string PassportExpiryDate { get; set; }

        [DynamoDBProperty("VoterIDCard")]
        public string VoterIDCard { get; set; }

        [DynamoDBProperty("PANCard")]
        public string PANCard { get; set; }

        [DynamoDBProperty("DrivingLicense")]
        public string DrivingLicense { get; set; }

        [DynamoDBProperty("DrivingLicenseExpiryDate")]
        public string DrivingLicenseExpiryDate { get; set; }

        [DynamoDBProperty("NREGAJOBCard")]
        public string NREGAJOBCard { get; set; }

        [DynamoDBProperty("Aadhar")]
        public string Aadhar { get; set; }
    }
}
