using Bank.Core.Entities.Commons;
using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;

namespace Bank.Core.Entities.Models
{
    public class Question:BaseAudiTable
    {
        public string Title { get; set; }
        public string Description { get; set; }
    }
}
