// Filename: Buckets.cs
// Date Created: 2019-06-07
// 
// ================================================
// Copyright © 2019, eDoxa. All rights reserved.
// 
// This file is subject to the terms and conditions
// defined in file 'LICENSE.md', which is part of
// this source code package.

using System.Collections.Generic;
using System.Linq;

using eDoxa.Arena.Challenges.Domain.Abstractions;

namespace eDoxa.Arena.Challenges.Domain.AggregateModels
{
    public class Buckets : List<Bucket>, IBuckets
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
