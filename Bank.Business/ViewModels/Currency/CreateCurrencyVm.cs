using FluentValidation;
using Microsoft.AspNetCore.Http;
using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;

namespace Bank.Business.ViewModels.Currency
{
    public class CreateCurrencyVm
    {
        public string Title { get; set; }
        public double SendMoney { get; set; }
        public double RecieveMoney { get; set; }
        public IFormFile Image { get; set; }
    }
    public class CurrencyValidator : AbstractValidator<CreateCurrencyVm>
    {
        public CurrencyValidator()
        {
            RuleFor(c => c.Title).NotEmpty();
            RuleFor(c => c.SendMoney).NotEmpty();
            RuleFor(c => c.RecieveMoney).NotEmpty();
        }
    }
}
