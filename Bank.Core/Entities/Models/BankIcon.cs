using Bank.Core.Entities.Commons;
using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;

namespace Bank.Core.Entities.Models
{
    public class BankIcon:BaseAudiTable
    {
        public string Title { get; set; }
        public string Description { get; set; }
        public string SubTitle  { get; set; }
        public string Icon { get; set; }
    }
}
