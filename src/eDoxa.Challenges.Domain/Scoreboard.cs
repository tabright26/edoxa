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

using eDoxa.Challenges.Domain.AggregateModels;
using eDoxa.Challenges.Domain.AggregateModels.ChallengeAggregate;
using eDoxa.Functional;
using eDoxa.Functional.Extensions;

namespace eDoxa.Challenges.Domain
{
    public sealed class Scoreboard : Dictionary<UserId, Option<Score>>
    {
        public Scoreboard(Challenge challenge)
        {
            challenge.Participants.OrderByDescending(participant => participant.AverageScore.SingleOrDefault()).ForEach(participant => this.Add(participant.UserId, participant.AverageScore));
        }
    }
}