using Bank.Business.ViewModels.Account;
using Bank.Core.Entities.Account;
using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;

namespace Bank.Business.Services.Interface
{
     public interface IAccountService
    {
        Task<List<string>> Register(RegisterVm vm);
        Task Login(LoginVm vm);
        Task Logout();
        Task CreateRoles();
        Task<List<string>> SendConfirmEmailAddress(AppUser user);
        Task<bool> ConfirmEmailAddress(ConfrimEmailVm vm, string userId, string token, string pincode);
    }
}