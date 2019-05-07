// Filename: PrizePoolRatioTest.cs
// Date Created: 2019-05-07
// 
// ================================================
// Copyright © 2019, eDoxa. All rights reserved.
// 
// This file is subject to the terms and conditions
// defined in file 'LICENSE.md', which is part of
// this source code package.

using System;

using eDoxa.Testing.MSTest;

using Microsoft.VisualStudio.TestTools.UnitTesting;

namespace eDoxa.Challenges.Domain.Entities.Tests
{
    [TestClass]
    public sealed class PrizePoolRatioTest
    {
        [TestMethod]
        public void Constructor_Tests()
        {
            const string message = "PrizePoolRatio validation failed.";

            ConstructorTests<PrizePoolRatio>.For(typeof(float))
                .WithName("PrizePoolRatio")
                .Fail(new object[] {PrizePoolRatio.Min - 0.1F}, typeof(ArgumentException), message)
                .Fail(new object[] {PrizePoolRatio.Max + 0.1F}, typeof(ArgumentException), message)
                .Succeed(new object[] {PrizePoolRatio.Min}, message)
                .Succeed(new object[] {PrizePoolRatio.Max}, message)
                .Assert();
        }
    }
}