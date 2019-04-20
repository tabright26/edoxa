// Filename: DefaultChallengePayoutStrategy.cs
// Date Created: 2019-04-14
// 
// ================================================
// Copyright © 2019, eDoxa. All rights reserved.
//  
// This file is subject to the terms and conditions
// defined in file 'LICENSE.md', which is part of
// this source code package.

using eDoxa.Challenges.Domain.AggregateModels.ChallengeAggregate.Helpers;

namespace eDoxa.Challenges.Domain.AggregateModels.ChallengeAggregate.Strategies
{
    public class DefaultChallengePayoutStrategy : IChallengePayoutStrategy
    {
        private readonly EntryFee _entryFee;
        private readonly PayoutEntries _payoutEntries;
        private readonly PrizePool _prizePool;
        private readonly WinnerPrize _winnerPrize;

        public DefaultChallengePayoutStrategy(PayoutEntries payoutEntries, PrizePool prizePool, EntryFee entryFee)
        {
            _payoutEntries = payoutEntries;
            _prizePool = prizePool;
            _entryFee = entryFee;
            _winnerPrize = new WinnerPrize(prizePool);
        }

        public IChallengePayout Payout
        {
            get
            {
                var buckets = new Buckets();

                var leftover = new PayoutLeftover(0);

                return new ChallengePayout(buckets, leftover);
            }
        }
    }
}