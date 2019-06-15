// Filename: MissingConstructorTester.cs
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

namespace eDoxa.Seedwork.Testing.TestConstructor.Testers
{
    public sealed class MissingConstructorTester<T> : Tester<T>
    {
        public override Tester<T> WithClassName(string className)
        {
            return this;
        }

        public override Tester<T> WithClassAttributes(params Type[] classAttributeTypes)
        {
            return this;
        }

        public override Tester<T> Failure(object[] parameters, Type exceptionType, string failureMessage)
        {
            return this;
        }

        public override Tester<T> Success(object[] parameters, string failureMessage)
        {
            return this;
        }

        public override void Assert()
        {
            Microsoft.VisualStudio.TestTools.UnitTesting.Assert.Fail("Missing constructor.");
        }
    }
}
