// Filename: PrizePool.cs
// Date Created: 2019-05-20
// 
// ================================================
// Copyright © 2019, eDoxa. All rights reserved.
// 
// This file is subject to the terms and conditions
// defined in file 'LICENSE.md', which is part of
// this source code package.

using System;
using System.Globalization;

using eDoxa.Seedwork.Domain.Aggregate;

namespace eDoxa.Arena.Challenges.Domain
{
    public class PrizePool : TypeObject<PrizePool, decimal>
    {
        public PrizePool(Entries entries, EntryFee entryFee, ServiceChargeRatio serviceChargeRatio) : base(Math.Floor(entries * entryFee * (1 - Convert.ToDecimal(serviceChargeRatio))))
        {
        }

        public override string ToString()
        {
            return Value.ToString(CultureInfo.InvariantCulture);
        }
    }
}
