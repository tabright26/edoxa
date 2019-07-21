// Filename: Buckets.cs
// Date Created: 2019-07-10
// 
// ================================================
// Copyright © 2019, eDoxa. All rights reserved.

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
