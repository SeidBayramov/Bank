using Bank.Business.ViewModels.Slider;
using Bank.Core.Entities.Models;
using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;

namespace Bank.Business.Services.Interface
{
    public interface ISliderService
    {
        Task<IQueryable<Slider>> GetAllAsync();
        Task<Slider> GetByIdAsync(int id);
        Task CreateAsync(CreateSliderVm vm, string env);
        Task UpdateAsync(UpdateSliderVm vm, string env);
        Task DeleteAsync(int id);
        Task RecoverAsync(int id);
        Task RemoveAsync(int id);
    }
}
