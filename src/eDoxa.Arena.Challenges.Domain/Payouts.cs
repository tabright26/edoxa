// Filename: Payouts.cs
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

using eDoxa.Arena.Challenges.Domain.Abstractions;

namespace eDoxa.Arena.Challenges.Domain
{
    public sealed class Payouts : ReadOnlyDictionary<PayoutEntries, IPayout>
    {
        public Payouts() : base(
            new Dictionary<PayoutEntries, IPayout>
            {
                [PayoutEntries.One] = new Payout(
                    new Buckets
                    {
                        new Bucket(new PrizeFactor(1.6M), new BucketSize(1))
                    }
                ),
                [PayoutEntries.Two] = new Payout(
                    new Buckets
                    {
                        new Bucket(new PrizeFactor(2.2M), new BucketSize(1)),
                        new Bucket(new PrizeFactor(1M), new BucketSize(1))
                    }
                ),
                [PayoutEntries.Three] = new Payout(
                    new Buckets
                    {
                        new Bucket(new PrizeFactor(4M), new BucketSize(1)),
                        new Bucket(new PrizeFactor(3M), new BucketSize(1)),
                        new Bucket(new PrizeFactor(1M), new BucketSize(1))
                    }
                ),
                [PayoutEntries.Four] = new Payout(
                    new Buckets
                    {
                        new Bucket(new PrizeFactor(2.8M), new BucketSize(1)),
                        new Bucket(new PrizeFactor(1.6M), new BucketSize(1)),
                        new Bucket(new PrizeFactor(1M), new BucketSize(2))
                    }
                ),
                [PayoutEntries.Five] = new Payout(
                    new Buckets
                    {
                        new Bucket(new PrizeFactor(3.2M), new BucketSize(1)),
                        new Bucket(new PrizeFactor(1.8M), new BucketSize(1)),
                        new Bucket(new PrizeFactor(1M), new BucketSize(3))
                    }
                ),
                [PayoutEntries.Ten] = new Payout(
                    new Buckets
                    {
                        new Bucket(new PrizeFactor(3.8M), new BucketSize(1)),
                        new Bucket(new PrizeFactor(2.4M), new BucketSize(1)),
                        new Bucket(new PrizeFactor(1.6M), new BucketSize(3)),
                        new Bucket(new PrizeFactor(1M), new BucketSize(5))
                    }
                ),
                [PayoutEntries.Fifteen] = new Payout(
                    new Buckets
                    {
                        new Bucket(new PrizeFactor(4M), new BucketSize(1)),
                        new Bucket(new PrizeFactor(3M), new BucketSize(1)),
                        new Bucket(new PrizeFactor(2M), new BucketSize(2)),
                        new Bucket(new PrizeFactor(1.4M), new BucketSize(4)),
                        new Bucket(new PrizeFactor(1M), new BucketSize(7))
                    }
                ),
                [PayoutEntries.Twenty] = new Payout(
                    new Buckets
                    {
                        new Bucket(new PrizeFactor(4.8M), new BucketSize(1)),
                        new Bucket(new PrizeFactor(3.6M), new BucketSize(1)),
                        new Bucket(new PrizeFactor(2.8M), new BucketSize(2)),
                        new Bucket(new PrizeFactor(1.4M), new BucketSize(5)),
                        new Bucket(new PrizeFactor(1M), new BucketSize(11))
                    }
                ),
                [PayoutEntries.TwentyFive] = new Payout(
                    new Buckets
                    {
                        new Bucket(new PrizeFactor(5M), new BucketSize(1)),
                        new Bucket(new PrizeFactor(4M), new BucketSize(1)),
                        new Bucket(new PrizeFactor(2.5M), new BucketSize(2)),
                        new Bucket(new PrizeFactor(2M), new BucketSize(3)),
                        new Bucket(new PrizeFactor(1.4M), new BucketSize(5)),
                        new Bucket(new PrizeFactor(1M), new BucketSize(13))
                    }
                ),
                [PayoutEntries.Fifty] = new Payout(
                    new Buckets
                    {
                        new Bucket(new PrizeFactor(6.8M), new BucketSize(1)),
                        new Bucket(new PrizeFactor(4.8M), new BucketSize(1)),
                        new Bucket(new PrizeFactor(3.8M), new BucketSize(1)),
                        new Bucket(new PrizeFactor(2.8M), new BucketSize(3)),
                        new Bucket(new PrizeFactor(2M), new BucketSize(5)),
                        new Bucket(new PrizeFactor(1.4M), new BucketSize(15)),
                        new Bucket(new PrizeFactor(1M), new BucketSize(25))
                    }
                ),
                [PayoutEntries.SeventyFive] = new Payout(
                    new Buckets
                    {
                        new Bucket(new PrizeFactor(7M), new BucketSize(1)),
                        new Bucket(new PrizeFactor(6M), new BucketSize(1)),
                        new Bucket(new PrizeFactor(5M), new BucketSize(1)),
                        new Bucket(new PrizeFactor(4M), new BucketSize(2)),
                        new Bucket(new PrizeFactor(3M), new BucketSize(4)),
                        new Bucket(new PrizeFactor(2M), new BucketSize(8)),
                        new Bucket(new PrizeFactor(1.4M), new BucketSize(20)),
                        new Bucket(new PrizeFactor(1M), new BucketSize(38))
                    }
                ),
                [PayoutEntries.OneHundred] = new Payout(
                    new Buckets
                    {
                        new Bucket(new PrizeFactor(8M), new BucketSize(1)),
                        new Bucket(new PrizeFactor(6M), new BucketSize(1)),
                        new Bucket(new PrizeFactor(4.8M), new BucketSize(1)),
                        new Bucket(new PrizeFactor(3.6M), new BucketSize(3)),
                        new Bucket(new PrizeFactor(3M), new BucketSize(5)),
                        new Bucket(new PrizeFactor(2.2M), new BucketSize(8)),
                        new Bucket(new PrizeFactor(1.8M), new BucketSize(15)),
                        new Bucket(new PrizeFactor(1.2M), new BucketSize(25)),
                        new Bucket(new PrizeFactor(1M), new BucketSize(41))
                    }
                )
            }
        )
        {
        }
    }
}
