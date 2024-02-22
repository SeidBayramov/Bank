using Bank.Business.Exceptions.Common;
using Bank.Business.Services.Interface;
using Bank.Business.ViewModels.Card;
using Bank.Business.ViewModels.Loan;
using Bank.Core.Entities.Models;
using Bank.DAL.Context;
using System;
using System.Collections.Generic;
using System.Linq;
using System.Net.Mail;
using System.Net;
using System.Text;
using System.Threading.Tasks;
using Microsoft.EntityFrameworkCore;
using Bank.Core.Entities.Account;

namespace Bank.Business.Services.Implementations
{
    public class LoanService : ILoanService
    {
        private readonly AppDbContext _context;

        public LoanService(AppDbContext context)
        {
            _context = context;
        }

        public async Task Send(LoanVm vm)
        {
            var exists = vm.Name == null || vm.Email == null || vm.Surname == null || vm.Country == null || vm.FinCode==null;

            //if (exists)
            //{
            //    throw new ObjectParamsNullException("Object parameters are required!", nameof(vm.Name));
            //}
            //var existingRecordWithSameFinCode = await _context.Loans.FirstOrDefaultAsync(x => x.FinCode == vm.FinCode);
            //var existingRecordWithSameEmail = await _context.Loans.FirstOrDefaultAsync(x => x.Email == vm.Email);

            //if (existingRecordWithSameFinCode != null)
            //{
            //    throw new ObjectSameParamsException("This FinCode is using before", nameof(vm.FinCode));
            //}

            //if (existingRecordWithSameEmail != null)
            //{
            //    throw new ObjectSameParamsException("This Email is using before!", nameof(vm.Email));
            //}

            Loan loan = new()
            {
                Country = vm.Country,
                Email = vm.Email,
                Name = vm.Name,
                Surname = vm.Surname,
                FinCode = vm.FinCode,
                Phone = vm.Phone,
                IsVerified=false,
                CreatedDate=DateTime.Now
            };

            _context.Loans.Add(loan);


            await _context.SaveChangesAsync();

        }
    }
}