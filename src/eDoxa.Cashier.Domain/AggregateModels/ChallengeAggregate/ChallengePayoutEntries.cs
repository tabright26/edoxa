// Filename: ChallengePayoutEntries.cs
// Date Created: 2019-11-25
//
// ================================================
// Copyright © 2020, eDoxa. All rights reserved.

using System.Collections.Generic;
using System.Linq;

using eDoxa.Seedwork.Domain;

namespace eDoxa.Cashier.Domain.AggregateModels.ChallengeAggregate
{
    // Francis: Pourquoi Payout Entries, pourquoi pas seulement ChallengeEntries ??? Je comprends pas le payout a rapport a quoi, ya déja bucket Size non ?
    public sealed class ChallengePayoutEntries : ValueObject
    {
        public static readonly ChallengePayoutEntries One = new ChallengePayoutEntries(1);
        public static readonly ChallengePayoutEntries Two = new ChallengePayoutEntries(2);
        public static readonly ChallengePayoutEntries Three = new ChallengePayoutEntries(3);
        public static readonly ChallengePayoutEntries Four = new ChallengePayoutEntries(4);
        public static readonly ChallengePayoutEntries Five = new ChallengePayoutEntries(5);
        public static readonly ChallengePayoutEntries Ten = new ChallengePayoutEntries(10);
        public static readonly ChallengePayoutEntries Fifteen = new ChallengePayoutEntries(15);
        public static readonly ChallengePayoutEntries Twenty = new ChallengePayoutEntries(20);
        public static readonly ChallengePayoutEntries TwentyFive = new ChallengePayoutEntries(25);
        public static readonly ChallengePayoutEntries Fifty = new ChallengePayoutEntries(50);
        public static readonly ChallengePayoutEntries SeventyFive = new ChallengePayoutEntries(75);
        public static readonly ChallengePayoutEntries OneHundred = new ChallengePayoutEntries(100);

        private readonly int _payoutEntries;

        public ChallengePayoutEntries(IChallengePayoutBuckets buckets)
        {
            _payoutEntries = buckets.Sum(bucket => bucket.Size);
        }

        public ChallengePayoutEntries(int payoutEntries)
        {
            _payoutEntries = payoutEntries;
        }

        public static implicit operator int(ChallengePayoutEntries payoutEntries)
        {
            return payoutEntries._payoutEntries;
        }

        public override string ToString()
        {
            return _payoutEntries.ToString();
        }

        protected override IEnumerable<object> GetAtomicValues()
        {
            yield return _payoutEntries;
        }
    }
}
