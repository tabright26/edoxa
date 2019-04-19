// Filename: DefaultChallengePayoutStrategy.cs
// Date Created: 2019-04-14
// 
// ================================================
// Copyright © 2019, eDoxa. All rights reserved.
//  
// This file is subject to the terms and conditions
// defined in file 'LICENSE.md', which is part of
// this source code package.

using System;
using System.Linq;

namespace eDoxa.Challenges.Domain.AggregateModels.ChallengeAggregate.Strategies
{
    public class DefaultChallengePayoutStrategy : IChallengePayoutStrategy
    {
        private const float Factor = 0.9F;

        private readonly int _payoutEntries;
        private readonly decimal _prizePool;

        // TODO: This algorithm does not work when registration fees are higher than entries.
        // TODO: Refactor the prize breakdown to manage prize groups.
        public DefaultChallengePayoutStrategy(int payoutEntries, decimal prizePool)
        {
            _payoutEntries = payoutEntries;
            _prizePool = prizePool;
        }

        public IChallengePayout Payout
        {
            get
            {
                var payout = new ChallengePayout();

                for (var index = 1; index <= _payoutEntries; index++)
                {
                    var prize = ComputePrize(index);

                    payout.Add(index.ToString(), prize);
                }

                if (payout.ContainsKey(1.ToString()))
                {
                    payout[1.ToString()] += _prizePool - payout.Sum(x => x.Value);
                }

                return payout;

                decimal ComputePrize(int index)
                {
                    return Math.Round(
                        Math.Floor(
                            ((1 - (decimal) Factor) / 1 - (decimal) Math.Pow(Factor, _payoutEntries)) * (decimal) Math.Pow(Factor, index - 1) * _prizePool
                        ),
                        2
                    );
                }
            }
        }
    }
}