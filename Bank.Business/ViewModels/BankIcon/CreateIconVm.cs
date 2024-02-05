using FluentValidation;
using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;

namespace Bank.Business.ViewModels.BankIcon
{
    public class CreateIconVm
    {
        public string TItle { get; set; }
        public string SubTitle { get; set; }
        public string Icon { get; set; }
        public string Description { get; set; }
    }
    public class IconValidator : AbstractValidator<CreateIconVm>
    {
        public IconValidator()
        {
            RuleFor(x => x.Icon).NotEmpty().WithMessage("Icon is not empty");
            RuleFor(x => x.TItle).NotEmpty().WithMessage("Title is not empty");
            RuleFor(x => x.SubTitle).NotEmpty().WithMessage("SubTitle is not empty");
            RuleFor(x => x.Description).NotEmpty().WithMessage("Description is not empty");
        }
    }
}
