// Filename: ChallengeFakerTest.cs
// Date Created: 2019-09-29
// 
// ================================================
// Copyright © 2019, eDoxa. All rights reserved.

using System.Linq;

using Bogus;

using eDoxa.Challenges.Domain.AggregateModels.ChallengeAggregate;
using eDoxa.Challenges.TestHelper;
using eDoxa.Challenges.TestHelper.Assertions.Extensions;
using eDoxa.Challenges.TestHelper.Fixtures;
using eDoxa.Seedwork.Domain.Miscs;

using FluentAssertions;

using Xunit;

namespace eDoxa.Challenges.UnitTests.Infrastructure.Data.Fakers
{
    public class ChallengeFakerTest : UnitTest
    {
        private static readonly Faker Faker = new Faker();

        public ChallengeFakerTest(TestDataFixture testData, TestMapperFixture testMapper, TestValidator validator) : base(testData, testMapper, validator)
        {
        }

        
        public static TheoryData<Game, ChallengeState, int> ChallengeFakerDataSets
        {
            get
            {
                var data = new TheoryData<Game, ChallengeState, int>();

                foreach (var state in ChallengeState.GetEnumerations())
                {
                    data.Add(Game.LeagueOfLegends, state, Faker.Random.Int());
                }

                return data;
            }
        }

        [Theory]
        [MemberData(nameof(ChallengeFakerDataSets))]
        public void Generate_ChallengesWithAnyStateGeneratedByAnySeed_ShouldBeValid(Game game, ChallengeState state, int seed)
        {
            // Arrange
            var challengeFaker = TestData.FakerFactory.CreateChallengeFaker(seed, game, state);

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
            var challengeFaker1 = TestData.FakerFactory.CreateChallengeFaker(seed, Game.LeagueOfLegends);
            var challengeFaker2 = TestData.FakerFactory.CreateChallengeFaker(seed, Game.LeagueOfLegends);

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
            var challengeFaker = TestData.FakerFactory.CreateChallengeFaker(seed, Game.LeagueOfLegends);

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
            var challengeFaker = TestData.FakerFactory.CreateChallengeFaker(seed, Game.LeagueOfLegends);
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
            var challengeFaker = TestData.FakerFactory.CreateChallengeFaker(seed, Game.LeagueOfLegends);
            var challenges = challengeFaker.FakeChallenges(10);

            // Act
            var participantUserIds = challenges.SelectMany(challenge => challenge.Participants).Select(participant => participant.UserId).ToList();

            // Assert
            challenges.Should().BeValid();
            participantUserIds.Distinct().Should().NotHaveCount(participantUserIds.Count);
        }
    }
}
