﻿using Bank.Core.Entities.Commons;
using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;

namespace Bank.Core.Entities.Models
{
    public class Category:BaseAudiTable
    {
        public string Name { get; set; }
        public List<Card>? Cards { get; set; }
    }
}
