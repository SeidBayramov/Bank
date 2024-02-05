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
            RuleFor(x => x.TItle).NotEmpty();
            RuleFor(x => x.SubTitle).NotEmpty();
            RuleFor(x => x.Description).NotEmpty();
            RuleFor(x => x.Icon).NotEmpty();

        }
    }

}
