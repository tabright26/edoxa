// Filename: ChallengeClosedDomainEvent.cs
// Date Created: 2019-11-05
// 
// ================================================
// Copyright © 2019, eDoxa. All rights reserved.

using eDoxa.Challenges.Domain.AggregateModels;
using eDoxa.Seedwork.Domain;

namespace eDoxa.Challenges.Domain.DomainEvents
{
    public sealed class ChallengeClosedDomainEvent : IDomainEvent
    {
        public ChallengeClosedDomainEvent(IChallenge challenge)
        {
            Challenge = challenge;
        }

        public IChallenge Challenge { get; }
    }
}
