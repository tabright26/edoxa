// Filename: EntityId.cs
// Date Created: 2019-04-09
// 
// ============================================================
// Copyright © 2019, Francis Quenneville
// All rights reserved.
// 
// This file is subject to the terms and conditions defined in file 'LICENSE.md', which is part of
// this source code package.

using System;
using System.Collections.Generic;
using System.ComponentModel;
using System.Globalization;
using System.Reflection;
using JetBrains.Annotations;

namespace eDoxa.Seedwork.Domain.Aggregate
{
    public abstract partial class EntityId<TEntityId> : BaseObject
    where TEntityId : EntityId<TEntityId>, new()
    {
        private Guid _value;

        protected EntityId()
        {
            _value = Guid.NewGuid();
        }

        protected Guid Value
        {
            get
            {
                return _value;
            }
            private set
            {
                if (value == Guid.Empty)
                {
                    throw new ArgumentException(nameof(Value));
                }

                _value = value;
            }
        }

        public static bool operator ==(EntityId<TEntityId> left, EntityId<TEntityId> right)
        {
            return EqualityComparer<EntityId<TEntityId>>.Default.Equals(left, right);
        }

        public static bool operator !=(EntityId<TEntityId> left, EntityId<TEntityId> right)
        {
            return !(left == right);
        }

        public static TEntityId NewId()
        {
            return new TEntityId();
        }

        public static TEntityId FromGuid(Guid value)
        {
            return new TEntityId
            {
                Value = value
            };
        }

        public static TEntityId Parse(string value)
        {
            return new TEntityId
            {
                Value = Guid.Parse(value)
            };
        }

        public sealed override bool Equals([CanBeNull] object obj)
        {
            return base.Equals(obj);
        }

        public sealed override int GetHashCode()
        {
            return base.GetHashCode();
        }

        public sealed override string ToString()
        {
            return Value.ToString();
        }

        public Guid ToGuid()
        {
            return Value;
        }

        public bool IsTransient()
        {
            return Value == default;
        }

        protected sealed override PropertyInfo[] TypeSignatureProperties()
        {
            return new[]
            {
                this.GetType().GetProperty(nameof(Value), BindingFlags.NonPublic | BindingFlags.Instance)
            };
        }

        private static bool TryParse(string input, [CanBeNull] out TEntityId entityId)
        {
            entityId = null;

            Guid.TryParse(input, out var value);

            if (value != Guid.Empty)
            {
                entityId = FromGuid(value);
            }

            return value != Guid.Empty;
        }
    }

    public abstract partial class EntityId<TEntityId> : IComparable, IComparable<TEntityId>
    {
        public int CompareTo([CanBeNull] object obj)
        {
            return this.CompareTo(obj as TEntityId);
        }

        public int CompareTo([CanBeNull] TEntityId other)
        {
            return Value.CompareTo(other?.Value);
        }
    }

    public abstract partial class EntityId<TEntityId>
    {
        protected sealed class EntityIdTypeConverter : TypeConverter
        {
            public override bool CanConvertFrom([NotNull] ITypeDescriptorContext context, Type sourceType)
            {
                if (sourceType == typeof(string) || sourceType == typeof(Guid))
                {
                    return true;
                }

                return base.CanConvertFrom(context, sourceType);
            }

            public override bool CanConvertTo([NotNull] ITypeDescriptorContext context, Type destinationType)
            {
                if (destinationType == typeof(string) || destinationType == typeof(Guid))
                {
                    return true;
                }

                return base.CanConvertTo(context, destinationType);
            }

            [CanBeNull]
            public override object ConvertFrom([NotNull] ITypeDescriptorContext context, CultureInfo culture, [CanBeNull] object value)
            {
                switch (value)
                {
                    case string input:

                        TryParse(input, out var entityId);

                        return entityId;

                    case Guid input:

                        return FromGuid(input);
                }

                return base.ConvertFrom(context, culture, value);
            }

            [CanBeNull]
            public override object ConvertTo([NotNull] ITypeDescriptorContext context, [NotNull] CultureInfo culture, [NotNull] object value, Type destinationType)
            {
                var entityId = value as TEntityId;

                if (entityId == null)
                {
                    return null;
                }

                if (destinationType == typeof(string))
                {
                    return entityId.ToString();
                }

                if (destinationType == typeof(Guid))
                {
                    return entityId.ToGuid();
                }

                return base.ConvertTo(context, culture, value, destinationType);
            }
        }
    }
}