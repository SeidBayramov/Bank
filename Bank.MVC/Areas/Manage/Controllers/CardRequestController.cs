using Bank.Business.Services.Implementations;
using Bank.DAL.Context;
using Microsoft.AspNetCore.Mvc;
using Microsoft.EntityFrameworkCore;
using System;
using System.Net.Mail;
using System.Net;
using System.Threading.Tasks;
using Bank.Business.Services.Interface;

namespace Bank.MVC.Areas.Manage.Controllers
{
    [Area("Manage")]
    public class CardRequestController : Controller
    {
        private readonly AppDbContext _context;
        private readonly ICardRequestService _cardRequestService;

        public CardRequestController(AppDbContext context, ICardRequestService cardRequestService)
        {
            _context = context;
            _cardRequestService = cardRequestService;
        }
        public async Task<IActionResult> Index()
        {
            var cardlist = await _context.CardRequests
                .Where(x => x.IsVerified != false) // Exclude denied requests
                .ToListAsync();

            return View(cardlist);
        }


        [HttpPost]
        public async Task<IActionResult> Accept(int requestId)
        {
            var cardRequest = await _context.CardRequests.FindAsync(requestId);

            if (cardRequest != null)
            {
                cardRequest.IsVerified = true;
                await _context.SaveChangesAsync();

                SendEmail(cardRequest.Email, cardRequest.FinCode);

                return RedirectToAction("Index");
            }

            return NotFound();
        }

        [HttpPost]
        public async Task<IActionResult> Deny(int requestId)
        {
            var cardRequest = await _context.CardRequests.FindAsync(requestId);

            if (cardRequest != null)
            {
                cardRequest.IsVerified = false;
                await _context.SaveChangesAsync();


                ViewBag.Message = "Request has been denied.";

                SendDenialEmail(cardRequest.Email);

                return RedirectToAction("Index");
            }

            return NotFound();
        }

        private void SendDenialEmail(string toUser)
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
                    Subject = "Card Request Denial Notification",
                    Body = $"Hello," +
                        $"<p>We regret to inform you that your Card request has been denied.</p>" +
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
                    Body = $"Hello," +
                        $"<p>Welcome to FinBank! Your Card request has been accepted.</p>" +
                        $"<p>Your FinCode: {finCode}</p>" +
                        $"<p>Your Bank query: {randomQuery}</p>" +
                        $"<p>Please come to our office with your identity</p>" +
                        $"<p>Your query is available at {tomorrowTime}.</p>",

                    IsBodyHtml = true
                };

                mailMessage.To.Add(toUser);

                client.Send(mailMessage);
            }
        }
    }
}
