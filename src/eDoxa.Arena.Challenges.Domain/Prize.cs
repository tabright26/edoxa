// Filename: Prize.cs
// Date Created: 2019-04-19
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

namespace eDoxa.Arena.Challenges.Domain
{
    public partial class Prize
    {
        private readonly decimal _value;

        public Prize(decimal prize)
        {
            if (prize < 0)
            {
                throw new ArgumentException(nameof(prize));
            }

            _value = prize;
        }

        protected Prize(FirstPrize prize)
        {
            _value = prize;
        }

        public static implicit operator decimal(Prize prize)
        {
            return prize._value;
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

    public partial class Prize : IEquatable<Prize>
    {
        public bool Equals(Prize other)
        {
            return _value.Equals(other?._value);
        }

        public override bool Equals(object obj)
        {
            return this.Equals(obj as Prize);
        }

        public override int GetHashCode()
        {
            return _value.GetHashCode();
        }
    }

    public partial class Prize : IComparable, IComparable<Prize>
    {
        public int CompareTo([CanBeNull] object obj)
        {
            return this.CompareTo(obj as Prize);
        }

        public int CompareTo([CanBeNull] Prize other)
        {
            return _value.CompareTo(other?._value);
        }
    }
}