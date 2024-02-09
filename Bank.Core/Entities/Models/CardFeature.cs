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
        public int FeatureId { get; set; }
        public Card? Card { get; set; }
        public Feature? Feature { get; set; }
    }
}
