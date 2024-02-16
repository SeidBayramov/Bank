using System.Net;
using System.Net.Mail;

namespace Bank.Business.Helpers
{
    public static class SendConfirmationService
    {
        public static void SendMessage(string to, string url)
        {
            using (var client = new SmtpClient("smtp.gmail.com", 587))
            {
                client.UseDefaultCredentials = false;
                client.DeliveryMethod = SmtpDeliveryMethod.Network;
                client.Credentials = new NetworkCredential("bayramovseid2004@gmail.com", "your_password"); 
                client.EnableSsl = true;

                var mailMessage = new MailMessage
                {
                    From = new MailAddress("your_email@gmail.com"), 
                    Subject = "Welcome to our Bank site",
                    Body = $"Please confirm your email: {url}",
                    IsBodyHtml = true
                };

                mailMessage.To.Add(to);

                client.Send(mailMessage);
            }
        }
    }
}
