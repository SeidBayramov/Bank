using Bank.Core.Entities.Commons;
using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;

namespace Bank.Core.Entities.Models
{
    public class Slider:BaseAudiTable
    {
        public string Title { get; set; }
        public string Descriptions { get; set; }
        public string ImageUrl { get; set; }
    }
}
