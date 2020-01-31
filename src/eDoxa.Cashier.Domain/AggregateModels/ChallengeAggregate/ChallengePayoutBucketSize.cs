// Filename: ChallengePayoutBucketSize.cs
// Date Created: 2019-11-25
// 
// ================================================
// Copyright © 2020, eDoxa. All rights reserved.

using System;
using System.Collections.Generic;

using eDoxa.Seedwork.Domain;

namespace eDoxa.Cashier.Domain.AggregateModels.ChallengeAggregate
{
    public sealed partial class ChallengePayoutBucketSize : ValueObject
    {
        public static readonly ChallengePayoutBucketSize Individual = new ChallengePayoutBucketSize(1);

        private readonly int _size;

        public ChallengePayoutBucketSize(int size)
        {
            if (size < 1)
            {
                throw new ArgumentException(nameof(size));
            }

            _size = size;
        }

        public static implicit operator int(ChallengePayoutBucketSize size)
        {
            return size._size;
        }

        protected override IEnumerable<object> GetAtomicValues()
        {
            yield return _size;
        }

        public override string ToString()
        {
            return _size.ToString();
        }
    }

    public sealed partial class ChallengePayoutBucketSize : IComparable, IComparable<ChallengePayoutBucketSize?>
    {
        public int CompareTo(object? obj)
        {
            return this.CompareTo(obj as ChallengePayoutBucketSize);
        }

        public int CompareTo(ChallengePayoutBucketSize? other)
        {
            return _size.CompareTo(other?._size);
        }
    }
}
