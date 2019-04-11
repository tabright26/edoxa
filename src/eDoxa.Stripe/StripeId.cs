﻿// Filename: StripeId.cs
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
using System.ComponentModel;
using System.Globalization;
using System.Reflection;

using eDoxa.Seedwork.Domain.Aggregate;

namespace eDoxa.Stripe
{
    public abstract partial class StripeId<TStripeId> : BaseObject
    where TStripeId : StripeId<TStripeId>, new()
    {
        private readonly string _prefix;

        private string _value;

        protected StripeId(string prefix)
        {
            _prefix = prefix ?? throw new ArgumentNullException(nameof(prefix));
        }

        protected string Value
        {
            get
            {
                return _value;
            }
            private set
            {
                if (string.IsNullOrWhiteSpace(value))
                {
                    throw new ArgumentException(value, nameof(value));
                }

                var validator = new StripeValidator();

                validator.Validate(value, _prefix);

                _value = value;
            }
        }

        public static TStripeId Parse(string input)
        {
            return new TStripeId
            {
                Value = input
            };
        }

        public static bool operator ==(StripeId<TStripeId> left, StripeId<TStripeId> right)
        {
            return EqualityComparer<StripeId<TStripeId>>.Default.Equals(left, right);
        }

        public static bool operator !=(StripeId<TStripeId> left, StripeId<TStripeId> right)
        {
            return !(left == right);
        }

        public sealed override bool Equals(object obj)
        {
            return base.Equals(obj);
        }

        public sealed override int GetHashCode()
        {
            return base.GetHashCode();
        }

        public sealed override string ToString()
        {
            return Value;
        }

        protected sealed override PropertyInfo[] TypeSignatureProperties()
        {
            return new[]
            {
                this.GetType().GetProperty(nameof(Value), BindingFlags.NonPublic | BindingFlags.Instance)
            };
        }
    }

    public abstract partial class StripeId<TStripeId> : IComparable, IComparable<TStripeId>
    {
        public int CompareTo(object obj)
        {
            return this.CompareTo(obj as TStripeId);
        }

        public int CompareTo(TStripeId other)
        {
            return string.Compare(Value, other.Value, StringComparison.Ordinal);
        }
    }

    public abstract partial class StripeId<TStripeId>
    {
        protected sealed class StripeIdTypeConverter : TypeConverter
        {
            public override bool CanConvertFrom(ITypeDescriptorContext context, Type sourceType)
            {
                if (sourceType == typeof(string))
                {
                    return true;
                }

                return base.CanConvertFrom(context, sourceType);
            }

            public override bool CanConvertTo(ITypeDescriptorContext context, Type destinationType)
            {
                if (destinationType == typeof(string))
                {
                    return true;
                }

                return base.CanConvertTo(context, destinationType);
            }

            public override object ConvertFrom(ITypeDescriptorContext context, CultureInfo culture, object value)
            {
                switch (value)
                {
                    case string input:

                        return Parse(input);
                }

                return base.ConvertFrom(context, culture, value);
            }

            public override object ConvertTo(ITypeDescriptorContext context, CultureInfo culture, object value, Type destinationType)
            {
                var stripeId = value as TStripeId;

                if (stripeId == null)
                {
                    return null;
                }

                if (destinationType == typeof(string))
                {
                    return stripeId.ToString();
                }

                return base.ConvertTo(context, culture, value, destinationType);
            }
        }
    }
}