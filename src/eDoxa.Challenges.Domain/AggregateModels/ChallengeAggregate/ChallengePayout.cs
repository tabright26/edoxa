// Filename: ChallengePayout.cs
// Date Created: 2019-04-14
// 
// ================================================
// Copyright © 2019, eDoxa. All rights reserved.
//  
// This file is subject to the terms and conditions
// defined in file 'LICENSE.md', which is part of
// this source code package.

using System.Collections.Generic;
using System.Linq;

using eDoxa.Challenges.Domain.AggregateModels.UserAggregate;

namespace eDoxa.Challenges.Domain.AggregateModels.ChallengeAggregate
{
    public sealed class ChallengePayout : Dictionary<Bucket, Prize>, IChallengePayout
    {
        public IUserPrizes Snapshot(IChallengeScoreboard scoreboard)
        {
            var userScores = scoreboard.OrderByDescending(x => x.Value).ToDictionary(x => x.Key, t => t.Value);

            var userPrizes = new UserPrizes();

            foreach (var payout in this)
            {
                for (var index = 0; index < payout.Key.Size; index++)
                {
                    var userId = userScores.First().Key;

                    userPrizes.Add(userId, payout.Value);

                    userScores.Remove(userId);
                }
            }

            return userPrizes;
        }
    }
}