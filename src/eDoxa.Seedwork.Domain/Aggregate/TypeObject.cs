// Filename: TypeObject.cs
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

using eDoxa.Reflection;

using JetBrains.Annotations;

namespace eDoxa.Seedwork.Domain.Aggregate
{
    public abstract partial class TypeObject<TTypeObject, TPrimitive>
    where TTypeObject : TypeObject<TTypeObject, TPrimitive>
    where TPrimitive : IComparable, IComparable<TPrimitive>, IEquatable<TPrimitive>
    {
        protected TypeObject(TPrimitive value)
        {
            Value = value;
        }

        protected TPrimitive Value { get; }

        public static implicit operator TPrimitive(TypeObject<TTypeObject, TPrimitive> typeObject)
        {
            return typeObject.Value;
        }

        public override string ToString()
        {
            return Value.ToString();
        }

        public static bool HasValue(TPrimitive value)
        {
            return GetValues().Any(primitive => primitive.Equals(value));
        }

        public static bool HasValue<T>(TPrimitive value)
        where T : TTypeObject
        {
            return GetValues<T>().Any(primitive => primitive.Equals(value));
        }

        public static IEnumerable<TPrimitive> GetValues()
        {
            return StaticUtils.GetDeclaredFields<TTypeObject>()
                .Select(typeObject => typeObject.Value)
                .ToList();
        }

        public static IEnumerable<TPrimitive> GetValues<T>()
        where T : TTypeObject
        {
            return StaticUtils.GetDeclaredFields<T>()
                .Select(typeObject => typeObject.Value)
                .ToList();
        }
        
        public static string DisplayNames()
        {
            return $"[ {string.Join(", ", GetValues())} ]";
        }

        public static string DisplayNames<T>()
        where T : TTypeObject
        {
            return $"[ {string.Join(", ", GetValues<T>())} ]";
        }
    }

    public abstract partial class TypeObject<TTypeObject, TPrimitive> : IEquatable<TTypeObject>
    {
        public virtual bool Equals([CanBeNull] TTypeObject other)
        {
            return this.GetType() == other?.GetType() && Value.Equals(other.Value);
        }

        public static bool operator ==(TypeObject<TTypeObject, TPrimitive> left, TypeObject<TTypeObject, TPrimitive> right)
        {
            return EqualityComparer<TypeObject<TTypeObject, TPrimitive>>.Default.Equals(left, right);
        }

        public static bool operator !=(TypeObject<TTypeObject, TPrimitive> left, TypeObject<TTypeObject, TPrimitive> right)
        {
            return !(left == right);
        }

        public sealed override bool Equals([CanBeNull] object obj)
        {
            return this.Equals(obj as TTypeObject);
        }

        public override int GetHashCode()
        {
            return Value.GetHashCode();
        }
    }

    public abstract partial class TypeObject<TTypeObject, TPrimitive> : IComparable, IComparable<TTypeObject>
    {
        public int CompareTo([CanBeNull] object obj)
        {
            return this.CompareTo(obj as TTypeObject);
        }

        public int CompareTo([CanBeNull] TTypeObject other)
        {
            return Value.CompareTo(other != null ? other.Value : default);
        }
    }
}
