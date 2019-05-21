// Filename: TypeObject.cs
// Date Created: 2019-05-20
// 
// ================================================
// Copyright © 2019, eDoxa. All rights reserved.
// 
// This file is subject to the terms and conditions
// defined in file 'LICENSE.md', which is part of
// this source code package.

// ReSharper disable ImplicitNotNullOverridesUnknownBaseMemberNullability
// ReSharper disable ImplicitNotNullConflictInHierarchy
// ReSharper disable PossibleNullReferenceException

using System;

namespace eDoxa.Seedwork.Domain.Aggregate
{
    public abstract class TypeObject<T, TPrimitive> : IComparable, IComparable<T>, IEquatable<T>
    where TPrimitive : IComparable, IComparable<TPrimitive>, IEquatable<TPrimitive>
    where T : TypeObject<T, TPrimitive>
    {
        protected readonly TPrimitive Value;

        protected TypeObject(TPrimitive value, bool validate = true)
        {
            Value = !validate || this.IsValid() ? value : throw new ArgumentException();
        }

        public int CompareTo(object obj)
        {
            return this.CompareTo(obj as T);
        }

        public int CompareTo(T other)
        {
            return Value.CompareTo(other.Value);
        }

        public bool Equals(T other)
        {
            return Value.Equals(other.Value);
        }

        public static bool operator ==(TypeObject<T, TPrimitive> left, TypeObject<T, TPrimitive> right)
        {
            return left.Equals(right);
        }

        public static bool operator !=(TypeObject<T, TPrimitive> left, TypeObject<T, TPrimitive> right)
        {
            return !(left == right);
        }

        public override bool Equals(object obj)
        {
            return this.Equals(obj as T);
        }

        public override int GetHashCode()
        {
            return Value.GetHashCode();
        }

        public override string ToString()
        {
            return Value.ToString();
        }

        protected virtual bool IsValid()
        {
            return true;
        }
    }
}
