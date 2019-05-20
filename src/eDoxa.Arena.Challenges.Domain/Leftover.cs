// Filename: PayoutLeftover.cs
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

namespace eDoxa.Arena.Challenges.Domain
{
    public partial class Leftover
    {
        internal const decimal Default = 0;

        internal static readonly Leftover DefaultValue = new Leftover(Default);

        private readonly decimal _value;

        public Leftover(decimal leftover)
        {
            _value = leftover;
        }

        public static implicit operator decimal(Leftover leftover)
        {
            return leftover._value;
        }

        public override string ToString()
        {
            return _value.ToString(CultureInfo.InvariantCulture);
        }
    }
    
    public partial class Leftover : IEquatable<Leftover>
    {
        public bool Equals(Leftover other)
        {
            return _value.Equals(other?._value);
        }

        public override bool Equals(object obj)
        {
            return this.Equals(obj as Leftover);
        }

        public override int GetHashCode()
        {
            return _value.GetHashCode();
        }
    }

    public partial class Leftover : IComparable, IComparable<Leftover>
    {
        public int CompareTo([CanBeNull] object obj)
        {
            return this.CompareTo(obj as Leftover);
        }

        public int CompareTo([CanBeNull] Leftover other)
        {
            return _value.CompareTo(other?._value);
        }
    }
}