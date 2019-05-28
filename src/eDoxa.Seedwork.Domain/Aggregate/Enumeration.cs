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
using System.Linq;
using System.Reflection;

using JetBrains.Annotations;

namespace eDoxa.Seedwork.Domain.Aggregate
{
    public abstract class Enumeration : IComparable
    {
        protected Enumeration(int value, string name)
        {
            Value = value;
            Name = name;
        }

        public int Value { get; private set; }

        public string Name { get; private set; }

        public int CompareTo([CanBeNull] object other)
        {
            return Value.CompareTo((other as Enumeration)?.Value);
        }

        public static bool operator ==([CanBeNull] Enumeration left, [CanBeNull] Enumeration right)
        {
            return !(left is null ^ right is null) && (left is null || left.Equals(right));
        }

        public static bool operator !=([CanBeNull] Enumeration left, [CanBeNull] Enumeration right)
        {
            return !(left == right);
        }

        public static IEnumerable<Type> GetTypes()
        {
            return AppDomain.CurrentDomain.GetAssemblies()
                .SelectMany(assembly => assembly.GetTypes())
                .Where(type => type.IsClass && !type.IsAbstract && typeof(Enumeration).IsAssignableFrom(type))
                .ToList();
        }

        [CanBeNull]
        public static TEnumeration FromValue<TEnumeration>(int value)
        where TEnumeration : Enumeration
        {
            return GetAll<TEnumeration>().SingleOrDefault(enumeration => enumeration.Value == value);
        }

        [CanBeNull]
        public static TEnumeration FromName<TEnumeration>([CanBeNull] string name)
        where TEnumeration : Enumeration
        {
            return GetAll<TEnumeration>().SingleOrDefault(enumeration => string.Equals(enumeration.Name, name, StringComparison.InvariantCultureIgnoreCase));
        }

        public static IEnumerable<TEnumeration> GetAll<TEnumeration>()
        where TEnumeration : Enumeration
        {
            return GetAll(typeof(TEnumeration)).Cast<TEnumeration>().ToList();
        }

        public static IEnumerable<Enumeration> GetAll(Type type)
        {
            return type.GetFields(BindingFlags.Public | BindingFlags.Static | BindingFlags.DeclaredOnly)
                .Select(fieldInfo => fieldInfo.GetValue(null))
                .Cast<Enumeration>()
                .ToList();
        }

        public static bool HasEnumeration<TEnumeration>([CanBeNull] TEnumeration enumeration)
        where TEnumeration : Enumeration
        {
            return GetAll<TEnumeration>().Contains(enumeration);
        }

        public override string ToString()
        {
            return Name;
        }

        public override bool Equals([CanBeNull] object obj)
        {
            if (!(obj is Enumeration enumeration))
            {
                return false;
            }

            return this.GetType() == obj.GetType() && Value.Equals(enumeration.Value);
        }

        public override int GetHashCode()
        {
            return Value.GetHashCode();
        }

        public bool HasFilter([CanBeNull] Enumeration enumeration)
        {
            return (Value & (enumeration?.Value ?? -1)) != 0;
        }
    }
}
