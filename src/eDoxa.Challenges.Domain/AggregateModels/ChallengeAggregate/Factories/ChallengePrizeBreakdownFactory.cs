// Filename: ChallengePrizeBreakdownFactory.cs
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
    public sealed class ChallengePrizeBreakdownFactory
    {
        private static readonly Lazy<ChallengePrizeBreakdownFactory> Lazy =
            new Lazy<ChallengePrizeBreakdownFactory>(() => new ChallengePrizeBreakdownFactory());

        public static ChallengePrizeBreakdownFactory Instance
        {
            get
            {
                return Lazy.Value;
            }
        }

        public IChallengePrizeBreakdownStrategy Create(ChallengeType type, int payoutEntries, decimal prizePool)
        {
            switch (type)
            {
                case ChallengeType.Default:
                    return new DefaultChallengePrizeBreakdownStrategy(payoutEntries, prizePool);
                default:
                    throw new NotImplementedException();
            }
        }
    }
}