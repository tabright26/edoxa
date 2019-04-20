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

using eDoxa.Challenges.Domain.AggregateModels.ChallengeAggregate.Helpers;
using eDoxa.Challenges.Domain.AggregateModels.UserAggregate;

namespace eDoxa.Challenges.Domain.AggregateModels.ChallengeAggregate
{
    public sealed class ChallengePayout : IChallengePayout
    {
        private readonly Buckets _buckets;

        public ChallengePayout(Buckets buckets, PayoutLeftover leftover)
        {
            _buckets = buckets;
            Leftover = leftover;
        }

        public IBuckets Buckets => _buckets;

        public PayoutLeftover Leftover { get; }

        public IUserPrizes Snapshot(IChallengeScoreboard scoreboard)
        {
            var userScores = scoreboard.OrderByDescending(x => x.Value).ToDictionary(x => x.Key, t => t.Value);

            var userPrizes = new UserPrizes();

            foreach (var bucket in Buckets)
            {
                for (var index = 0; index < bucket.Size; index++)
                {
                    var userId = userScores.First().Key;

                    userPrizes.Add(userId, bucket.Prize);

                    userScores.Remove(userId);
                }
            }

            return userPrizes;
        }
    }
}