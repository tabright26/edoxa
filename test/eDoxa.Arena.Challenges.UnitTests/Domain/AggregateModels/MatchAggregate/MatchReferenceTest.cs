// Filename: MatchReferenceTest.cs
// Date Created: 2019-06-01
// 
// ================================================
// Copyright © 2019, eDoxa. All rights reserved.
// 
// This file is subject to the terms and conditions
// defined in file 'LICENSE.md', which is part of
// this source code package.

using System;

using eDoxa.Arena.Challenges.Domain.AggregateModels.MatchAggregate;
using eDoxa.Seedwork.Testing.TestConstructor;

using Microsoft.VisualStudio.TestTools.UnitTesting;

namespace eDoxa.Arena.Challenges.UnitTests.Domain.AggregateModels.MatchAggregate
{
    [TestClass]
    public sealed class MatchReferenceTest
    {
        [TestMethod]
        public void Constructor_Tests()
        {
            const string message = "Match reference validation failed.";

            TestConstructor<MatchReference>.ForParameters(typeof(string))
                .WithClassName("MatchReference")
                .Failure(new object[] {null}, typeof(ArgumentException), message)
                .Failure(new object[] {"  "}, typeof(ArgumentException), message)
                .Failure(new object[] {"9i8h7g 6f5e4d3 c2b1a0"}, typeof(ArgumentException), message)
                .Failure(new object[] {"!@#$%^&*()"}, typeof(ArgumentException), message)
                .Success(new object[] {"9876543210"}, message)
                .Success(new object[] {"abcdefghijklmnopqrstuvwxyz"}, message)
                .Success(new object[] {"ABCDEFGHIJKLMNOPQRSTUVWXYZ"}, message)
                .Success(new object[] {"9i8h7g6f5e4d3c2b1a0"}, message)
                .Success(new object[] {"9i8h7g-6f5e4d3_c2b1a0"}, message)
                .Assert();
        }

        [TestMethod]
        public void Constructor_TypeOfGuid_Tests()
        {
            const string message = "Match reference validation failed.";

            TestConstructor<MatchReference>.ForParameters(typeof(Guid))
                .WithClassName("MatchReference")
                .Failure(new object[] {Guid.Empty}, typeof(ArgumentException), message)
                .Success(new object[] {Guid.NewGuid()}, message)
                .Assert();
        }
    }
}
