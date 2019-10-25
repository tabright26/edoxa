// Filename: UserTransactionFailedIntegrationEvent.cs
// Date Created: 2019-08-27
// 
// ================================================
// Copyright © 2019, eDoxa. All rights reserved.

using eDoxa.Seedwork.Application;
using eDoxa.Seedwork.Domain.Miscs;
using eDoxa.ServiceBus.Abstractions;

using Newtonsoft.Json;

namespace eDoxa.Payment.Api.IntegrationEvents
{
    [JsonObject]
    public sealed class UserTransactionFailedIntegrationEvent : IIntegrationEvent
    {
        [JsonConstructor]
        public UserTransactionFailedIntegrationEvent(UserId userId, TransactionId transactionId)
        {
            UserId = userId;
            TransactionId = transactionId;
        }

        [JsonProperty]
        public UserId UserId { get; }

        [JsonProperty]
        public TransactionId TransactionId { get; }

        [JsonIgnore]
        public string Name => IntegrationEventNames.UserTransactionFailed;
    }
}
