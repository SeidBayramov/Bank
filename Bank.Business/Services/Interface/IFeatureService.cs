using Bank.Business.ViewModels.Category;
using Bank.Business.ViewModels.Feature;
using Bank.Core.Entities.Models;
using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;

namespace Bank.Business.Services.Interface
{
    public interface IFeatureService
    {
        Task<IQueryable<Feature>> GetAllAsync();
        Task<Feature> GetByIdAsync(int id);
        Task CreateAsync(CreateFeatureVm vm);
        Task UpdateAsync(UpdateFeatureVm vm);
        Task DeleteAsync(int id);
        Task RecoverAsync(int id);
        Task RemoveAsync(int id);
    }
}
