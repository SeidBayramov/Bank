using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;

namespace Bank.Business.ViewModels.BankIcon
{
    public class UpdateIconVm
    {
        public int Id { get; set; }
        public string TItle { get; set; }
        public string SubTitle { get; set; }
        public string? Icon { get; set; }
        public string Description { get; set; }
    }
}
