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
        internal const int Min = 1;
        internal const int Max = 7;
        internal const int Default = 3;

        internal static readonly BestOf MinValue = new BestOf(Min);
        internal static readonly BestOf MaxValue = new BestOf(Max);
        internal static readonly BestOf DefaultValue = new BestOf(Default);

        private readonly int _value;

        public BestOf(int bestOf, bool validate = true)
        {
            if (validate)
            {
                if (bestOf < Min ||
                    bestOf > Max)
                {
                    throw new ArgumentException(nameof(bestOf));
                }
            }

            _value = bestOf;
        }

        public static implicit operator int(BestOf bestOf)
        {
            return bestOf._value;
        }

        public static BestOf Random(BestOf minValue, BestOf maxValue)
        {
            if (minValue > maxValue)
            {
                throw new ArgumentOutOfRangeException(nameof(minValue));
            }

            var random = new Random();

            return new BestOf(random.Next(minValue, maxValue + 1));
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
            return _value.CompareTo(other?._value);
        }
    }
}