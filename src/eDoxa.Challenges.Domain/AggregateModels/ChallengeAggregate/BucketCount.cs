// Filename: BucketCount.cs
// Date Created: 2019-04-22
// 
// ================================================
// Copyright © 2019, eDoxa. All rights reserved.
//  
// This file is subject to the terms and conditions
// defined in file 'LICENSE.md', which is part of
// this source code package.

using System;

using JetBrains.Annotations;

namespace eDoxa.Challenges.Domain.AggregateModels.ChallengeAggregate
{
    public sealed partial class BucketCount
    {
        internal static readonly BucketCount EmptyValue = new BucketCount();

        private readonly int _value;

        public BucketCount(PayoutEntries payoutEntries)
        {
            _value =
                payoutEntries >= 5000 ? 15 :
                payoutEntries >= 4000 ? 14 :
                payoutEntries >= 3000 ? 13 :
                payoutEntries >= 2000 ? 12 :
                payoutEntries >= 1500 ? 11 :
                payoutEntries >= 1000 ? 10 :
                payoutEntries >= 500 ? 9 :
                payoutEntries >= 100 ? 8 :
                payoutEntries >= 50 ? 7 :
                payoutEntries >= 20 ? 6 :
                payoutEntries >= 5 ? 5 :
                payoutEntries;
        }

        private BucketCount()
        {
            _value = 0;
        }

        public static implicit operator int(BucketCount bucketSize)
        {
            return bucketSize._value;
        }

        public override string ToString()
        {
            return _value.ToString();
        }
    }

    public sealed partial class BucketCount : IEquatable<BucketCount>
    {
        public bool Equals([CanBeNull] BucketCount other)
        {
            return _value.Equals(other?._value);
        }

        public override bool Equals([CanBeNull] object obj)
        {
            return this.Equals(obj as BucketCount);
        }

        public override int GetHashCode()
        {
            return _value.GetHashCode();
        }
    }

    public sealed partial class BucketCount : IComparable, IComparable<BucketCount>
    {
        public int CompareTo([CanBeNull] object obj)
        {
            return this.CompareTo(obj as BucketCount);
        }

        public int CompareTo([CanBeNull] BucketCount other)
        {
            return _value.CompareTo(other?._value);
        }
    }
}