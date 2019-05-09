// Filename: Enumeration.cs
// Date Created: 2019-05-06
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

using eDoxa.Seedwork.Domain.Exceptions;

namespace eDoxa.Seedwork.Domain.Aggregate
{
    public static class Enumeration
    {
        public static Type[] GetTypes()
        {
            return AppDomain.CurrentDomain.GetAssemblies().SelectMany(assembly => assembly.GetTypes())
                .Where(type => type.IsClass && !type.IsAbstract && typeof(IEnumeration).IsAssignableFrom(type)).ToArray();
        }

        public static Array GetValues(Type enumerationType)
        {
            return GetAll(enumerationType).Select(enumeration => enumeration.Value).ToArray();
        }

        public static string[] GetNames(Type enumerationType)
        {
            return GetAll(enumerationType).Select(enumeration => enumeration.Name).ToArray();
        }

        public static TEnumerable FromValue<TEnumerable>(int value)
        where TEnumerable : IEnumeration
        {
            return (TEnumerable) FromValue(value, typeof(TEnumerable));
        }

        private static IEnumeration FromValue(int value, Type enumerationType)
        {
            try
            {
                return GetAll(enumerationType).Single(enumeration => enumeration.Value == value);
            }
            catch (ArgumentNullException exception)
            {
                throw new EnumerationException(enumerationType, value, exception);
            }
            catch (InvalidOperationException exception)
            {
                throw new EnumerationException(enumerationType, value, exception);
            }
        }

        public static TEnumerable FromName<TEnumerable>(string name)
        where TEnumerable : IEnumeration
        {
            return (TEnumerable) FromName(name, typeof(TEnumerable));
        }

        private static IEnumeration FromName(string name, Type enumerationType)
        {
            try
            {
                return GetAll(enumerationType).Single(enumeration => enumeration.Name == name);
            }
            catch (ArgumentNullException exception)
            {
                throw new EnumerationException(enumerationType, name, exception);
            }
            catch (InvalidOperationException exception)
            {
                throw new EnumerationException(enumerationType, name, exception);
            }
        }

        public static IEnumerable<TEnumeration> GetAll<TEnumeration>()
        where TEnumeration : IEnumeration
        {
            return GetAll(typeof(TEnumeration)).Cast<TEnumeration>();
        }

        private static IEnumerable<IEnumeration> GetAll(Type enumerationType)
        {
            return enumerationType.GetFields(BindingFlags.Public | BindingFlags.Static | BindingFlags.DeclaredOnly)
                .Select(field => field.GetValue(null))
                .Where(obj => obj is IEnumeration)
                .Cast<IEnumeration>();
        }

        public static IEnumerable<TEnumeration> GetFlags<TEnumeration>()
        where TEnumeration : IEnumeration
        {
            return GetFlags(typeof(TEnumeration)).Cast<TEnumeration>();
        }

        public static IEnumerable<IEnumeration> GetFlags(Type enumerationType)
        {
            return enumerationType.GetFields(BindingFlags.Public | BindingFlags.Static | BindingFlags.FlattenHierarchy)
                .Select(field => field.GetValue(null))
                .Where(obj => obj is IEnumeration)
                .Cast<IEnumeration>();
        }
    }

    public abstract partial class Enumeration<TEnumeration> : IEnumeration
    where TEnumeration : Enumeration<TEnumeration>
    {
        public static readonly TEnumeration None = (TEnumeration) Activator.CreateInstance(typeof(TEnumeration), BindingFlags.Instance | BindingFlags.NonPublic,
            null, new object[] {0, nameof(None)}, null);

        public static readonly TEnumeration All = (TEnumeration) Activator.CreateInstance(typeof(TEnumeration), BindingFlags.Instance | BindingFlags.NonPublic,
            null, new object[] {-1, nameof(All)}, null);

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

        public static IEnumerable<TEnumeration> GetFlags()
        {
            return Enumeration.GetFlags<TEnumeration>();
        }

        public static IEnumerable<TEnumeration> GetAll()
        {
            return Enumeration.GetAll<TEnumeration>();
        }

        public static TEnumeration FromValue(int value)
        {
            return Enumeration.FromValue<TEnumeration>(value);
        }

        public static TEnumeration FromName(string name)
        {
            return Enumeration.FromName<TEnumeration>(name);
        }

        private static TEnumeration GetFlag(int value)
        {
            return value == (int) None ? None : value == (int) All ? All : FromValue(value);
        }

        private static TEnumeration GetFlag(string name)
        {
            return name == None.ToString() ? None : name == All.ToString() ? All : FromName(name);
        }

        public override string ToString()
        {
            return _name;
        }

        public bool HasFlag(TEnumeration enumeration)
        {
            return (_value & enumeration._value) != None._value;
        }
    }

    public abstract partial class Enumeration<TEnumeration> : IEquatable<TEnumeration>
    {
        public bool Equals(TEnumeration other)
        {
            return this.GetType() == other?.GetType() && _value.Equals(other._value);
        }

        public override bool Equals(object obj)
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
        public int CompareTo(object other)
        {
            return this.CompareTo(other as TEnumeration);
        }

        public int CompareTo(TEnumeration other)
        {
            return _value.CompareTo(other?._value);
        }
    }

    public abstract partial class Enumeration<TEnumeration>
    {
        protected sealed class EnumerationConverter : TypeConverter
        {
            public override bool CanConvertFrom(ITypeDescriptorContext context, Type sourceType)
            {
                return sourceType == typeof(int) || sourceType == typeof(string) || base.CanConvertFrom(context, sourceType);
            }

            public override bool CanConvertTo(ITypeDescriptorContext context, Type destinationType)
            {
                return destinationType == typeof(int) || destinationType == typeof(string) || base.CanConvertTo(context, destinationType);
            }

            public override object ConvertFrom(ITypeDescriptorContext context, CultureInfo culture, object value)
            {
                switch (value)
                {
                    case int val:
                    {
                        return GetFlag(val);
                    }

                    case string name when !string.IsNullOrWhiteSpace(name):
                    {
                        return GetFlag(name);
                    }

                    default:
                    {
                        return base.ConvertFrom(context, culture, value);
                    }
                }
            }

            public override object ConvertTo(ITypeDescriptorContext context, CultureInfo culture, object value, Type destinationType)
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