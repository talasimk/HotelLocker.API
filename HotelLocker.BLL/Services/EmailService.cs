using MailKit.Net.Smtp;
using MimeKit;
using System.Threading.Tasks;

namespace HotelLocker.BLL.Services
{
    public static class EmailService
    {
        public static  async Task SendEmailAsync(string email, string subject, string message)
        {
            var emailMessage = new MimeMessage();

            emailMessage.From.Add(new MailboxAddress("Готельна система \"HotelLocker\"", "hotellockersystem@gmail.com"));
            emailMessage.To.Add(new MailboxAddress("", email));
            emailMessage.Subject = subject;
            emailMessage.Body = new TextPart(MimeKit.Text.TextFormat.Html)
            {
                Text = message
            };

            using (var client = new SmtpClient())
            {
                await client.ConnectAsync("smtp.gmail.com", 587, false);
                await client.AuthenticateAsync("hotellockersystem@gmail.com", "_Aa123456");
                await client.SendAsync(emailMessage);
                await client.DisconnectAsync(true);
            }
        }
    }
}
