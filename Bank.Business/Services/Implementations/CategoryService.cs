using Bank.Business.Exceptions.Common;
using Bank.Business.Services.Interface;
using Bank.Business.ViewModels.Category;
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
    public class CategoryService:ICategoryService
    {
        private readonly ICategoryRepository _rep;

        public CategoryService(ICategoryRepository rep)
        {
            _rep = rep;
        }
        public async Task<IQueryable<Category>> GetAllAsync()
        {
            return await _rep.GetAllAsync();

        }
        public async Task<Category> GetByIdAsync(int id)
        {
            if (id <= 0) throw new IdNegativeOrZeroException("Id must be positive and over than zero!", nameof(id));

            return await _rep.GetByIdAsync(id);
        }
        public async Task CreateAsync(CategoryCreateVm vm)
        {
            var exists = vm.Name == null;

            if (exists) throw new ObjectParamsNullException("Object parameters is required!", nameof(vm.Name));

            Category category = new()
            {
                Name = vm.Name,
                CreatedDate = DateTime.Now,
                UpdatedDate = DateTime.Now,
            };
            await _rep.CreateAsync(category);
            await _rep.SaveChangesAsync();
        }

        public async Task UpdateAsync(CategoryUpdateVm vm)
        {
            if (vm.Id <= 0) throw new IdNegativeOrZeroException("Id must be positive and over than zero!", nameof(vm.Id));

            Category oldcategory = await _rep.GetByIdAsync(vm.Id);

            if (oldcategory is null) throw new ObjectNullException("There is no blog in data!", nameof(oldcategory));

            if (vm.Name is null)
            {
                throw new ObjectNullException("Object is required!", nameof(vm.Name));
            }


            oldcategory.Name = vm.Name;
            oldcategory.UpdatedDate = DateTime.Now;

            await _rep.UpdateAsync(oldcategory);
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
        public async Task<Category> CheckService(int id)
        {
            if (id <= 0) throw new IdNegativeOrZeroException("Id must be not equal and over than zero!", nameof(id));
            var category = await _rep.GetByIdAsync(id);
            if (category == null) throw new ObjectNullException("There is no object like in data!", nameof(category));

            return category;
        }

    }
}
