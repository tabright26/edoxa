// Filename: ChallengePayoutFactory.cs
// Date Created: 2019-04-21
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
    public sealed class ChallengePayoutFactory
    {
        private static readonly Lazy<ChallengePayoutFactory> Lazy = new Lazy<ChallengePayoutFactory>(() => new ChallengePayoutFactory());

        public static ChallengePayoutFactory Instance => Lazy.Value;

        public IPayoutStrategy CreatePayout(ChallengeType type, PayoutEntries payoutEntries, PrizePool prizePool, EntryFee entryFee)
        {
            switch (type)
            {
                case ChallengeType.Default:

                    return new DefaultPayoutStrategy(payoutEntries, prizePool, entryFee);

                default:

                    throw new ArgumentException(nameof(type));
            }
        }
    }
}