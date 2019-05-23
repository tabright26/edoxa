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
                        new Bucket(new Prize(4M), new BucketSize(1))
                    }
                ),
                [PayoutEntries.Two] = new Payout(
                    new Buckets
                    {
                        new Bucket(new Prize(5.5M), new BucketSize(1)),
                        new Bucket(new Prize(2.5M), new BucketSize(1))
                    }
                ),
                [PayoutEntries.Three] = new Payout(
                    new Buckets
                    {
                        new Bucket(new Prize(10M), new BucketSize(1)),
                        new Bucket(new Prize(7.5M), new BucketSize(1)),
                        new Bucket(new Prize(2.5M), new BucketSize(1))
                    }
                ),
                [PayoutEntries.Four] = new Payout(
                    new Buckets
                    {
                        new Bucket(new Prize(7M), new BucketSize(1)),
                        new Bucket(new Prize(4M), new BucketSize(1)),
                        new Bucket(new Prize(2.5M), new BucketSize(2))
                    }
                ),
                [PayoutEntries.Five] = new Payout(
                    new Buckets
                    {
                        new Bucket(new Prize(8M), new BucketSize(1)),
                        new Bucket(new Prize(4.5M), new BucketSize(1)),
                        new Bucket(new Prize(2.5M), new BucketSize(3))
                    }
                ),
                [PayoutEntries.Ten] = new Payout(
                    new Buckets
                    {
                        new Bucket(new Prize(9.5M), new BucketSize(1)),
                        new Bucket(new Prize(6M), new BucketSize(1)),
                        new Bucket(new Prize(4M), new BucketSize(3)),
                        new Bucket(new Prize(2.5M), new BucketSize(5))
                    }
                ),
                [PayoutEntries.Fifteen] = new Payout(
                    new Buckets
                    {
                        new Bucket(new Prize(20M), new BucketSize(1)),
                        new Bucket(new Prize(15M), new BucketSize(1)),
                        new Bucket(new Prize(10M), new BucketSize(2)),
                        new Bucket(new Prize(7M), new BucketSize(4)),
                        new Bucket(new Prize(5M), new BucketSize(7))
                    }
                ),
                [PayoutEntries.Twenty] = new Payout(
                    new Buckets
                    {
                        new Bucket(new Prize(12M), new BucketSize(1)),
                        new Bucket(new Prize(9M), new BucketSize(1)),
                        new Bucket(new Prize(7M), new BucketSize(2)),
                        new Bucket(new Prize(3.5M), new BucketSize(5)),
                        new Bucket(new Prize(2.5M), new BucketSize(11))
                    }
                ),
                [PayoutEntries.TwentyFive] = new Payout(
                    new Buckets
                    {
                        new Bucket(new Prize(12.5M), new BucketSize(1)),
                        new Bucket(new Prize(10M), new BucketSize(1)),
                        new Bucket(new Prize(6.25M), new BucketSize(2)),
                        new Bucket(new Prize(5M), new BucketSize(3)),
                        new Bucket(new Prize(3.5M), new BucketSize(5)),
                        new Bucket(new Prize(2.5M), new BucketSize(13))
                    }
                ),
                [PayoutEntries.Fifty] = new Payout(
                    new Buckets
                    {
                        new Bucket(new Prize(17M), new BucketSize(1)),
                        new Bucket(new Prize(12M), new BucketSize(1)),
                        new Bucket(new Prize(9.5M), new BucketSize(1)),
                        new Bucket(new Prize(7M), new BucketSize(3)),
                        new Bucket(new Prize(5M), new BucketSize(5)),
                        new Bucket(new Prize(3.5M), new BucketSize(15)),
                        new Bucket(new Prize(2.5M), new BucketSize(25))
                    }
                ),
                [PayoutEntries.SeventyFive] = new Payout(
                    new Buckets
                    {
                        new Bucket(new Prize(17.5M), new BucketSize(1)),
                        new Bucket(new Prize(15M), new BucketSize(1)),
                        new Bucket(new Prize(12.5M), new BucketSize(1)),
                        new Bucket(new Prize(10M), new BucketSize(2)),
                        new Bucket(new Prize(7.5M), new BucketSize(4)),
                        new Bucket(new Prize(5M), new BucketSize(8)),
                        new Bucket(new Prize(3.5M), new BucketSize(20)),
                        new Bucket(new Prize(2.5M), new BucketSize(38))
                    }
                ),
                [PayoutEntries.OneHundred] = new Payout(
                    new Buckets
                    {
                        new Bucket(new Prize(20M), new BucketSize(1)),
                        new Bucket(new Prize(15M), new BucketSize(1)),
                        new Bucket(new Prize(12M), new BucketSize(1)),
                        new Bucket(new Prize(9M), new BucketSize(3)),
                        new Bucket(new Prize(7.5M), new BucketSize(5)),
                        new Bucket(new Prize(5.5M), new BucketSize(8)),
                        new Bucket(new Prize(4.5M), new BucketSize(15)),
                        new Bucket(new Prize(3M), new BucketSize(25)),
                        new Bucket(new Prize(2.5M), new BucketSize(41))
                    }
                )
            }
        )
        {
        }
    }
}
