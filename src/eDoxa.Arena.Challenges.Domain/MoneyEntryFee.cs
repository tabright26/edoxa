// Filename: MoneyEntryFee.cs
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

using eDoxa.Arena.Domain;
using eDoxa.Seedwork.Domain.Enumerations;

using JetBrains.Annotations;

namespace eDoxa.Arena.Challenges.Domain
{
    [TypeConverter(typeof(MoneyEntryFeeConverter))]
    public sealed partial class MoneyEntryFee : EntryFee
    {
        public static readonly MoneyEntryFee TwoAndHalf = new MoneyEntryFee(2.5M);
        public static readonly MoneyEntryFee Five = new MoneyEntryFee(5M);
        public static readonly MoneyEntryFee Ten = new MoneyEntryFee(10M);
        public static readonly MoneyEntryFee Twenty = new MoneyEntryFee(20M);
        public static readonly MoneyEntryFee TwentyFive = new MoneyEntryFee(25M);
        public static readonly MoneyEntryFee Fifty = new MoneyEntryFee(50M);
        public static readonly MoneyEntryFee SeventyFive = new MoneyEntryFee(75M);
        public static readonly MoneyEntryFee OneHundred = new MoneyEntryFee(100M);

        private MoneyEntryFee(decimal entryFee) : base(entryFee, Currency.Money)
        {
        }

        private MoneyEntryFee(string entryFee) : base(Convert.ToDecimal(entryFee), Currency.Money)
        {
        }
    }

    public sealed partial class MoneyEntryFee
    {
        private sealed class MoneyEntryFeeConverter : TypeConverter
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
                        return new MoneyEntryFee(type);
                    }

                    case string type:
                    {
                        return new MoneyEntryFee(type);
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
                if (value is MoneyEntryFee moneyEntryFee)
                {
                    if (destinationType == typeof(decimal))
                    {
                        return moneyEntryFee.Amount;
                    }

                    if (destinationType == typeof(string))
                    {
                        return moneyEntryFee.Amount.ToString(CultureInfo.InvariantCulture);
                    }
                }

                return base.ConvertTo(context, culture, value, destinationType);
            }
        }
    }
}
