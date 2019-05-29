// Filename: TypedObject.cs
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

using eDoxa.Seedwork.Domain.Reflection;

using JetBrains.Annotations;

namespace eDoxa.Seedwork.Domain.Aggregate
{
    public abstract partial class TypedObject<TTypedObject, TTypeObject>
    where TTypedObject : TypedObject<TTypedObject, TTypeObject>
    where TTypeObject : IComparable, IComparable<TTypeObject>, IEquatable<TTypeObject>
    {
        protected TTypeObject Value { get; set; }

        public static implicit operator TTypeObject(TypedObject<TTypedObject, TTypeObject> typedObject)
        {
            return typedObject.Value;
        }

        public override string ToString()
        {
            return Value.ToString();
        }

        // TODO: Should be remove.
        public static bool HasValue(TTypeObject value)
        {
            return GetValues().Any(primitive => primitive.Equals(value));
        }

        // TODO: Should be remove.
        public static bool HasValue<T>(TTypeObject value)
        where T : TTypedObject
        {
            return GetValues<T>().Any(primitive => primitive.Equals(value));
        }

        // TODO: Should be remove.
        public static IEnumerable<TTypeObject> GetValues()
        {
            return ReflectionUtilities.GetDeclaredOnlyFields<TTypedObject>().Select(typeObject => typeObject.Value).ToList();
        }

        // TODO: Should be remove.
        public static IEnumerable<TTypeObject> GetValues<T>()
        where T : TTypedObject
        {
            return ReflectionUtilities.GetDeclaredOnlyFields<T>().Select(typeObject => typeObject.Value).ToList();
        }

        // TODO: Should be remove.
        public static string DisplayNames()
        {
            return $"[ {string.Join(", ", GetValues())} ]";
        }

        // TODO: Should be remove.
        public static string DisplayNames<T>()
        where T : TTypedObject
        {
            return $"[ {string.Join(", ", GetValues<T>())} ]";
        }
    }

    public abstract partial class TypedObject<TTypedObject, TTypeObject> : IEquatable<TTypedObject>
    {
        public virtual bool Equals([CanBeNull] TTypedObject other)
        {
            return this.GetType() == other?.GetType() && Value.Equals(other.Value);
        }

        public static bool operator ==(TypedObject<TTypedObject, TTypeObject> left, TypedObject<TTypedObject, TTypeObject> right)
        {
            return EqualityComparer<TypedObject<TTypedObject, TTypeObject>>.Default.Equals(left, right);
        }

        public static bool operator !=(TypedObject<TTypedObject, TTypeObject> left, TypedObject<TTypedObject, TTypeObject> right)
        {
            return !(left == right);
        }

        public sealed override bool Equals([CanBeNull] object obj)
        {
            return this.Equals(obj as TTypedObject);
        }

        public sealed override int GetHashCode()
        {
            return Value.GetHashCode();
        }
    }

    public abstract partial class TypedObject<TTypedObject, TTypeObject> : IComparable, IComparable<TTypedObject>
    {
        public int CompareTo([CanBeNull] object obj)
        {
            return this.CompareTo(obj as TTypedObject);
        }

        public int CompareTo([CanBeNull] TTypedObject other)
        {
            return Value.CompareTo(other != null ? other.Value : default);
        }
    }
}
