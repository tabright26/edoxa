﻿// Filename: ChallengeFakerTest.cs
// Date Created: 2019-06-10
// 
// ================================================
// Copyright © 2019, eDoxa. All rights reserved.
// 
// This file is subject to the terms and conditions
// defined in file 'LICENSE.md', which is part of
// this source code package.

using System.Linq;

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
            var challenge = challengeFaker.FakeChallenge(Game.LeagueOfLegends);

            // Assert
            var participants = challenge.Participants.ToList();

            participants.Should().HaveCount(participants.Distinct().Count());

            var matches = challenge.Participants.SelectMany(participant => participant.Matches).ToList();

            matches.Should().HaveCount(matches.Distinct().Count());

            challenge.DumbAsJson(true);
        }

        [TestMethod]
        public void FakeChallenge_ShouldNotThrow()
        {
            // Arrange
            var challengeFaker = new ChallengeFaker();

            // Act
            var challenges = challengeFaker.FakeChallenges(5);

            // Assert
            var matchReferences = challenges.SelectMany(
                    challenge => challenge.Participants.SelectMany(participant => participant.Matches.Select(match => match.Reference))
                )
                .ToList();

            matchReferences.Should().HaveCount(matchReferences.Distinct().Count());
        }
    }
}
