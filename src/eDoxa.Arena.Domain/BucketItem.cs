// Filename: BucketItem.cs
// Date Created: 2019-05-21
// 
// ================================================
// Copyright © 2019, eDoxa. All rights reserved.
// 
// This file is subject to the terms and conditions
// defined in file 'LICENSE.md', which is part of
// this source code package.

using eDoxa.Arena.Domain.Abstractions;

namespace eDoxa.Arena.Domain
{
    public sealed class BucketItem : Bucket
    {
        public BucketItem(Prize prize) : base(prize, BucketSize.Default)
        {
        }
    }
}
