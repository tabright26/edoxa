// Filename: Tester.cs
// Date Created: 2019-06-08
// 
// ================================================
// Copyright © 2019, eDoxa. All rights reserved.
// 
// This file is subject to the terms and conditions
// defined in file 'LICENSE.md', which is part of
// this source code package.

using System;

namespace eDoxa.Seedwork.Testing.TestConstructor.Abstractions
{
    public abstract class Tester<T>
    {
        public abstract Tester<T> WithClassName(string className);

        public abstract Tester<T> WithClassAttributes(params Type[] classAttributeTypes);

        public abstract Tester<T> Failure(object[] parameters, Type exceptionType, string failureMessage);

        public abstract Tester<T> Success(object[] parameters, string failureMessage);

        public abstract void Assert();
    }
}
