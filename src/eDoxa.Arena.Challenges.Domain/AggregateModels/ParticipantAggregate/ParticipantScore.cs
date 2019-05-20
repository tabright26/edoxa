// Filename: ParticipantScore.cs
// Date Created: 2019-04-21
// 
// ================================================
// Copyright © 2019, eDoxa. All rights reserved.
//  
// This file is subject to the terms and conditions
// defined in file 'LICENSE.md', which is part of
// this source code package.

using System.Linq;

namespace eDoxa.Arena.Challenges.Domain.AggregateModels.ParticipantAggregate
{
    public sealed class ParticipantScore : Score
    {
        internal ParticipantScore(Participant participant) : base(participant.Matches.OrderBy(match => match.TotalScore)
            .Take(participant.Challenge.Setup.BestOf).Average(match => match.TotalScore))
        {
        }
    }
}