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
                .NotEmpty();
        }
    }
}
