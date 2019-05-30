// Filename: ChallengeTest.cs
// Date Created: 2019-05-29
// 
// ================================================
// Copyright � 2019, eDoxa. All rights reserved.
// 
// This file is subject to the terms and conditions
// defined in file 'LICENSE.md', which is part of
// this source code package.

using System;
using System.Linq;

using eDoxa.Arena.Challenges.Tests.Utilities.Fakes;
using eDoxa.Arena.Domain;
using eDoxa.Seedwork.Domain.Common;

using FluentAssertions;

using Microsoft.VisualStudio.TestTools.UnitTesting;

namespace eDoxa.Arena.Challenges.Tests.Domain.AggregateModels.ChallengeAggregate
{
    [TestClass]
    public sealed class ChallengeTest
    {
        private static readonly FakeChallengeFactory FakeChallengeFactory = FakeChallengeFactory.Instance;

        [TestMethod]
        public void RegisterParticipant_IntoEmptyCollection_ShouldNotBeEmpty()
        {
            // Arrange
            var challenge = FakeChallengeFactory.CreateChallenge();

            // Act
            challenge.RegisterParticipant(new UserId(), new ExternalAccount(Guid.NewGuid()));

            // Assert
            challenge.Participants.Should().NotBeEmpty();
        }

        [TestMethod]
        public void RegisterParticipant_WhoAlreadyExist_ShouldThrowInvalidOperationException()
        {
            // Arrange
            var userId = new UserId();
            var challenge = FakeChallengeFactory.CreateChallengeWithParticipant(userId);

            // Act
            var action = new Action(() => challenge.RegisterParticipant(userId, new ExternalAccount(Guid.NewGuid())));

            // Act => Assert
            action.Should().Throw<InvalidOperationException>();
        }

        [TestMethod]
        public void RegisterParticipant_WithEntriesFull_ShouldThrowInvalidOperationException()
        {
            // Arrange
            var challenge = FakeChallengeFactory.CreateChallengeWithParticipants();

            // Act
            var action = new Action(() => challenge.RegisterParticipant(new UserId(), new ExternalAccount(Guid.NewGuid())));

            // Assert
            action.Should().Throw<InvalidOperationException>();
        }

        [TestMethod]
        public void SnapshotParticipantMatch_ParticipantRegistered_ShouldNotThrowArgumentException()
        {
            // Arrange
            var challenge = FakeChallengeFactory.CreateChallengeWithParticipant(new UserId());

            // Act
            var action = new Action(() => challenge.SnapshotParticipantMatch(challenge.Participants.First().Id, FakeChallengeFactory.CreateMatchStats()));

            // Assert
            action.Should().NotThrow<ArgumentException>();
        }
    }
}
