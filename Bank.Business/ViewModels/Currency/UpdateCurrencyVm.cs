using FluentValidation;
using Microsoft.AspNetCore.Http;
using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;

namespace Bank.Business.ViewModels.Currency
{
    public class UpdateCurrencyVm
    {
        public int Id { get; set; }
        public string Title { get; set; }
        public double SendMoney { get; set; }
        public double RecieveMoney { get; set; }
        public string? ImageUrl { get; set; }
        public IFormFile? Image { get; set; }
    }
    public class UpdateCurrencyValidator : AbstractValidator<UpdateCurrencyVm>
    {
        public UpdateCurrencyValidator()
        {
            RuleFor(x => x.Title).NotEmpty();
            RuleFor(x => x.SendMoney).NotEmpty();
            RuleFor(x => x.RecieveMoney).NotEmpty();
        }
    }
}