// Filename: StripeId.cs
// Date Created: 2019-05-10
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

namespace eDoxa.Cashier.Domain.Services.Stripe.Abstractions
{
    public static class StripeId
    {
        public static bool IsValid(string stripeId, string prefix)
        {
            if (string.IsNullOrWhiteSpace(stripeId))
            {
                return false;
            }

            var substrings = GetSubstrings(stripeId);

            return IsValid(substrings) && GetPrefix(substrings) == prefix && GetSuffix(substrings).All(char.IsLetterOrDigit);

            //throw new FormatException("The substrings of identity are in an incorrect format.");
            //throw new FormatException($"The identity prefix ({prefix}) is ​​an incorrect format.");
            //throw new FormatException($"The identity suffix ({suffix}) is ​​an incorrect format.");
        }

        private static bool IsValid(string[] substrings)
        {
            return substrings.Length == 2;
        }

        private static string GetPrefix(string[] substrings)
        {
            return substrings[0];
        }

        private static string GetSuffix(string[] substrings)
        {
            return substrings[1];
        }

        private static string[] GetSubstrings(string stripeId)
        {
            return stripeId.Split('_');
        }
    }

    public abstract partial class StripeId<TStripeId>
    where TStripeId : StripeId<TStripeId>
    {
        private readonly string _value;

        protected StripeId(string stripeId, string prefix)
        {
            if (!StripeId.IsValid(stripeId, prefix))
            {
                throw new ArgumentException(nameof(stripeId));
            }

            _value = stripeId;
        }

        public sealed override string ToString()
        {
            return _value;
        }
    }

    public abstract partial class StripeId<TStripeId> : IEquatable<TStripeId>
    {
        public bool Equals(TStripeId other)
        {
            return _value.Equals(other?._value);
        }

        public sealed override bool Equals(object obj)
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
        public int CompareTo(object obj)
        {
            return this.CompareTo(obj as TStripeId);
        }

        public int CompareTo(TStripeId other)
        {
            return string.Compare(_value, other?._value, StringComparison.Ordinal);
        }
    }

    public abstract partial class StripeId<TStripeId>
    {
        protected sealed class StripeIdConverter : TypeConverter
        {
            public override bool CanConvertFrom(ITypeDescriptorContext context, Type sourceType)
            {
                return sourceType == typeof(string) || base.CanConvertFrom(context, sourceType);
            }

            public override bool CanConvertTo(ITypeDescriptorContext context, Type destinationType)
            {
                return destinationType == typeof(string) || base.CanConvertTo(context, destinationType);
            }

            public override object ConvertFrom(ITypeDescriptorContext context, CultureInfo culture, object value)
            {
                switch (value)
                {
                    case string stripeId when string.IsNullOrWhiteSpace(stripeId):
                    {
                        return null;
                    }

                    case string stripeId:
                    {
                        return (TStripeId) Activator.CreateInstance(typeof(TStripeId), stripeId);
                    }

                    default:
                    {
                        return base.ConvertFrom(context, culture, value);
                    }
                }
            }

            public override object ConvertTo(ITypeDescriptorContext context, CultureInfo culture, object value, Type destinationType)
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