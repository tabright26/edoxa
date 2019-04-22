// Filename: Option.cs
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
    public abstract class Option<T>
    {
        public static implicit operator Option<T>(T value)
        {
            return new Some<T>(value);
        }

        public static implicit operator Option<T>(None none)
        {
            return new None<T>();
        }

        public abstract Option<TResult> Map<TResult>(Func<T, TResult> map);

        public abstract Option<TResult> MapOptional<TResult>(Func<T, Option<TResult>> map);

        [CanBeNull]
        public abstract T Reduce([CanBeNull] T whenNone);

        public abstract T Reduce(Func<T> whenNone);

        [CanBeNull]
        public T Default()
        {
            return default;
        }

        public Option<TCast> OfType<TCast>()
        where TCast : class
        {
            return this is Some<T> some && typeof(TCast).IsAssignableFrom(typeof(T))
                ? (Option<TCast>) new Some<TCast>(some.Content as TCast ?? throw new InvalidOperationException())
                : None.Value;
        }
    }
}