// Filename: UserEmailSentIntegrationEvent.cs
// Date Created: 2019-10-21
// 
// ================================================
// Copyright © 2019, eDoxa. All rights reserved.

using eDoxa.Seedwork.Application;
using eDoxa.Seedwork.Domain.Miscs;
using eDoxa.ServiceBus.Abstractions;

using Newtonsoft.Json;

namespace eDoxa.Organizations.Clans.Api.IntegrationEvents
{
    [JsonObject]
    public sealed class UserEmailSentIntegrationEvent : IIntegrationEvent
    {
        [JsonConstructor]
        public UserEmailSentIntegrationEvent(UserId userId, string subject, string htmlMessage)
        {
            UserId = userId;
            Subject = subject;
            HtmlMessage = htmlMessage;
        }

        [JsonProperty]
        public UserId UserId { get; }

        [JsonProperty]
        public string Subject { get; }

        [JsonProperty]
        public string HtmlMessage { get; }

        [JsonIgnore]
        public string Name => IntegrationEventNames.UserEmailSent;
    }
}
