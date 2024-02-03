using Bank.Business.ViewModels.Account;
using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;

namespace Bank.Business.Services.Interface
{
     public interface IAccountService
    {
        Task Register(RegisterVm vm);
        Task Login(LoginVm vm);
        Task Logout();
        Task CreateRoles();
    }
}