using Bank.Core.Entities.Models;
using FluentValidation;
using Microsoft.AspNetCore.Http;
using System;
using System.Collections.Generic;
using System.ComponentModel.DataAnnotations.Schema;
using System.Linq;
using System.Text;
using System.Threading.Tasks;

namespace Bank.Business.ViewModels.Card
{
    public class UpdateCardVm
    {
        public int Id { get; set; }
        public string Title { get; set; }
        public string Description { get; set; }
        public int CategoryId { get; set; }
        public bool IsInStock { get; set; }
        public List<int>? FeaturesIds { get; set; }
        public List<int>? CardImageIds { get; set; }
        public ICollection<IFormFile>? CardFiles { get; set; }
        public List<CardsmageVm>? CardImageVms { get; set; }
    }
    public class CardsmageVm
    {
        public int Id {get; set; }
        public string ImageUrl { get; set; }
    }
    public class UpdatecardValidator : AbstractValidator<UpdateCardVm>
    {
        public UpdatecardValidator()
        {
            RuleFor(x => x.Title).NotEmpty();
            RuleFor(x => x.Description).NotEmpty();
        }
    }
}
