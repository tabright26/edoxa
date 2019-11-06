// Filename: ChallengeStartedDomainEvent.cs
// Date Created: 2019-10-06
// 
// ================================================
// Copyright © 2019, eDoxa. All rights reserved.

using eDoxa.Challenges.Domain.AggregateModels;
using eDoxa.Seedwork.Domain;

namespace eDoxa.Challenges.Domain.DomainEvents
{
    public sealed class ChallengeStartedDomainEvent : IDomainEvent
    {
        public ChallengeStartedDomainEvent(IChallenge challenge)
        {
            Challenge = challenge;
        }

        public IChallenge Challenge { get; }
    }
}
