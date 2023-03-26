using Microsoft.Extensions.Options;
using System;
using System.Collections.Generic;
using System.Linq;
using System.Net;
using System.Net.Mail;
using System.Threading.Tasks;
using WebApp.Settings;

namespace WebApp.Services
{
    public class EmailService : IEmailService
    {
        private readonly IOptions<SmtpSetting> smtpSetting;

        public EmailService(IOptions<SmtpSetting> smtpSetting)
        {
            this.smtpSetting = smtpSetting;
        }

        public async Task SendAsync(string from, string to, string subject, string body)
        {
            var message = new MailMessage(from,
                    to,
                    subject,
                    body);

            var emailClient = new SmtpClient()
            {
                Credentials = new NetworkCredential(
                    smtpSetting.Value.User,
                    smtpSetting.Value.Password),
                DeliveryMethod= SmtpDeliveryMethod.Network,
                Host=smtpSetting.Value.Host,
                Port=smtpSetting.Value.Port
            };
            emailClient.Send(message);
        }
    }
}
