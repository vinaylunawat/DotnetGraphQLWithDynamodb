using FluentValidation;
using Geography.Business.State.Models;

namespace Geography.Business.State.Validator
{
    public class StateUpdateModelValidator : AbstractValidator<StateUpdateModel>
    {
        public StateUpdateModelValidator()
        {
            RuleFor(x => x.Id).NotEmpty().WithMessage("Id is required");
            RuleFor(x => x.Name).NotEmpty().WithMessage("Name is required");
            RuleFor(x => x.CountryId).NotEmpty().WithMessage("CountryId is required");
        }

    }
}
