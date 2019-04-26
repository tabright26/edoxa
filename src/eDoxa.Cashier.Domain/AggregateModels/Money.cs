// Filename: Money.cs
// Date Created: 2019-04-26
// 
// ================================================
// Copyright © 2019, eDoxa. All rights reserved.
//  
// This file is subject to the terms and conditions
// defined in file 'LICENSE.md', which is part of
// this source code package.

using System;

using JetBrains.Annotations;

namespace eDoxa.Cashier.Domain.AggregateModels
{
    public sealed partial class Money : ICurrency
    {
        internal static readonly Money Zero = new Money(0);
        internal static readonly Money Five = new Money(5);
        internal static readonly Money Ten = new Money(10);
        internal static readonly Money Twenty = new Money(20);
        internal static readonly Money TwentyFive = new Money(25);
        internal static readonly Money Fifty = new Money(50);
        internal static readonly Money OneHundred = new Money(100);
        internal static readonly Money FiveHundred = new Money(500);

        private readonly decimal _value;

        public Money(decimal amount)
        {
            _value = amount;
        }

        public static implicit operator decimal(Money money)
        {
            return money._value;
        }

        public static Money operator -(Money money)
        {
            return new Money(-money._value);
        }

        public override string ToString()
        {
            return Convert.ToDecimal(this).ToString("C");
        }

        public int AsCents()
        {
            return Convert.ToInt32(this * 100);
        }
    }

    public sealed partial class Money : IEquatable<Money>
    {
        public bool Equals([CanBeNull] Money other)
        {
            return _value.Equals(other?._value);
        }

        public override bool Equals([CanBeNull] object obj)
        {
            return this.Equals(obj as Money);
        }

        public override int GetHashCode()
        {
            return _value.GetHashCode();
        }
    }

    public sealed partial class Money : IComparable, IComparable<Money>
    {
        public int CompareTo([CanBeNull] object obj)
        {
            return this.CompareTo(obj as Money);
        }

        public int CompareTo([CanBeNull] Money other)
        {
            return _value.CompareTo(other?._value);
        }
    }
}