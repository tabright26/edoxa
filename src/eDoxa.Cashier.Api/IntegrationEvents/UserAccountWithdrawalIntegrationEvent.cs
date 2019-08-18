// Filename: UserAccountWithdrawalIntegrationEvent.cs
// Date Created: 2019-07-05
// 
// ================================================
// Copyright © 2019, eDoxa. All rights reserved.

using System;

using eDoxa.Seedwork.Application;
using eDoxa.ServiceBus.Abstractions;

using Newtonsoft.Json;

namespace eDoxa.Cashier.Api.IntegrationEvents
{
    [JsonObject]
    internal sealed class UserAccountWithdrawalIntegrationEvent : IIntegrationEvent
    {
        [JsonConstructor]
        public UserAccountWithdrawalIntegrationEvent(
            Guid transactionId,
            string transactionDescription,
            string connectAccountId,
            long amount
        )
        {
            TransactionId = transactionId;
            TransactionDescription = transactionDescription;
            ConnectAccountId = connectAccountId;
            Amount = amount;
        }

        [JsonProperty]
        public Guid TransactionId { get; }

        [JsonProperty]
        public string TransactionDescription { get; }

        [JsonProperty]
        public string ConnectAccountId { get; }

        [JsonProperty]
        public long Amount { get; }

        [JsonIgnore]
        public string Name => IntegrationEventNames.UserAccountWithdrawal;
    }
}
