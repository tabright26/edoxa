// Filename: Enumeration.cs
// Date Created: 2019-05-20
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

using eDoxa.Seedwork.Domain.Extensions;

using JetBrains.Annotations;

namespace eDoxa.Seedwork.Domain.Aggregate
{
    public static class Enumeration
    {
        public static IEnumerable<Type> GetTypes()
        {
            return AppDomain.CurrentDomain.GetAssemblies()
                .SelectMany(assembly => assembly.GetTypes())
                .Where(type => type.IsClass && !type.IsAbstract && typeof(IEnumeration).IsAssignableFrom(type))
                .ToArray();
        }

        public static IEnumerable<int> GetValues(Type enumerationType)
        {
            return GetAll(enumerationType).Select(enumeration => enumeration.Value).ToArray();
        }

        public static IEnumerable<string> GetNames<TEnumerable>()
        where TEnumerable : IEnumeration
        {
            return GetNames(typeof(TEnumerable));
        }

        public static IEnumerable<string> GetNames(Type enumerationType)
        {
            return GetAll(enumerationType).Select(enumeration => enumeration.Name).ToArray();
        }

        [CanBeNull]
        public static TEnumerable FromValue<TEnumerable>(int value)
        where TEnumerable : IEnumeration
        {
            return (TEnumerable) FromValue(value, typeof(TEnumerable));
        }

        [CanBeNull]
        private static IEnumeration FromValue(int value, Type enumerationType)
        {
            return GetAll(enumerationType).SingleOrDefault(enumeration => enumeration.Value == value);
        }

        [CanBeNull]
        public static TEnumerable FromName<TEnumerable>(string name)
        where TEnumerable : IEnumeration
        {
            return (TEnumerable) FromName(name, typeof(TEnumerable));
        }

        [CanBeNull]
        private static IEnumeration FromName(string name, Type enumerationType)
        {
            return GetAll(enumerationType).SingleOrDefault(enumeration => enumeration.Name == name.ToPascalcase());
        }

        public static IEnumerable<TEnumeration> GetAll<TEnumeration>()
        where TEnumeration : IEnumeration
        {
            return GetAll(typeof(TEnumeration)).Cast<TEnumeration>();
        }

        public static IEnumerable<IEnumeration> GetAll(Type enumerationType)
        {
            return enumerationType.GetFields(BindingFlags.Public | BindingFlags.Static | BindingFlags.DeclaredOnly)
                .Select(field => field.GetValue(null))
                .Where(obj => obj is IEnumeration)
                .Cast<IEnumeration>();
        }

        public static string DisplayNames<TEnumeration>()
        where TEnumeration : IEnumeration
        {
            return $"[ {string.Join(", ", GetAll<TEnumeration>().Select(enumeration => enumeration.ToString()))} ]";
        }

        public static bool IsInEnumeration<TEnumeration>(TEnumeration enumeration)
        where TEnumeration : IEnumeration
        {
            return GetAll<TEnumeration>().Any(x => x.Equals(enumeration));
        }
    }

    public abstract partial class Enumeration<TEnumeration> : IEnumeration
    where TEnumeration : Enumeration<TEnumeration>
    {
        public static readonly TEnumeration All = (TEnumeration) Activator.CreateInstance(
            typeof(TEnumeration),
            BindingFlags.Instance | BindingFlags.NonPublic,
            null,
            new object[] {-1, nameof(All)},
            null
        );

        private static readonly TEnumeration None = (TEnumeration) Activator.CreateInstance(
            typeof(TEnumeration),
            BindingFlags.Instance | BindingFlags.NonPublic,
            null,
            new object[] {0, nameof(None)},
            null
        );

        private string _name;
        private int _value;

        protected Enumeration(int value, string name)
        {
            _value = value;
            _name = name;
        }

        public int Value => _value;

        public string Name => _name;

        public static explicit operator int(Enumeration<TEnumeration> enumeration)
        {
            return enumeration._value;
        }

        [CanBeNull]
        public static TEnumeration FromValue(int value)
        {
            return Enumeration.FromValue<TEnumeration>(value);
        }

        [CanBeNull]
        public static TEnumeration FromName(string name)
        {
            return Enumeration.FromName<TEnumeration>(name);
        }

        public override string ToString()
        {
            return _name.ToCamelcase();
        }

        public bool Filter([CanBeNull] TEnumeration enumeration)
        {
            enumeration = enumeration ?? All;

            return (_value & enumeration._value) != None._value;
        }
    }

    public abstract partial class Enumeration<TEnumeration> : IEquatable<TEnumeration>
    {
        public bool Equals([CanBeNull] TEnumeration other)
        {
            return this.GetType() == other?.GetType() && _value.Equals(other._value);
        }

        public static bool operator ==(Enumeration<TEnumeration> left, Enumeration<TEnumeration> right)
        {
            return EqualityComparer<Enumeration<TEnumeration>>.Default.Equals(left, right);
        }

        public static bool operator !=(Enumeration<TEnumeration> left, Enumeration<TEnumeration> right)
        {
            return !(left == right);
        }

        public sealed override bool Equals([CanBeNull] object obj)
        {
            return this.Equals(obj as TEnumeration);
        }

        public override int GetHashCode()
        {
            return _value.GetHashCode();
        }
    }

    public abstract partial class Enumeration<TEnumeration> : IComparable, IComparable<TEnumeration>
    {
        public int CompareTo([CanBeNull] object other)
        {
            return this.CompareTo(other as TEnumeration);
        }

        public int CompareTo([CanBeNull] TEnumeration other)
        {
            return _value.CompareTo(other?._value);
        }
    }

    public abstract partial class Enumeration<TEnumeration>
    {
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

                    case int val:
                    {
                        return FromValue(val) ?? None;
                    }

                    case string name:
                    {
                        return FromName(name) ?? None;
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
                        return enumeration.ToString();
                    }
                }

                return base.ConvertTo(context, culture, value, destinationType);
            }
        }
    }
}
