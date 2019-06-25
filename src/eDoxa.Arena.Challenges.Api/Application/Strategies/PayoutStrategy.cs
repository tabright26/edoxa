// Filename: PayoutStrategy.cs
// Date Created: 2019-06-25
// 
// ================================================
// Copyright © 2019, eDoxa. All rights reserved.
// 
// This file is subject to the terms and conditions
// defined in file 'LICENSE.md', which is part of
// this source code package.

using eDoxa.Arena.Challenges.Domain.AggregateModels;
using eDoxa.Arena.Challenges.Domain.AggregateModels.ChallengeAggregate;
using eDoxa.Arena.Challenges.Domain.Strategies;
using eDoxa.Arena.Challenges.Infrastructure.Data;

namespace eDoxa.Arena.Challenges.Api.Application.Strategies
{
    public sealed class PayoutStrategy : IPayoutStrategy
    {
        private static readonly PayoutChart PayoutChart = new PayoutChart();

        public IPayout GetPayout(PayoutEntries payoutEntries, EntryFee entryFee)
        {
            return PayoutChart.GetPayout(payoutEntries, entryFee);
        }
    }
}
