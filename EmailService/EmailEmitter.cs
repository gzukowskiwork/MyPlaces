using System;
using System.Collections.Generic;
using MailKit.Net.Smtp;
using MimeKit;
using System.Threading.Tasks;


namespace EmailService
{
    class EmailEmitter : IEmailEmitter
    {
        private readonly EmailConfig _emailConfig;

        public EmailEmitter(EmailConfig emailConfig)
        {
            _emailConfig = emailConfig;
        }
        
        public async Task SendMailAsync(Email message)
        {
            var emailMessage = CreateEmailMessage(message);
            await SendAsync(emailMessage);
        }

        private MimeMessage CreateEmailMessage(Email message)
        {
            var emailMessage = new MimeMessage();
            emailMessage.From.Add(new MailboxAddress(_emailConfig.From));
            emailMessage.To.Add(message.Recipient);
            emailMessage.Subject = message.Subject;
            var bodyBuilder = new BodyBuilder
            {
                HtmlBody = string.Format($"" +
                $"<h3>Wiadomość wygenerowana automatycznie</h3>" +
                $"<p style='color:red;'>{message.Content}</p>" +
                $"<a href='http://192.166.218.136:4200/' style='color:blue;'>Link do serwisu!</a>")
            };

            emailMessage.Body = bodyBuilder.ToMessageBody();
            return emailMessage;
        }

        private async Task SendAsync(MimeMessage mailMessage)
        {
            using (var client = new SmtpClient())
            {
                try
                {
                    await client.ConnectAsync(_emailConfig.SmtpServer, _emailConfig.Port, true);
                    client.AuthenticationMechanisms.Remove("XOAUTH2");
                    await client.AuthenticateAsync(_emailConfig.UserName, _emailConfig.Password);

                    await client.SendAsync(mailMessage);
                }
                catch (Exception e)
                {
                    throw;
                }
                finally
                {
                    await client.DisconnectAsync(true);
                    client.Dispose();
                }
            }

        }
    }
}
