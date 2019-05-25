// Filename: TokenEntryFee.cs
// Date Created: 2019-05-23
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

using JetBrains.Annotations;

namespace eDoxa.Arena.Challenges.Domain
{
    [TypeConverter(typeof(TokenEntryFeeConverter))]
    public sealed partial class TokenEntryFee : EntryFee
    {
        public static readonly TokenEntryFee TwoAndHalf = new TokenEntryFee(2500M);
        public static readonly TokenEntryFee Five = new TokenEntryFee(5000M);
        public static readonly TokenEntryFee Ten = new TokenEntryFee(10000M);
        public static readonly TokenEntryFee Twenty = new TokenEntryFee(20000M);
        public static readonly TokenEntryFee TwentyFive = new TokenEntryFee(25000M);
        public static readonly TokenEntryFee Fifty = new TokenEntryFee(50000M);
        public static readonly TokenEntryFee SeventyFive = new TokenEntryFee(75000M);
        public static readonly TokenEntryFee OneHundred = new TokenEntryFee(100000M);

        internal TokenEntryFee(decimal entryFee) : base(entryFee, Currency.Token)
        {
        }

        private TokenEntryFee(string entryFee) : base(Convert.ToDecimal(entryFee), Currency.Token)
        {
        }
    }

    public sealed partial class TokenEntryFee
    {
        private sealed class TokenEntryFeeConverter : TypeConverter
        {
            public override bool CanConvertFrom([CanBeNull] ITypeDescriptorContext context, Type sourceType)
            {
                return sourceType == typeof(decimal) || sourceType == typeof(string) || base.CanConvertFrom(context, sourceType);
            }

            public override bool CanConvertTo([CanBeNull] ITypeDescriptorContext context, Type destinationType)
            {
                return destinationType == typeof(decimal) || destinationType == typeof(string) || base.CanConvertTo(context, destinationType);
            }

            [CanBeNull]
            public override object ConvertFrom([CanBeNull] ITypeDescriptorContext context, CultureInfo culture, [CanBeNull] object value)
            {
                switch (value)
                {
                    case null:
                    {
                        return Ten;
                    }

                    case decimal type:
                    {
                        return new TokenEntryFee(type);
                    }

                    case string type:
                    {
                        return new TokenEntryFee(type);
                    }

                    default:
                    {
                        return base.ConvertFrom(context, culture, value);
                    }
                }
            }

            [CanBeNull]
            public override object ConvertTo(
                [CanBeNull] ITypeDescriptorContext context,
                [NotNull] CultureInfo culture,
                [CanBeNull] object value,
                Type destinationType
            )
            {
                if (value is TokenEntryFee tokenEntryFee)
                {
                    if (destinationType == typeof(decimal))
                    {
                        return tokenEntryFee.Amount;
                    }

                    if (destinationType == typeof(string))
                    {
                        return tokenEntryFee.Amount.ToString(CultureInfo.InvariantCulture);
                    }
                }

                return base.ConvertTo(context, culture, value, destinationType);
            }
        }
    }
}
