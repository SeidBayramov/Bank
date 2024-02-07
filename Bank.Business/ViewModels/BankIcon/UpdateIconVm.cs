using FluentValidation;
using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;

namespace Bank.Business.ViewModels.BankIcon
{
    public class UpdateIconVm
    {
        public int Id { get; set; }
        public string TItle { get; set; }
        public string SubTitle { get; set; }
        public string? Icon { get; set; }
        public string Description { get; set; }
    }
    public class UpdateValidator : AbstractValidator<UpdateIconVm>
    {
        public UpdateValidator()
        {
            RuleFor(x => x.TItle).NotEmpty().MaximumLength(50);
            RuleFor(x => x.SubTitle).NotEmpty().MaximumLength(50);
            RuleFor(x => x.Description).NotEmpty().MaximumLength(2000);
            RuleFor(x => x.Icon).NotEmpty();

        }
    }

}
