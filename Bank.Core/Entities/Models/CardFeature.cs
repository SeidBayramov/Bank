using Bank.Core.Entities.Commons;
using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;

namespace Bank.Core.Entities.Models
{
    public class CardFeature:BaseAudiTable
    {
        public int CardId { get; set; }
        public int TagId { get; set; }
        public Card Card { get; set; }
        public Feature Tag { get; set; }
    }
}
