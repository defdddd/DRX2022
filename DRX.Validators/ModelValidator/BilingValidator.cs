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
                .NotEmpty()
                .Length(4, 25)
                .Must(MustBeAValidName).WithMessage("Your name must not contain numbers or special caracters.");

            RuleFor(x => x.Phone)
                .Cascade(CascadeMode.Stop)
                .NotEmpty()
                .Length(6, 12)
                .Must(MustBeAValidePhoneNumber).WithMessage("Invalid phone number");

            RuleFor(x => x.Email)
                .Cascade(CascadeMode.Stop)
                .NotEmpty()
                .Length(4, 100)
                .EmailAddress();

            RuleFor(x => x.UserId)
                .Cascade(CascadeMode.Stop)
                .NotEmpty();

            RuleFor(x => x.Adress)
                .Cascade(CascadeMode.Stop)
                .NotEmpty();
        }

        private Boolean MustBeAValidName(string name)
        {
            char[] special = { '@', '#', '$', '%', '^', '&', '+', '=', '-' };

            if (name.Any(char.IsDigit)) return false;
            if (name.IndexOfAny(special) >= 0) return false;

            return true;
        }

        private Boolean MustBeAValidePhoneNumber(string phone)
        {
            if (phone.Any(char.IsLetter)) return false;
            return true;
        }
    }
}
