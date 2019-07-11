// Filename: PayoutEntries.cs
// Date Created: 2019-07-10
// 
// ================================================
// Copyright © 2019, eDoxa. All rights reserved.

using System.Collections.Generic;

using eDoxa.Seedwork.Domain.Aggregate;

namespace eDoxa.Cashier.Domain.AggregateModels.ChallengeAggregate
{
    public sealed class PayoutEntries : ValueObject
    {
        public static readonly PayoutEntries One = new PayoutEntries(1);
        public static readonly PayoutEntries Two = new PayoutEntries(2);
        public static readonly PayoutEntries Three = new PayoutEntries(3);
        public static readonly PayoutEntries Four = new PayoutEntries(4);
        public static readonly PayoutEntries Five = new PayoutEntries(5);
        public static readonly PayoutEntries Ten = new PayoutEntries(10);
        public static readonly PayoutEntries Fifteen = new PayoutEntries(15);
        public static readonly PayoutEntries Twenty = new PayoutEntries(20);
        public static readonly PayoutEntries TwentyFive = new PayoutEntries(25);
        public static readonly PayoutEntries Fifty = new PayoutEntries(50);
        public static readonly PayoutEntries SeventyFive = new PayoutEntries(75);
        public static readonly PayoutEntries OneHundred = new PayoutEntries(100);

        private readonly int _payoutEntries;

        public PayoutEntries(int payoutEntries)
        {
            _payoutEntries = payoutEntries;
        }

        public static implicit operator int(PayoutEntries payoutEntries)
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
