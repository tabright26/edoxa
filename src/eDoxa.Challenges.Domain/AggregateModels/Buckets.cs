// Filename: Buckets.cs
// Date Created: 2019-04-20
// 
// ================================================
// Copyright © 2019, eDoxa. All rights reserved.
//  
// This file is subject to the terms and conditions
// defined in file 'LICENSE.md', which is part of
// this source code package.

using System.Collections.ObjectModel;

namespace eDoxa.Challenges.Domain.AggregateModels
{
    public sealed class Buckets : Collection<Bucket>, IBuckets
    {
        public Buckets(IBucketSizes bucketSizes, IPrizes prizes)
        {
            for (var index = 0; index < bucketSizes.Count; index++)
            {
                this.Add(new Bucket(bucketSizes[index], prizes[index]));
            }
        }
    }
}