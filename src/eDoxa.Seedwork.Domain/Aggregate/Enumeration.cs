// Filename: Enumeration.cs
// Date Created: 2019-05-29
// 
// ================================================
// Copyright © 2019, eDoxa. All rights reserved.
// 
// This file is subject to the terms and conditions
// defined in file 'LICENSE.md', which is part of
// this source code package.

using System;
using System.Collections.Generic;
using System.ComponentModel;
using System.Globalization;
using System.Linq;
using System.Reflection;

using JetBrains.Annotations;

namespace eDoxa.Seedwork.Domain.Aggregate
{
    public interface IEnumeration
    {
        string ToString();
    }

    public static class Enumeration
    {
        public static IEnumerable<Type> GetTypes()
        {
            return AppDomain.CurrentDomain.GetAssemblies()
                .SelectMany(assembly => assembly.GetTypes())
                .Where(type => type.IsClass && !type.IsAbstract && typeof(IEnumeration).IsAssignableFrom(type))
                .ToList();
        }

        public static IEnumerable<IEnumeration> GetAll(Type type)
        {
            return type.GetFields(BindingFlags.Public | BindingFlags.Static | BindingFlags.DeclaredOnly)
                .Select(fieldInfo => fieldInfo.GetValue(null))
                .Cast<IEnumeration>()
                .ToList();
        }
    }

    public abstract class Enumeration<TEnumeration> : IEnumeration, IComparable
    where TEnumeration : Enumeration<TEnumeration>, new()
    {
        public static readonly TEnumeration All = new TEnumeration
        {
            Value = -1,
            Name = nameof(All)
        };

        private static readonly TEnumeration None = new TEnumeration();

        protected Enumeration(int value, string name) : this()
        {
            Value = value != None.Value && value != All.Value ? value : throw new ArgumentException(nameof(value));
            Name = !string.IsNullOrWhiteSpace(name) && name != None.Name && name != All.Name ? name : throw new ArgumentException(nameof(name));
        }

        protected Enumeration()
        {
            Value = default;
            Name = nameof(None);
        }

        public int Value { get; private set; }

        public string Name { get; private set; }

        public int CompareTo([CanBeNull] object other)
        {
            return Value.CompareTo((other as TEnumeration)?.Value);
        }

        public override string ToString()
        {
            return Name;
        }

        public static bool operator ==([CanBeNull] Enumeration<TEnumeration> left, [CanBeNull] Enumeration<TEnumeration> right)
        {
            return !(left is null ^ right is null) && (left is null || left.Equals(right));
        }

        public static bool operator !=([CanBeNull] Enumeration<TEnumeration> left, [CanBeNull] Enumeration<TEnumeration> right)
        {
            return !(left == right);
        }

        [CanBeNull]
        public static TEnumeration FromValue(int value)
        {
            return GetAll().SingleOrDefault(enumeration => enumeration.Value == value) ?? None;
        }

        [CanBeNull]
        public static TEnumeration FromName([CanBeNull] string name)
        {
            return GetAll().SingleOrDefault(enumeration => string.Equals(enumeration.Name, name, StringComparison.InvariantCultureIgnoreCase)) ?? None;
        }

        public static IEnumerable<TEnumeration> GetAll()
        {
            return Enumeration.GetAll(typeof(TEnumeration)).Cast<TEnumeration>().ToList();
        }

        public override bool Equals([CanBeNull] object obj)
        {
            if (!(obj is TEnumeration enumeration))
            {
                return false;
            }

            return this.GetType() == obj.GetType() && Value.Equals(enumeration.Value);
        }

        public override int GetHashCode()
        {
            return Value.GetHashCode();
        }

        public static bool HasEnumeration([CanBeNull] TEnumeration enumeration)
        {
            return enumeration != null && enumeration != All && enumeration != None && GetAll().Contains(enumeration);
        }

        public bool HasFilter([CanBeNull] TEnumeration enumeration)
        {
            return (Value & (enumeration?.Value ?? All.Value)) != None.Value;
        }

        protected sealed class EnumerationTypeConverter : TypeConverter
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
                        return All;
                    }

                    case int obj:
                    {
                        return FromValue(obj);
                    }

                    case string obj:
                    {
                        return FromName(obj);
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
}
