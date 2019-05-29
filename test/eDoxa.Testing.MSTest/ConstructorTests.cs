// Filename: ConstructorTests.cs
// Date Created: 2019-05-20
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
    public static class ConstructorTests<T>
    {
        public static Tester<T> For(params Type[] types)
        {
            var info = typeof(T).GetConstructor(types);

            if (info == null)
            {
                return new MissingConstructorTester<T>();
            }

            return new ConstructorTester<T>(info);
        }
    }
}
