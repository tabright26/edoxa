// Filename: ChallengeNameTest.cs
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
    public sealed class ChallengeNameTest
    {
        [TestMethod]
        public void Constructor_Tests()
        {
            const string message = "ChallengeName validation failed.";

            TestConstructor<ChallengeName>.ForParameters(typeof(string))
                .WithClassName("ChallengeName")
                .Failure(new object[] {null}, typeof(ArgumentException), message)
                .Failure(new object[] {"  "}, typeof(ArgumentException), message)
                .Failure(new object[] {"challenge_name"}, typeof(ArgumentException), message)
                .Failure(new object[] {"!@#$!@*&"}, typeof(ArgumentException), message)
                .Success(new object[] {"Challenge"}, message)
                .Success(new object[] {"Challenge 1"}, message)
                .Success(new object[] {"Challenge (1)"}, message)
                .Success(new object[] {"Challenge one"}, message)
                .Assert();
        }
    }
}
