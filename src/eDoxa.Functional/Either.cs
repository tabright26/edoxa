// Filename: Either.cs
// Date Created: 2019-05-06
// 
// ================================================
// Copyright © 2019, eDoxa. All rights reserved.
// 
// This file is subject to the terms and conditions
// defined in file 'LICENSE.md', which is part of
// this source code package.

using System;

namespace eDoxa.Functional
{
    public class Either<TLeft, TRight>
    {
        private readonly bool isLeft;
        private readonly TLeft _left;
        private readonly TRight _right;

        public Either(TLeft left)
        {
            _left = left;
            isLeft = true;
        }

        public Either(TRight right)
        {
            _right = right;
            isLeft = false;
        }

        public T Match<T>(Func<TLeft, T> left, Func<TRight, T> right)
        {
            if (left == null)
            {
                throw new ArgumentNullException(nameof(left));
            }

            if (right == null)
            {
                throw new ArgumentNullException(nameof(right));
            }

            return isLeft ? left(_left) : right(_right);
        }

        /// <summary>
        ///     If right value is assigned, execute an action on it.
        /// </summary>
        /// <param name="right">Action to execute.</param>
        public void DoRight(Action<TRight> right)
        {
            if (right == null)
            {
                throw new ArgumentNullException(nameof(right));
            }

            if (!isLeft)
            {
                right(_right);
            }
        }

        public TLeft LeftOrDefault()
        {
            return this.Match(left => left, right => default);
        }

        public TRight RightOrDefault()
        {
            return this.Match(left => default, right => right);
        }

        public static implicit operator Either<TLeft, TRight>(TLeft left)
        {
            return new Either<TLeft, TRight>(left);
        }

        public static implicit operator Either<TLeft, TRight>(TRight right)
        {
            return new Either<TLeft, TRight>(right);
        }
    }
}