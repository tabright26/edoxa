// Filename: PayoutBucket.cs
// Date Created: 2019-04-20
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
        public Bucket(BucketSize size, Prize prize)
        {
            Size = size;
            Prize = prize;
        }

        public BucketSize Size { get; }

        public Prize Prize { get; }
    }

    public partial class Bucket : IComparable, IComparable<Bucket>
    {
        public int CompareTo([CanBeNull] object obj)
        {
            return this.CompareTo(obj as Bucket);
        }

        public int CompareTo([CanBeNull] Bucket other)
        {
            return Prize.CompareTo(other?.Prize);
        }
    }
}