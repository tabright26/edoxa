// Filename: Scoreboard.cs
// Date Created: 2019-06-12
// 
// ================================================
// Copyright © 2019, eDoxa. All rights reserved.
// 
// This file is subject to the terms and conditions
// defined in file 'LICENSE.md', which is part of
// this source code package.

using System.Collections.Generic;
using System.Linq;

using eDoxa.Arena.Challenges.Domain.Abstractions;
using eDoxa.Seedwork.Common.ValueObjects;

namespace eDoxa.Arena.Challenges.Domain.AggregateModels.ChallengeAggregate
{
    public sealed class Scoreboard : Dictionary<UserId, Score>, IScoreboard
    {
        public Scoreboard(IEnumerable<Participant> participants, BestOf bestOf) : base(
            participants.OrderByDescending(participant => participant.AverageScore(bestOf))
                .ToDictionary(participant => participant.UserId, participant => participant.AverageScore(bestOf))
        )
        {
        }

        public UserId UserIdAt(int index)
        {
            return this.ElementAt(index).Key;
        }

        public bool IsValidScore(int index)
        {
            return this.ElementAtOrDefault(index).Value != null;
        }
    }
}
