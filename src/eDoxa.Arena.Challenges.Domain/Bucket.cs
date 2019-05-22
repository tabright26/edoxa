// Filename: Bucket.cs
// Date Created: 2019-05-20
// 
// ================================================
// Copyright © 2019, eDoxa. All rights reserved.
// 
// This file is subject to the terms and conditions
// defined in file 'LICENSE.md', which is part of
// this source code package.

using eDoxa.Arena.Challenges.Domain.Abstractions;
using eDoxa.Seedwork.Domain.Aggregate;

namespace eDoxa.Arena.Challenges.Domain
{
    public class Bucket : ValueObject
    {
        public Bucket(Prize prize, int size)
        {
            Size = size;
            Prize = prize;
        }

        public Prize Prize { get; }

        public int Size { get; }

        public IBuckets Items
        {
            get
            {
                var buckets = new Buckets();

                for (var index = 0; index < Size; index++)
                {
                    buckets.Add(new BucketItem(Prize));
                }

                return buckets;
            }
        }

        public Bucket ApplyFactor(EntryFeeType factor)
        {
            return new Bucket(new PrizeFactor(Prize, factor), Size);
        }
    }
}
