using FluentValidation;
using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;

namespace Bank.Business.ViewModels.Category
{
    public class CategoryUpdateVm
    {
        public int Id { get; set; }
        public string Name { get; set; }
    }
    public class CategoryUpdateValidator : AbstractValidator<CategoryUpdateVm>
    {
        public CategoryUpdateValidator()
        {
            RuleFor(x => x.Name).NotEmpty();
        }
    }
}
