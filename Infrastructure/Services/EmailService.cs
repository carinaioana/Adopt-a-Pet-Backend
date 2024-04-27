using AdoptPets.Application.Contracts;
using AdoptPets.Application.Models;
using Microsoft.Extensions.Logging;
using Microsoft.Extensions.Options;
using SendGrid;
using SendGrid.Helpers.Mail;

namespace AdoptPets.Infrastructure.Services
{
    public class EmailService : IEmailService
    {
        private readonly IOptions<EmailSettings> options;
        private readonly ILogger<EmailService> logger;

        public EmailService(IOptions<EmailSettings> options, ILogger<EmailService> logger)
        {
            this.options = options;
            this.logger = logger;
        }
        public Task<bool> SendEmailAsync(Mail email)
        {
            var client = new SendGridClient(options.Value.ApiKey);
            var subject = email.Subject;
            var body = email.Body;
            var to = new EmailAddress(email.To);

            var from = new EmailAddress
            {
                Email = options.Value.FromAdress,
                Name = options.Value.FromName,
            };

            var message = MailHelper.CreateSingleEmail(from, to, subject, body, body);
            var response = client.SendEmailAsync(message);
            logger.LogInformation(response.Status.ToString());
            return Task.FromResult(true);
        }

    }
}
