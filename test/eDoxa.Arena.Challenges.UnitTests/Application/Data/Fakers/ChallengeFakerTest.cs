// Filename: ChallengeFakerTest.cs
// Date Created: 2019-06-10
// 
// ================================================
// Copyright © 2019, eDoxa. All rights reserved.
// 
// This file is subject to the terms and conditions
// defined in file 'LICENSE.md', which is part of
// this source code package.

using System;

using eDoxa.Arena.Challenges.Domain.Fakers;
using eDoxa.Seedwork.Common.Enumerations;
using eDoxa.Seedwork.Common.Extensions;

using FluentAssertions;

using Microsoft.VisualStudio.TestTools.UnitTesting;

namespace eDoxa.Arena.Challenges.UnitTests.Application.Data.Fakers
{
    [TestClass]
    public sealed class ChallengeFakerTest
    {
        [TestMethod]
        public void FakeChallenges_ShouldNotThrow()
        {
            // Arrange
            var challengeFaker = new ChallengeFaker();

            // Act
            var action = new Action(() => challengeFaker.FakeChallenge(Game.LeagueOfLegends));

            // Assert
            action.Should().NotThrow();
        }
        
        [TestMethod]
        public void FakeChallenge_ShouldNotThrow()
        {
            // Arrange
            var challengeFaker = new ChallengeFaker();

            // Act
            var action = new Action(
                () =>
                {
                    var challenge = challengeFaker.FakeChallenge();

                    Console.WriteLine(challenge.DumbAsJson());
                }
            );

            // Assert
            action.Should().NotThrow();
        }
    }
}
