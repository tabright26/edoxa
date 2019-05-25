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

using eDoxa.Seedwork.Domain.Aggregate;

namespace eDoxa.Arena.Domain
{
    public sealed class BucketSize : TypeObject<BucketSize, int>
    {
        private const int DefaultValue = 1;

        public static readonly BucketSize Default = new BucketSize(DefaultValue);

        public BucketSize(int size) : base(size)
        {
            if (size < 1)
            {
                throw new ArgumentException(nameof(size));
            }
        }
    }
}
