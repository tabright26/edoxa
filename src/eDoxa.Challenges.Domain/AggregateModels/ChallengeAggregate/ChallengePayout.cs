// Filename: ChallengePayout.cs
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
using System.Linq;

namespace eDoxa.Challenges.Domain.AggregateModels.ChallengeAggregate
{
    public sealed class ChallengePayout : Dictionary<string, decimal>, IChallengePayout
    {
        public IReadOnlyDictionary<Guid, decimal?> Snapshot(IChallengeScoreboard scoreboard)
        {
            var prizes = new Dictionary<Guid, decimal?>();

            for (var index = 0; index < scoreboard.Count; index++)
            {
                var board = scoreboard.ElementAt(index);

                var userId = board.Key;

                var score = board.Value;

                if (score == null)
                {
                    prizes.Add(userId.ToGuid(), null);

                    continue;
                }

                try
                {
                    // TODO: Refactor this part of the algorithm when prize breakdown default strategy will be debug.
                    var payout = this.ElementAt(index);

                    var prize = payout.Value;

                    prizes.Add(userId.ToGuid(), prize);
                }
                catch (ArgumentOutOfRangeException)
                {
                    prizes.Add(userId.ToGuid(), decimal.Zero);
                }
            }

            return prizes;
        }
    }
}