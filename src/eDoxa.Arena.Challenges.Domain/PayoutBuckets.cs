// Filename: PayoutBuckets.cs
// Date Created: 2019-05-22
// 
// ================================================
// Copyright © 2019, eDoxa. All rights reserved.
// 
// This file is subject to the terms and conditions
// defined in file 'LICENSE.md', which is part of
// this source code package.

using System.Collections.Generic;
using System.Collections.ObjectModel;

using eDoxa.Arena.Domain;
using eDoxa.Arena.Domain.Abstractions;

namespace eDoxa.Arena.Challenges.Domain
{
    public sealed class PayoutBuckets : ReadOnlyDictionary<PayoutEntries, IBucketFactors>
    {
        public PayoutBuckets() : base(
            new Dictionary<PayoutEntries, IBucketFactors>
            {
                [PayoutEntries.One] = new BucketFactors
                {
                    new BucketFactor(new PrizeFactor(1.6M), new BucketSize(1))
                },
                [PayoutEntries.Two] = new BucketFactors
                {
                    new BucketFactor(new PrizeFactor(2.2M), new BucketSize(1)),
                    new BucketFactor(new PrizeFactor(1M), new BucketSize(1))
                },
                [PayoutEntries.Three] = new BucketFactors
                {
                    new BucketFactor(new PrizeFactor(4M), new BucketSize(1)),
                    new BucketFactor(new PrizeFactor(3M), new BucketSize(1)),
                    new BucketFactor(new PrizeFactor(1M), new BucketSize(1))
                },
                [PayoutEntries.Four] = new BucketFactors
                {
                    new BucketFactor(new PrizeFactor(2.8M), new BucketSize(1)),
                    new BucketFactor(new PrizeFactor(1.6M), new BucketSize(1)),
                    new BucketFactor(new PrizeFactor(1M), new BucketSize(2))
                },
                [PayoutEntries.Five] = new BucketFactors
                {
                    new BucketFactor(new PrizeFactor(3.2M), new BucketSize(1)),
                    new BucketFactor(new PrizeFactor(1.8M), new BucketSize(1)),
                    new BucketFactor(new PrizeFactor(1M), new BucketSize(3))
                },
                [PayoutEntries.Ten] = new BucketFactors
                {
                    new BucketFactor(new PrizeFactor(3.8M), new BucketSize(1)),
                    new BucketFactor(new PrizeFactor(2.4M), new BucketSize(1)),
                    new BucketFactor(new PrizeFactor(1.6M), new BucketSize(3)),
                    new BucketFactor(new PrizeFactor(1M), new BucketSize(5))
                },
                [PayoutEntries.Fifteen] = new BucketFactors
                {
                    new BucketFactor(new PrizeFactor(4M), new BucketSize(1)),
                    new BucketFactor(new PrizeFactor(3M), new BucketSize(1)),
                    new BucketFactor(new PrizeFactor(2M), new BucketSize(2)),
                    new BucketFactor(new PrizeFactor(1.4M), new BucketSize(4)),
                    new BucketFactor(new PrizeFactor(1M), new BucketSize(7))
                },
                [PayoutEntries.Twenty] = new BucketFactors
                {
                    new BucketFactor(new PrizeFactor(4.8M), new BucketSize(1)),
                    new BucketFactor(new PrizeFactor(3.6M), new BucketSize(1)),
                    new BucketFactor(new PrizeFactor(2.8M), new BucketSize(2)),
                    new BucketFactor(new PrizeFactor(1.4M), new BucketSize(5)),
                    new BucketFactor(new PrizeFactor(1M), new BucketSize(11))
                },
                [PayoutEntries.TwentyFive] = new BucketFactors
                {
                    new BucketFactor(new PrizeFactor(5M), new BucketSize(1)),
                    new BucketFactor(new PrizeFactor(4M), new BucketSize(1)),
                    new BucketFactor(new PrizeFactor(2.5M), new BucketSize(2)),
                    new BucketFactor(new PrizeFactor(2M), new BucketSize(3)),
                    new BucketFactor(new PrizeFactor(1.4M), new BucketSize(5)),
                    new BucketFactor(new PrizeFactor(1M), new BucketSize(13))
                },
                [PayoutEntries.Fifty] = new BucketFactors
                {
                    new BucketFactor(new PrizeFactor(6.8M), new BucketSize(1)),
                    new BucketFactor(new PrizeFactor(4.8M), new BucketSize(1)),
                    new BucketFactor(new PrizeFactor(3.8M), new BucketSize(1)),
                    new BucketFactor(new PrizeFactor(2.8M), new BucketSize(3)),
                    new BucketFactor(new PrizeFactor(2M), new BucketSize(5)),
                    new BucketFactor(new PrizeFactor(1.4M), new BucketSize(15)),
                    new BucketFactor(new PrizeFactor(1M), new BucketSize(25))
                },
                [PayoutEntries.SeventyFive] = new BucketFactors
                {
                    new BucketFactor(new PrizeFactor(7M), new BucketSize(1)),
                    new BucketFactor(new PrizeFactor(6M), new BucketSize(1)),
                    new BucketFactor(new PrizeFactor(5M), new BucketSize(1)),
                    new BucketFactor(new PrizeFactor(4M), new BucketSize(2)),
                    new BucketFactor(new PrizeFactor(3M), new BucketSize(4)),
                    new BucketFactor(new PrizeFactor(2M), new BucketSize(8)),
                    new BucketFactor(new PrizeFactor(1.4M), new BucketSize(20)),
                    new BucketFactor(new PrizeFactor(1M), new BucketSize(38))
                },
                [PayoutEntries.OneHundred] = new BucketFactors
                {
                    new BucketFactor(new PrizeFactor(8M), new BucketSize(1)),
                    new BucketFactor(new PrizeFactor(6M), new BucketSize(1)),
                    new BucketFactor(new PrizeFactor(4.8M), new BucketSize(1)),
                    new BucketFactor(new PrizeFactor(3.6M), new BucketSize(3)),
                    new BucketFactor(new PrizeFactor(3M), new BucketSize(5)),
                    new BucketFactor(new PrizeFactor(2.2M), new BucketSize(8)),
                    new BucketFactor(new PrizeFactor(1.8M), new BucketSize(15)),
                    new BucketFactor(new PrizeFactor(1.2M), new BucketSize(25)),
                    new BucketFactor(new PrizeFactor(1M), new BucketSize(41))
                }
            }
        )
        {
        }
    }
}
