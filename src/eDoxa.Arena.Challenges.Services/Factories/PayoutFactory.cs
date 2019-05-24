// Filename: PayoutFactory.cs
// Date Created: 2019-05-22
// 
// ================================================
// Copyright © 2019, eDoxa. All rights reserved.
// 
// This file is subject to the terms and conditions
// defined in file 'LICENSE.md', which is part of
// this source code package.

using System;

using eDoxa.Arena.Challenges.Domain;
using eDoxa.Arena.Challenges.Domain.Abstractions;

namespace eDoxa.Arena.Challenges.Services.Factories
{
    public sealed class PayoutFactory
    {
        private static readonly Payouts Payouts = new Payouts();
        private static readonly Lazy<PayoutFactory> Lazy = new Lazy<PayoutFactory>(() => new PayoutFactory());

        public static PayoutFactory Instance => Lazy.Value;

        public IPayout Create(PayoutEntries payoutEntries, EntryFee entryFee, Currency type)
        {
            return Payouts[payoutEntries].ApplyEntryFee(entryFee, type);
        }
    }
}
