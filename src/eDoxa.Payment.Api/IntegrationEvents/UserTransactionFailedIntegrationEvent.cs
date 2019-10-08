// Filename: UserTransactionFailedIntegrationEvent.cs
// Date Created: 2019-09-29
// 
// ================================================
// Copyright © 2019, eDoxa. All rights reserved.

using eDoxa.Payment.Domain.Models;
using eDoxa.Seedwork.Application;
using eDoxa.ServiceBus.Abstractions;

using Newtonsoft.Json;

namespace eDoxa.Payment.Api.IntegrationEvents
{
    [JsonObject]
    public sealed class UserTransactionFailedIntegrationEvent : IIntegrationEvent
    {
        [JsonConstructor]
        public UserTransactionFailedIntegrationEvent(TransactionId transactionId)
        {
            TransactionId = transactionId;
        }

        [JsonProperty]
        public TransactionId TransactionId { get; }

        [JsonIgnore]
        public string Name => IntegrationEventNames.UserTransactionFailed;
    }
}
