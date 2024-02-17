using System.Net;
using System.Net.Mail;

namespace Bank.Business.Helpers
{
    public static class SendConfirmationService
    {
        public static void SendEmailMessage(string toUser, string webUser, string pincode)
        {
            switch (pincode)
            {
                case "1000":
                    using (var client = new SmtpClient("smtp.gmail.com", 587))
                    {
                        client.UseDefaultCredentials = false;
                        client.DeliveryMethod = SmtpDeliveryMethod.Network;
                        client.Credentials = new NetworkCredential("seidbayramli2004@gmail.com", "pkal bwah hhke dtzb");
                        client.EnableSsl = true;

                        var mailMessage = new MailMessage()
                        {
                            From = new MailAddress("seidbayramli2004@gmail.com"),
                            Subject = "Welcome to FINBANK Website",
                            Body = $"Hello I am from {webUser}" +
                            $"<p>Welcome to FinBank, your ultimate destination for style inspiration and exclusive updates! As a valued subscriber, you're in for a treat. Get ready to elevate your inbox with a curated selection of fashion insights, lifestyle trends, and insider news tailored specifically for you. Delight in early access to our latest collections, limited-time offers, and exclusive promotions, all delivered directly to your inbox. Your preferences matter, and we're committed to providing a personalized experience that matches your unique taste. Join our vibrant community, share your thoughts, and engage with fellow subscribers. Your privacy is important to us, and we ensure the security of your information. Expect surprises – from exciting giveaways to special treats, we love to spoil our subscribers. Get set for a subscription journey filled with elegance, sophistication, and endless style possibilities. Embrace your individuality with Diana, where every newsletter is a celebration of your distinct persona.<p>",
                            IsBodyHtml = true
                        };

                        mailMessage.To.Add(toUser);
                        client.Send(mailMessage);
                    }
                    break;
                case "2000":
                    using (var client = new SmtpClient("smtp.gmail.com", 587))
                    {
                        client.UseDefaultCredentials = false;
                        client.DeliveryMethod = SmtpDeliveryMethod.Network;
                        client.Credentials = new NetworkCredential("seidbayramli2004@gmail.com", "pkal bwah hhke dtzb");
                        client.EnableSsl = true;

                        var mailMessage = new MailMessage()
                        {
                            From = new MailAddress("seidbayramli2004@gmail.com"),
                            Subject = "Welcome to Diana Website",
                            Body = $"Hello I am from {webUser} :D" +
                            $"<p>" +
                            $"Hello and have a good time\r\n" +
                            $"I hope you are having a good day. As the FINBANK group, " +
                            $"We would like to inform you about a new Card and Loan added to our website. " +
                            $"If you want to browse that condition, you can visit the website.\r\n" +
                            $"FinBank wishes you a nice day" +
                            $"<p>",
                            IsBodyHtml = true
                        };

                        mailMessage.To.Add(toUser);
                        client.Send(mailMessage);
                    }
                    break;
                default:
                    using (var client = new SmtpClient("smtp.gmail.com", 587))
                    {
                        client.UseDefaultCredentials = false;
                        client.DeliveryMethod = SmtpDeliveryMethod.Network;
                        client.Credentials = new NetworkCredential("seidbayramli2004@gmail.com", "pkal bwah hhke dtzb");
                        client.EnableSsl = true;

                        var mailMessage = new MailMessage()
                        {
                            From = new MailAddress("seidbayramli2004@gmail.com"),
                            Subject = "Welcome to FINBANK Website",
                            Body = $"Hello {webUser}, " +
                            $"Thank you for visiting our website. " +
                            $"Please write this code to confirmation section\n" +
                            $"\n" +
                            $"Pincode: {pincode}",
                            IsBodyHtml = true
                        };

                        mailMessage.To.Add(toUser);
                        client.Send(mailMessage);
                    }
                    break;
            }
        }
    }
}