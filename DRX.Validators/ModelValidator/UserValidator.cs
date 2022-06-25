using DRX.Models;
using FluentValidation;
using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;

namespace DRX.Validators.ModelValidator
{
    public class UserValidator : AbstractValidator<UserData>
    {
        public UserValidator()
        {
            RuleFor(x => x.UserName)
                .Cascade(CascadeMode.Stop)
                .Length(4, 25)
                .NotEmpty();

            RuleFor(x => x.Password)
               .Cascade(CascadeMode.Stop)
               .NotEmpty()
               .Length(4, 25)
               .Must(MustBeAValidPassowrd).WithMessage("The selected password does not meet the requirements.");

            RuleFor(x => x.Email)
               .Cascade(CascadeMode.Stop)
               .NotEmpty()
               .Length(4, 100)
               .EmailAddress();

        }
        private Boolean MustBeAValidPassowrd(string passWord)
        {
            char[] special = { '@', '#', '$', '%', '^', '&', '+', '=', '-' };

            if (!passWord.Any(char.IsUpper)) return false;

            if (!passWord.Any(char.IsLower)) return false;

            if (!passWord.Any(char.IsDigit)) return false;

            if (passWord.IndexOfAny(special) == -1) return false;

            return true;
        }
    }
}
