// Filename: PayoutEntries.cs
// Date Created: 2019-05-20
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

using eDoxa.Seedwork.Domain.Aggregate;

using JetBrains.Annotations;

namespace eDoxa.Arena.Challenges.Domain
{
    [TypeConverter(typeof(PayoutEntriesTypeConverter))]
    public sealed partial class PayoutEntries : TypeObject<PayoutEntries, int>
    {
        public static readonly PayoutEntries One = new PayoutEntries(1);
        public static readonly PayoutEntries Two = new PayoutEntries(2);
        public static readonly PayoutEntries Three = new PayoutEntries(3);
        public static readonly PayoutEntries Four = new PayoutEntries(4);
        public static readonly PayoutEntries Five = new PayoutEntries(5);
        public static readonly PayoutEntries Ten = new PayoutEntries(10);
        public static readonly PayoutEntries Fifteen = new PayoutEntries(15);
        public static readonly PayoutEntries Twenty = new PayoutEntries(20);
        public static readonly PayoutEntries TwentyFive = new PayoutEntries(25);
        public static readonly PayoutEntries Fifty = new PayoutEntries(50);
        public static readonly PayoutEntries SeventyFive = new PayoutEntries(75);
        public static readonly PayoutEntries OneHundred = new PayoutEntries(100);

        public PayoutEntries(Entries entries, PayoutRatio payoutRatio) : base(Convert.ToInt32(Math.Floor(entries * payoutRatio)))
        {
        }

        internal PayoutEntries(int payoutEntries) : base(payoutEntries)
        {
        }

        private PayoutEntries(string payoutEntries) : base(Convert.ToInt32(payoutEntries))
        {
        }
    }

    public sealed partial class PayoutEntries
    {
        private sealed class PayoutEntriesTypeConverter : TypeConverter
        {
            public override bool CanConvertFrom([CanBeNull] ITypeDescriptorContext context, Type sourceType)
            {
                return sourceType == typeof(int) || sourceType == typeof(string) || base.CanConvertFrom(context, sourceType);
            }

            public override bool CanConvertTo([CanBeNull] ITypeDescriptorContext context, Type destinationType)
            {
                return destinationType == typeof(int) || destinationType == typeof(string) || base.CanConvertTo(context, destinationType);
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

                    case int cast:
                    {
                        return new PayoutEntries(cast);
                    }

                    case string cast:
                    {
                        return new PayoutEntries(cast);
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
                if (value is PayoutEntries payoutEntries)
                {
                    if (destinationType == typeof(int))
                    {
                        return payoutEntries.Value;
                    }

                    if (destinationType == typeof(string))
                    {
                        return payoutEntries.Value.ToString();
                    }
                }

                return base.ConvertTo(context, culture, value, destinationType);
            }
        }
    }
}
