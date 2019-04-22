// Filename: DefaultChallengeScoreboardStrategy.cs
// Date Created: 2019-04-21
// 
// ================================================
// Copyright © 2019, eDoxa. All rights reserved.
//  
// This file is subject to the terms and conditions
// defined in file 'LICENSE.md', which is part of
// this source code package.

using eDoxa.Functional.Extensions;

namespace eDoxa.Challenges.Domain.AggregateModels.ChallengeAggregate.Strategies
{
    public sealed class DefaultChallengeScoreboardStrategy : IChallengeScoreboardStrategy
    {
        private readonly Challenge _challenge;

        internal DefaultChallengeScoreboardStrategy(Challenge challenge)
        {
            _challenge = challenge;
        }

        public IChallengeScoreboard Scoreboard
        {
            get
            {
                var scoreboard = new ChallengeScoreboard();

                foreach (var participant in _challenge.Participants.OptionalOrderByDescending(participant =>
                    participant.AverageScore.Map<decimal?>(score => score)))
                {
                    scoreboard.Add(participant.UserId, participant.AverageScore);
                }

                return scoreboard;
            }
        }
    }
}