// Filename: BestOf.cs
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
    [TypeConverter(typeof(BestOfTypeConverter))]
    public sealed partial class BestOf : TypeObject<BestOf, int>
    {
        public static readonly BestOf One = new BestOf(1);
        public static readonly BestOf Three = new BestOf(3);
        public static readonly BestOf Five = new BestOf(5);
        public static readonly BestOf Seven = new BestOf(7);

        internal BestOf(int bestOf) : base(bestOf)
        {
        }

        private BestOf(string bestOf) : base(Convert.ToInt32(bestOf))
        {
        }
    }

    public sealed partial class BestOf
    {
        private sealed class BestOfTypeConverter : TypeConverter
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
                        return Three;
                    }

                    case int cast:
                    {
                        return new BestOf(cast);
                    }

                    case string cast:
                    {
                        return new BestOf(cast);
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
                if (value is BestOf bestOf)
                {
                    if (destinationType == typeof(int))
                    {
                        return bestOf.Value;
                    }

                    if (destinationType == typeof(string))
                    {
                        return bestOf.Value.ToString();
                    }
                }

                return base.ConvertTo(context, culture, value, destinationType);
            }
        }
    }
}
