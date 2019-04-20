// Filename: BestOf.cs
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

namespace eDoxa.Challenges.Domain.ValueObjects
{
    public partial class BestOf : ValueObject
    {
        internal const int MinBestOf = 1;
        internal const int MaxBestOf = 7;
        internal const int DefaultPrimitive = 3;

        public static readonly BestOf Default = new BestOf(DefaultPrimitive);

        private readonly int _bestOf;

        public BestOf(int bestOf, bool validate = true)
        {
            if (validate)
            {
                if (bestOf < MinBestOf ||
                    bestOf > MaxBestOf)
                {
                    throw new ArgumentException(nameof(bestOf));
                }
            }

            _bestOf = bestOf;
        }

        public static implicit operator int(BestOf bestOf)
        {
            return bestOf._bestOf;
        }
    }

    public partial class BestOf : IComparable, IComparable<BestOf>
    {
        public int CompareTo([CanBeNull] object obj)
        {
            return this.CompareTo(obj as BestOf);
        }

        public int CompareTo([CanBeNull] BestOf other)
        {
            return _bestOf.CompareTo(other?._bestOf);
        }
    }
}