// Filename: EmailSentIntegrationEvent.cs
// Date Created: 2019-03-04
// 
// ============================================================
// Copyright © 2019, Francis Quenneville
// All rights reserved.
// 
// This file is subject to the terms and conditions defined in file 'LICENSE.md', which is part of
// this source code package.

using eDoxa.ServiceBus;

namespace eDoxa.IdentityServer.IntegrationEvents
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