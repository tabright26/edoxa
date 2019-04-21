// Filename: Some.cs
// Date Created: 2019-04-21
// 
// ================================================
// Copyright © 2019, eDoxa. All rights reserved.
//  
// This file is subject to the terms and conditions
// defined in file 'LICENSE.md', which is part of
// this source code package.

using System;
using System.Collections.Generic;

using JetBrains.Annotations;

namespace eDoxa.Functional.Option
{
    public sealed class Some<T> : Option<T>, IEquatable<Some<T>>
    {
        public Some(T value)
        {
            Content = value;
        }

        public T Content { get; }

        public bool Equals([CanBeNull] Some<T> other)
        {
            if (other is null)
            {
                return false;
            }

            return ReferenceEquals(this, other) || EqualityComparer<T>.Default.Equals(Content, other.Content);
        }

        public static implicit operator T(Some<T> some)
        {
            return some.Content;
        }

        public static implicit operator Some<T>(T value)
        {
            return new Some<T>(value);
        }

        public static bool operator ==(Some<T> left, Some<T> right)
        {
            return left.Equals(right);
        }

        public static bool operator !=(Some<T> left, Some<T> right)
        {
            return !(left == right);
        }

        public override Option<TResult> Map<TResult>(Func<T, TResult> map)
        {
            return map(Content);
        }

        public override Option<TResult> MapOptional<TResult>(Func<T, Option<TResult>> map)
        {
            return map(Content);
        }

        public override T Reduce(T whenNone)
        {
            return Content;
        }

        public override T Reduce(Func<T> whenNone)
        {
            return Content;
        }

        public override string ToString()
        {
            return $"Some({Content.ToString()})";
        }

        public override int GetHashCode()
        {
            return EqualityComparer<T>.Default.GetHashCode(Content);
        }

        public override bool Equals([CanBeNull] object obj)
        {
            if (obj is null)
            {
                return false;
            }

            if (ReferenceEquals(this, obj))
            {
                return true;
            }

            return obj is Some<T> some && this.Equals(some);
        }
    }
}