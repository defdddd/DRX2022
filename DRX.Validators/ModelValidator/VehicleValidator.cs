using DRX.Models;
using FluentValidation;
using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;

namespace DRX.Validators.ModelValidator
{
    public class VehicleValidator : AbstractValidator<VehicleData>
    {
        public VehicleValidator()
        {
            RuleFor(x => x.Name)
                .Cascade(CascadeMode.Stop)
                .NotEmpty();
        }
    }
}
