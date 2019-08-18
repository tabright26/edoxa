// Filename: ParticipantTest.cs
// Date Created: 2019-07-01
// 
// ================================================
// Copyright © 2019, eDoxa. All rights reserved.

using System.Collections.Generic;
using System.Linq;

using Bogus;

using eDoxa.Arena.Challenges.Api.Infrastructure.Data.Fakers;
using eDoxa.Arena.Challenges.Api.Infrastructure.Data.Fakers.Extensions;
using eDoxa.Arena.Challenges.Domain.AggregateModels.ChallengeAggregate;
using eDoxa.Seedwork.Domain;

using FluentAssertions;

using Microsoft.VisualStudio.TestTools.UnitTesting;

namespace eDoxa.Arena.Challenges.UnitTests.Domain.AggregateModels.ChallengeAggregate
{
    [TestClass]
    public sealed class ParticipantTest
    {
        private static readonly Faker Faker = new Faker();

        private static IEnumerable<object[]> GameDataSets => ChallengeGame.GetEnumerations().Select(game => new object[] {game});

        [DataTestMethod]
        [DynamicData(nameof(GameDataSets))]
        public void SnapshotMatch_ParticipantMatches_ShouldNotBeEmpty(ChallengeGame game)
        {
            // Arrange
            var challengeFaker = new ChallengeFaker(game);
            var challenge = challengeFaker.Generate();
            var participant = challenge.Participants.First();
            var gameReference = Faker.Game().Reference(game);
            var stats = Faker.Game().Stats(game);
            var match = new StatMatch(challenge.Scoring, stats, gameReference, new UtcNowDateTimeProvider());

            // Act
            participant.Snapshot(match);

            // Assert
            participant.Matches.Should().NotBeEmpty();
        }

        [TestMethod]
        public void Matches_ShouldHaveCountOf()
        {
            // Arrange
            var challengeFaker = new ChallengeFaker(ChallengeGame.LeagueOfLegends, ChallengeState.InProgress);
            var challenge = challengeFaker.Generate();

            // Act
            var matches = challenge.Participants.First().Matches;

            // Assert
            matches.Should().NotBeEmpty();
        }
    }
}
