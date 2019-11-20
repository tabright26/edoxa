﻿// Filename: ChallengesSynchronizedIntegrationEvent.cs
// Date Created: 2019-10-06
// 
// ================================================
// Copyright © 2019, eDoxa. All rights reserved.

using eDoxa.Seedwork.Application;
using eDoxa.Seedwork.Domain.Miscs;
using eDoxa.ServiceBus.Abstractions;

using Newtonsoft.Json;

namespace eDoxa.Challenges.Api.IntegrationEvents
{
    [JsonObject]
    public sealed class ChallengesSynchronizedIntegrationEvent : IIntegrationEvent
    {
        [JsonConstructor]
        public ChallengesSynchronizedIntegrationEvent(Game game)
        {
            Game = game;
        }

        [JsonProperty]
        public Game Game { get; }

        public string Name => IntegrationEventNames.ChallengesSynchronized;
    }
}