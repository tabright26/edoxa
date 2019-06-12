// Filename: ChallengeFakerTest.cs
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
using eDoxa.Seedwork.Common.Extensions;

using FluentAssertions;

using Microsoft.VisualStudio.TestTools.UnitTesting;

namespace eDoxa.Arena.Challenges.UnitTests.Application.Data.Fakers
{
    [TestClass]
    public sealed class ChallengeFakerTest
    {
        [TestMethod]
        public void FakeChallenges_ShouldNotThrow1()
        {
            // Arrange
            var challengeFaker1 = new ChallengeFaker();

            var challengeFaker2 = new ChallengeFaker();

            // Act
            var challenge1 = challengeFaker1.Generate();
            
            var challenge2 = challengeFaker2.Generate();

            // Assert
            challenge1.Should().Be(challenge2);
        }

        [TestMethod]
        public void FakeChallenges_ShouldNotThrow2()
        {
            // Arrange
            var challengeFaker = new ChallengeFaker();

            // Act
            var challenge1 = challengeFaker.FakeChallenge();

            var challenge2 = challengeFaker.FakeChallenge();

            // Assert
            challenge1.Should().NotBe(challenge2);
        }

        [TestMethod]
        public void FakeChallenges_ShouldNotThrow3()
        {
            // Arrange
            var challengeFaker = new ChallengeFaker();

            // Act
            var challenge1 = challengeFaker.FakeChallenge();

            challengeFaker.ParticipantFaker = new ParticipantFaker();

            var challenge2 = challengeFaker.FakeChallenge();

            challengeFaker.ParticipantFaker = new ParticipantFaker();

            var challenge3 = challengeFaker.FakeChallenge();

            var participants1 = challenge1.Participants.OrderBy(x => x.Id).ToList();

            var participants2 = challenge2.Participants.OrderBy(x => x.Id).ToList();

            var participants3 = challenge3.Participants.OrderBy(x => x.Id).ToList();

            var r = participants1.Union(participants2).Union(participants3).Distinct().ToList();

            // Assert
            r.Should().HaveCount(200);
        }

        [TestMethod]
        public void FakeChallenges_ShouldNotThrow4()
        {
            // Arrange
            var challengeFaker = new ChallengeFaker();

            // Act
            var challenge1 = challengeFaker.FakeChallenge();

            var challenge2 = challengeFaker.FakeChallenge();

            var challenge3 = challengeFaker.FakeChallenge();

            var participants1 = challenge1.Participants.OrderBy(x => x.Id).ToList();

            var participants2 = challenge2.Participants.OrderBy(x => x.Id).ToList();

            var participants3 = challenge3.Participants.OrderBy(x => x.Id).ToList();

            var r = participants1.Union(participants2).Union(participants3).Distinct().ToList();

            // Assert
            r.Should().HaveCount(270);
        }

        [TestMethod]
        public void FakeChallenges_ShouldNotThrow()
        {
            // Arrange
            var challengeFaker = new ChallengeFaker();

            // Act
            var challenge = challengeFaker.FakeChallenge();

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
