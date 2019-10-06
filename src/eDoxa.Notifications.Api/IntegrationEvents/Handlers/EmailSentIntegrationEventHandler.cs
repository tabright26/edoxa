// Filename: EmailSentIntegrationEventHandler.cs
// Date Created: 2019-10-04
// 
// ================================================
// Copyright © 2019, eDoxa. All rights reserved.

using System.Threading.Tasks;

using eDoxa.Notifications.Domain.Services;
using eDoxa.ServiceBus.Abstractions;

namespace eDoxa.Notifications.Api.IntegrationEvents.Handlers
{
    public sealed class EmailSentIntegrationEventHandler : IIntegrationEventHandler<EmailSentIntegrationEvent>
    {
        private readonly IEmailService _emailService;

        public EmailSentIntegrationEventHandler(IEmailService emailService)
        {
            _emailService = emailService;
        }

        public async Task HandleAsync(EmailSentIntegrationEvent integrationEvent)
        {
            await _emailService.SendEmailAsync(integrationEvent.Email, integrationEvent.Subject, integrationEvent.HtmlMessage);
        }
    }
}
