// Filename: ChallengeFakerTest.cs
// Date Created: 2019-07-21
// 
// ================================================
// Copyright © 2019, eDoxa. All rights reserved.

using System.Collections.Generic;
using System.Linq;

using Bogus;

using eDoxa.Arena.Challenges.Api.Infrastructure.Data.Fakers;
using eDoxa.Arena.Challenges.Domain.AggregateModels.ChallengeAggregate;
using eDoxa.Arena.Challenges.UnitTests.Helpers.Assertions.Extensions;

using FluentAssertions;

using Microsoft.VisualStudio.TestTools.UnitTesting;

namespace eDoxa.Arena.Challenges.UnitTests.Infrastructure.Data.Fakers
{
    [TestClass]
    public sealed class ChallengeFakerTest
    {
        private static readonly Faker Faker = new Faker();

        private static IEnumerable<object[]> ChallengeFakerDataSets =>
            ChallengeGame.GetEnumerations()
                .SelectMany(game => ChallengeState.GetEnumerations().Select(state => new object[] {game, state, Faker.Random.Int()}));

        [DataTestMethod]
        [DynamicData(nameof(ChallengeFakerDataSets))]
        public void Generate_ChallengesWithAnyStateGeneratedByAnySeed_ShouldBeValid(ChallengeGame game, ChallengeState state, int seed)
        {
            // Arrange
            var challengeFaker = new ChallengeFaker(game, state);
            challengeFaker.UseSeed(seed);

            // Act
            var challenges = challengeFaker.Generate(20);

            // Assert
            challenges.Should().BeValid();
        }

        [DataTestMethod]
        [DataRow(1)]
        [DataRow(10)]
        [DataRow(100)]
        [DataRow(1000)]
        [DataRow(10000)]
        public void GenerateTwo_FromDifferentFakerWithSameSeed_ShouldBothBeValid(int seed)
        {
            // Arrange
            var challengeFaker1 = new ChallengeFaker();
            challengeFaker1.UseSeed(seed);
            var challengeFaker2 = new ChallengeFaker();
            challengeFaker2.UseSeed(seed);

            // Act
            var challenge1 = challengeFaker1.Generate();
            var challenge2 = challengeFaker2.Generate();

            // Assert
            challenge1.Should().BeValid();
            challenge2.Should().BeValid();
            challenge1.Should().Be(challenge2);
        }

        [DataTestMethod]
        [DataRow(1)]
        [DataRow(10)]
        [DataRow(100)]
        [DataRow(1000)]
        [DataRow(10000)]
        public void GenerateTwo_FromSameFakerWithDifferentSeeds_ShouldBothNotBeValid(int seed)
        {
            // Arrange
            var challengeFaker = new ChallengeFaker();
            challengeFaker.UseSeed(seed);

            // Act
            var challenge1 = challengeFaker.Generate();
            var challenge2 = challengeFaker.Generate();

            // Assert
            challenge1.Should().BeValid();
            challenge2.Should().BeValid();
            challenge1.Should().NotBe(challenge2);
        }

        [DataTestMethod]
        [DataRow(1)]
        [DataRow(10)]
        [DataRow(100)]
        [DataRow(1000)]
        [DataRow(10000)]
        public void Generate_DistinctParticipants_BeValid(int seed)
        {
            // Arrange
            var challengeFaker = new ChallengeFaker();
            challengeFaker.UseSeed(seed);
            var challenges = challengeFaker.Generate(10);

            // Act
            var participantIds = challenges.SelectMany(challenge => challenge.Participants).Select(participant => participant.Id).ToList();

            // Assert
            challenges.Should().BeValid();
            participantIds.Distinct().Should().HaveCount(participantIds.Count);
        }

        [DataTestMethod]
        [DataRow(1)]
        [DataRow(10)]
        [DataRow(100)]
        [DataRow(1000)]
        [DataRow(10000)]
        public void Generate_DistinctUserIds_ShouldBeValid(int seed)
        {
            // Arrange
            var challengeFaker = new ChallengeFaker();
            challengeFaker.UseSeed(seed);
            var challenges = challengeFaker.Generate(10);

            // Act
            var participantUserIds = challenges.SelectMany(challenge => challenge.Participants).Select(participant => participant.UserId).ToList();

            // Assert
            challenges.Should().BeValid();
            participantUserIds.Distinct().Should().NotHaveCount(participantUserIds.Count);
        }
    }
}
