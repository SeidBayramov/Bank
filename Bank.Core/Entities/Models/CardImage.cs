using Bank.Core.Entities.Commons;
using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;

namespace Bank.Core.Entities.Models
{
    public class CardImage:BaseAudiTable
    {
        public string ImageUrl { get; set; }
        public bool? isPoster { get; set; }
        public int CardId { get; set; }
        public Card? Book { get; set; }
    }
}
