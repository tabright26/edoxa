// Filename: Bucket.cs
// Date Created: 2019-04-19
// 
// ================================================
// Copyright © 2019, eDoxa. All rights reserved.
//  
// This file is subject to the terms and conditions
// defined in file 'LICENSE.md', which is part of
// this source code package.

using System;

using eDoxa.Seedwork.Domain.Aggregate;

using JetBrains.Annotations;

namespace eDoxa.Challenges.Domain.AggregateModels
{
    public partial class Bucket : ValueObject
    {
        public Bucket(int size = 1)
        {
            if (size < 1)
            {
                throw new ArgumentException(nameof(size));
            }

            Size = size;
        }

        public int Size { get; }
    }

    public partial class Bucket : IComparable, IComparable<Bucket>
    {
        public int CompareTo([CanBeNull] object obj)
        {
            return this.CompareTo(obj as Bucket);
        }

        public int CompareTo([CanBeNull] Bucket other)
        {
            return Size.CompareTo(other?.Size);
        }
    }
}