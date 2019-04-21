// Filename: Either.cs
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
    public abstract class Either<TLeft, TRight>
    {
        public abstract Either<TLeftMapped, TRight> MapLeft<TLeftMapped>(Func<TLeft, TLeftMapped> mapping);

        public abstract Either<TLeft, TRightMapped> MapRight<TRightMapped>(Func<TRight, TRightMapped> mapping);

        public abstract TLeft Reduce(Func<TRight, TLeft> mapping);
    }
}