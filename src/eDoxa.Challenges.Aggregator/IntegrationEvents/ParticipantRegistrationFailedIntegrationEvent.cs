// Filename: ParticipantRegistrationFailedIntegrationEvent.cs
// Date Created: 2019-11-08
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
    public sealed class ParticipantRegistrationFailedIntegrationEvent : IIntegrationEvent
    {
        [JsonConstructor]
        public ParticipantRegistrationFailedIntegrationEvent(ChallengeId challengeId, ParticipantId participantId)
        {
            ChallengeId = challengeId;
            ParticipantId = participantId;
        }

        [JsonProperty]
        public ChallengeId ChallengeId { get; }

        [JsonProperty]
        public ParticipantId ParticipantId { get; }

        public string Name => IntegrationEventNames.ChallengeParticipantRegistrationFailed;
    }
}
