using Bank.Business.Exceptions.Common;
using Bank.Business.Helpers;
using Bank.Business.Services.Interface;
using Bank.Business.ViewModels.Currency;
using Bank.Business.ViewModels.Slider;
using Bank.Core.Entities.Models;
using Bank.DAL.Repositories.Interface;
using Microsoft.Data.SqlClient;
using System;
using System.Collections.Generic;
using System.ComponentModel.DataAnnotations;
using System.Linq;
using System.Text;
using System.Threading.Tasks;

namespace Bank.Business.Services.Implementations
{
    public class CurrencyService : ICurrencyService
    {
        private readonly ICurrecnyRepository _rep;

        public CurrencyService(ICurrecnyRepository rep)
        {
            _rep = rep;
        }

        public async Task<IQueryable<Currency>> GetAllAsync()
        {
            return await _rep.GetAllAsync();
        }

        public async Task<Currency> GetByIdAsync(int id)
        {
            if (id <= 0) throw new IdNegativeOrZeroException("Id must be positive and over than zero!", nameof(id));

            return await _rep.GetByIdAsync(id);
        }
        public async Task CreateAsync(CreateCurrencyVm vm, string env)
        {
            var exists = vm.Title == null || vm.SendMoney == null || vm.Image == null || vm.RecieveMoney == null;
            if (vm.Title.Length > 10)
            {
                throw new ValidationException("Title length must be 10 characters or less");
                
            }

            if (exists) throw new ObjectParamsNullException("Object parameters is required!", nameof(vm.Title));
            if (!vm.Image.CheckImage()) throw new ObjectParamsNullException("File format must be image and size must be lower than 3MB", nameof(vm.Image));

            Currency NewCurrency = new()
            {
                Title = vm.Title,
                SendMoney = vm.SendMoney,
                RecieveMoney = vm.RecieveMoney,
                CreatedDate = DateTime.Now,
                UpdatedDate = DateTime.Now,
            };
            NewCurrency.ImageUrl = vm.Image.Upload(env, @"/Upload/CurrencyImages/");

            await _rep.CreateAsync(NewCurrency);
            await _rep.SaveChangesAsync();
        }
        public async Task UpdateAsync(UpdateCurrencyVm vm, string webRoot)
        {
            if (vm.Id <= 0) throw new IdNegativeOrZeroException("Id must be positive and over than zero!", nameof(vm.Id));

            Currency oldcurrency = await _rep.GetByIdAsync(vm.Id);
            if (vm.Title.Length > 10)
            {
                throw new ValidationException("Title length must be 10 characters or less");

            }

            if (oldcurrency is null) throw new ObjectNullException("There is no currency in data!", nameof(oldcurrency));

            if (vm.Title is null)
            {
                throw new ObjectNullException("Object is required!", nameof(vm.Title));
            }
            if (vm.SendMoney == null)
            {
                throw new ObjectNullException("Object is required!", nameof(vm.SendMoney));
            }

            if (vm.RecieveMoney == null)
            {
                throw new ObjectNullException("Object is required!", nameof(vm.RecieveMoney));
            }

            oldcurrency.Title = vm.Title;
            oldcurrency.SendMoney = vm.SendMoney;
            oldcurrency.RecieveMoney = vm.RecieveMoney;
            oldcurrency.UpdatedDate = DateTime.Now;

            if (vm.Image is not null)
            {
                if (!vm.Image.CheckImage())
                {
                    throw new ImageException("Image size must be over than 3MB! or check Image Type", nameof(vm.Image));
                }

                FileManager.Delete(oldcurrency.ImageUrl, webRoot, @"\Upload\CurrencyImages\");
                oldcurrency.ImageUrl = vm.Image.Upload(webRoot, @"\Upload\CurrencyImages\");
            }

            await _rep.UpdateAsync(oldcurrency);
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
        public async Task<Currency> CheckService(int id)
        {
            if (id <= 0) throw new IdNegativeOrZeroException("Id must be not equal and over than zero!", nameof(id));
            var oldIcon = await _rep.GetByIdAsync(id);
            if (oldIcon == null) throw new ObjectNullException("There is no object like in data!", nameof(oldIcon));

            return oldIcon;
        }

    }
}
