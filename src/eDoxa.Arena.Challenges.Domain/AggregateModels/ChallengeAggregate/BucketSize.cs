// Filename: BucketSize.cs
// Date Created: 2019-06-20
// 
// ================================================
// Copyright © 2019, eDoxa. All rights reserved.
// 
// This file is subject to the terms and conditions
// defined in file 'LICENSE.md', which is part of
// this source code package.

using System;
using System.Collections.Generic;

using eDoxa.Seedwork.Domain.Aggregate;

using JetBrains.Annotations;

namespace eDoxa.Arena.Challenges.Domain.AggregateModels.ChallengeAggregate
{
    public sealed partial class BucketSize : ValueObject
    {
        public static readonly BucketSize Individual = new BucketSize(1);

        private readonly int _bucketSize;

        public BucketSize(int size)
        {
            if (size < 1)
            {
                throw new ArgumentException(nameof(size));
            }

            _bucketSize = size;
        }

        public static implicit operator int(BucketSize bucketSize)
        {
            return bucketSize._bucketSize;
        }

        protected override IEnumerable<object> GetAtomicValues()
        {
            yield return _bucketSize;
        }

        public override string ToString()
        {
            return _bucketSize.ToString();
        }
    }

    public sealed partial class BucketSize : IComparable
    {
        public int CompareTo([CanBeNull] object obj)
        {
            return _bucketSize.CompareTo(((BucketSize) obj)?._bucketSize);
        }
    }
}
