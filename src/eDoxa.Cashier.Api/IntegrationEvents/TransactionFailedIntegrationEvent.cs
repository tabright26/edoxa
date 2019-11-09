// Filename: TransactionFailedIntegrationEvent.cs
// Date Created: 2019-11-09
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
    public sealed class TransactionFailedIntegrationEvent : IIntegrationEvent
    {
        [JsonConstructor]
        public TransactionFailedIntegrationEvent(IDictionary<string, string> metadata)
        {
            Metadata = metadata;
        }

        [JsonProperty]
        public IDictionary<string, string> Metadata { get; }

        public string Name => IntegrationEventNames.TransactionFailed;
    }
}
