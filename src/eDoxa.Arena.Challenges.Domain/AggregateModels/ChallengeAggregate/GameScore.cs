// Filename: GameScore.cs
// Date Created: 2019-07-09
// 
// ================================================
// Copyright © 2019, eDoxa. All rights reserved.
// 
// This file is subject to the terms and conditions
// defined in file 'LICENSE.md', which is part of
// this source code package.

namespace eDoxa.Arena.Challenges.Domain.AggregateModels.ChallengeAggregate
{
    public sealed class GameScore : Score
    {
        public GameScore(ChallengeGame game, decimal score) : base(score)
        {
            Game = game;
        }

        public ChallengeGame Game { get; }
    }
}
