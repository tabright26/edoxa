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

using eDoxa.Challenges.Domain.Entities.AggregateModels;
using eDoxa.Challenges.Domain.Entities.AggregateModels.ChallengeAggregate;
using eDoxa.Functional.Extensions;
using eDoxa.Functional.Maybe;

namespace eDoxa.Challenges.Domain.Entities
{
    public sealed class Scoreboard : Dictionary<UserId, Option<Score>>
    {
        public Scoreboard(Challenge challenge)
        {
            challenge.Participants.OrderByDescending(participant => participant.AverageScore.SingleOrDefault()).ForEach(participant => this.Add(participant.UserId, participant.AverageScore));
        }
    }
}