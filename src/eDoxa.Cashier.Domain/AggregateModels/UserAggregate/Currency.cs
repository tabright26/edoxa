﻿// Filename: Currency.cs
// Date Created: 2019-04-09
// 
// ============================================================
// Copyright © 2019, Francis Quenneville
// All rights reserved.
// 
// This file is subject to the terms and conditions defined in file 'LICENSE.md', which is part of
// this source code package.

using System;
using System.Collections.Generic;
using System.Reflection;

using eDoxa.Seedwork.Domain.Aggregate;

namespace eDoxa.Cashier.Domain.AggregateModels.UserAggregate
{
    public abstract partial class Currency<TCurrency> : BaseObject
    where TCurrency : Currency<TCurrency>, new()
    {
        public static readonly TCurrency Empty = FromDecimal(decimal.Zero);

        private decimal _amount;

        protected decimal Amount
        {
            get
            {
                return _amount;
            }
            private set
            {
                if (value < 0)
                {
                    throw new InvalidOperationException("The currency can not be below zero.");
                }

                _amount = value;
            }
        }

        public static bool operator ==(Currency<TCurrency> left, Currency<TCurrency> right)
        {
            return EqualityComparer<Currency<TCurrency>>.Default.Equals(left, right);
        }

        public static bool operator !=(Currency<TCurrency> left, Currency<TCurrency> right)
        {
            return !(left == right);
        }

        public static bool operator <(Currency<TCurrency> left, Currency<TCurrency> right)
        {
            return left.Amount < right.Amount;
        }

        public static bool operator >(Currency<TCurrency> left, Currency<TCurrency> right)
        {
            return left.Amount > right.Amount;
        }

        public static bool operator <=(Currency<TCurrency> left, Currency<TCurrency> right)
        {
            return left.Amount <= right.Amount;
        }

        public static bool operator >=(Currency<TCurrency> left, Currency<TCurrency> right)
        {
            return left.Amount >= right.Amount;
        }

        public static TCurrency operator +(Currency<TCurrency> left, Currency<TCurrency> right)
        {
            return FromDecimal(left.Amount + right.Amount);
        }

        public static TCurrency operator -(Currency<TCurrency> left, Currency<TCurrency> right)
        {
            return left < right ?
                throw new InvalidOperationException("The currency difference can not be less than zero.") :
                FromDecimal(left.Amount - right.Amount);
        }

        public static TCurrency operator *(Currency<TCurrency> currency, int multiplier)
        {
            return multiplier < 1 ?
                throw new InvalidOperationException("The currency multiplier can not be less than one.") :
                FromDecimal(currency.Amount * multiplier);
        }

        public static TCurrency FromDecimal(decimal amount)
        {
            return new TCurrency
            {
                Amount = amount
            };
        }

        public sealed override bool Equals(object obj)
        {
            return base.Equals(obj);
        }

        public sealed override int GetHashCode()
        {
            return base.GetHashCode();
        }

        public abstract override string ToString();

        public decimal ToDecimal()
        {
            return Amount;
        }

        protected sealed override PropertyInfo[] TypeSignatureProperties()
        {
            return new[]
            {
                this.GetType().GetProperty(nameof(Amount), BindingFlags.NonPublic | BindingFlags.Instance)
            };
        }
    }

    public abstract partial class Currency<TCurrency> : IComparable, IComparable<TCurrency>
    {
        public int CompareTo(object obj)
        {
            return this.CompareTo(obj as TCurrency);
        }

        public int CompareTo(TCurrency other)
        {
            return Amount.CompareTo(other.Amount);
        }
    }
}