// Filename: None.cs
// Date Created: 2019-04-21
// 
// ================================================
// Copyright © 2019, eDoxa. All rights reserved.
//  
// This file is subject to the terms and conditions
// defined in file 'LICENSE.md', which is part of
// this source code package.

using System;

using JetBrains.Annotations;

namespace eDoxa.Functional.Option
{
    public sealed class None<T> : Option<T>, IEquatable<None<T>>, IEquatable<None>
    {
        public bool Equals([CanBeNull] None<T> other)
        {
            return true;
        }

        public bool Equals([CanBeNull] None other)
        {
            return true;
        }

        public override Option<TResult> Map<TResult>(Func<T, TResult> map)
        {
            return None.Value;
        }

        public override Option<TResult> MapOptional<TResult>(Func<T, Option<TResult>> map)
        {
            return None.Value;
        }
        
        [CanBeNull]
        public override T Reduce([CanBeNull] T whenNone = default)
        {
            return whenNone;
        }

        public override T Reduce(Func<T> whenNone)
        {
            return whenNone();
        }

        public override bool Equals([CanBeNull] object obj)
        {
            return !(obj is null) && (obj is None<T> || obj is None);
        }

        public override int GetHashCode()
        {
            return 0;
        }

        public static bool operator ==(None<T> left, None<T> right)
        {
            return left.Equals(right);
        }

        public static bool operator !=(None<T> left, None<T> right)
        {
            return !(left == right);
        }

        public override string ToString()
        {
            return nameof(None);
        }
    }

    public sealed class None : IEquatable<None>
    {
        private None()
        {
        }

        public static None Value { get; } = new None();

        public bool Equals([CanBeNull] None other)
        {
            return true;
        }

        public override string ToString()
        {
            return nameof(None);
        }

        public override int GetHashCode()
        {
            return 0;
        }

        public override bool Equals([CanBeNull] object obj)
        {
            return !(obj is null) && (obj is None || IsGenericNone(obj.GetType()));
        }

        private static bool IsGenericNone(Type type)
        {
            return type.GenericTypeArguments.Length == 1 &&
                   typeof(None<>).MakeGenericType(type.GenericTypeArguments[0]) == type;
        }
    }
}