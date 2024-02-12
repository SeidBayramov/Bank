using FluentValidation;
using System;
using System.ComponentModel.DataAnnotations;

namespace Bank.Business.ViewModels.Account
{
    public class LoginVm
    {
        public string LoginName { get; set; }

        public string Password { get; set; }
    }
    public class LoginVmValidation : AbstractValidator<LoginVm>
    {
        public LoginVmValidation()
        {
            RuleFor(x => x.LoginName).NotEmpty().WithMessage("Username/Email or password is wrong");
            RuleFor(x => x.Password).NotEmpty().WithMessage("Username/Email or password is wrong"); ;
        }
    }
}