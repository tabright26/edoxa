// Filename: UserTransactionSuccededIntegrationEvent.cs
// Date Created: 2019-09-29
// 
// ================================================
// Copyright © 2019, eDoxa. All rights reserved.

using System;

using eDoxa.Seedwork.Application;
using eDoxa.ServiceBus.Abstractions;

using Newtonsoft.Json;

namespace eDoxa.Payment.Api.IntegrationEvents
{
    [JsonObject]
    internal sealed class UserTransactionSuccededIntegrationEvent : IIntegrationEvent
    {
        [JsonConstructor]
        public UserTransactionSuccededIntegrationEvent(Guid transactionId)
        {
            TransactionId = transactionId;
        }

        [JsonProperty]
        public Guid TransactionId { get; }

        [JsonIgnore]
        public string Name => IntegrationEventNames.UserTransactionSucceded;
    }
}
