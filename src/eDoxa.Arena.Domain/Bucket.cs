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
using eDoxa.Seedwork.Domain.Aggregate;

namespace eDoxa.Arena.Domain
{
    public class Bucket : ValueObject
    {
        public Bucket(Prize prize, BucketSize size)
        {
            Size = size;
            Prize = prize;
        }

        public Prize Prize { get; }

        public BucketSize Size { get; }

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
            yield return Items;
        }

        public override string ToString()
        {
            return Prize.ToString();
        }
    }
}
