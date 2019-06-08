// Filename: TestConstructor.cs
// Date Created: 2019-06-08
// 
// ================================================
// Copyright © 2019, eDoxa. All rights reserved.
// 
// This file is subject to the terms and conditions
// defined in file 'LICENSE.md', which is part of
// this source code package.

using System;

using eDoxa.Seedwork.Testing.TestConstructor.Abstractions;
using eDoxa.Seedwork.Testing.TestConstructor.Testers;

namespace eDoxa.Seedwork.Testing.TestConstructor
{
    public static class TestConstructor<T>
    {
        public static Tester<T> ForParameters(params Type[] types)
        {
            var constructorInfo = typeof(T).GetConstructor(types);

            return constructorInfo == null ? (Tester<T>) new MissingConstructorTester<T>() : new ConstructorTester<T>(constructorInfo);
        }
    }
}
