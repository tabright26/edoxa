// Filename: TransactionSuccededIntegrationEvent.cs
// Date Created: 2019-11-09
// 
// ================================================
// Copyright © 2019, eDoxa. All rights reserved.

using System.Collections.Generic;

using eDoxa.ServiceBus.Abstractions;

using Newtonsoft.Json;

namespace eDoxa.Cashier.Api.IntegrationEvents
{
    [JsonObject]
    public sealed class TransactionSuccededIntegrationEvent : IIntegrationEvent
    {
        [JsonConstructor]
        public TransactionSuccededIntegrationEvent(IDictionary<string, string> metadata)
        {
            Metadata = metadata;
        }

        [JsonProperty]
        public IDictionary<string, string> Metadata { get; }

        public string Name => Seedwork.Application.Constants.IntegrationEvents.TransactionSucceded;
    }
}
