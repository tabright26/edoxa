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
        public PayoutLevel(BucketSize bucketSize, decimal weighting)
        {
            BucketSize = bucketSize;
            Weighting = weighting;
        }

        public int BucketSize { get; }

        public decimal Weighting { get; }

        protected override IEnumerable<object> GetAtomicValues()
        {
            yield return BucketSize;
            yield return Weighting;
        }

        public override string ToString()
        {
            return this.GetType().ToString();
        }
    }
}
