// Filename: ParticipantFakerTest.cs
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

using FluentAssertions;

using Microsoft.VisualStudio.TestTools.UnitTesting;

namespace eDoxa.Arena.Challenges.UnitTests.Application.Data.Fakers
{
    [TestClass]
    public sealed class ParticipantFakerTest
    {
        [TestMethod]
        public void FakeParticipant_ShouldNotThrow()
        {
            // Arrange
            var challengeFaker = new ChallengeFaker();

            var participantFaker = new ParticipantFaker(challengeFaker.FakeChallenge());

            // Act
            var action = new Action(() => participantFaker.FakeParticipant(Game.LeagueOfLegends));

            // Assert
            action.Should().NotThrow();
        }
    }
}
