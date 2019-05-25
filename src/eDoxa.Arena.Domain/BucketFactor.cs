// Filename: BucketFactor.cs
// Date Created: 2019-05-23
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
    public sealed class BucketFactor
    {
        private readonly PrizeFactor _factor;
        private readonly BucketSize _size;

        public BucketFactor(PrizeFactor factor, BucketSize size)
        {
            _factor = factor;
            _size = size;
        }

        public Bucket CreateBucket(Prize prize)
        {
            return new Bucket(prize.ApplyFactor(_factor), _size);
        }
    }
}
