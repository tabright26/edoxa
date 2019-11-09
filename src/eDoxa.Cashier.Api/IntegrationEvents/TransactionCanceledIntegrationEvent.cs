// Filename: TransactionCanceledIntegrationEvent.cs
// Date Created: 2019-11-08
// 
// ================================================
// Copyright © 2019, eDoxa. All rights reserved.

using System.Collections.Generic;

using eDoxa.Seedwork.Application;
using eDoxa.ServiceBus.Abstractions;

using Newtonsoft.Json;

namespace eDoxa.Cashier.Api.IntegrationEvents
{
    [JsonObject]
    public sealed class TransactionCanceledIntegrationEvent : IIntegrationEvent
    {
        [JsonConstructor]
        public TransactionCanceledIntegrationEvent(IDictionary<string, string> metadata)
        {
            Metadata = metadata;
        }

        [JsonProperty]
        public IDictionary<string, string> Metadata { get; }

        public string Name => IntegrationEventNames.TransactionCanceled;
    }
}
