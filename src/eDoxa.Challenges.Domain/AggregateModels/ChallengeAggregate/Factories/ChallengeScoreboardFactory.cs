// Filename: ChallengeScoreboardFactory.cs
// Date Created: 2019-03-20
// 
// ============================================================
// Copyright © 2019, Francis Quenneville
// All rights reserved.
// 
// This file is subject to the terms and conditions defined in file 'LICENSE.md', which is part of
// this source code package.

using System;

using eDoxa.Challenges.Domain.AggregateModels.ChallengeAggregate.Strategies;

namespace eDoxa.Challenges.Domain.AggregateModels.ChallengeAggregate.Factories
{
    public sealed class ChallengeScoreboardFactory
    {
        private static readonly Lazy<ChallengeScoreboardFactory> Lazy = new Lazy<ChallengeScoreboardFactory>(() => new ChallengeScoreboardFactory());

        public static ChallengeScoreboardFactory Instance
        {
            get
            {
                return Lazy.Value;
            }
        }

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