using FluentValidation;
using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;

namespace Bank.Business.ViewModels.Feature
{
    public class CreateFeatureVm
    {
        public string Title { get; set; }
    }
    public class CreateFeatureValidator : AbstractValidator<CreateFeatureVm>
    {
        public CreateFeatureValidator()
        {
                RuleFor(x=> x.Title).NotEmpty();    
        }
    }
}
