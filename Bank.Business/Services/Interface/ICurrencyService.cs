﻿using Bank.Business.ViewModels.BankIcon;
using Bank.Business.ViewModels.Currency;
using Bank.Core.Entities.Models;
using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;

namespace Bank.Business.Services.Interface
{
    public interface ICurrencyService
    {
        Task<List<Currency>> GetAllAsync();
        Task<Currency> GetByIdAsync(int id);
        Task CreateAsync(CreateCurrencyVm vm,string env);
        Task UpdateAsync(UpdateCurrencyVm vm,string env);
        Task DeleteAsync(int id);
        Task RecoverAsync(int id);
        Task RemoveAsync(int id);
    }
}