using FluentValidation;
using Geography.Business.Country.Models;

namespace Geography.Business.Country.Validator
{
    public class CountryCreateModelValidator : AbstractValidator<CountryCreateModel>
    {
        public CountryCreateModelValidator()
        {
            RuleFor(x => x.Name).NotEmpty().WithMessage("Name is required");
            RuleFor(x => x.IsoCode).MaximumLength(3).WithMessage("IsoCode should not more than 3 characters.");
        }
    }
}
