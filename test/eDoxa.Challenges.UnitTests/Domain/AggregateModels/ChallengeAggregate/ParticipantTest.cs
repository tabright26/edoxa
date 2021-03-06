// Filename: ParticipantTest.cs
// Date Created: 2019-11-25
// 
// ================================================
// Copyright � 2020, eDoxa. All rights reserved.

using System;
using System.Collections.Generic;
using System.Linq;

using Bogus;

using eDoxa.Challenges.Api.Infrastructure.Data.Fakers.Extensions;
using eDoxa.Challenges.Domain.AggregateModels;
using eDoxa.Challenges.Domain.AggregateModels.ChallengeAggregate;
using eDoxa.Challenges.TestHelper;
using eDoxa.Challenges.TestHelper.Fixtures;
using eDoxa.Seedwork.Domain;
using eDoxa.Seedwork.Domain.Misc;

using FluentAssertions;

using Xunit;

namespace eDoxa.Challenges.UnitTests.Domain.AggregateModels.ChallengeAggregate
{
    public sealed class ParticipantTest : UnitTest
    {
        public ParticipantTest(TestDataFixture testData, TestMapperFixture testMapper, TestValidator validator) : base(testData, testMapper, validator)
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
            var gameReference = Faker.Game().Uuid();
            var stats = Faker.Game().Stats();

            var match = new Match(
                gameReference,
                new UtcNowDateTimeProvider(),
                TimeSpan.FromSeconds(3600),
                challenge.Scoring.Map(stats),
                new UtcNowDateTimeProvider());

            // Act
            participant.Snapshot(
                new List<IMatch>
                {
                    match
                },
                new UtcNowDateTimeProvider());

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
