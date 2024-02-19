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
            var exists = vm.Name == null || vm.Email == null || vm.Surname == null || vm.Country == null;

            if (exists)
            {
                throw new ObjectParamsNullException("Object parameters are required!", nameof(vm.Name));
            }
            Loan loan = new()
            {
                Country = vm.Country,
                Email = vm.Email,
                Name = vm.Name,
                Surname = vm.Surname,
                FinCode = vm.FinCode,
            };

            _context.Loans.Add(loan);

            SendEmail(vm.Email, "FinBank", vm.Name);
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

                // Generate a random 4-digit number for the user's query
                var randomQuery = new Random().Next(1000, 9999).ToString();

                // Get tomorrow's date and time
                var tomorrowTime = DateTime.Now.AddDays(1).ToString("yyyy-MM-dd HH:mm:ss");

                var mailMessage = new MailMessage()
                {
                    From = new MailAddress("seidbayramli2004@gmail.com"),
                    Subject = "Welcome to FINBANK Website",
                    Body = $"Hello, {webUser}!" +
                        $"<p>Welcome to FinBank! Your Loan request has been accepted.</p>" +
                        $"<p>Your FinCode: {finCode}</p>" +
                        $"<p>Your random query: {randomQuery}</p>" +
                        $"<p>Please come to our office with your identity tomorrow at {tomorrowTime}.</p>",
                    IsBodyHtml = true
                };

                mailMessage.To.Add(toUser);
                client.Send(mailMessage);
            }
        }
    }
}