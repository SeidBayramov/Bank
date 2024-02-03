using FluentValidation;
using Microsoft.AspNetCore.Http;
using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;

namespace Bank.Business.ViewModels.Slider
{
    public class UpdateSliderVm
    {
        public int Id { get; set; }
        public string Title { get; set; }
        public string Descriptions { get; set; }
        public string? ImageUrl { get; set; }
        public IFormFile? Image { get; set; }
    }
    public class UpdateSliderValidator : AbstractValidator<UpdateSliderVm>
    {
        public UpdateSliderValidator()
        {
            RuleFor(x => x.Title).NotNull().NotEmpty().WithMessage("Title cannot be empty.")
                .MaximumLength(30).WithMessage("Length cannot be more than 30 characters.");
        }
    }
}
