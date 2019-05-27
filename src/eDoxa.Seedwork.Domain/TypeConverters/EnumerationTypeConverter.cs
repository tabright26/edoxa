// Filename: EnumerationTypeConverter.cs
// Date Created: 2019-05-26
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

namespace eDoxa.Seedwork.Domain.TypeConverters
{
    public sealed class EnumerationTypeConverter<TEnumeration> : TypeConverter
    where TEnumeration : Enumeration
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
                    return null;
                }

                case int obj:
                {
                    return Enumeration.FromValue<TEnumeration>(obj);
                }

                case string obj:
                {
                    return Enumeration.FromName<TEnumeration>(obj);
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
            if (value is TEnumeration enumeration)
            {
                if (destinationType == typeof(int))
                {
                    return enumeration.Value;
                }

                if (destinationType == typeof(string))
                {
                    return enumeration.Name;
                }
            }

            return base.ConvertTo(context, culture, value, destinationType);
        }
    }
}
