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
    public abstract partial class EntityId<TEntityId> : ValueObject, IComparable
    where TEntityId : EntityId<TEntityId>, new()
    {
        public static readonly TEntityId Empty = new TEntityId
        {
            Value = Guid.Empty
        };

        protected EntityId()
        {
            Value = Guid.NewGuid();
        }

        protected Guid Value { get; set; }

        public int CompareTo(object? other)
        {
            return Value.CompareTo((other as TEntityId)?.Value);
        }

        public static implicit operator Guid(EntityId<TEntityId> entityId)
        {
            return entityId.Value;
        }

        public static TEntityId FromGuid(Guid value)
        {
            return new TEntityId
            {
                Value = value
            };
        }

        public static TEntityId Parse(string entityId)
        {
            return Guid.TryParse(entityId, out var result) ? FromGuid(result) : Empty;
        }

        public Guid ToGuid()
        {
            return Value;
        }

        public bool IsTransient()
        {
            return Value == Empty.Value;
        }

        public override string ToString()
        {
            return Value.ToString();
        }

        protected override IEnumerable<object> GetAtomicValues()
        {
            yield return Value;
        }
    }

    public abstract partial class EntityId<TEntityId>
    {
        protected sealed class EntityIdTypeConverter : TypeConverter
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

                    case string entityId:
                    {
                        return Parse(entityId);
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
                if (value is TEntityId entityId)
                {
                    if (destinationType == typeof(string))
                    {
                        return entityId.ToString();
                    }
                }

                return base.ConvertTo(context, culture, value, destinationType);
            }
        }
    }
}
