using System.Collections.Generic;

using eDoxa.Seedwork.Domain.Miscs;
using eDoxa.ServiceBus.Abstractions;

using Newtonsoft.Json;

namespace eDoxa.Challenges.Api.IntegrationEvents
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
