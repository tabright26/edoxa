// Filename: Bucket.cs
// Date Created: 2019-05-24
// 
// ================================================
// Copyright © 2019, eDoxa. All rights reserved.
// 
// This file is subject to the terms and conditions
// defined in file 'LICENSE.md', which is part of
// this source code package.

using System.Collections.Generic;

using eDoxa.Arena.Domain.Abstractions;
using eDoxa.Arena.Domain.ValueObjects;
using eDoxa.Seedwork.Domain.Aggregate;

namespace eDoxa.Arena.Domain
{
    public class Bucket : ValueObject
    {
        public Bucket(Prize prize, BucketSize size) : this()
        {
            Size = size;
            Prize = prize;
        }

        private Bucket()
        {
            // Required by EF Core.
        }

        public Prize Prize { get; private set; }

        public BucketSize Size { get; private set; }

        public IBuckets Items
        {
            get
            {
                var buckets = new List<BucketItem>();

                for (var index = 0; index < Size; index++)
                {
                    buckets.Add(new BucketItem(Prize));
                }

                return new Buckets(buckets);
            }
        }

        protected override IEnumerable<object> GetAtomicValues()
        {
            yield return Prize;
            yield return Size;
        }
    }
}
