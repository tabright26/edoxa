// Filename: ChallengeScoreboard.cs
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

using eDoxa.Arena.Challenges.Domain.AggregateModels;
using eDoxa.Arena.Challenges.Domain.AggregateModels.ChallengeAggregate;
using eDoxa.Functional;
using eDoxa.Functional.Extensions;

namespace eDoxa.Arena.Challenges.Domain
{
    public sealed class Scoreboard : Dictionary<UserId, Option<Score>>
    {
        public Scoreboard(Challenge challenge)
        {
            challenge.Participants.OrderByDescending(participant => participant.AverageScore.SingleOrDefault()).ForEach(participant => this.Add(participant.UserId, participant.AverageScore));
        }
    }
}