// Filename: ChallengeUserPrizesSnapshottedDomainEvent.cs
// Date Created: 2019-04-14
// 
// ================================================
// Copyright © 2019, eDoxa. All rights reserved.
//  
// This file is subject to the terms and conditions
// defined in file 'LICENSE.md', which is part of
// this source code package.

using System;
using System.Collections.Generic;

using eDoxa.Seedwork.Domain;

namespace eDoxa.Challenges.Domain.AggregateModels.ChallengeAggregate.DomainEvent
{
    public sealed class ChallengeUserPrizesSnapshottedDomainEvent : IDomainEvent
    {
        public ChallengeUserPrizesSnapshottedDomainEvent(Guid challengeId, IReadOnlyDictionary<UserId, Prize> userPrizes)
        {
            ChallengeId = challengeId;
            UserPrizes = userPrizes;
        }

        public Guid ChallengeId { get; private set; }

        public IReadOnlyDictionary<UserId, Prize> UserPrizes { get; private set; }
    }
}