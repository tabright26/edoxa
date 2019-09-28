// Filename: ChallengeFakerTest.cs
// Date Created: 2019-09-16
// 
// ================================================
// Copyright © 2019, eDoxa. All rights reserved.

using System.Collections.Generic;
using System.Linq;

using Bogus;

using eDoxa.Arena.Challenges.Domain.AggregateModels.ChallengeAggregate;
using eDoxa.Arena.Challenges.UnitTests.Helpers;
using eDoxa.Arena.Challenges.UnitTests.Helpers.Assertions.Extensions;

using FluentAssertions;

using Xunit;

namespace eDoxa.Arena.Challenges.UnitTests.Infrastructure.Data.Fakers
{
    public class ChallengeFakerTest : UnitTest
    {
        private static readonly Faker Faker = new Faker();

        public ChallengeFakerTest(ChallengeFakerFixture challengeFaker) : base(challengeFaker)
        {
        }

        public static IEnumerable<object[]> ChallengeFakerDataSets =>
            ChallengeGame.GetEnumerations()
                .SelectMany(game => ChallengeState.GetEnumerations().Select(state => new object[] {game, state, Faker.Random.Int()}));

        [Theory]
        [MemberData(nameof(ChallengeFakerDataSets))]
        public void Generate_ChallengesWithAnyStateGeneratedByAnySeed_ShouldBeValid(ChallengeGame game, ChallengeState state, int seed)
        {
            // Arrange
            var challengeFaker = ChallengeFaker.Factory.CreateFaker(seed, game, state);

            // Act
            var challenges = challengeFaker.FakeChallenges(20);

            // Assert
            challenges.Should().BeValid();
        }

        [Theory]
        [InlineData(1)]
        [InlineData(10)]
        [InlineData(100)]
        [InlineData(1000)]
        [InlineData(10000)]
        public void GenerateTwo_FromDifferentFakerWithSameSeed_ShouldBothBeValid(int seed)
        {
            // Arrange
            var challengeFaker1 = ChallengeFaker.Factory.CreateFaker(seed);
            var challengeFaker2 = ChallengeFaker.Factory.CreateFaker(seed);

            // Act
            var challenge1 = challengeFaker1.FakeChallenge();
            var challenge2 = challengeFaker2.FakeChallenge();

            // Assert
            challenge1.Should().BeValid();
            challenge2.Should().BeValid();
            challenge1.Should().Be(challenge2);
        }

        [Theory]
        [InlineData(1)]
        [InlineData(10)]
        [InlineData(100)]
        [InlineData(1000)]
        [InlineData(10000)]
        public void GenerateTwo_FromSameFakerWithDifferentSeeds_ShouldBothNotBeValid(int seed)
        {
            // Arrange
            var challengeFaker = ChallengeFaker.Factory.CreateFaker(seed);

            // Act
            var challenge1 = challengeFaker.FakeChallenge();
            var challenge2 = challengeFaker.FakeChallenge();

            // Assert
            challenge1.Should().BeValid();
            challenge2.Should().BeValid();
            challenge1.Should().NotBe(challenge2);
        }

        [Theory]
        [InlineData(1)]
        [InlineData(10)]
        [InlineData(100)]
        [InlineData(1000)]
        [InlineData(10000)]
        public void Generate_DistinctParticipants_BeValid(int seed)
        {
            // Arrange
            var challengeFaker = ChallengeFaker.Factory.CreateFaker(seed);
            var challenges = challengeFaker.FakeChallenges(10);

            // Act
            var participantIds = challenges.SelectMany(challenge => challenge.Participants).Select(participant => participant.Id).ToList();

            // Assert
            challenges.Should().BeValid();
            participantIds.Distinct().Should().HaveCount(participantIds.Count);
        }

        [Theory]
        [InlineData(1)]
        [InlineData(10)]
        [InlineData(100)]
        [InlineData(1000)]
        [InlineData(10000)]
        public void Generate_DistinctUserIds_ShouldBeValid(int seed)
        {
            // Arrange
            var challengeFaker = ChallengeFaker.Factory.CreateFaker(seed);
            var challenges = challengeFaker.FakeChallenges(10);

            // Act
            var participantUserIds = challenges.SelectMany(challenge => challenge.Participants).Select(participant => participant.UserId).ToList();

            // Assert
            challenges.Should().BeValid();
            participantUserIds.Distinct().Should().NotHaveCount(participantUserIds.Count);
        }
    }
}
