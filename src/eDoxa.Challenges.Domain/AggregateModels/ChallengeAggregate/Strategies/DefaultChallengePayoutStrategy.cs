// Filename: DefaultChallengePayoutStrategy.cs
// Date Created: 2019-04-14
// 
// ================================================
// Copyright © 2019, eDoxa. All rights reserved.
//  
// This file is subject to the terms and conditions
// defined in file 'LICENSE.md', which is part of
// this source code package.

namespace eDoxa.Challenges.Domain.AggregateModels.ChallengeAggregate.Strategies
{
    public class DefaultChallengePayoutStrategy : IChallengePayoutStrategy
    {
        private readonly PayoutEntries _payoutEntries;
        private readonly PrizePool _prizePool;

        public DefaultChallengePayoutStrategy(PayoutEntries payoutEntries, PrizePool prizePool)
        {
            _payoutEntries = payoutEntries;
            _prizePool = prizePool;
        }

        public IChallengePayout Payout => new ChallengePayout();
    }
}