using Bank.Core.Entities.Commons;
using Microsoft.AspNetCore.Http;
using System;
using System.Collections.Generic;
using System.ComponentModel.DataAnnotations.Schema;
using System.Linq;
using System.Text;
using System.Threading.Tasks;

namespace Bank.Core.Entities.Models
{
    public class Card:BaseAudiTable
    {
        public string Title {get;set;}
        public string ImageUrl {get;set;}
        public string Description {get;set;}
        public int CategoryId {get;set;}
        public List<CardFeature> CardFeatures {get;set;}
        [NotMapped]
        public List<int>? FeaturesIds { get; set; }
        public List<CardImage>? CardImages {get; set; }
        [NotMapped]
        public List<int>? CardImageIds { get; set; }
        [NotMapped]
        public IFormFile? CardPoster { get; set; }
        [NotMapped]
        public IFormFile? CardHower { get; set; }
    }
}
