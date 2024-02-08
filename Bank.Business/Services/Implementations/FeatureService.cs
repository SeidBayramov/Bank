using Bank.Business.Exceptions.Common;
using Bank.Business.Services.Interface;
using Bank.Business.ViewModels.BankIcon;
using Bank.Business.ViewModels.Feature;
using Bank.Core.Entities.Models;
using Bank.DAL.Repositories.Interface;
using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;

namespace Bank.Business.Services.Implementations
{
    public class FeatureService:IFeatureService
    {
        private readonly IFeatureRepository _rep;

        public FeatureService(IFeatureRepository rep)
        {
            _rep = rep;
        }

        public async Task<IQueryable<Feature>> GetAllAsync()
        {
            return await _rep.GetAllAsync();

        }
        public async Task<Feature> GetByIdAsync(int id)
        {
            if (id <= 0) throw new IdNegativeOrZeroException("Id must be positive and over than zero!", nameof(id));

            return await _rep.GetByIdAsync(id);
        }

        public async Task CreateAsync(CreateFeatureVm vm)
        {
            var exists = vm.Title == null;

            if (exists) throw new ObjectParamsNullException("Object parameters is required!", nameof(vm.Title));

            Feature feature = new()
            {
                Title = vm.Title,
                CreatedDate = DateTime.Now,
                UpdatedDate = DateTime.Now,
            };
            await _rep.CreateAsync(feature);
            await _rep.SaveChangesAsync();
        }

        public async Task UpdateAsync(UpdateFeatureVm vm)
        {
            if (vm.Id <= 0) throw new IdNegativeOrZeroException("Id must be positive and over than zero!", nameof(vm.Id));

            Feature oldfeature = await _rep.GetByIdAsync(vm.Id);

            if (oldfeature is null) throw new ObjectNullException("There is no blog in data!", nameof(oldfeature));

            if (vm.Title is null)
            {
                throw new ObjectNullException("Object is required!", nameof(vm.Title));
            }


            oldfeature.Title = vm.Title;
            oldfeature.UpdatedDate = DateTime.Now;

            await _rep.UpdateAsync(oldfeature);
            await _rep.SaveChangesAsync();
        }

        public async Task DeleteAsync(int id)
        {
            await CheckService(id);

            await _rep.DeleteAsync(id);
            await _rep.SaveChangesAsync();
        }


        public async Task RecoverAsync(int id)
        {
            await CheckService(id);

            await _rep.RecoverAsync(id);
            await _rep.SaveChangesAsync();
        }

        public async Task RemoveAsync(int id)
        {
            await CheckService(id);

            await _rep.RemoveAsync(id);
            await _rep.SaveChangesAsync();
        }
        public async Task<Feature> CheckService(int id)
        {
            if (id <= 0) throw new IdNegativeOrZeroException("Id must be not equal and over than zero!", nameof(id));
            var feature = await _rep.GetByIdAsync(id);
            if (feature == null) throw new ObjectNullException("There is no object like in data!", nameof(feature));

            return feature;
        }

    }
}
