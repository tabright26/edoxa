// Filename: EntryFee.cs
// Date Created: 2019-04-20
// 
// ================================================
// Copyright © 2019, eDoxa. All rights reserved.
//  
// This file is subject to the terms and conditions
// defined in file 'LICENSE.md', which is part of
// this source code package.

using System;
using System.Globalization;

using JetBrains.Annotations;

namespace eDoxa.Challenges.Domain.AggregateModels.ChallengeAggregate
{
    public partial class EntryFee
    {
        internal const decimal Min = 0.25M;
        internal const decimal Max = 1500M;
        internal const decimal Default = 5M;

        public static readonly EntryFee MinValue = new EntryFee(Min);
        public static readonly EntryFee MaxValue = new EntryFee(Max);
        public static readonly EntryFee DefaultValue = new EntryFee(Default);

        private readonly decimal _value;

        public EntryFee(decimal entryFee, bool validate = true)
        {
            if (validate)
            {
                if (entryFee < Min ||
                    entryFee > Max ||
                    entryFee % 0.25M != 0)
                {
                    throw new ArgumentException(nameof(entryFee));
                }
            }

            _value = entryFee;
        }

        public static implicit operator decimal(EntryFee entryFee)
        {
            return entryFee._value;
        }

        public override string ToString()
        {
            return _value.ToString(CultureInfo.InvariantCulture);
        }

        public double ToDouble()
        {
            return Convert.ToDouble(this);
        }
    }

    public partial class EntryFee : IEquatable<EntryFee>
    {
        public bool Equals([CanBeNull] EntryFee other)
        {
            return _value.Equals(other?._value);
        }

        public override bool Equals([CanBeNull] object obj)
        {
            return this.Equals(obj as EntryFee);
        }

        public override int GetHashCode()
        {
            return _value.GetHashCode();
        }
    }

    public partial class EntryFee : IComparable, IComparable<EntryFee>
    {
        public int CompareTo([CanBeNull] object obj)
        {
            return this.CompareTo(obj as EntryFee);
        }

        public int CompareTo([CanBeNull] EntryFee other)
        {
            return _value.CompareTo(other?._value);
        }
    }
}