// Filename: SendgridService.cs
// Date Created: 2020-02-05
// 
// ================================================
// Copyright © 2020, eDoxa. All rights reserved.

using System.Collections.Generic;
using System.Threading.Tasks;

using eDoxa.Sendgrid.Services.Abstractions;

using Microsoft.Extensions.Logging;
using Microsoft.Extensions.Options;

using SendGrid;
using SendGrid.Helpers.Mail;

namespace eDoxa.Sendgrid.Services
{
    public sealed class SendgridService : ISendgridService
    {
        private readonly ISendGridClient _client;
        private readonly ILogger _logger;
        private readonly IOptions<SendgridOptions> _options;

        public SendgridService(ISendGridClient client, ILogger<SendgridService> logger, IOptionsSnapshot<SendgridOptions> options)
        {
            _client = client;
            _logger = logger;
            _options = options;
        }

        private SendgridOptions Options => _options.Value;

        public async Task SendEmailAsync(string email, string templateId, object templateData)
        {
            var message = new SendGridMessage
            {
                From = new EmailAddress(Options.Message.From.Email, Options.Message.From.Name),
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
