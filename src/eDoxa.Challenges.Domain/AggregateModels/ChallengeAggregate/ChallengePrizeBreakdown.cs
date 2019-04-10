// Filename: ChallengePrizeBreakdown.cs
// Date Created: 2019-03-21
// 
// ============================================================
// Copyright © 2019, Francis Quenneville
// All rights reserved.
// 
// This file is subject to the terms and conditions defined in file 'LICENSE.md', which is part of
// this source code package.

using System;
using System.Collections.Generic;
using System.Linq;

namespace eDoxa.Challenges.Domain.AggregateModels.ChallengeAggregate
{
    public sealed class ChallengePrizeBreakdown : Dictionary<string, decimal>, IChallengePrizeBreakdown
    {
        public IReadOnlyDictionary<Guid, decimal?> SnapshotUserPrizes(IChallengeScoreboard scoreboard)
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
                    var prizeBreakdown = this.ElementAt(index);

                    var prize = prizeBreakdown.Value;

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