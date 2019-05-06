// Filename: Enumeration.cs
// Date Created: 2019-05-05
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
    public abstract partial class Enumeration<TEnumeration>
    where TEnumeration : Enumeration<TEnumeration>, new()
    {
        public static readonly TEnumeration None = new TEnumeration
        {
            _value = 0,
            _name = nameof(None)
        };

        public static readonly TEnumeration All = new TEnumeration
        {
            _value = -1,
            _name = nameof(All)
        };

        private string _name;
        private int _value;

        protected Enumeration(int value, string name) : this()
        {
            _value = value;
            _name = name;
        }

        protected Enumeration()
        {
        }

        public static IEnumerable<int> GetValues()
        {
            return GetEnums()
                .OrderBy(enumeration => enumeration._value)
                .Select(enumeration => enumeration._value)
                .ToList();
        }

        public static IEnumerable<string> GetNames()
        {
            return GetEnums()
                .OrderBy(enumeration => enumeration._value)
                .Select(enumeration => enumeration._name)
                .ToList();
        }

        public static IEnumerable<TEnumeration> GetEnums()
        {
            return typeof(TEnumeration)
                .GetFields(BindingFlags.Public | BindingFlags.Static | BindingFlags.DeclaredOnly)
                .Select(info => info.GetValue(null))
                .Cast<TEnumeration>()
                .ToList();
        }

        public override string ToString()
        {
            return _name;
        }
    }

    public abstract partial class Enumeration<TEnumeration> : IEquatable<TEnumeration>
    {
        public bool Equals(TEnumeration other)
        {
            return _value.Equals(other?._value);
        }

        public override bool Equals(object obj)
        {
            return this.Equals((TEnumeration) obj);
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
            return this.CompareTo((TEnumeration) other);
        }

        public int CompareTo(TEnumeration other)
        {
            return _value.CompareTo(other?._value);
        }
    }
}