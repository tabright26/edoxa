// Filename: EntityId.cs
// Date Created: 2019-08-18
// 
// ================================================
// Copyright © 2019, eDoxa. All rights reserved.

using System;
using System.Collections.Generic;
using System.ComponentModel;
using System.Globalization;

namespace eDoxa.Seedwork.Domain
{
    public abstract partial class StringId<TStringId> : ValueObject, IComparable
    where TStringId : StringId<TStringId>, new()
    {
        public static readonly TStringId Empty = new TStringId
        {
            Value = string.Empty
        };

        protected StringId()
        {
            Value = Guid.NewGuid().ToString();
        }

        protected string Value { get; set; }

        public int CompareTo(object? other)
        {
            return string.Compare(Value, (other as TStringId)?.Value, StringComparison.Ordinal);
        }

        public static implicit operator string(StringId<TStringId> id)
        {
            return id.Value;
        }

        public static TStringId Parse(string value)
        {
            return new TStringId
            {
                Value = value
            };
        }

        public bool IsTransient()
        {
            return Value == Empty.Value;
        }

        public override string ToString()
        {
            return Value;
        }

        protected override IEnumerable<object> GetAtomicValues()
        {
            yield return Value;
        }
    }

    public abstract partial class StringId<TStringId>
    {
        protected sealed class StringIdTypeConverter : TypeConverter
        {
            public override bool CanConvertFrom(ITypeDescriptorContext? context, Type sourceType)
            {
                return sourceType == typeof(string) || base.CanConvertFrom(context, sourceType);
            }

            public override bool CanConvertTo(ITypeDescriptorContext? context, Type destinationType)
            {
                return destinationType == typeof(string) || base.CanConvertTo(context, destinationType);
            }

            public override object? ConvertFrom(ITypeDescriptorContext context, CultureInfo culture, object? value)
            {
                switch (value)
                {
                    case null:
                    {
                        return Empty;
                    }

                    case string stringId:
                    {
                        return Parse(stringId);
                    }

                    default:
                    {
                        return base.ConvertFrom(context, culture, value);
                    }
                }
            }

            public override object? ConvertTo(
                ITypeDescriptorContext? context,
                CultureInfo culture,
                object? value,
                Type destinationType
            )
            {
                if (value is TStringId stringId)
                {
                    if (destinationType == typeof(string))
                    {
                        return stringId.ToString();
                    }
                }

                return base.ConvertTo(context, culture, value, destinationType);
            }
        }
    }
}
