// Filename: PayoutStrategy.cs
// Date Created: 2019-06-02
// 
// ================================================
// Copyright © 2019, eDoxa. All rights reserved.
// 
// This file is subject to the terms and conditions
// defined in file 'LICENSE.md', which is part of
// this source code package.

using eDoxa.Arena.Challenges.Domain.Abstractions;
using eDoxa.Arena.Challenges.Domain.Abstractions.Strategies;
using eDoxa.Arena.Challenges.Domain.AggregateModels.ChallengeAggregate;

namespace eDoxa.Arena.Challenges.Domain.Strategies
{
    public sealed class PayoutStrategy : IPayoutStrategy
    {
        private readonly IPayout _payout;

        public PayoutStrategy(PayoutEntries payoutEntries, EntryFee entryFee)
        {
            var payoutChart = new PayoutChart();

            _payout = payoutChart.GetPayout(payoutEntries, entryFee);
        }

        public IPayout Payout => _payout;
    }
}
