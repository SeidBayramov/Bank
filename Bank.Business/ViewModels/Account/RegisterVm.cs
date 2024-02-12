using FluentValidation;
using System;
using System.Collections.Generic;
using System.ComponentModel.DataAnnotations;
using System.Linq;
using System.Text;
using System.Threading.Tasks;

namespace Bank.Business.ViewModels.Account
{
    public class RegisterVm
    {
        public string Name { get; set; }

        public string Surname { get; set; }

        public string UserName { get; set; }

        public string Email { get; set; }

        public string Password { get; set; }

        public string ConfirmdPassword { get; set; }
    }
    public class RegisterVmValidation : AbstractValidator<RegisterVm>
    {
        public RegisterVmValidation()
        {
            RuleFor(x => x.Name).NotEmpty().WithMessage("Name is required");
            RuleFor(x => x.Surname).NotEmpty().WithMessage("Surname is required");
            RuleFor(x => x.UserName).NotEmpty().WithMessage("Username is required");
            RuleFor(x => x.Email).EmailAddress().NotEmpty().WithMessage("Email is required");
            RuleFor(x => x.Password).NotNull().NotEmpty().WithMessage("Password is required");
            RuleFor(x => x.ConfirmdPassword).NotNull().NotEmpty().WithMessage("ConfirmPassword is required").Equal(x => x.Password).WithMessage("Password must match");
        }
    }
}