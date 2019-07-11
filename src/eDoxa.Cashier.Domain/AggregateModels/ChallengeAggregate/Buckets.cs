// Filename: Buckets.cs
// Date Created: 2019-06-20
// 
// ================================================
// Copyright © 2019, eDoxa. All rights reserved.
// 
// This file is subject to the terms and conditions
// defined in file 'LICENSE.md', which is part of
// this source code package.

using System.Collections.Generic;
using System.Linq;

namespace eDoxa.Cashier.Domain.AggregateModels.ChallengeAggregate
{
    public sealed class Buckets : List<Bucket>, IBuckets
    {
        public Buckets(IEnumerable<Bucket> buckets) : base(buckets)
        {
        }

        public Prize PrizeAtOrDefault(int index)
        {
            return this.ElementAtOrDefault(index)?.Prize ?? Prize.None;
        }
    }
}
