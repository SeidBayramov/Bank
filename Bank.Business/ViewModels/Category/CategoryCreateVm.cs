using FluentValidation;
using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;

namespace Bank.Business.ViewModels.Category
{
    public class CategoryCreateVm
    {
        public string Name { get; set; }
    }
    public class CategoryCreateValidator : AbstractValidator<CategoryCreateVm>
    {
        public CategoryCreateValidator()
        {
            RuleFor(c => c.Name).NotEmpty();
        }
    }
}
