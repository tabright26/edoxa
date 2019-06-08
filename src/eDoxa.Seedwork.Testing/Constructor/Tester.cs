// Filename: Tester.cs
// Date Created: 2019-05-20
// 
// ================================================
// Copyright © 2019, eDoxa. All rights reserved.
// 
// This file is subject to the terms and conditions
// defined in file 'LICENSE.md', which is part of
// this source code package.

using System;

namespace eDoxa.Seedwork.Testing.Constructor
{
    public abstract class Tester<T>
    {
        public abstract Tester<T> WithName(string name);

        public abstract Tester<T> WithAttributes(params Type[] attributeTypes);

        public abstract Tester<T> Fail(object[] args, Type exceptionType, string failMessage);

        public abstract Tester<T> Succeed(object[] args, string failMessage);

        public abstract void Assert();
    }
}
