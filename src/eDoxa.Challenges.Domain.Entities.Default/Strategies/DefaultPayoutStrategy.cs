// Filename: DefaultChallengePayoutStrategy.cs
// Date Created: 2019-04-14
// 
// ================================================
// Copyright © 2019, eDoxa. All rights reserved.
//  
// This file is subject to the terms and conditions
// defined in file 'LICENSE.md', which is part of
// this source code package.

using eDoxa.Challenges.Domain.Entities.Abstractions;
using eDoxa.Challenges.Domain.Entities.AggregateModels.ChallengeAggregate;

namespace eDoxa.Challenges.Domain.Entities.Default.Strategies
{
    public sealed class DefaultPayoutStrategy : IPayoutStrategy
    {
        private readonly EntryFee _entryFee;
        private readonly PayoutEntries _payoutEntries;
        private readonly PrizePool _prizePool;

        public DefaultPayoutStrategy(PayoutEntries payoutEntries, PrizePool prizePool, EntryFee entryFee)
        {
            _payoutEntries = payoutEntries;
            _prizePool = prizePool;
            _entryFee = entryFee;
        }

        public IPayout Payout => new Payout();
    }
}