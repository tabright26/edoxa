// Filename: ChallengeCreationFailedIntegrationEvent.cs
// Date Created: 2019-11-07
// 
// ================================================
// Copyright © 2019, eDoxa. All rights reserved.

using eDoxa.Seedwork.Domain.Misc;
using eDoxa.ServiceBus.Abstractions;

using Newtonsoft.Json;

namespace eDoxa.Challenges.Api.IntegrationEvents
{
    [JsonObject]
    public sealed class ChallengeDeletedIntegrationEvent : IIntegrationEvent
    {
        [JsonConstructor]
        public ChallengeDeletedIntegrationEvent(ChallengeId challengeId)
        {
            ChallengeId = challengeId;
        }

        [JsonProperty]
        public ChallengeId ChallengeId { get; }

        public string Name => Seedwork.Application.Constants.IntegrationEvents.ChallengeDeleted;
    }
}
