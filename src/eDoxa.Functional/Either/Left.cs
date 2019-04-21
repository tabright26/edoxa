// Filename: Left.cs
// Date Created: 2019-04-21
// 
// ================================================
// Copyright © 2019, eDoxa. All rights reserved.
//  
// This file is subject to the terms and conditions
// defined in file 'LICENSE.md', which is part of
// this source code package.

using System;

namespace eDoxa.Functional.Either
{
    public sealed class Left<TLeft, TRight> : Either<TLeft, TRight>
    {
        public Left(TLeft value)
        {
            Value = value;
        }

        private TLeft Value { get; }

        public override Either<TLeftMapped, TRight> MapLeft<TLeftMapped>(Func<TLeft, TLeftMapped> mapping)
        {
            return new Left<TLeftMapped, TRight>(mapping(Value));
        }

        public override Either<TLeft, TRightMapped> MapRight<TRightMapped>(Func<TRight, TRightMapped> mapping)
        {
            return new Left<TLeft, TRightMapped>(Value);
        }

        public override TLeft Reduce(Func<TRight, TLeft> mapping)
        {
            return Value;
        }
    }
}