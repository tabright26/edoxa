﻿// Filename: ParticipantRegisteredIntegrationEvent.cs
// Date Created: 2019-11-08
// 
// ================================================
// Copyright © 2019, eDoxa. All rights reserved.

using eDoxa.Seedwork.Application;
using eDoxa.Seedwork.Domain.Miscs;
using eDoxa.ServiceBus.Abstractions;

using Newtonsoft.Json;

namespace eDoxa.Cashier.Api.IntegrationEvents
{
    [JsonObject]
    public sealed class ParticipantRegisteredIntegrationEvent : IIntegrationEvent
    {
        [JsonConstructor]
        public ParticipantRegisteredIntegrationEvent(ChallengeId challengeId, ParticipantId participantId)
        {
            ChallengeId = challengeId;
            ParticipantId = participantId;
        }

        [JsonProperty]
        public ChallengeId ChallengeId { get; }

        [JsonProperty]
        public ParticipantId ParticipantId { get; }

        public string Name => IntegrationEventNames.ChallengeParticipantRegistered;
    }
}
