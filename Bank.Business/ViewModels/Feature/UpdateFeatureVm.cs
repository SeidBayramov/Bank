using FluentValidation;
using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;

namespace Bank.Business.ViewModels.Feature
{
    public class UpdateFeatureVm
    {
        public int Id { get; set; }
        public string Title { get; set; }
    }
    public class UpdateFeatureValidator : AbstractValidator<UpdateFeatureVm>
    {
        public UpdateFeatureValidator()
        {
            RuleFor(x => x.Title).NotEmpty();
        }
    }
}
