using Bank.Core.Entities.Commons;
using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;

namespace Bank.Core.Entities.Models
{
    public class Currency:BaseAudiTable
    {
        public string Title { get; set; }
        public double SendMoney { get; set; }
        public double RecieveMoney { get; set; }
        public string ImageUrl { get; set; }
    }
}
