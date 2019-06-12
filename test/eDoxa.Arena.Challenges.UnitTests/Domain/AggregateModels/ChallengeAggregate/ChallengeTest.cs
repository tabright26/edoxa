// Filename: ChallengeTest.cs
// Date Created: 2019-06-01
// 
// ================================================
// Copyright © 2019, eDoxa. All rights reserved.
// 
// This file is subject to the terms and conditions
// defined in file 'LICENSE.md', which is part of
// this source code package.

using System;
using System.Linq;

using eDoxa.Arena.Challenges.Domain.AggregateModels.ChallengeAggregate;
using eDoxa.Arena.Challenges.Domain.Fakers;
using eDoxa.Seedwork.Common.ValueObjects;

using FluentAssertions;

using Microsoft.VisualStudio.TestTools.UnitTesting;

namespace eDoxa.Arena.Challenges.UnitTests.Domain.AggregateModels.ChallengeAggregate
{
    [TestClass]
    public sealed class ChallengeTest
    {
        private ChallengeFaker _challengeFaker;

        [TestInitialize]
        public void TestInitialize()
        {
            _challengeFaker = new ChallengeFaker();
        }

        [TestMethod]
        public void RegisterParticipant_IntoEmptyCollection_ShouldNotBeEmpty()
        {
            // Arrange
            var challenge = _challengeFaker.FakeChallenge(state: ChallengeState.Inscription);

            var participantCount = challenge.Participants.Count;

            // Act
            challenge.RegisterParticipant(new UserId(), new UserGameReference(Guid.NewGuid().ToString()));

            // Assert
            challenge.Participants.Should().HaveCount(participantCount + 1);
        }

        [TestMethod]
        public void RegisterParticipant_WhoAlreadyExist_ShouldThrowInvalidOperationException()
        {
            // Arrange
            var challenge = _challengeFaker.FakeChallenge(state: ChallengeState.Inscription);

            // Act
            var action = new Action(() => challenge.RegisterParticipant(challenge.Participants.First().UserId, new UserGameReference(Guid.NewGuid().ToString())));

            // Act => Assert
            action.Should().Throw<InvalidOperationException>();
        }

        [TestMethod]
        public void RegisterParticipant_WithEntriesFull_ShouldThrowInvalidOperationException()
        {
            // Arrange
            var challenge = _challengeFaker.FakeChallenge(state: ChallengeState.Inscription);

            var participantCount = challenge.Setup.Entries - challenge.Participants.Count;

            for (var index = 0; index < participantCount; index++)
            {
                challenge.RegisterParticipant(new UserId(), new UserGameReference(Guid.NewGuid().ToString()));
            }

            // Act
            var action = new Action(() => challenge.RegisterParticipant(new UserId(), new UserGameReference(Guid.NewGuid().ToString())));

            // Assert
            action.Should().Throw<InvalidOperationException>();
        }
    }
}
