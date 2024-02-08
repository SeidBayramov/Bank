using Bank.Business.ViewModels.BankIcon;
using Bank.Business.ViewModels.Slider;
using Bank.Core.Entities.Models;
using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;

namespace Bank.Business.Services.Interface
{
    public interface IBankIconService
    {
        Task<IQueryable<BankIcon>> GetAllAsync();
        Task<BankIcon> GetByIdAsync(int id);
        Task CreateAsync(CreateIconVm vm);
        Task UpdateAsync(UpdateIconVm vm);
        Task DeleteAsync(int id);
        Task RecoverAsync(int id);
        Task RemoveAsync(int id);
    }
}
