// Filename: BucketFactors.cs
// Date Created: 2019-05-23
// 
// ================================================
// Copyright © 2019, eDoxa. All rights reserved.
// 
// This file is subject to the terms and conditions
// defined in file 'LICENSE.md', which is part of
// this source code package.

using System.Collections.Generic;
using System.Linq;

using eDoxa.Arena.Domain;
using eDoxa.Arena.Domain.Abstractions;

namespace eDoxa.Arena.Challenges.Domain
{
    public sealed class BucketFactors : List<BucketFactor>, IBucketFactors
    {
        public IPayout CreatePayout(Prize prize)
        {
            return new Payout(new Buckets(this.Select(bucketFactor => bucketFactor.CreateBucket(prize))));
        }
    }
}
