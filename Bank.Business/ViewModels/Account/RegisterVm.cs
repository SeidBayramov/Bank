using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;

namespace Bank.Business.ViewModels.Account
{
    public class RegisterVm
    {
        public string Name { get; set; }
        public string Surname { get; set; }
        public string UserName { get; set; }
        public string Email { get; set; }
        private DateTime _bornTime;
        public DateTime BornTime
        {
            get => _bornTime;
            set => _bornTime = value;
        }
        public string FormattedBornTime => _bornTime.ToString("dd/MM/yyyy");
        public string PhoneNumber { get; set; }
        public string Password { get; set; }
        public string ConfirmdPassword { get; set; }
    }
}
