// Filename: ChallengePayout.cs
// Date Created: 2019-04-14
// 
// ================================================
// Copyright © 2019, eDoxa. All rights reserved.
//  
// This file is subject to the terms and conditions
// defined in file 'LICENSE.md', which is part of
// this source code package.

using System.Linq;

using eDoxa.Challenges.Domain.AggregateModels.UserAggregate;
using eDoxa.Seedwork.Domain.Aggregate;

namespace eDoxa.Challenges.Domain.AggregateModels.ChallengeAggregate
{
    public sealed class Payout : ValueObject, IPayout
    {
        private readonly Buckets _buckets;

        public Payout(Buckets buckets)
        {
            _buckets = buckets;
        }

        public IBuckets Buckets => _buckets;

        public IUserPayoff Payoff(IScoreboard scoreboard)
        {
            return new UserPayoff();
        }
    }
}