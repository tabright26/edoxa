// Filename: BestOfRangeTest.cs
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
    public sealed class BestOfRangeTest
    {
        [TestMethod]
        public void Constructor_Tests()
        {
            const string message = "BestOfRange validation failed.";

            ConstructorTests<BestOfRange>.For(typeof(BestOf), typeof(BestOf))
                .WithName("BestOfRange")
                .Succeed(new object[] {new BestOf(BestOf.Min), new BestOf(BestOf.Max)}, message)
                .Fail(new object[] {new BestOf(BestOf.Max), new BestOf(BestOf.Min)}, typeof(ArgumentException), message)
                .Assert();
        }
    }
}