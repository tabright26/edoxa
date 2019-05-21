// Filename: Bucket.cs
// Date Created: 2019-05-06
// 
// ================================================
// Copyright © 2019, eDoxa. All rights reserved.
// 
// This file is subject to the terms and conditions
// defined in file 'LICENSE.md', which is part of
// this source code package.

using eDoxa.Seedwork.Domain.Aggregate;

namespace eDoxa.Arena.Challenges.Domain
{
    public sealed class Bucket : ValueObject
    {
        public Bucket(Prize prize, int size)
        {
            Size = size;
            Prize = prize;
        }

        public Prize Prize { get; }

        public int Size { get; }
    }
}