// Filename: MissingConstructorTester.cs
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
    public class MissingConstructorTester<T> : Tester<T>
    {
        public override Tester<T> Fail(object[] args, Type exceptionType, string failMessage)
        {
            return this;
        }

        public override Tester<T> Succeed(object[] args, string failMessage)
        {
            return this;
        }

        public override void Assert()
        {
            Microsoft.VisualStudio.TestTools.UnitTesting.Assert.Fail("Missing constructor.");
        }
    }
}