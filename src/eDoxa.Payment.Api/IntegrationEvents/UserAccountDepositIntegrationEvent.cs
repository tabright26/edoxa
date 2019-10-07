﻿// Filename: UserAccountDepositIntegrationEvent.cs
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
    internal sealed class UserAccountDepositIntegrationEvent : IIntegrationEvent
    {
        [JsonConstructor]
        public UserAccountDepositIntegrationEvent(
            Guid transactionId,
            string transactionDescription,
            string customerId,
            long amount
        )
        {
            TransactionId = transactionId;
            TransactionDescription = transactionDescription;
            CustomerId = customerId;
            Amount = amount;
        }

        [JsonProperty]
        public Guid TransactionId { get; }

        [JsonProperty]
        public string TransactionDescription { get; }

        [JsonProperty]
        public string CustomerId { get; }

        [JsonProperty]
        public long Amount { get; }

        [JsonIgnore]
        public string Name => IntegrationEventNames.UserAccountDeposit;
    }
}
