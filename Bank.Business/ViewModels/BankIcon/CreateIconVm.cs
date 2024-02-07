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
            RuleFor(x => x.TItle).NotEmpty().WithMessage("Title is not empty").MaximumLength(50).WithMessage("Title is not bigger than 50 words ");
            RuleFor(x => x.SubTitle).NotEmpty().WithMessage("SubTitle is not empty").MaximumLength(50).WithMessage("SubTitle is not bigger than 50 words ");
            RuleFor(x => x.Description).NotEmpty().WithMessage("Description is not empty").MaximumLength(2000);
        }
    }
}
