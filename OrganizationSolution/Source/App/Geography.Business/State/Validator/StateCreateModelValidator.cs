using FluentValidation;
using Geography.Business.State.Models;
using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;

namespace Geography.Business.State.Validator
{
    public class StateCreateModelValidator : AbstractValidator<StateCreateModel>
    {
        public StateCreateModelValidator()
        {
            RuleFor(x => x.Name).NotEmpty().WithMessage("Name is required");
            
            RuleFor(x => x.CountryId).NotEmpty().WithMessage("CountryId is required");
        }
        
    }
}
