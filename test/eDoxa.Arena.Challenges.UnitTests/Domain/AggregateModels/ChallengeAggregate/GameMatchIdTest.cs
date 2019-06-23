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

using eDoxa.Arena.Challenges.Domain.AggregateModels.ChallengeAggregate;
using eDoxa.Seedwork.Testing.TestConstructor;

using Microsoft.VisualStudio.TestTools.UnitTesting;

namespace eDoxa.Arena.Challenges.UnitTests.Domain.AggregateModels.ChallengeAggregate
{
    [TestClass]
    public sealed class GameMatchIdTest
    {
        [TestMethod]
        public void Constructor_Tests()
        {
            const string message = "Game match id validation failed.";

            TestConstructor<GameMatchId>.ForParameters(typeof(string))
                .WithClassName("GameMatchId")
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
            const string message = "Game match id validation failed.";

            TestConstructor<GameMatchId>.ForParameters(typeof(Guid))
                .WithClassName("GameMatchId")
                .Failure(new object[] {Guid.Empty}, typeof(ArgumentException), message)
                .Success(new object[] {Guid.NewGuid()}, message)
                .Assert();
        }
    }
}
