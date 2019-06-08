// Filename: BucketItem.cs
// Date Created: 2019-06-07
// 
// ================================================
// Copyright © 2019, eDoxa. All rights reserved.
// 
// This file is subject to the terms and conditions
// defined in file 'LICENSE.md', which is part of
// this source code package.

namespace eDoxa.Arena.Challenges.Domain.AggregateModels
{
    public sealed class BucketItem : Bucket
    {
        public BucketItem(Prize prize) : base(prize, BucketSize.Single)
        {
        }
    }
}
