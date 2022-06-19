using DRX.Models;
using FluentValidation;
using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;

namespace DRX.Validators.ModelValidator
{
    public class InoviceValidator : AbstractValidator<InoviceData>
    {
        public InoviceValidator()
        {
            RuleFor(x => x.VehicleId)
                .Cascade(CascadeMode.Stop)
                .NotEmpty();
        }
    }
}
