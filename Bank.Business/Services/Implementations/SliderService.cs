using Bank.Business.Exceptions.Common;
using Bank.Business.Helpers;
using Bank.Business.Services.Interface;
using Bank.Business.ViewModels.Slider;
using Bank.Core.Entities.Models;
using Bank.DAL.Repositories.Absrtactions;
using Bank.DAL.Repositories.Interface;
using Microsoft.AspNetCore.Hosting;
using System;
using System.Collections.Generic;
using System.Linq;
using System.Reflection.Metadata;
using System.Text;
using System.Threading.Tasks;

namespace Bank.Business.Services.Implementations
{
    public class SliderService : ISliderService
    {
        private readonly ISliderRepository _rep;

        public SliderService(ISliderRepository rep)
        {
            _rep = rep;
        }

        public async Task<List<Slider>> GetAllAsync()
        {
            return await _rep.GetAllAsync();
        }

        public async Task<Slider> GetByIdAsync(int id)
        {
            if (id <= 0) throw new IdNegativeOrZeroException("Id must be positive and over than zero!", nameof(id));

            return await _rep.GetByIdAsync(id);
        }

        public async Task CreateAsync(CreateSliderVm vm,string env)
        {
            var exists = vm.Title == null || vm.Descriptions == null || vm.Image == null;

            if (exists) throw new ObjectParamsNullException("Object parameters is required!", nameof(vm.Title));
            if (!vm.Image.CheckImage()) throw new ObjectParamsNullException("File format must be image and size must be lower than 3MB", nameof(vm.Image));

            Slider newService = new()
            {
                Title = vm.Title,
                Descriptions = vm.Descriptions,
                CreatedDate = DateTime.Now,
                UpdatedDate = DateTime.Now,
            };
            newService.ImageUrl = vm.Image.Upload(env, @"/Upload/SliderImages/");

            await _rep.CreateAsync(newService);
            await _rep.SaveChangesAsync();
        }
        public async Task UpdateAsync(UpdateSliderVm vm, string webRoot)
        {
            if (vm.Id <= 0) throw new IdNegativeOrZeroException("Id must be positive and over than zero!", nameof(vm.Id));

            Slider oldBlog = await _rep.GetByIdAsync(vm.Id);

            if (oldBlog is null) throw new ObjectNullException("There is no blog in data!", nameof(oldBlog));

            if (vm.Title is null)
            {
                throw new ObjectNullException("Object is required!", nameof(vm.Title));
            }
            if (vm.Descriptions is null)
            {
                throw new ObjectNullException("Object is required!", nameof(vm.Descriptions));
            }

            oldBlog.Title = vm.Title;
            oldBlog.Descriptions = vm.Descriptions;
            oldBlog.UpdatedDate = DateTime.Now;

            if (vm.Image is not null)
            {
                if (!vm.Image.CheckImage())
                {
                    throw new ImageException("Image size must be over than 3MB! or check Image Type", nameof(vm.Image));
                }
              
                FileManager.Delete(oldBlog.ImageUrl, webRoot, @"\Upload\SliderImages\");
                oldBlog.ImageUrl = vm.Image.Upload(webRoot, @"\Upload\SliderImages\");
            }

            await _rep.UpdateAsync(oldBlog);
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
        public async Task<Slider> CheckService(int id)
        {
            if (id <= 0) throw new IdNegativeOrZeroException("Id must be not equal and over than zero!", nameof(id));
            var oldService = await _rep.GetByIdAsync(id);
            if (oldService == null) throw new ObjectNullException("There is no object like in data!", nameof(oldService));

            return oldService;
        }

    }
}
