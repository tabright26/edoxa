// Filename: UserTransactionSuccededIntegrationEvent.cs
// Date Created: 2019-09-29
// 
// ================================================
// Copyright © 2019, eDoxa. All rights reserved.

using eDoxa.Payment.Domain.Stripe.Models;
using eDoxa.Seedwork.Application;
using eDoxa.ServiceBus.Abstractions;

using Newtonsoft.Json;

namespace eDoxa.Payment.Api.IntegrationEvents
{
    [JsonObject]
    public sealed class UserTransactionSuccededIntegrationEvent : IIntegrationEvent
    {
        [JsonConstructor]
        public UserTransactionSuccededIntegrationEvent(TransactionId transactionId)
        {
            TransactionId = transactionId;
        }

        [JsonProperty]
        public TransactionId TransactionId { get; }

        [JsonIgnore]
        public string Name => IntegrationEventNames.UserTransactionSucceded;
    }
}
