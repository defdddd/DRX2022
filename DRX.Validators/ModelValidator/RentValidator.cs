using DRX.Models;
using FluentValidation;
using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;

namespace DRX.Validators.ModelValidator
{
    public class RentValidator : AbstractValidator<RentData>
    {
        public RentValidator()
        {
            RuleFor(x => x.LastLocation)
                .Cascade(CascadeMode.Stop)
                .NotEmpty();
        }
    }
}
