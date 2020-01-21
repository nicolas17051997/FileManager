using MailKit;
using MailKit.Net.Smtp;
using MimeKit;
using System.Threading.Tasks;

namespace File_Sharing.Services
{
    public class EmailServices
    {
        public async Task SendEmailLinkAsync(string email, string subject, string message)
        {
            var emailMessage = new MimeMessage();
            emailMessage.From.Add(new MailboxAddress("File_Sharing", "filesharingimgs.com@gmail.com"));
            emailMessage.To.Add(new MailboxAddress("", email));
            emailMessage.Subject = subject;
            emailMessage.Body = new TextPart(MimeKit.Text.TextFormat.Html)
            {
                Text = message
            };
            using(var client = new SmtpClient())
            {
                await client.ConnectAsync("smtp.gmail.com", 587, MailKit.Security.SecureSocketOptions.StartTls);
                await client.AuthenticateAsync("filesharingimgs.com@gmail.com", "N16012020");
                await client.SendAsync(emailMessage);
                await client.DisconnectAsync(true);
            }
        }
    }
}
