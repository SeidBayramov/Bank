using Bank.Core.Entities.Commons;
using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;

namespace Bank.Core.Entities.Models
{
    public class CardRequest:BaseAudiTable
    {
        public string FinCode { get; set; }
        public string Email { get; set; }
        public bool IsVerified { get; set; }
        public bool IsDenied { get; set; }
    }
}
