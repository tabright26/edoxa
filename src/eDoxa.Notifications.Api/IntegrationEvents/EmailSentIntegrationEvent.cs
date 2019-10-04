// Filename: EmailSentIntegrationEvent.cs
// Date Created: 2019-10-04
// 
// ================================================
// Copyright © 2019, eDoxa. All rights reserved.

using eDoxa.Notifications.Domain.Models;
using eDoxa.Seedwork.Application;
using eDoxa.ServiceBus.Abstractions;

using Newtonsoft.Json;

namespace eDoxa.Notifications.Api.IntegrationEvents
{
    [JsonObject]
    internal sealed class EmailSentIntegrationEvent : IIntegrationEvent
    {
        [JsonConstructor]
        public EmailSentIntegrationEvent(
            UserId userId,
            string email,
            string subject,
            string htmlMessage
        )
        {
            UserId = userId;
            Email = email;
            Subject = subject;
            HtmlMessage = htmlMessage;
        }

        [JsonProperty]
        public UserId UserId { get; }

        [JsonProperty]
        public string Email { get; }

        [JsonProperty]
        public string Subject { get; }

        [JsonProperty]
        public string HtmlMessage { get; }

        [JsonIgnore]
        public string Name => IntegrationEventNames.EmailSent;
    }
}
