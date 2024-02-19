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

            if (exists)
            {
                throw new ObjectParamsNullException("Object parameters are required!", nameof(vm.Name));
            }
            var existingRecordWithSameFinCode = await _context.Loans.FirstOrDefaultAsync(x => x.FinCode == vm.FinCode);
            //var existingRecordWithSameEmail = await _context.Loans.FirstOrDefaultAsync(x => x.Email == vm.Email);

            if (existingRecordWithSameFinCode != null)
            {
                throw new ObjectSameParamsException("This FinCode is using before", nameof(vm.FinCode));
            }

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
                CreatedDate=DateTime.Now
            };

            _context.Loans.Add(loan);


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
                    Body = $"Hello!" +
                        $"<p>Welcome to FinBank! Your Loan request has been accepted.</p>" +
                        $"<p>Your FinCode: {finCode}</p>" +
                        $"<p>Your Bank query: {randomQuery}</p>" +
                        $"<p>Please come to our office with your identity.</p>" +
                        $"<p>Your query is aviable at {tomorrowTime}.</p>",

                    IsBodyHtml = true
                };

                mailMessage.To.Add(toUser);
                client.Send(mailMessage);
            }
        }
    }
}