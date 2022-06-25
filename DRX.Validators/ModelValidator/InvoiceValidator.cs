using DRX.Models;
using FluentValidation;
using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;

namespace DRX.Validators.ModelValidator
{
    public class InvoiceValidator : AbstractValidator<InvoiceData>
    {
        public InvoiceValidator()
        {
            RuleFor(x => x.VehicleId)
                .Cascade(CascadeMode.Stop)
                .NotEmpty();

            RuleFor(x => x.Date)
                .Cascade(CascadeMode.Stop)
                .NotEmpty();

            RuleFor(x => x.BilingId)
                .Cascade(CascadeMode.Stop)
                .NotEmpty();

            RuleFor(x => x.Price)
                .Cascade(CascadeMode.Stop)
                .NotEmpty();

            RuleFor(x => x.UsedTime)
                .Cascade(CascadeMode.Stop)
                .NotEmpty();
        }
    }
}
