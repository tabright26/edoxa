// Filename: EmailSender.cs
// Date Created: 2019-08-27
// 
// ================================================
// Copyright © 2019, eDoxa. All rights reserved.

using System;
using System.Threading.Tasks;

using Microsoft.AspNetCore.Identity.UI.Services;
using Microsoft.Extensions.Configuration;
using Microsoft.Extensions.Logging;

using SendGrid;
using SendGrid.Helpers.Mail;

namespace eDoxa.Identity.Api.Areas.Identity.Services
{
    public sealed class EmailSender : IEmailSender
    {
        private readonly SendGridClient _client;
        private readonly ILogger _logger;

        public EmailSender(IConfiguration configuration, ILogger<EmailSender> logger)
        {
            _client = new SendGridClient(configuration["SendGrid:ApiKey"] ?? throw new InvalidOperationException());
            _logger = logger;
        }

        public async Task SendEmailAsync(string email, string subject, string htmlMessage)
        {
            var message = new SendGridMessage
            {
                From = new EmailAddress("no-reply@edoxa.gg", "eDoxa Team"),
                Subject = subject,
                HtmlContent = htmlMessage
            };

            message.AddTo(new EmailAddress(email));

            _logger.LogInformation($"{email} message sending...");

            await _client.SendEmailAsync(message);

            _logger.LogInformation($"{email} message sent...");
        }
    }
}
