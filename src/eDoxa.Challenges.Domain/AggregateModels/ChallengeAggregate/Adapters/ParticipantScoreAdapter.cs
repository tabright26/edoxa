// Filename: ParticipantScoreAdapter.cs
// Date Created: 2019-04-19
// 
// ================================================
// Copyright © 2019, eDoxa. All rights reserved.
//  
// This file is subject to the terms and conditions
// defined in file 'LICENSE.md', which is part of
// this source code package.

using System.Collections.Generic;
using System.Linq;

using eDoxa.Challenges.Domain.ValueObjects;

namespace eDoxa.Challenges.Domain.AggregateModels.ChallengeAggregate.Adapters
{
    internal sealed class ParticipantScoreAdapter : IScoreAdapter
    {
        private readonly BestOf _bestOf;
        private readonly IReadOnlyCollection<Match> _matches;

        public ParticipantScoreAdapter(Participant participant)
        {
            _bestOf = participant.Challenge.Settings.BestOf;
            _matches = participant.Matches;
        }

        public Score Score
        {
            get
            {
                IEnumerable<Match> score = _matches.OrderBy(match => match.TotalScore);

                var bestOf = _bestOf.ToInt32();

                if (_matches.Count >= bestOf)
                {
                    score = score.Take(bestOf);
                }

                return new Score(score.Average(match => match.TotalScore));
            }
        }
    }
}