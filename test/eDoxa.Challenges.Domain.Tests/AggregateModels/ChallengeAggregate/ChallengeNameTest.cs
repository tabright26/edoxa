// Filename: ChallengeNameTest.cs
// Date Created: 2019-05-06
// 
// ================================================
// Copyright © 2019, eDoxa. All rights reserved.
// 
// This file is subject to the terms and conditions
// defined in file 'LICENSE.md', which is part of
// this source code package.

using System;

using eDoxa.Challenges.Domain.AggregateModels.ChallengeAggregate;
using eDoxa.Testing.MSTest;

using Microsoft.VisualStudio.TestTools.UnitTesting;

namespace eDoxa.Challenges.Domain.Tests.AggregateModels.ChallengeAggregate
{
    [TestClass]
    public sealed class ChallengeNameTest
    {
        [TestMethod]
        public void Constructor_Tests()
        {
            const string message = "ChallengeName validation failed.";

            ConstructorTests<ChallengeName>.For(typeof(string))
                .WithName("ChallengeName")
                .Fail(new object[] {null}, typeof(ArgumentException), message)
                .Fail(new object[] {"  "}, typeof(ArgumentException), message)
                .Fail(new object[] {"challenge_name"}, typeof(ArgumentException), message)
                .Fail(new object[] {"!@#$!@*&"}, typeof(ArgumentException), message)
                .Succeed(new object[] {"Challenge"}, message)
                .Succeed(new object[] {"Challenge 1"}, message)
                .Succeed(new object[] {"Challenge (1)"}, message)
                .Succeed(new object[] {"Challenge one"}, message)
                .Assert();
        }
    }
}