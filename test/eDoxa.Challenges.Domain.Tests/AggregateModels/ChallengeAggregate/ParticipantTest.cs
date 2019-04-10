// Filename: ParticipantTest.cs
// Date Created: 2019-03-20
// 
// ============================================================
// Copyright © 2019, Francis Quenneville
// All rights reserved.
// 
// This file is subject to the terms and conditions defined in file 'LICENSE.md', which is part of
// this source code package.

using System;

using eDoxa.Challenges.Domain.AggregateModels;
using eDoxa.Challenges.Domain.Factories;

using FluentAssertions;

using Microsoft.VisualStudio.TestTools.UnitTesting;

namespace eDoxa.Challenges.Domain.Tests.AggregateModels.ChallengeAggregate
{
    [TestClass]
    public sealed class ParticipantTest
    {
        private static readonly ChallengeAggregateFactory _factory = ChallengeAggregateFactory.Instance;

        [TestMethod]
        public void Participant_ShouldNotBeNull()
        {
            // Act
            var participant = _factory.CreateParticipant();

            // Assert
            participant.Should().NotBeNull();
        }

        [TestMethod]
        public void Constructor_Challenge_ShouldThrowArgumentNullException()
        {
            // Arrange
            var userId = new UserId();
            var linkedAccount = _factory.CreateLinkedAccount();

            // Act
            var action = new Action(() => _factory.CreateParticipant(null, userId, linkedAccount));

            // Assert
            action.Should().Throw<ArgumentNullException>();
        }

        [TestMethod]
        public void Constructor_UserId_ShouldThrowArgumentNullException()
        {
            // Arrange
            var challenge = _factory.CreateChallenge();
            var linkedAccount = _factory.CreateLinkedAccount();

            // Act
            var action = new Action(() => _factory.CreateParticipant(challenge, null, linkedAccount));

            // Assert
            action.Should().Throw<ArgumentNullException>();
        }

        [TestMethod]
        public void Constructor_LinkedAccount_ShouldThrowArgumentNullException()
        {
            // Arrange
            var challenge = _factory.CreateChallenge();
            var userId = new UserId();

            // Act
            var action = new Action(() => _factory.CreateParticipant(challenge, userId, null));

            // Assert
            action.Should().Throw<ArgumentNullException>();
        }

        [TestMethod]
        public void SnapshotMatch_Matches_ShouldNotBeEmpty()
        {
            // Arrange
            var participant = _factory.CreateParticipant();
            var stats = _factory.CreateChallengeStats();
            var scoring = _factory.CreateChallengeScoring();

            // Act
            participant.SnapshotMatch(stats, scoring);

            // Assert
            participant.Matches.Should().NotBeEmpty();
        }

        [DataRow(1)]
        [DataRow(3)]
        [DataRow(5)]
        [DataTestMethod]
        public void Matches_ShouldHaveCountOf(int matchCount)
        {
            // Arrange
            var participant = _factory.CreateParticipantWithMatches(matchCount);

            // Act
            var matches = participant.Matches;

            // Assert
            matches.Should().HaveCount(matchCount);
        }

        [DataRow(1, 1)]
        [DataRow(3, 3)]
        [DataRow(5, 5)]
        [DataTestMethod]
        public void AverageScore_MatchCountGreaterThanOrEqualToBestOf_ShouldNotBeNull(int matchCount, int bestOf)
        {
            // Arrange
            var participant = _factory.CreateParticipantWithMatches(matchCount, bestOf);

            // Act
            var score = participant.AverageScore;

            // Assert
            score.Should().NotBeNull();
        }

        [DataRow(0, 1)]
        [DataRow(1, 2)]
        [DataRow(2, 3)]
        [DataRow(3, 4)]
        [DataRow(4, 5)]
        [DataTestMethod]
        public void AverageScore_MatchCountLowerThanBestOf_ShouldBeNull(int matchCount, int bestOf)
        {
            // Arrange
            var participant = _factory.CreateParticipantWithMatches(matchCount, bestOf);

            // Act
            var score = participant.AverageScore;

            // Assert
            score.Should().BeNull();
        }
    }
}