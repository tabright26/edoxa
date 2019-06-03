// Filename: BucketSize.cs
// Date Created: 2019-05-22
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

namespace eDoxa.Arena.Domain
{
    public sealed class BucketSize : ValueObject
    {
        public static readonly BucketSize Single = new BucketSize(1);

        public BucketSize(int size) : this()
        {
            if (size < 1)
            {
                throw new ArgumentException(nameof(size));
            }

            Value = size;
        }

        private BucketSize()
        {
            // Required by EF Core.
        }

        public int Value { get; private set; }

        public static implicit operator int(BucketSize bucketSize)
        {
            return bucketSize.Value;
        }

        protected override IEnumerable<object> GetAtomicValues()
        {
            yield return Value;
        }

        public override string ToString()
        {
            return Value.ToString();
        }
    }
}
