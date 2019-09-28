// Filename: ParticipantTest.cs
// Date Created: 2019-09-16
// 
// ================================================
// Copyright © 2019, eDoxa. All rights reserved.

using System.Collections.Generic;
using System.Linq;

using Bogus;

using eDoxa.Arena.Challenges.Api.Infrastructure.Data.Fakers.Extensions;
using eDoxa.Arena.Challenges.Domain.AggregateModels.ChallengeAggregate;
using eDoxa.Arena.Challenges.UnitTests.TestHelpers;
using eDoxa.Seedwork.Domain;

using FluentAssertions;

using Xunit;

namespace eDoxa.Arena.Challenges.UnitTests.Domain.AggregateModels.ChallengeAggregate
{
    public sealed class ParticipantTestClass : UnitTestClass
    {
        public ParticipantTestClass(TestDataFixture testData) : base(testData)
        {
        }

        private static readonly Faker Faker = new Faker();

        public static IEnumerable<object[]> GameDataSets => ChallengeGame.GetEnumerations().Select(game => new object[] {game});

        [Theory]
        [MemberData(nameof(GameDataSets))]
        public void SnapshotMatch_ParticipantMatches_ShouldNotBeEmpty(ChallengeGame game)
        {
            // Arrange
            var challengeFaker = TestData.FakerFactory.CreateChallengeFaker(null, game);
            var challenge = challengeFaker.FakeChallenge();
            var participant = challenge.Participants.First();
            var gameReference = Faker.Game().Reference(game);
            var stats = Faker.Game().Stats(game);

            var match = new StatMatch(
                challenge.Scoring,
                stats,
                gameReference,
                new UtcNowDateTimeProvider());

            // Act
            participant.Snapshot(match);

            // Assert
            participant.Matches.Should().NotBeEmpty();
        }

        [Fact]
        public void Matches_ShouldHaveCountOf()
        {
            // Arrange
            var challengeFaker = TestData.FakerFactory.CreateChallengeFaker(null, ChallengeGame.LeagueOfLegends, ChallengeState.InProgress);
            var challenge = challengeFaker.FakeChallenge();

            // Act
            var matches = challenge.Participants.First().Matches;

            // Assert
            matches.Should().NotBeEmpty();
        }
    }
}
