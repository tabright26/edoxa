// Filename: Tester.cs
// Date Created: 2019-04-20
// 
// ================================================
// Copyright © 2019, eDoxa. All rights reserved.
//  
// This file is subject to the terms and conditions
// defined in file 'LICENSE.md', which is part of
// this source code package.

using System;

namespace eDoxa.Testing.MSTest
{
    public abstract class Tester<T>
    {
        public abstract Tester<T> Fail(object[] args, Type exceptionType, string failMessage);

        public abstract Tester<T> Succeed(object[] args, string failMessage);

        public abstract void Assert();
    }
}