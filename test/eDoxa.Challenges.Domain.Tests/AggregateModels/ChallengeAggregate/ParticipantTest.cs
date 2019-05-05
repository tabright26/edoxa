// Filename: ParticipantTest.cs
// Date Created: 2019-05-03
// 
// ================================================
// Copyright © 2019, eDoxa. All rights reserved.
// 
// This file is subject to the terms and conditions
// defined in file 'LICENSE.md', which is part of
// this source code package.

using eDoxa.Challenges.Domain.Factories;

using FluentAssertions;

using Microsoft.VisualStudio.TestTools.UnitTesting;

namespace eDoxa.Challenges.Domain.Tests.AggregateModels.ChallengeAggregate
{
    [TestClass]
    public sealed class ParticipantTest
    {
        private readonly ChallengeAggregateFactory _challengeAggregateFactory = ChallengeAggregateFactory.Instance;

        [TestMethod]
        public void Participant_ShouldNotBeNull()
        {
            // Act
            var participant = _challengeAggregateFactory.CreateParticipant();

            // Assert
            participant.Should().NotBeNull();
        }

        [TestMethod]
        public void SnapshotMatch_Matches_ShouldNotBeEmpty()
        {
            // Arrange
            var participant = _challengeAggregateFactory.CreateParticipant();
            var stats = _challengeAggregateFactory.CreateMatchStats();
            var scoring = _challengeAggregateFactory.CreateScoring();

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
            var participant = _challengeAggregateFactory.CreateParticipantWithMatches(matchCount);

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
            var participant = _challengeAggregateFactory.CreateParticipantWithMatches(matchCount, bestOf);

            // Act
            var score = participant.AverageScore;

            // Assert
            score.Should().NotBeNull();
        }
    }
}