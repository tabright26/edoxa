// Filename: Scoreboard.cs
// Date Created: 2019-05-20
// 
// ================================================
// Copyright © 2019, eDoxa. All rights reserved.
// 
// This file is subject to the terms and conditions
// defined in file 'LICENSE.md', which is part of
// this source code package.

using System.Collections.Generic;
using System.Linq;

using eDoxa.Arena.Challenges.Domain.AggregateModels.ChallengeAggregate;
using eDoxa.Arena.Domain.Abstractions;
using eDoxa.Seedwork.Domain.Entities;

namespace eDoxa.Arena.Challenges.Domain
{
    public sealed class Scoreboard : Dictionary<UserId, Score>, IScoreboard
    {
        public Scoreboard(Challenge challenge) : base(
            challenge.Participants.OrderByDescending(participant => participant.AverageScore.SingleOrDefault())
                     .ToDictionary(participant => participant.UserId, participant => participant.AverageScore.SingleOrDefault())
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
