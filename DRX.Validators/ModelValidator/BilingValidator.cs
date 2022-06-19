using DRX.Models;
using FluentValidation;
using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;

namespace DRX.Validators.ModelValidator
{
    public class BilingValidator : AbstractValidator<BilingData>
    {
        public BilingValidator()
        {
            RuleFor(x => x.FullName)
                .Cascade(CascadeMode.Stop)
                .NotEmpty();
        }
    }
}
