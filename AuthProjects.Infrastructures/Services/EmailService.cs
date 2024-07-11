using System.Net;
using System.Net.Mail;
using AuthProjects.Core.Services;
using Microsoft.Extensions.Configuration;

namespace AuthProjects.Infrastructures.Services
{
    public class EmailService : IEmailService
    {
        private readonly IConfiguration _configuration;

        public EmailService(IConfiguration configuration)
        {
            _configuration = configuration;
        }

        public async Task SendEmailAsync(string toEmail, string subject, string templateName, Dictionary<string, string> placeholders)
        {
            var email = new MailMessage();
            var baseEmailPath = _configuration["BaseEmailTemplatePath"];
            var templatePath = Path.Combine(baseEmailPath, templateName);

            var emailBody = await File.ReadAllTextAsync(templatePath);

            foreach (var placeholder in placeholders)
            {
                emailBody = emailBody.Replace($"[{placeholder.Key}]", placeholder.Value);
            }

            email.From = new MailAddress(_configuration["SmtpSettings:SenderEmail"], _configuration["SmtpSettings:SenderName"]);
            email.Subject = subject;
            email.IsBodyHtml = true;
            email.Body = emailBody;
            email.To.Add(toEmail);

            using var smtp = new SmtpClient();
            smtp.Host = _configuration["SmtpSettings:Host"];
            smtp.Port = 2525;
            smtp.Credentials = new NetworkCredential(_configuration["SmtpSettings:UserName"], _configuration["SmtpSettings:Password"]);
            smtp.EnableSsl = true;

            try
            {
                await smtp.SendMailAsync(email);
            }
            catch (SmtpException ex)
            {
                Console.WriteLine(ex.ToString());
            }
        }
    }
}