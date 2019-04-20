// Filename: ChallengeScoreboardFactory.cs
// Date Created: 2019-04-14
// 
// ================================================
// Copyright © 2019, eDoxa. All rights reserved.
//  
// This file is subject to the terms and conditions
// defined in file 'LICENSE.md', which is part of
// this source code package.

using System;

using eDoxa.Challenges.Domain.AggregateModels.ChallengeAggregate.Strategies;

namespace eDoxa.Challenges.Domain.AggregateModels.ChallengeAggregate.Factories
{
    public sealed class ChallengeScoreboardFactory
    {
        private static readonly Lazy<ChallengeScoreboardFactory> Lazy = new Lazy<ChallengeScoreboardFactory>(() => new ChallengeScoreboardFactory());

        public static ChallengeScoreboardFactory Instance => Lazy.Value;

        public IChallengeScoreboardStrategy Create(Challenge challenge)
        {
            switch (challenge.Settings.Type)
            {
                case ChallengeType.Default:

                    return new DefaultChallengeScoreboardStrategy(challenge);
                default:

                    throw new NotImplementedException();
            }
        }
    }
}