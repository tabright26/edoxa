﻿// Filename: ChallengePayout.cs
// Date Created: 2019-04-14
// 
// ================================================
// Copyright © 2019, eDoxa. All rights reserved.
//  
// This file is subject to the terms and conditions
// defined in file 'LICENSE.md', which is part of
// this source code package.

using eDoxa.Challenges.Domain.AggregateModels.UserAggregate;
using eDoxa.Seedwork.Domain.Aggregate;

namespace eDoxa.Challenges.Domain.AggregateModels.ChallengeAggregate
{
    public sealed class ChallengePayout : ValueObject, IChallengePayout
    {
        private readonly Buckets _buckets;
        private readonly PayoutLeftover _leftover;

        public ChallengePayout(Buckets buckets, PayoutLeftover leftover)
        {
            _buckets = buckets;
            _leftover = leftover;
        }

        public PayoutLeftover Leftover => _leftover;

        public IBuckets Buckets => _buckets;

        public IUserPrizes Snapshot(IChallengeScoreboard scoreboard)
        {
            return new UserPrizes();
        }
    }
}