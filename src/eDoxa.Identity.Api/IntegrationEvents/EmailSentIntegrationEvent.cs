// Filename: EmailSentIntegrationEvent.cs
// Date Created: 2019-07-17
// 
// ================================================
// Copyright © 2019, eDoxa. All rights reserved.

using eDoxa.IntegrationEvents;

namespace eDoxa.Identity.Api.IntegrationEvents
{
    public class EmailSentIntegrationEvent : IntegrationEvent
    {
        public EmailSentIntegrationEvent(string email, string subject, string htmlMessage)
        {
            Email = email;
            Subject = subject;
            HtmlMessage = htmlMessage;
        }

        public string Email { get; private set; }

        public string Subject { get; private set; }

        public string HtmlMessage { get; private set; }
    }
}
