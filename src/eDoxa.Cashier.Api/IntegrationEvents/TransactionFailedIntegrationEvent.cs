// Filename: TransactionFailedIntegrationEvent.cs
// Date Created: 2019-11-09
// 
// ================================================
// Copyright © 2019, eDoxa. All rights reserved.

using eDoxa.Seedwork.Domain.Misc;
using eDoxa.ServiceBus.Abstractions;

using Newtonsoft.Json;

namespace eDoxa.Cashier.Api.IntegrationEvents
{
    [JsonObject]
    public sealed class TransactionFailedIntegrationEvent : IIntegrationEvent
    {
        [JsonConstructor]
        public TransactionFailedIntegrationEvent(TransactionId transactionId)
        {
            TransactionId = transactionId;
        }

        [JsonProperty]
        public TransactionId TransactionId { get; }

        public string Name => Seedwork.Application.Constants.IntegrationEvents.TransactionFailed;
    }
}
