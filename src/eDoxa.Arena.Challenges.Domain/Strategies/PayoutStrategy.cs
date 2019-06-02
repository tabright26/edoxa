// Filename: PayoutStrategy.cs
// Date Created: 2019-06-02
// 
// ================================================
// Copyright © 2019, eDoxa. All rights reserved.
// 
// This file is subject to the terms and conditions
// defined in file 'LICENSE.md', which is part of
// this source code package.

using System;

using eDoxa.Arena.Challenges.Domain.Abstractions;
using eDoxa.Arena.Domain.Abstractions;
using eDoxa.Arena.Domain.ValueObjects;

namespace eDoxa.Arena.Challenges.Domain.Strategies
{
    public sealed class PayoutStrategy : IPayoutStrategy
    {
        private static readonly PayoutBuckets PayoutBuckets = new PayoutBuckets();
        private readonly PayoutEntries _payoutEntries;
        private readonly EntryFee _entryFee;

        public PayoutStrategy(PayoutEntries payoutEntries, EntryFee entryFee)
        {
            _payoutEntries = payoutEntries;
            _entryFee = entryFee;
        }

        public IPayout Payout
        {
            get
            {
                if (PayoutBuckets.TryGetValue(_payoutEntries, out var bucketFactors))
                {
                    return bucketFactors.CreatePayout(_entryFee.GetLowestPrize());
                }

                throw new NotImplementedException();
            }
        }
    }
}
