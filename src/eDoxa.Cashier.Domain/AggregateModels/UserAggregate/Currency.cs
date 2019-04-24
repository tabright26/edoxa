// Filename: Currency.cs
// Date Created: 2019-04-21
// 
// ================================================
// Copyright © 2019, eDoxa. All rights reserved.
//  
// This file is subject to the terms and conditions
// defined in file 'LICENSE.md', which is part of
// this source code package.

using System;

using JetBrains.Annotations;

namespace eDoxa.Cashier.Domain.AggregateModels.UserAggregate
{
    public abstract partial class Currency<TCurrency>
    where TCurrency : Currency<TCurrency>
    {
        private readonly decimal _value;

        protected Currency(decimal value)
        {
            _value = value;
        }

        protected Currency()
        {
            _value = 0;
        }

        public static implicit operator decimal(Currency<TCurrency> currency)
        {
            return currency._value;
        }

        public abstract override string ToString();
    }

    public abstract partial class Currency<TCurrency> : IEquatable<TCurrency>
    {
        public bool Equals([CanBeNull] TCurrency other)
        {
            return _value.Equals(other?._value);
        }

        public override bool Equals([CanBeNull] object obj)
        {
            return this.Equals(obj as TCurrency);
        }

        public override int GetHashCode()
        {
            return _value.GetHashCode();
        }
    }

    public abstract partial class Currency<TCurrency> : IComparable, IComparable<TCurrency>
    {
        public int CompareTo([CanBeNull] object obj)
        {
            return this.CompareTo(obj as TCurrency);
        }

        public int CompareTo([CanBeNull] TCurrency other)
        {
            return _value.CompareTo(other?._value);
        }
    }
}