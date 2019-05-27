// Filename: EntityId.cs
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

using JetBrains.Annotations;

namespace eDoxa.Seedwork.Domain.Aggregate
{
    public abstract partial class EntityId<TEntityId> : TypedObject<TEntityId, Guid>
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
    }

    public abstract partial class EntityId<TEntityId>
    {
        protected sealed class EntityIdTypeConverter : TypeConverter
        {
            public override bool CanConvertFrom([CanBeNull] ITypeDescriptorContext context, Type sourceType)
            {
                return sourceType == typeof(string) || base.CanConvertFrom(context, sourceType);
            }

            public override bool CanConvertTo([CanBeNull] ITypeDescriptorContext context, Type destinationType)
            {
                return destinationType == typeof(string) || base.CanConvertTo(context, destinationType);
            }

            [CanBeNull]
            public override object ConvertFrom(ITypeDescriptorContext context, CultureInfo culture, [CanBeNull] object value)
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

            [CanBeNull]
            public override object ConvertTo(
                [CanBeNull] ITypeDescriptorContext context,
                [NotNull] CultureInfo culture,
                [CanBeNull] object value,
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
