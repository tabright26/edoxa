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
    public sealed class Payouts : ReadOnlyDictionary<PayoutEntryType, IPayout>
    {
        public Payouts() : base(
            new Dictionary<PayoutEntryType, IPayout>
            {
                [PayoutEntryType.One] = new Payout(
                    new Buckets
                    {
                        new Bucket(new Prize(4M), 1)
                    }
                ),
                [PayoutEntryType.Two] = new Payout(
                    new Buckets
                    {
                        new Bucket(new Prize(5.5M), 1),
                        new Bucket(new Prize(2.5M), 1)
                    }
                ),
                [PayoutEntryType.Three] = new Payout(
                    new Buckets
                    {
                        new Bucket(new Prize(10M), 1),
                        new Bucket(new Prize(7.5M), 1),
                        new Bucket(new Prize(2.5M), 1)
                    }
                ),
                [PayoutEntryType.Four] = new Payout(
                    new Buckets
                    {
                        new Bucket(new Prize(7M), 1),
                        new Bucket(new Prize(4M), 1),
                        new Bucket(new Prize(2.5M), 2)
                    }
                ),
                [PayoutEntryType.Five] = new Payout(
                    new Buckets
                    {
                        new Bucket(new Prize(8M), 1),
                        new Bucket(new Prize(4.5M), 1),
                        new Bucket(new Prize(2.5M), 3)
                    }
                ),
                [PayoutEntryType.Ten] = new Payout(
                    new Buckets
                    {
                        new Bucket(new Prize(9.5M), 1),
                        new Bucket(new Prize(6M), 1),
                        new Bucket(new Prize(4M), 3),
                        new Bucket(new Prize(2.5M), 5)
                    }
                ),
                [PayoutEntryType.Fifteen] = new Payout(
                    new Buckets
                    {
                        new Bucket(new Prize(20M), 1),
                        new Bucket(new Prize(15M), 1),
                        new Bucket(new Prize(10M), 2),
                        new Bucket(new Prize(7M), 4),
                        new Bucket(new Prize(5M), 7)
                    }
                ),
                [PayoutEntryType.Twenty] = new Payout(
                    new Buckets
                    {
                        new Bucket(new Prize(12.5M), 1),
                        new Bucket(new Prize(10M), 1),
                        new Bucket(new Prize(7M), 2),
                        new Bucket(new Prize(4M), 4),
                        new Bucket(new Prize(2.5M), 11)
                    }
                ),
                [PayoutEntryType.TwentyFive] = new Payout(
                    new Buckets
                    {
                        new Bucket(new Prize(12.5M), 1),
                        new Bucket(new Prize(10M), 1),
                        new Bucket(new Prize(6.25M), 2),
                        new Bucket(new Prize(5M), 3),
                        new Bucket(new Prize(3.5M), 5),
                        new Bucket(new Prize(2.5M), 13)
                    }
                ),
                [PayoutEntryType.Fifty] = new Payout(
                    new Buckets
                    {
                        new Bucket(new Prize(17M), 1),
                        new Bucket(new Prize(12M), 1),
                        new Bucket(new Prize(9.5M), 1),
                        new Bucket(new Prize(7M), 3),
                        new Bucket(new Prize(5M), 5),
                        new Bucket(new Prize(3.5M), 15),
                        new Bucket(new Prize(2.5M), 25)
                    }
                ),
                [PayoutEntryType.SeventyFive] = new Payout(
                    new Buckets
                    {
                        new Bucket(new Prize(17.5M), 1),
                        new Bucket(new Prize(15M), 1),
                        new Bucket(new Prize(12.5M), 1),
                        new Bucket(new Prize(10M), 2),
                        new Bucket(new Prize(7.5M), 4),
                        new Bucket(new Prize(5M), 8),
                        new Bucket(new Prize(3.5M), 20),
                        new Bucket(new Prize(2.5M), 38)
                    }
                ),
                [PayoutEntryType.OneHundred] = new Payout(
                    new Buckets
                    {
                        new Bucket(new Prize(20M), 1),
                        new Bucket(new Prize(15M), 1),
                        new Bucket(new Prize(12M), 1),
                        new Bucket(new Prize(9M), 3),
                        new Bucket(new Prize(7.5M), 5),
                        new Bucket(new Prize(5.5M), 8),
                        new Bucket(new Prize(4.5M), 15),
                        new Bucket(new Prize(3M), 25),
                        new Bucket(new Prize(2.5M), 41)
                    }
                )
            }
        )
        {
        }
    }
}
