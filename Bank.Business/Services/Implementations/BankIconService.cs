using Bank.Business.Exceptions.Common;
using Bank.Business.Helpers;
using Bank.Business.Services.Interface;
using Bank.Business.ViewModels.BankIcon;
using Bank.Core.Entities.Models;
using Bank.DAL.Repositories.Interface;
using Microsoft.AspNetCore;
using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;

namespace Bank.Business.Services.Implementations
{
    public class BankIconService : IBankIconService
    {
        private readonly IBankIconRepository _rep;

        public BankIconService(IBankIconRepository rep)
        {
            _rep = rep;
        }

        public async Task<IQueryable<BankIcon>> GetAllAsync()
        {
            return await _rep.GetAllAsync();
        }

        public async Task<BankIcon> GetByIdAsync(int id)
        {
            if (id <= 0) throw new IdNegativeOrZeroException("Id must be positive and over than zero!", nameof(id));

            return await _rep.GetByIdAsync(id);
        }
        public async Task CreateAsync(CreateIconVm vm)
        {
            var exists = vm.TItle == null || vm.SubTitle == null || vm.Icon == null || vm.SubTitle == null;

            if (exists) throw new ObjectParamsNullException("Object parameters is required!", nameof(vm.TItle));

            BankIcon bankIcon = new()
            {
                Title = vm.TItle,
                Description = vm.Description,
                SubTitle=vm.SubTitle,
                Icon=vm.Icon,
                CreatedDate = DateTime.Now,
                UpdatedDate = DateTime.Now,
            };
            await _rep.CreateAsync(bankIcon);
            await _rep.SaveChangesAsync();
        }

        public async Task UpdateAsync(UpdateIconVm vm)
        {
            if (vm.Id <= 0) throw new IdNegativeOrZeroException("Id must be positive and over than zero!", nameof(vm.Id));

            BankIcon OldBankICon = await _rep.GetByIdAsync(vm.Id);

            if (OldBankICon is null) throw new ObjectNullException("There is no blog in data!", nameof(OldBankICon));

            if (vm.TItle is null)
            {
                throw new ObjectNullException("Object is required!", nameof(vm.TItle));
            }
            if (vm.Description is null)
            {
                throw new ObjectNullException("Object is required!", nameof(vm.Description));
            }
            if (vm.SubTitle is null)
            {
                throw new ObjectNullException("Object is required!", nameof(vm.SubTitle));
            }
            if (vm.Icon is null)
            {
                throw new ObjectNullException("Object is required!", nameof(vm.Icon));
            }

            OldBankICon.Title = vm.TItle;
            OldBankICon.Description = vm.Description;
            OldBankICon.SubTitle = vm.SubTitle;
            OldBankICon.Icon = vm.Icon;
            OldBankICon.UpdatedDate = DateTime.Now;

            await _rep.UpdateAsync(OldBankICon);
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
        public async Task<BankIcon> CheckService(int id)
        {
            if (id <= 0) throw new IdNegativeOrZeroException("Id must be not equal and over than zero!", nameof(id));
            var oldIcon = await _rep.GetByIdAsync(id);
            if (oldIcon == null) throw new ObjectNullException("There is no object like in data!", nameof(oldIcon));

            return oldIcon;
        }
    }
}
