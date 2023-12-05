using Microsoft.AspNetCore.Identity.UI.Services;

namespace KutuphaneSistemi.Utulity
{
    public class EmailSender : IEmailSender
    {
        public Task SendEmailAsync(string email, string subject, string htmlMessage)
        {

            //Email gönderme işlemlerimizi buraya ekleyebiliriz.
            return Task.CompletedTask;
        }
    }
}
