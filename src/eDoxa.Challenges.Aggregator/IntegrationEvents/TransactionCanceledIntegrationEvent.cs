﻿// Filename: TransactionCanceledIntegrationEvent.cs
// Date Created: 2019-11-09
// 
// ================================================
// Copyright © 2019, eDoxa. All rights reserved.

using eDoxa.Seedwork.Application;
using eDoxa.Seedwork.Domain.Miscs;
using eDoxa.ServiceBus.Abstractions;

using Newtonsoft.Json;

namespace eDoxa.Challenges.Aggregator.IntegrationEvents
{
    [JsonObject]
    public sealed class TransactionCanceledIntegrationEvent : IIntegrationEvent
    {
        [JsonConstructor]
        public TransactionCanceledIntegrationEvent(TransactionId transactionId)
        {
            TransactionId = transactionId;
        }

        [JsonProperty]
        public TransactionId TransactionId { get; }

        public string Name => IntegrationEventNames.TransactionCanceled;
    }
}
