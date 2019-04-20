// Filename: BucketSize.cs
// Date Created: 2019-04-20
// 
// ================================================
// Copyright © 2019, eDoxa. All rights reserved.
//  
// This file is subject to the terms and conditions
// defined in file 'LICENSE.md', which is part of
// this source code package.

using System;

using eDoxa.Seedwork.Domain.Aggregate;

using JetBrains.Annotations;

namespace eDoxa.Challenges.Domain.AggregateModels
{
    public partial class BucketSize : ValueObject
    {
        internal const int Default = 1;

        internal static readonly BucketSize DefaultValue = new BucketSize(Default);

        private readonly int _value;

        public BucketSize(int size)
        {
            if (size < 1)
            {
                throw new ArgumentException(nameof(size));
            }

            _value = size;
        }

        public static implicit operator int(BucketSize bucketSize)
        {
            return bucketSize._value;
        }
    }

    public partial class BucketSize : IComparable, IComparable<BucketSize>
    {
        public int CompareTo([CanBeNull] object obj)
        {
            return this.CompareTo(obj as BucketSize);
        }

        public int CompareTo([CanBeNull] BucketSize other)
        {
            return _value.CompareTo(other?._value);
        }
    }
}