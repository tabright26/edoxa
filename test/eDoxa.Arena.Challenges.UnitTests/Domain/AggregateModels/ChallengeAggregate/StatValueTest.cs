// Filename: StatValueTest.cs
// Date Created: 2019-06-25
// 
// ================================================
// Copyright © 2019, eDoxa. All rights reserved.
// 
// This file is subject to the terms and conditions
// defined in file 'LICENSE.md', which is part of
// this source code package.

using System;

using eDoxa.Arena.Challenges.Domain.AggregateModels.ChallengeAggregate;
using eDoxa.Seedwork.Testing.TestConstructor;

using Microsoft.VisualStudio.TestTools.UnitTesting;

namespace eDoxa.Arena.Challenges.UnitTests.Domain.AggregateModels.ChallengeAggregate
{
    [TestClass]
    public sealed class StatValueTest
    {
        [TestMethod]
        public void Constructor_Tests()
        {
            const string message = "StatValue validation failed.";

            TestConstructor<StatValue>.ForParameters(typeof(object))
                .WithClassName("StatValue")
                .Failure(new object[] {"Ten"}, typeof(FormatException), message)
                .Success(new object[] {true}, message)
                .Success(new object[] {false}, message)
                .Success(new object[] {byte.MinValue}, message)
                .Success(new object[] {byte.MaxValue}, message)
                .Success(new object[] {short.MinValue}, message)
                .Success(new object[] {short.MaxValue}, message)
                .Success(new object[] {int.MinValue}, message)
                .Success(new object[] {int.MaxValue}, message)
                .Success(new object[] {long.MinValue}, message)
                .Success(new object[] {long.MaxValue}, message)
                .Success(new object[] {float.MinValue}, message)
                .Success(new object[] {float.MaxValue}, message)
                .Success(new object[] {double.MinValue}, message)
                .Success(new object[] {double.MaxValue}, message)
                .Success(new object[] {decimal.MinValue}, message)
                .Success(new object[] {decimal.MaxValue}, message)
                .Assert();
        }
    }
}
