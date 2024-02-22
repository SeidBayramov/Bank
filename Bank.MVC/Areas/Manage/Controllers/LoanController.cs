using Bank.Business.Services.Interface;
using Bank.DAL.Context;
using Microsoft.AspNetCore.Mvc;
using System.Net.Mail;
using System.Net;
using Microsoft.EntityFrameworkCore;
using NuGet.Protocol.Plugins;
using Bank.Business.Exceptions.Common;

namespace Bank.MVC.Areas.Manage.Controllers
{
    [Area("Manage")]

    public class LoanController : Controller
    {
        private readonly AppDbContext _context;
        private readonly ILoanService _loanService;
        public LoanController(AppDbContext context, ILoanService loanService)
        {
            _context = context;
            _loanService = loanService;
        }

        public async Task<IActionResult> Index()
        {
            var cardlist = await _context.Loans
                .ToListAsync();
            return View(cardlist);
        }

        public async Task<IActionResult> Detail(int id)
        {
            var loan = await _context.Loans.FirstOrDefaultAsync(l => l.Id == id);

            if (loan == null)
            {
                return View();
            }

            return View(loan);
        }

        [HttpPost]
        public async Task<IActionResult> Accept(int requestId)
        {
            var loanRequest = await _context.Loans.FindAsync(requestId);

            if (loanRequest != null)
            {
                loanRequest.IsVerified = true;
                await _context.SaveChangesAsync();

                SendEmail(loanRequest.Email, loanRequest.FinCode);
                ViewBag.AcceptMessage = "Request has been accepted.";

                return RedirectToAction("Index");
            }

            return NotFound();
        }

        [HttpPost]
        public async Task<IActionResult> Deny(int requestId)
        {
            var loanRequest = await _context.Loans.FindAsync(requestId);

            if (loanRequest != null)
            {
                loanRequest.IsVerified = false;
                loanRequest.isDenied = true; // Set IsDenied to true
                await _context.SaveChangesAsync();
                ViewBag.DenyMessage = "Request has been denied.";

                SendLoanDenialEmail(loanRequest.Email);

                return RedirectToAction("Index");
            }

            return NotFound();
        }



        [HttpPost]
        public async Task<IActionResult> Remove(int requestId)
        {
            var loanRequest = await _context.Loans.FindAsync(requestId);

            if (loanRequest != null)
            {
                _context.Loans.Remove(loanRequest);
                await _context.SaveChangesAsync();

                ViewBag.Message = "Request has been removed.";

                return RedirectToAction("Index");
            }

            return NotFound();
        }



        private void SendLoanDenialEmail(string toUser)
        {
            using (var client = new SmtpClient("smtp.gmail.com", 587))
            {
                client.UseDefaultCredentials = false;
                client.DeliveryMethod = SmtpDeliveryMethod.Network;
                client.Credentials = new NetworkCredential("seidbayramli2004@gmail.com", "pkal bwah hhke dtzb");
                client.EnableSsl = true;

                var mailMessage = new MailMessage()
                {
                    From = new MailAddress("seidbayramli2004@gmail.com"),
                    Subject = "Loan Request Denial Notification",
                    Body = $"Hello," +
                        $"<p>We regret to inform you that your loan request has been denied.</p>" +
                        $"<p>Please contact our support team for further details.</p>",

                    IsBodyHtml = true
                };

                mailMessage.To.Add(toUser);

                client.Send(mailMessage);
            }
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

