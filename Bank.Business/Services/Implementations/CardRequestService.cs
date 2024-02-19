﻿using Bank.Business.Services.Interface;
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

namespace Bank.Business.Services.Implementations
{
    public class CardRequestService : ICardRequestService
    {
        private readonly AppDbContext _context;

        public CardRequestService(AppDbContext context)
        {
            _context = context;
        }

        public async Task Apply(CardRequestVm vm)
        {

            var exists = vm.FinCode == null || vm.Email == null || vm.IsVerified == null;

            if (exists)
            {
                throw new ObjectParamsNullException("Object parameters are required!", nameof(vm.FinCode));
            }

            CardRequest cardRequest = new()
            {
                FinCode = vm.FinCode,
                Email = vm.Email,
                IsVerified = vm.IsVerified,
                CreatedDate = DateTime.Now
            };


            _context.CardRequests.Add(cardRequest);

            SendEmail(vm.Email, "FinBank", vm.FinCode);

            await _context.SaveChangesAsync();
        }


        private void SendEmail(string toUser, string webUser, string finCode)
        {
            using (var client = new SmtpClient("smtp.gmail.com", 587))
            {
                client.UseDefaultCredentials = false;
                client.DeliveryMethod = SmtpDeliveryMethod.Network;
                client.Credentials = new NetworkCredential("seidbayramli2004@gmail.com", "pkal bwah hhke dtzb");
                client.EnableSsl = true;

                // Get tomorrow's date and time
                var tomorrowTime = DateTime.Now.AddDays(1).ToString("yyyy-MM-dd HH:mm:ss");

                var mailMessage = new MailMessage()
                {
                    From = new MailAddress("seidbayramli2004@gmail.com"),
                    Subject = "Welcome to FINBANK Website",
                    Body = $"Hello, {webUser}!" +
                        $"<p>Welcome to FinBank! Your Card request has been accepted.</p>" +
                        $"<p>Your FinCode: {finCode}</p>" +
                        $"<p>Please come to our office with your identity tomorrow at {tomorrowTime}.</p>",
                    IsBodyHtml = true
                };

                mailMessage.To.Add(toUser);

                client.Send(mailMessage);
            }
        }
    }
}