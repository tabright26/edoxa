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
    where TEnumeration : Enumeration<TEnumeration>
    {
        private string _name;
        private int _value;

        protected Enumeration(int value, string name)
        {
            _value = value;
            _name = name;
        }

        public static explicit operator int(Enumeration<TEnumeration> enumeration)
        {
            return enumeration._value;
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