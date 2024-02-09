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
        public string Description {get;set;}
        public bool IsInStock { get; set; }
        public int CategoryId {get;set;}
        public Category Category {get;set;}
        public List<CardFeature> CardFeatures {get;set;}
        public List<CardImage> CardImages {get; set; }
    }
}
