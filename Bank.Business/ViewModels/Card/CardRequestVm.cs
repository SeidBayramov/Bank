using FluentValidation;
using System.Text.RegularExpressions;

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
            RuleFor(x => x.Email)
              .NotEmpty().WithMessage("Email is required")
              .MaximumLength(40).WithMessage("Email must not exceed 40 characters")
              .Matches(new Regex(@"^[a-zA-Z0-9._%+-]+@[a-zA-Z0-9.-]+\.[a-zA-Z]{2,40}$"))
              .WithMessage("Invalid email format");

            RuleFor(x => x.FinCode)
  .NotEmpty().WithMessage("FinCode is required")
  .Matches(new Regex(@"^[a-zA-Z0-9]{1,7}$"))
  .WithMessage("Invalid FinCode format. It should be alphanumeric and have a maximum length of 7 characters.");


            RuleFor(x => x.IsVerified)
                .NotEmpty().WithMessage("Please click the permission");
        }
    }
}
