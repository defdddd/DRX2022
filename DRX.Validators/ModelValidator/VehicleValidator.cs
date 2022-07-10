using DRX.DTOs;
using FluentValidation;
using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;

namespace DRX.Validators.ModelValidator
{
    public class VehicleValidator : AbstractValidator<VehicleDTO>
    {
        public VehicleValidator()
        {
            RuleFor(x => x.Name)
                .Cascade(CascadeMode.Stop)
                .NotEmpty();

            RuleFor(x => x.Model)
                .Cascade(CascadeMode.Stop)
                .NotEmpty();

            RuleFor(x => x.PricePerMinute)
                .Cascade(CascadeMode.Stop)
                .NotEmpty();

            RuleFor(x => x.Location)
                .Cascade(CascadeMode.Stop)
                .NotEmpty();

            RuleFor(x => x.Type)
                .Cascade(CascadeMode.Stop)
                .NotEmpty();
        }
    }
}
