using FluentValidation;
using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;

namespace Bank.Business.ViewModels.Card
{
    public class CardRequestVm
    {
        public string FinCode { get; set; }
        public string Email { get; set; }
        public bool IsVerified { get; set; }
    }
    public class RequestVMValidator : AbstractValidator<CardRequestVm>
    {
        public RequestVMValidator()
        {
            RuleFor(x=>x.Email).EmailAddress().NotEmpty();
            RuleFor(x => x.FinCode).NotEmpty().WithMessage("FinCode is required").MaximumLength(7).WithMessage("FinCode is maximum length is 7");
            RuleFor(x => x.IsVerified).NotEmpty().WithMessage("Please click the permission");
        }
    }
}
