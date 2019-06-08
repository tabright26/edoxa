// Filename: ChallengeCreatedDomainEvent.cs
// Date Created: 2019-06-01
// 
// ================================================
// Copyright © 2019, eDoxa. All rights reserved.
// 
// This file is subject to the terms and conditions
// defined in file 'LICENSE.md', which is part of
// this source code package.

using eDoxa.Arena.Challenges.Domain.AggregateModels.ChallengeAggregate;
using eDoxa.Seedwork.Domain;

namespace eDoxa.Arena.Challenges.Domain.DomainEvents
{
    public sealed class ChallengeCreatedDomainEvent : IDomainEvent
    {
        public ChallengeCreatedDomainEvent(Challenge challenge)
        {
            Challenge = challenge;
        }

        public Challenge Challenge { get; private set; }
    }
}
