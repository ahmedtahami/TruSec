using Azure.Communication.Email;
using Microsoft.Extensions.Options;
using System;
using System.Net.Mail;
using System.Threading.Tasks;
using TruSec.Backend.Models;
using TruSec.BLL.Interfaces;

namespace TruSec.BLL.Services
{
    public class EmailService : IEmailService
    {
        private readonly EmailClient _emailClient;
        private readonly AzureCommunicationServiceSettings _azureCommunicationServiceSettings;

        public EmailService(IOptions<AzureCommunicationServiceSettings> options)
        {
            _azureCommunicationServiceSettings = options.Value;
            _emailClient = new EmailClient(_azureCommunicationServiceSettings.ConnectionString);
        }

        public async Task SendEmailAsync(string toEmail, string subject, string htmlContent)
        {
            var emailContent = new EmailContent(subject)
            {
                Html = htmlContent
            };

            var emailMessage = new EmailMessage(_azureCommunicationServiceSettings.NoReplyEmailAddress, toEmail, emailContent);

            try
            {
                var response = await _emailClient.SendAsync(Azure.WaitUntil.Started, emailMessage);
                Console.WriteLine($"Email sent successfully: {response.Id}");
            }
            catch (Exception ex)
            {
                Console.WriteLine($"Failed to send email: {ex.Message}");
            }
        }
    }

}
