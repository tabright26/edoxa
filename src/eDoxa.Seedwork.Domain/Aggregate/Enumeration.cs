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
using System.Linq;
using System.Reflection;

namespace eDoxa.Seedwork.Domain.Aggregate
{
    public abstract partial class Enumeration
    {
        private string _displayName;
        private int _value;

        protected Enumeration(int value, string displayName) : this()
        {
            _value = value;
            _displayName = displayName;
        }

        protected Enumeration()
        {
        }

        public int Value => _value;

        public string DisplayName => _displayName;

        public static TEnumeration None<TEnumeration>()
        where TEnumeration : Enumeration, new()
        {
            return new TEnumeration
            {
                _value = 0,
                _displayName = nameof(None)
            };
        }

        public static TEnumeration All<TEnumeration>()
        where TEnumeration : Enumeration, new()
        {
            return new TEnumeration
            {
                _value = -1,
                _displayName = nameof(All)
            };
        }

        public static IEnumerable<TEnumeration> GetAll<TEnumeration>()
        where TEnumeration : Enumeration, new()
        {
            foreach (var fieldInfo in typeof(TEnumeration).GetFields(BindingFlags.Public | BindingFlags.Static | BindingFlags.DeclaredOnly))
            {
                if (fieldInfo.GetValue(new TEnumeration()) is TEnumeration enumeration)
                {
                    yield return enumeration;
                }
            }
        }

        private static TEnumeration Parse<TEnumeration, TKey>(TKey value, string description, Func<TEnumeration, bool> predicate)
        where TEnumeration : Enumeration, new()
        {
            var enumeration = GetAll<TEnumeration>().FirstOrDefault(predicate);

            if (enumeration == null)
            {
                throw new ApplicationException($"'{value}' is not a valid {description} in {typeof(TEnumeration)}");
            }

            return enumeration;
        }

        public static int AbsoluteDifference(Enumeration left, Enumeration right)
        {
            return Math.Abs(left.Value - right.Value);
        }

        public static TEnumeration FromValue<TEnumeration>(int value)
        where TEnumeration : Enumeration, new()
        {
            return Parse<TEnumeration, int>(value, "value", enumeration => enumeration.Value == value);
        }

        public static TEnumeration FromDisplayName<TEnumeration>(string displayName)
        where TEnumeration : Enumeration, new()
        {
            return Parse<TEnumeration, string>(displayName, "display name", enumeration => enumeration.DisplayName == displayName);
        }

        public override string ToString()
        {
            return _displayName;
        }
    }

    public abstract partial class Enumeration : IEquatable<Enumeration>
    {
        public bool Equals(Enumeration other)
        {
            return this.GetType() == other?.GetType() && _value.Equals(other._value);
        }

        public override bool Equals(object obj)
        {
            return this.Equals(obj as Enumeration);
        }

        public override int GetHashCode()
        {
            return _value.GetHashCode();
        }
    }

    public abstract partial class Enumeration : IComparable, IComparable<Enumeration>
    {
        public int CompareTo(object other)
        {
            return this.CompareTo(other as Enumeration);
        }

        public int CompareTo(Enumeration other)
        {
            return _value.CompareTo(other?._value);
        }
    }
}