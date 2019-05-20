// Filename: StatWeightingTest.cs
// Date Created: 2019-05-07
// 
// ================================================
// Copyright © 2019, eDoxa. All rights reserved.
// 
// This file is subject to the terms and conditions
// defined in file 'LICENSE.md', which is part of
// this source code package.

using eDoxa.Challenges.Domain.AggregateModels.MatchAggregate;
using eDoxa.Testing.MSTest;

using Microsoft.VisualStudio.TestTools.UnitTesting;

namespace eDoxa.Challenges.Domain.Entities.Tests.AggregateModels.MatchAggregate
{
    [TestClass]
    public sealed class StatWeightingTest
    {
        [TestMethod]
        public void Constructor_Tests()
        {
            const string message = "StatWeighting validation failed.";

            ConstructorTests<StatWeighting>.For(typeof(float))
                .WithName("StatWeighting")
                .Succeed(new object[] {byte.MinValue}, message)
                .Succeed(new object[] {byte.MaxValue}, message)
                .Succeed(new object[] {short.MinValue}, message)
                .Succeed(new object[] {short.MaxValue}, message)
                .Succeed(new object[] {int.MinValue}, message)
                .Succeed(new object[] {int.MaxValue}, message)
                .Succeed(new object[] {float.MinValue}, message)
                .Succeed(new object[] {float.MaxValue}, message)
                .Assert();
        }
    }
}