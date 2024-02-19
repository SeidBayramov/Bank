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

            var existingRecordWithSameFinCode = await _context.CardRequests.FirstOrDefaultAsync(x => x.FinCode == vm.FinCode);
            var existingRecordWithSameEmail = await _context.CardRequests.FirstOrDefaultAsync(x => x.Email == vm.Email);

            if (existingRecordWithSameFinCode != null)
            {
                throw new ObjectSameParamsException("This FinCode is using before", nameof(vm.FinCode));
            }

            if (existingRecordWithSameEmail != null)
            {
                throw new ObjectSameParamsException("This Email is using before!", nameof(vm.Email));
            }



            CardRequest cardRequest = new()
            {
                FinCode = vm.FinCode,
                Email = vm.Email,
                IsVerified = vm.IsVerified,
                CreatedDate = DateTime.Now
            };


            _context.CardRequests.Add(cardRequest);

            

            SendEmail(vm.Email, vm.FinCode);

            await _context.SaveChangesAsync();
        }


        private void SendEmail(string toUser, string finCode)
        {
            using (var client = new SmtpClient("smtp.gmail.com", 587))
            {
                client.UseDefaultCredentials = false;
                client.DeliveryMethod = SmtpDeliveryMethod.Network;
                client.Credentials = new NetworkCredential("seidbayramli2004@gmail.com", "pkal bwah hhke dtzb");
                client.EnableSsl = true;


                var randomQuery = new Random().Next(1000, 9999).ToString();

                var tomorrowTime = DateTime.Now.AddDays(1).ToString("yyyy-MM-dd");

                var mailMessage = new MailMessage()
                {
                    From = new MailAddress("seidbayramli2004@gmail.com"),
                    Subject = "Welcome to FINBANK Website",
                    Body = $"Hello," +
                        $"<p>Welcome to FinBank! Your Card request has been accepted.</p>" +
                        $"<p>Your FinCode: {finCode}</p>" +
                        $"<p>Your Bank query: {randomQuery}</p>" +
                        $"<p>Please come to our office with your identity</p>"+
                        $"<p>Your query is aviable at {tomorrowTime}.</p>",

                    IsBodyHtml = true
                };

                mailMessage.To.Add(toUser);

                client.Send(mailMessage);
            }
        }
    }
}