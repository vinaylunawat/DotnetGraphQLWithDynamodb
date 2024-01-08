using FluentValidation;
using Geography.Business.ProofOfIdentity.Models;
using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;

namespace Geography.Business.ProofOfIdentity.Validatior
{
    public class ProofOfIdentityUpdateModelValidator : AbstractValidator<ProofOfIdentityUpdateModel>
    {
        public ProofOfIdentityUpdateModelValidator()
        {
            RuleFor(x => x.Id).NotEmpty().WithMessage("Id is required");

            RuleFor(x => x.Passport).NotEmpty().WithMessage("Passport is required");

            RuleFor(x => x.PassportExpiryDate).NotEmpty().WithMessage("Passport expiry date is required");

            RuleFor(x => x.VoterIDCard).NotEmpty().WithMessage("VoterIDCard is required");

            RuleFor(x => x.PANCard).NotEmpty().WithMessage("PANCard is required");

            RuleFor(x => x.VoterIDCard).NotEmpty().WithMessage("VoterIDCard is required");

            RuleFor(x => x.DrivingLicense).NotEmpty().WithMessage("DrivingLicense is required");

            RuleFor(x => x.DrivingLicenseExpiryDate).NotEmpty().WithMessage("DrivingLicenseExpiryDate is required");

            RuleFor(x => x.NREGAJOBCard).NotEmpty().WithMessage("NREGAJOBCard is required");

            RuleFor(x => x.Aadhar).NotEmpty().WithMessage("Aadhar is required");
        }
    }
}
