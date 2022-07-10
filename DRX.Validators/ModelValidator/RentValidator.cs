using DRX.DTOs;
using FluentValidation;
using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;

namespace DRX.Validators.ModelValidator
{
    public class RentValidator : AbstractValidator<RentDTO>
    {
        public RentValidator()
        {
            RuleFor(x => x.LastLocation)
                .Cascade(CascadeMode.Stop)
                .NotEmpty();

            RuleFor(x => x.UserId)
                .Cascade(CascadeMode.Stop)
                .NotEmpty();

            RuleFor(x => x.VehicleId)
                .Cascade(CascadeMode.Stop)
                .NotEmpty();

            RuleFor(x => x.RentDate)
                .Cascade(CascadeMode.Stop)
                .NotEmpty();
        }
    }
}
