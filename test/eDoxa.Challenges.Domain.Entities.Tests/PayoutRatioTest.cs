// Filename: PayoutRatioTest.cs
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
    public sealed class PayoutRatioTest
    {
        [TestMethod]
        public void Constructor_Tests()
        {
            const string message = "PayoutRatio validation failed.";

            ConstructorTests<PayoutRatio>.For(typeof(float), typeof(bool))
                .WithName("PayoutRatio")
                .Fail(new object[] {PayoutRatio.Min - 0.1F, true}, typeof(ArgumentException), message)
                .Fail(new object[] {PayoutRatio.Max + 0.1F, true}, typeof(ArgumentException), message)
                .Succeed(new object[] {PayoutRatio.Min, true}, message)
                .Succeed(new object[] {PayoutRatio.Max, true}, message)
                .Assert();
        }
    }
}