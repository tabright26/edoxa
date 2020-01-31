// Filename: ChallengePayoutChart.cs
// Date Created: 2020-01-30
// 
// ================================================
// Copyright © 2020, eDoxa. All rights reserved.

using System.Collections.Generic;

using eDoxa.Seedwork.Domain;

namespace eDoxa.Cashier.Domain.AggregateModels.ChallengeAggregate
{
    public sealed class ChallengePayoutChart : ValueObject
    {
        public ChallengePayoutChart(ChallengePayoutBucketSize size, decimal weigthing)
        {
            Size = size;
            Weigthing = weigthing;
        }

        public ChallengePayoutBucketSize Size { get; }

        public decimal Weigthing { get; }

        protected override IEnumerable<object?> GetAtomicValues()
        {
            yield return Size;
            yield return Weigthing;
        }

        public override string ToString()
        {
            return Size.ToString();
        }
    }
}
