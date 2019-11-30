﻿// Filename: EmailSentIntegrationEvent.cs
// Date Created: 2019-09-29
// 
// ================================================
// Copyright © 2019, eDoxa. All rights reserved.

using eDoxa.Seedwork.Domain.Miscs;
using eDoxa.ServiceBus.Abstractions;

using Newtonsoft.Json;

namespace eDoxa.Identity.Api.IntegrationEvents
{
    [JsonObject]
    public sealed class EmailSentIntegrationEvent : IIntegrationEvent
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
        public string Name => Seedwork.Application.Constants.IntegrationEvents.EmailSent;
    }
}
