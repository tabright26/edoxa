// Filename: Right.cs
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
    public sealed class Right<TLeft, TRight> : Either<TLeft, TRight>
    {
        public Right(TRight value)
        {
            Value = value;
        }

        private TRight Value { get; }

        public override Either<TLeftMapped, TRight> MapLeft<TLeftMapped>(Func<TLeft, TLeftMapped> mapping)
        {
            return new Right<TLeftMapped, TRight>(Value);
        }

        public override Either<TLeft, TRightMapped> MapRight<TRightMapped>(Func<TRight, TRightMapped> mapping)
        {
            return new Right<TLeft, TRightMapped>(mapping(Value));
        }

        public override TLeft Reduce(Func<TRight, TLeft> mapping)
        {
            return mapping(Value);
        }
    }
}