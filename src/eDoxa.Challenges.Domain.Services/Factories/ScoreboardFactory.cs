// Filename: ChallengeScoreboardFactory.cs
// Date Created: 2019-04-21
// 
// ================================================
// Copyright © 2019, eDoxa. All rights reserved.
//  
// This file is subject to the terms and conditions
// defined in file 'LICENSE.md', which is part of
// this source code package.

using System;

using eDoxa.Challenges.Domain.Entities;
using eDoxa.Challenges.Domain.Entities.Abstractions;
using eDoxa.Challenges.Domain.Entities.AggregateModels.ChallengeAggregate;
using eDoxa.Challenges.Domain.Entities.Default.Strategies;

namespace eDoxa.Challenges.Domain.Services.Factories
{
    public sealed class ScoreboardFactory
    {
        private static readonly Lazy<ScoreboardFactory> Lazy = new Lazy<ScoreboardFactory>(() => new ScoreboardFactory());

        public static ScoreboardFactory Instance => Lazy.Value;

        public IScoreboardStrategy CreateScoreboard(Challenge challenge)
        {
            switch (challenge.Setup.Type)
            {
                case ChallengeType.Default:

                    return new DefaultScoreboardStrategy(challenge);

                default:

                    throw new ArgumentException(nameof(challenge.Setup.Type));
            }
        }
    }
}