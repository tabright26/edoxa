// Filename: Either.cs
// Date Created: 2019-05-13
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
    public class Either : Either<Failure, Success>
    {
        public Either(Failure left) : base(left)
        {
        }

        public Either(Success right) : base(right)
        {
        }

        public static implicit operator Either(Failure left)
        {
            return new Either(left);
        }

        public static implicit operator Either(Success right)
        {
            return new Either(right);
        }
    }

    public class Either<TRight> : Either<Failure, TRight>
    {
        public Either(Failure left) : base(left)
        {
        }

        public Either(TRight right) : base(right)
        {
        }

        public static implicit operator Either<TRight>(Failure left)
        {
            return new Either<TRight>(left);
        }

        public static implicit operator Either<TRight>(TRight right)
        {
            return new Either<TRight>(right);
        }
    }

    public class Either<TLeft, TRight>
    {
        private readonly bool _isLeft;
        private readonly TLeft _left;
        private readonly TRight _right;

        public Either(TLeft left)
        {
            _left = left;
            _isLeft = true;
        }

        public Either(TRight right)
        {
            _right = right;
            _isLeft = false;
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

            return _isLeft ? left(_left) : right(_right);
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

            if (!_isLeft)
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