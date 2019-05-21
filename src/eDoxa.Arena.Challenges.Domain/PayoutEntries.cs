// Filename: PayoutEntries.cs
// Date Created: 2019-05-20
// 
// ================================================
// Copyright © 2019, eDoxa. All rights reserved.
// 
// This file is subject to the terms and conditions
// defined in file 'LICENSE.md', which is part of
// this source code package.

using System;
using System.Linq;

using eDoxa.Arena.Challenges.Domain.Abstractions;
using eDoxa.Seedwork.Domain.Aggregate;

namespace eDoxa.Arena.Challenges.Domain
{
    public class PayoutEntries : TypeObject<PayoutEntries, int>
    {
        public PayoutEntries(Entries entries, PayoutRatio payoutRatio) : base(Convert.ToInt32(Math.Floor(entries * payoutRatio)))
        {
        }

        public PayoutEntries(IPayout payout) : base(payout.Buckets.Sum(bucket => bucket.Size))
        {
        }
    }
}
