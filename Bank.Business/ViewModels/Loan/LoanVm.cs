using FluentValidation;
using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Text.RegularExpressions;
using System.Threading.Tasks;

namespace Bank.Business.ViewModels.Loan
{
    public class LoanVm
    {
        public string Name { get; set; }
        public string Surname { get; set; }
        public string Email { get; set; }
        public string Country { get; set; }
        public string FinCode { get; set; }
        public string Phone { get; set; }
    }
    public class LoanVmValidator : AbstractValidator<LoanVm>
    {
        public LoanVmValidator()
        {
            RuleFor(x => x.Name).NotEmpty().WithMessage("Name is required").MaximumLength(30).WithMessage("Name length must be 30 words ");
            RuleFor(x => x.Surname).NotEmpty().WithMessage("Surname is required").MaximumLength(40).WithMessage("Surname length must be 40 words ");
            RuleFor(x => x.Email)
                      .NotEmpty().WithMessage("Email is required")
                      .MaximumLength(40).WithMessage("Email must not exceed 40 characters")
                      .Matches(new Regex(@"^[a-zA-Z0-9._%+-]+@[a-zA-Z0-9.-]+\.[a-zA-Z]{2,40}$"))
                      .WithMessage("Invalid email format");

            RuleFor(x => x.FinCode)
    .NotEmpty().WithMessage("FinCode is required")
    .Matches(new Regex(@"^[a-zA-Z0-9]{1,7}$"))
    .WithMessage("Invalid FinCode format. It should be alphanumeric and have a maximum length of 7 characters.");

            RuleFor(x => x.Country).NotEmpty().WithMessage("Country is required").MaximumLength(15).WithMessage("Country must 15 words");
            RuleFor(x => x.Phone).NotEmpty().WithMessage("Phone is required").MaximumLength(20).WithMessage("Country must 20 ");
        }
    }
}
