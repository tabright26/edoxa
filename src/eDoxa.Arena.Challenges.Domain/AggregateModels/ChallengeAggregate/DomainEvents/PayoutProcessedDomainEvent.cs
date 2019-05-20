// Filename: ChallengeUserPrizesSnapshottedDomainEvent.cs
// Date Created: 2019-04-14
// 
// ================================================
// Copyright © 2019, eDoxa. All rights reserved.
//  
// This file is subject to the terms and conditions
// defined in file 'LICENSE.md', which is part of
// this source code package.

using eDoxa.Arena.Challenges.Domain.Abstractions;
using eDoxa.Seedwork.Domain;

namespace eDoxa.Arena.Challenges.Domain.AggregateModels.ChallengeAggregate.DomainEvents
{
    public sealed class PayoutProcessedDomainEvent : IDomainEvent
    {
        public PayoutProcessedDomainEvent(ChallengeId challengeId, IParticipantPrizes participantPrizes)
        {
            ChallengeId = challengeId;
            ParticipantPrizes = participantPrizes;
        }

        public ChallengeId ChallengeId { get; private set; }

        public IParticipantPrizes ParticipantPrizes { get; private set; }
    }
}