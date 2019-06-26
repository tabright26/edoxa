// Filename: ParticipantScore.cs
// Date Created: 2019-06-20
// 
// ================================================
// Copyright © 2019, eDoxa. All rights reserved.
// 
// This file is subject to the terms and conditions
// defined in file 'LICENSE.md', which is part of
// this source code package.

using System.Collections.Generic;
using System.Linq;

namespace eDoxa.Arena.Challenges.Domain.AggregateModels.ChallengeAggregate
{
    public sealed class ParticipantScore : Score
    {
        internal ParticipantScore(Participant participant, BestOf bestOf) : base(
            Resolve(participant.Matches.Select(match => (decimal) match.TotalScore), bestOf)
        )
        {
        }

        public static decimal Resolve(IEnumerable<decimal> totalScores, int bestOf)
        {
            return totalScores.OrderByDescending(totalScore => totalScore).Take(bestOf).Average(totalScore => totalScore);
        }
    }
}
