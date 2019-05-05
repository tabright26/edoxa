// Filename: ChallengeUserPrizesSnapshottedDomainEvent.cs
// Date Created: 2019-04-14
// 
// ================================================
// Copyright © 2019, eDoxa. All rights reserved.
//  
// This file is subject to the terms and conditions
// defined in file 'LICENSE.md', which is part of
// this source code package.

using eDoxa.Seedwork.Domain;

namespace eDoxa.Challenges.Domain.AggregateModels.ChallengeAggregate.DomainEvent
{
    public sealed class PayoutProcessedDomainEvent : IDomainEvent
    {
        public PayoutProcessedDomainEvent(ChallengeId challengeId, IUserPayoff payoff)
        {
            ChallengeId = challengeId;
            Payoff = payoff;
        }

        public ChallengeId ChallengeId { get; private set; }

        public IUserPayoff Payoff { get; private set; }
    }
}