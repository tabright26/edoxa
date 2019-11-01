// Filename: ParticipantTest.cs
// Date Created: 2019-09-29
// 
// ================================================
// Copyright © 2019, eDoxa. All rights reserved.

using System.Linq;

using Bogus;

using eDoxa.Arena.Challenges.Api.Infrastructure.Data.Fakers.Extensions;
using eDoxa.Arena.Challenges.Domain.AggregateModels.ChallengeAggregate;
using eDoxa.Arena.Challenges.TestHelper;
using eDoxa.Arena.Challenges.TestHelper.Fixtures;
using eDoxa.Seedwork.Domain;
using eDoxa.Seedwork.Domain.Miscs;

using FluentAssertions;

using Xunit;

namespace eDoxa.Arena.Challenges.UnitTests.Domain.AggregateModels.ChallengeAggregate
{
    public sealed class ParticipantTest : UnitTest
    {
        public ParticipantTest(TestDataFixture testData, TestMapperFixture testMapper) : base(testData, testMapper)
        {
        }

        private static readonly Faker Faker = new Faker();

        public static TheoryData<Game> GameDataSets =>
            new TheoryData<Game>
            {
                Game.LeagueOfLegends
            };

        [Theory]
        [MemberData(nameof(GameDataSets))]
        public void SnapshotMatch_ParticipantMatches_ShouldNotBeEmpty(Game game)
        {
            // Arrange
            var challengeFaker = TestData.FakerFactory.CreateChallengeFaker(null, game);
            var challenge = challengeFaker.FakeChallenge();
            var participant = challenge.Participants.First();
            var gameReference = Faker.Game().Reference();
            var stats = Faker.Game().Stats();

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
            var challengeFaker = TestData.FakerFactory.CreateChallengeFaker(null, Game.LeagueOfLegends, ChallengeState.InProgress);
            var challenge = challengeFaker.FakeChallenge();

            // Act
            var matches = challenge.Participants.First().Matches;

            // Assert
            matches.Should().NotBeEmpty();
        }
    }
}
