﻿// Filename: Scoreboard.cs
// Date Created: 2019-06-25
// 
// ================================================
// Copyright © 2019, eDoxa. All rights reserved.

using System.Collections.Generic;
using System.Linq;

using eDoxa.Seedwork.Domain.Misc;

namespace eDoxa.Challenges.Domain.AggregateModels.ChallengeAggregate
{
    public sealed class Scoreboard : Dictionary<UserId, Score?>, IScoreboard
    {
        public Scoreboard(IChallenge challenge) : base(
            challenge.Participants.OrderByDescending(participant => participant.ComputeScore(challenge.BestOf))
                .ToDictionary(participant => participant.UserId, participant => participant.ComputeScore(challenge.BestOf))
        )
        {
        }
    }
}
