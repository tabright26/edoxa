// Filename: EmailService.cs
// Date Created: 2019-12-17
// 
// ================================================
// Copyright © 2019, eDoxa. All rights reserved.

using System;
using System.Collections.Generic;
using System.Threading.Tasks;

using eDoxa.Notifications.Domain.Services;

using Microsoft.Extensions.Configuration;
using Microsoft.Extensions.Logging;

using SendGrid;
using SendGrid.Helpers.Mail;

namespace eDoxa.Notifications.Api.Application.Services
{
    public sealed class EmailService : IEmailService
    {
        private readonly SendGridClient _client;
        private readonly ILogger _logger;

        public EmailService(IConfiguration configuration, ILogger<EmailService> logger)
        {
            _client = new SendGridClient(configuration["Notifications:SendGrid:ApiKey"] ?? throw new InvalidOperationException());
            _logger = logger;
        }

        public async Task SendEmailAsync(string email, string templateId, object templateData)
        {
            var message = new SendGridMessage
            {
                From = new EmailAddress("noreply@edoxa.gg", "eDoxa"),
                TemplateId = templateId,
                Personalizations = new List<Personalization>
                {
                    new Personalization
                    {
                        Tos = new List<EmailAddress>
                        {
                            new EmailAddress(email)
                        },
                        TemplateData = templateData
                    }
                }
            };

            _logger.LogInformation($"{email} message sending...");

            await _client.SendEmailAsync(message);

            _logger.LogInformation($"{email} message sent...");
        }
    }
}
