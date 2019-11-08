// Filename: ChallengeCreationFailedIntegrationEvent.cs
// Date Created: 2019-11-07
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
    public sealed class ChallengeCreationFailedIntegrationEvent : IIntegrationEvent
    {
        [JsonConstructor]
        public ChallengeCreationFailedIntegrationEvent(ChallengeId challengeId)
        {
            ChallengeId = challengeId;
        }

        [JsonProperty]
        public ChallengeId ChallengeId { get; }

        public string Name => IntegrationEventNames.ChallengeCreationFailed;
    }
}
