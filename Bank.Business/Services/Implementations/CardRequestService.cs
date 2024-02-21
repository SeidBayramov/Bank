using Bank.Business.Services.Interface;
using Bank.Business.ViewModels.Card;
using Bank.DAL.Context;
using System;
using System.Collections.Generic;
using System.Linq;
using System.Net.Mail;
using System.Net;
using System.Text;
using System.Threading.Tasks;
using Bank.Core.Entities.Models;
using Microsoft.AspNetCore.Mvc;
using Bank.Business.Exceptions.Common;
using Microsoft.EntityFrameworkCore;
using Bank.Core.Entities.Account;
using Microsoft.AspNetCore.Identity;

namespace Bank.Business.Services.Implementations
{
    public class CardRequestService : ICardRequestService
    {
        private readonly AppDbContext _context;
        private readonly UserManager<AppUser> _userManager;

        public CardRequestService(AppDbContext context, UserManager<AppUser> userManager)
        {
            _context = context;
            _userManager = userManager;
        }

        public async Task Apply(CardRequestVm vm)
        {

            var exists = vm.FinCode == null || vm.Email == null || vm.IsVerified == null;

            if (exists)
            {
                throw new ObjectParamsNullException("Object parameters are required!", nameof(vm.FinCode));
            }

            //var existingRecordWithSameFinCode = await _context.CardRequests.FirstOrDefaultAsync(x => x.FinCode == vm.FinCode);
            ////var existingRecordWithSameEmail = await _context.CardRequests.FirstOrDefaultAsync(x => x.Email == vm.Email);

            //if (existingRecordWithSameFinCode != null)
            //{
            //    throw new ObjectSameParamsException("This FinCode is using before", nameof(vm.FinCode));
            //}

            //if (existingRecordWithSameEmail != null)
            //{
            //    throw new ObjectSameParamsException("This Email is using before!", nameof(vm.Email));
            //}



            CardRequest cardRequest = new()
            {
                FinCode = vm.FinCode,
                Email = vm.Email,
                IsVerified = vm.IsVerified,
                CreatedDate = DateTime.Now
            };


            _context.CardRequests.Add(cardRequest);
            await _context.SaveChangesAsync();
        }



    }
}