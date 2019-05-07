// Filename: PrizePool.cs
// Date Created: 2019-04-19
// 
// ================================================
// Copyright © 2019, eDoxa. All rights reserved.
//  
// This file is subject to the terms and conditions
// defined in file 'LICENSE.md', which is part of
// this source code package.

using System;
using System.Globalization;
using System.Linq;

using JetBrains.Annotations;

namespace eDoxa.Challenges.Domain.AggregateModels.ChallengeAggregate
{
    public partial class PrizePool
    {
        private readonly decimal _value;

        public PrizePool(Entries entries, EntryFee entryFee, ServiceChargeRatio serviceChargeRatio)
        {
            _value = Math.Floor(entries * entryFee * (1 - Convert.ToDecimal(serviceChargeRatio)));
        }

        public PrizePool(IPayout payout)
        {
            _value = payout.Buckets.Sum(bucket => bucket.Prize * bucket.Size);
        }

        public static implicit operator decimal(PrizePool prizePool)
        {
            return prizePool._value;
        }

        public static FirstPrize operator *(PrizePool prizePool, PrizePoolRatio prizePoolRatio)
        {
            return new FirstPrize(prizePool * Convert.ToDecimal(prizePoolRatio));
        }

        public override string ToString()
        {
            return _value.ToString(CultureInfo.InvariantCulture);
        }

        public double ToDouble()
        {
            return Convert.ToDouble(this);
        }
    }

    public partial class PrizePool : IEquatable<PrizePool>
    {
        public bool Equals(PrizePool other)
        {
            return _value.Equals(other?._value);
        }

        public override bool Equals(object obj)
        {
            return this.Equals(obj as PrizePool);
        }

        public override int GetHashCode()
        {
            return _value.GetHashCode();
        }
    }

    public partial class PrizePool : IComparable, IComparable<PrizePool>
    {
        public int CompareTo([CanBeNull] object obj)
        {
            return this.CompareTo(obj as PrizePool);
        }

        public int CompareTo([CanBeNull] PrizePool other)
        {
            return _value.CompareTo(other?._value);
        }
    }
}