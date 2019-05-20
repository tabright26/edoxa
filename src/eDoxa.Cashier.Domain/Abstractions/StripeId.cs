﻿// Filename: StripeId.cs
// Date Created: 2019-05-13
// 
// ================================================
// Copyright © 2019, eDoxa. All rights reserved.
// 
// This file is subject to the terms and conditions
// defined in file 'LICENSE.md', which is part of
// this source code package.

using System;
using System.ComponentModel;
using System.Globalization;
using System.Linq;
using System.Reflection;

using eDoxa.Cashier.Domain.Exceptions;

using JetBrains.Annotations;

namespace eDoxa.Cashier.Domain.Abstractions
{
    public abstract partial class StripeId<TStripeId>
    where TStripeId : StripeId<TStripeId>
    {
        private readonly string _value;

        protected StripeId(string stripeId, string prefix)
        {
            if (!IsValid(stripeId, prefix))
            {
                throw new StripeIdException(stripeId, this.GetType());
            }

            _value = stripeId;
        }

        private static bool IsValid(string stripeId, string prefix)
        {
            if (string.IsNullOrWhiteSpace(stripeId))
            {
                return false;
            }

            var substrings = GetSubstrings();

            return substrings.Length == 2 && GetPrefix() == prefix && GetSuffix().All(char.IsLetterOrDigit);

            string GetPrefix()
            {
                return substrings[0];
            }

            string GetSuffix()
            {
                return substrings[1];
            }

            string[] GetSubstrings()
            {
                return stripeId.Split('_');
            }
        }

        public sealed override string ToString()
        {
            return _value;
        }
    }

    public abstract partial class StripeId<TStripeId> : IEquatable<TStripeId>
    {
        public bool Equals([CanBeNull] TStripeId other)
        {
            return _value.Equals(other?._value);
        }

        public sealed override bool Equals([CanBeNull] object obj)
        {
            return this.Equals(obj as TStripeId);
        }

        public sealed override int GetHashCode()
        {
            return _value.GetHashCode();
        }
    }

    public abstract partial class StripeId<TStripeId> : IComparable, IComparable<TStripeId>
    {
        public int CompareTo([CanBeNull] object obj)
        {
            return this.CompareTo(obj as TStripeId);
        }

        public int CompareTo([CanBeNull] TStripeId other)
        {
            return string.Compare(_value, other?._value, StringComparison.Ordinal);
        }
    }

    public abstract partial class StripeId<TStripeId>
    {
        protected sealed class StripeIdConverter : TypeConverter
        {
            public override bool CanConvertFrom([NotNull] ITypeDescriptorContext context, Type sourceType)
            {
                return sourceType == typeof(string) || base.CanConvertFrom(context, sourceType);
            }

            public override bool CanConvertTo([NotNull] ITypeDescriptorContext context, Type destinationType)
            {
                return destinationType == typeof(string) || base.CanConvertTo(context, destinationType);
            }

            [CanBeNull]
            public override object ConvertFrom([NotNull] ITypeDescriptorContext context, [NotNull] CultureInfo culture, [CanBeNull] object value)
            {
                switch (value)
                {
                    case string stripeId:
                    {
                        try
                        {
                            return (TStripeId) Activator.CreateInstance(typeof(TStripeId), stripeId);
                        }
                        catch (TargetInvocationException exception) when (exception.InnerException?.GetType() == typeof(StripeIdException))
                        {
                            throw exception.InnerException;
                        }
                    }

                    default:
                    {
                        return base.ConvertFrom(context, culture, value);
                    }
                }
            }

            [CanBeNull]
            public override object ConvertTo(
                [NotNull] ITypeDescriptorContext context,
                [NotNull] CultureInfo culture,
                [CanBeNull] object value,
                Type destinationType
            )
            {
                if (value is TStripeId stripeId)
                {
                    if (destinationType == typeof(string))
                    {
                        return stripeId.ToString();
                    }
                }

                return base.ConvertTo(context, culture, value, destinationType);
            }
        }
    }
}