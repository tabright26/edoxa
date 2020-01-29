// Filename: PayoutLevel.cs
// Date Created: 2019-08-20
// 
// ================================================
// Copyright © 2019, eDoxa. All rights reserved.

using System.Collections.Generic;

using eDoxa.Seedwork.Domain;

namespace eDoxa.Cashier.Domain.AggregateModels.ChallengeAggregate
{
    public class PayoutLevel : ValueObject
    {
        public PayoutLevel(ChallengePayoutBucketSize size, decimal weighting)
        {
            Size = size;
            Weighting = weighting;
        }

        public int Size { get; }

        public decimal Weighting { get; }

        protected override IEnumerable<object> GetAtomicValues()
        {
            yield return Size;
            yield return Weighting;
        }

        public override string ToString()
        {
            return this.GetType().ToString();
        }
    }
}
