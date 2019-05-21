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

using JetBrains.Annotations;

namespace eDoxa.Seedwork.Domain.Aggregate
{
    public abstract partial class TypeObject<TObject, TPrimitive>
    where TObject : TypeObject<TObject, TPrimitive>
    where TPrimitive : IComparable, IComparable<TPrimitive>, IEquatable<TPrimitive>
    {
        protected readonly TPrimitive Value;

        protected TypeObject(TPrimitive value)
        {
            Value = value;
        }

        public static implicit operator TPrimitive(TypeObject<TObject, TPrimitive> obj)
        {
            return obj.Value;
        }

        public override string ToString()
        {
            return Value.ToString();
        }
    }

    public abstract partial class TypeObject<TObject, TPrimitive> : IEquatable<TObject>
    {
        public bool Equals([CanBeNull] TObject other)
        {
            return Value.Equals(other != null ? other.Value : default);
        }

        public override bool Equals([CanBeNull] object obj)
        {
            return this.Equals(obj as TObject);
        }

        public override int GetHashCode()
        {
            return Value.GetHashCode();
        }
    }

    public abstract partial class TypeObject<TObject, TPrimitive> : IComparable, IComparable<TObject>
    {
        public int CompareTo([CanBeNull] object obj)
        {
            return this.CompareTo(obj as TObject);
        }

        public int CompareTo([CanBeNull] TObject other)
        {
            return Value.CompareTo(other != null ? other.Value : default);
        }
    }
}
