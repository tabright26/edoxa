// Filename: PayoutFactory.cs
// Date Created: 2019-05-23
// 
// ================================================
// Copyright © 2019, eDoxa. All rights reserved.
// 
// This file is subject to the terms and conditions
// defined in file 'LICENSE.md', which is part of
// this source code package.

using System;

using eDoxa.Arena.Domain;
using eDoxa.Arena.Domain.Abstractions;
using eDoxa.Seedwork.Domain.Enumerations;

namespace eDoxa.Arena.Challenges.Domain.Factories
{
    public sealed partial class PayoutFactory
    {
        private static readonly Lazy<PayoutFactory> Lazy = new Lazy<PayoutFactory>(() => new PayoutFactory());

        public static PayoutFactory Instance => Lazy.Value;
    }

    public sealed partial class PayoutFactory
    {
        private static readonly PayoutBuckets PayoutBuckets = new PayoutBuckets();

        public IPayout Create(PayoutEntries payoutEntries, EntryFee entryFee)
        {
            if (PayoutBuckets.TryGetValue(payoutEntries, out var bucketFactors))
            {
                return bucketFactors.CreatePayout(DeterminePayoutPrize(entryFee));
            }

            throw new NotImplementedException();
        }

        private static Prize DeterminePayoutPrize(EntryFee entryFee)
        {
            if (entryFee.Currency == Currency.Money)
            {
                return new MoneyPrize(entryFee);
            }

            if (entryFee.Currency == Currency.Token)
            {
                return new TokenPrize(entryFee);
            }

            throw new InvalidOperationException();
        }
    }
}
