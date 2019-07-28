// Filename: PrizePool.cs
// Date Created: 2019-07-10
// 
// ================================================
// Copyright � 2019, eDoxa. All rights reserved.

using System.Collections.Generic;
using System.Globalization;
using System.Linq;

using eDoxa.Seedwork.Domain.Aggregate;

namespace eDoxa.Cashier.Domain.AggregateModels.ChallengeAggregate
{
    public sealed class PrizePool : ValueObject
    {
        public PrizePool(IBuckets buckets)
        {
            Amount = buckets.SelectMany(bucket => bucket.AsIndividualBuckets()).Sum(bucket => bucket.Prize.Amount);
            Currency = buckets.First().Prize.Currency;
        }

        public decimal Amount { get; }

        public Currency Currency { get; }

        public static implicit operator decimal(PrizePool prizePool)
        {
            return prizePool.Amount;
        }

        protected override IEnumerable<object> GetAtomicValues()
        {
            yield return Amount;
            yield return Currency;
        }

        public override string ToString()
        {
            if (Currency == Currency.Money)
            {
                return $"${Amount}";
            }

            return Amount.ToString(CultureInfo.InvariantCulture);
        }
    }
}