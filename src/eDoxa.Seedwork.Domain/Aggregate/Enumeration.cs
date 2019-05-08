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
    public interface IEnumeration
    {
        int Value { get; }

        string DisplayName { get; }
    }

    public abstract partial class Enumeration<TEnumeration> : IEnumeration
    where TEnumeration : Enumeration<TEnumeration>, new()
    {
        public static readonly TEnumeration None = new TEnumeration
        {
            _value = 0,
            _displayName = nameof(None)
        };

        public static readonly TEnumeration All = new TEnumeration
        {
            _value = -1,
            _displayName = nameof(All)
        };

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

        public static TEnumeration FromAnyDisplayName(string displayName)
        {
            if (displayName == None.DisplayName)
            {
                return None;
            }

            if (displayName == All.DisplayName)
            {
                return All;
            }

            return FromDisplayName(displayName);
        }

        public static IEnumerable<TEnumeration> GetAll()
        {
            foreach (var fieldInfo in typeof(TEnumeration).GetFields(BindingFlags.Public | BindingFlags.Static | BindingFlags.DeclaredOnly))
            {
                if (fieldInfo.GetValue(new TEnumeration()) is TEnumeration enumeration)
                {
                    yield return enumeration;
                }
            }
        }

        private static TEnumeration Parse<TKey>(TKey value, string description, Func<TEnumeration, bool> predicate)
        {
            var enumeration = GetAll().FirstOrDefault(predicate);

            if (enumeration == null)
            {
                throw new ApplicationException($"'{value}' is not a valid {description} in {typeof(TEnumeration)}");
            }

            return enumeration;
        }

        public static TEnumeration FromValue(int value)
        {
            return Parse<int>(value, "value", enumeration => enumeration.Value == value);
        }

        public static TEnumeration FromDisplayName(string displayName)
        {
            return Parse<string>(displayName, "display name", enumeration => enumeration.DisplayName == displayName);
        }

        public override string ToString()
        {
            return _displayName;
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
}