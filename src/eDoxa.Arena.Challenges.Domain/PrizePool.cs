// Filename: PrizePool.cs
// Date Created: 2019-06-01
// 
// ================================================
// Copyright © 2019, eDoxa. All rights reserved.
// 
// This file is subject to the terms and conditions
// defined in file 'LICENSE.md', which is part of
// this source code package.

using System;

using eDoxa.Arena.Domain.ValueObjects;
using eDoxa.Seedwork.Domain.Common.Enumerations;

namespace eDoxa.Arena.Challenges.Domain
{
    public sealed class PrizePool : Prize
    {
        public PrizePool(Entries entries, EntryFee entryFee, ServiceChargeRatio serviceChargeRatio) : base(
            Math.Floor(entries * entryFee.Amount * (1 - Convert.ToDecimal(serviceChargeRatio))),
            entryFee.Type
        )
        {
        }

        public PrizePool(decimal amount, CurrencyType currency) : base(amount, currency)
        {
        }

        private PrizePool()
        {
            // Required by EF Core.
        }
    }
}
