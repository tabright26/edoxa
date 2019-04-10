// Filename: MatchTest.cs
// Date Created: 2019-03-20
// 
// ============================================================
// Copyright © 2019, Francis Quenneville
// All rights reserved.
// 
// This file is subject to the terms and conditions defined in file 'LICENSE.md', which is part of
// this source code package.

using System;

using eDoxa.Challenges.Domain.Factories;

using FluentAssertions;

using Microsoft.VisualStudio.TestTools.UnitTesting;

namespace eDoxa.Challenges.Domain.Tests.AggregateModels.ChallengeAggregate
{
    [TestClass]
    public sealed class MatchTest
    {
        private static readonly ChallengeAggregateFactory _factory = ChallengeAggregateFactory.Instance;

        [TestMethod]
        public void Match_ShouldNotBeNull()
        {
            // Act
            var match = _factory.CreateMatch();

            // Assert
            match.Should().NotBeNull();
        }

        [TestMethod]
        public void Constructor_Participant_ShouldThrowArgumentNullException()
        {
            // Arrange
            var linkedMatch = _factory.CreateLinkedMatch();

            // Act
            var action = new Action(() => _factory.CreateMatch(null, linkedMatch));

            // Assert
            action.Should().Throw<ArgumentNullException>();
        }

        [TestMethod]
        public void Constructor_LinkedMatch_ShouldThrowArgumentNullException()
        {
            // Arrange
            var participant = _factory.CreateParticipant();

            // Act
            var action = new Action(() => _factory.CreateMatch(participant, null));

            // Assert
            action.Should().Throw<ArgumentNullException>();
        }

        [TestMethod]
        public void SnapshotStats_Stats_ShouldHaveCountOfScoring()
        {
            // Arrange
            var scoring = _factory.CreateChallengeScoring();
            var stats = _factory.CreateChallengeStats();
            var match = _factory.CreateMatch();

            // Act
            match.SnapshotStats(stats, scoring);

            // Assert
            match.Stats.Should().HaveCount(scoring.Count);
        }

        [TestMethod]
        public void SnapshotStats_ChallengeStatsNullReference_ShouldThrowArgumentNullException()
        {
            // Arrange
            var scoring = _factory.CreateChallengeScoring();
            var match = _factory.CreateMatch();

            // Act
            var action = new Action(() => match.SnapshotStats(null, scoring));

            // Assert
            action.Should().Throw<ArgumentNullException>();
        }

        [TestMethod]
        public void SnapshotStats_ChallengeScoringNullReference_ShouldThrowArgumentNullException()
        {
            // Arrange
            var stats = _factory.CreateChallengeStats();
            var match = _factory.CreateMatch();

            // Act
            var action = new Action(() => match.SnapshotStats(stats, null));

            // Assert
            action.Should().Throw<ArgumentNullException>();
        }
    }
}