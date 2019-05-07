// Filename: DefaultChallengeScoreboardStrategy.cs
// Date Created: 2019-04-21
// 
// ================================================
// Copyright © 2019, eDoxa. All rights reserved.
//  
// This file is subject to the terms and conditions
// defined in file 'LICENSE.md', which is part of
// this source code package.

using System.Linq;

namespace eDoxa.Challenges.Domain.Entities.AggregateModels.ChallengeAggregate.Strategies
{
    public sealed class DefaultScoreboardStrategy : IScoreboardStrategy
    {
        private readonly Challenge _challenge;

        internal DefaultScoreboardStrategy(Challenge challenge)
        {
            _challenge = challenge;
        }

        public IScoreboard Scoreboard
        {
            get
            {
                var scoreboard = new Scoreboard();

                foreach (var participant in _challenge.Participants.OrderByDescending(participant => participant.AverageScore.SingleOrDefault()))
                {
                    scoreboard.Add(participant.UserId, participant.AverageScore);
                }

                return scoreboard;
            }
        }
    }
}