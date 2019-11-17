// Filename: UserTransactionSuccededIntegrationEvent.cs
// Date Created: 2019-10-06
// 
// ================================================
// Copyright © 2019, eDoxa. All rights reserved.

using eDoxa.Seedwork.Application;
using eDoxa.Seedwork.Domain.Miscs;
using eDoxa.ServiceBus.Abstractions;

using Newtonsoft.Json;

namespace eDoxa.Cashier.Api.IntegrationEvents
{
    [JsonObject]
    public sealed class UserTransactionSuccededIntegrationEvent : IIntegrationEvent
    {
        [JsonConstructor]
        public UserTransactionSuccededIntegrationEvent(UserId userId, TransactionId transactionId)
        {
            UserId = userId;
            TransactionId = transactionId;
        }

        [JsonProperty]
        public UserId UserId { get; }

        [JsonProperty]
        public TransactionId TransactionId { get; }

        [JsonIgnore]
        public string Name => IntegrationEventNames.UserTransactionSucceded;
    }
}
