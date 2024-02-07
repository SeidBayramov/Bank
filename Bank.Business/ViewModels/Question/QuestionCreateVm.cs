using FluentValidation;
using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;

namespace Bank.Business.ViewModels.Question
{
    public class QuestionCreateVm
    {
        public string Title { get; set; }
        public string Description { get; set; }
    }
    public class QuestionCreateValidator : AbstractValidator<QuestionCreateVm>
    {
        public QuestionCreateValidator()
        {
            RuleFor(x => x.Title).NotEmpty().MaximumLength(100).WithMessage("Question is not bigger than 100 words");
            RuleFor(x => x.Description).NotEmpty().MaximumLength(3000).WithMessage("Question answer is not bigger than 3000 words");
        }
    }
}
