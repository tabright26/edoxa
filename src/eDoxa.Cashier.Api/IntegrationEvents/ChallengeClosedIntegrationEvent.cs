// Filename: ChallengeClosedIntegrationEvent.cs
// Date Created: 2019-11-15
// 
// ================================================
// Copyright © 2019, eDoxa. All rights reserved.

using System.Collections.Generic;

using eDoxa.Seedwork.Domain.Misc;
using eDoxa.ServiceBus.Abstractions;

using Newtonsoft.Json;

namespace eDoxa.Cashier.Api.IntegrationEvents
{
    [JsonObject]
    public sealed class ChallengeClosedIntegrationEvent : IIntegrationEvent
    {
        [JsonConstructor]
        public ChallengeClosedIntegrationEvent(ChallengeId challengeId, IDictionary<UserId, decimal?> scoreboard)
        {
            ChallengeId = challengeId;
            Scoreboard = scoreboard;
        }

        [JsonProperty]
        public ChallengeId ChallengeId { get; }

        [JsonProperty]
        public IDictionary<UserId, decimal?> Scoreboard { get; }

        public string Name => Seedwork.Application.Constants.IntegrationEvents.ChallengeClosed;
    }
}
