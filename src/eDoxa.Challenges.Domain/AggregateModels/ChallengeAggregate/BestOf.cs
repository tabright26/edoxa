﻿// Filename: BestOf.cs
// Date Created: 2019-04-20
// 
// ================================================
// Copyright © 2019, eDoxa. All rights reserved.
//  
// This file is subject to the terms and conditions
// defined in file 'LICENSE.md', which is part of
// this source code package.

using System;

using JetBrains.Annotations;

namespace eDoxa.Challenges.Domain.AggregateModels.ChallengeAggregate
{
    public partial class BestOf
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

        public override string ToString()
        {
            return _value.ToString();
        }
    }

    public partial class BestOf : IEquatable<BestOf>
    {
        public bool Equals([CanBeNull] BestOf other)
        {
            return _value.Equals(other?._value);
        }

        public override bool Equals([CanBeNull] object obj)
        {
            return this.Equals(obj as BestOf);
        }

        public override int GetHashCode()
        {
            return _value.GetHashCode();
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