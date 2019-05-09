// Filename: BestOfTest.cs
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
    public sealed class BestOfTest
    {
        [TestMethod]
        public void Constructor_Tests()
        {
            const string message = "BestOf validation failed.";

            ConstructorTests<BestOf>.For(typeof(int), typeof(bool))
                .WithName("BestOf")
                .Fail(new object[] {BestOf.Min - 1, true}, typeof(ArgumentException), message)
                .Fail(new object[] {BestOf.Max + 1, true}, typeof(ArgumentException), message)
                .Succeed(new object[] {BestOf.Min, true}, message)
                .Succeed(new object[] {BestOf.Max, true}, message)
                .Assert();
        }
    }
}