// Filename: StatValueTest.cs
// Date Created: 2019-05-29
// 
// ================================================
// Copyright © 2019, eDoxa. All rights reserved.
// 
// This file is subject to the terms and conditions
// defined in file 'LICENSE.md', which is part of
// this source code package.

using System;

using eDoxa.Arena.Challenges.Domain.AggregateModels.MatchAggregate;
using eDoxa.Testing.MSTest.Constructor;

using Microsoft.VisualStudio.TestTools.UnitTesting;

namespace eDoxa.Arena.Challenges.Tests.Domain.AggregateModels.MatchAggregate
{
    [TestClass]
    public sealed class StatValueTest
    {
        [TestMethod]
        public void Constructor_Tests()
        {
            const string message = "StatValue validation failed.";

            ConstructorTests<StatValue>.For(typeof(object))
                .WithName("StatValue")
                .Fail(new object[] {"Ten"}, typeof(FormatException), message)
                .Succeed(new object[] {true}, message)
                .Succeed(new object[] {false}, message)
                .Succeed(new object[] {byte.MinValue}, message)
                .Succeed(new object[] {byte.MaxValue}, message)
                .Succeed(new object[] {short.MinValue}, message)
                .Succeed(new object[] {short.MaxValue}, message)
                .Succeed(new object[] {int.MinValue}, message)
                .Succeed(new object[] {int.MaxValue}, message)
                .Succeed(new object[] {long.MinValue}, message)
                .Succeed(new object[] {long.MaxValue}, message)
                .Succeed(new object[] {float.MinValue}, message)
                .Succeed(new object[] {float.MaxValue}, message)
                .Succeed(new object[] {double.MinValue}, message)
                .Succeed(new object[] {double.MaxValue}, message)
                .Succeed(new object[] {decimal.MinValue}, message)
                .Succeed(new object[] {decimal.MaxValue}, message)
                .Assert();
        }
    }
}
