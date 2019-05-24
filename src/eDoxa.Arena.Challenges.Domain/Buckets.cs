﻿// Filename: Buckets.cs
// Date Created: 2019-05-22
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

namespace eDoxa.Arena.Challenges.Domain
{
    public class Buckets : List<Bucket>, IBuckets
    {
        internal Buckets(IEnumerable<Bucket> buckets) : base(buckets)
        {
        }

        internal Buckets()
        {
        }

        public Prize GetPrize(int index)
        {
            return this.ElementAtOrDefault(index)?.Prize ?? Prize.None;
        }
    }
}
