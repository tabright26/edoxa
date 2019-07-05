// Filename: ChallengeFakerTest.cs
// Date Created: 2019-06-28
// 
// ================================================
// Copyright © 2019, eDoxa. All rights reserved.
// 
// This file is subject to the terms and conditions
// defined in file 'LICENSE.md', which is part of
// this source code package.

using System.Collections.Generic;
using System.Linq;

using Bogus;

using eDoxa.Arena.Challenges.Api.Application.Fakers;
using eDoxa.Arena.Challenges.Api.Extensions;
using eDoxa.Arena.Challenges.Domain.AggregateModels.ChallengeAggregate;
using eDoxa.Arena.Challenges.UnitTests.Helpers.Extensions;

using FluentAssertions;

using Microsoft.VisualStudio.TestTools.UnitTesting;

namespace eDoxa.Arena.Challenges.UnitTests.Application.Fakers
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
        public void Generate_ChallengesWithAnyStateGeneratedByAnySeed_ShouldBeValidState(ChallengeGame game, ChallengeState state, int seed)
        {
            // Arrange
            var challengeFaker = new ChallengeFaker(game, state);
            challengeFaker.UseSeed(seed);

            // Act
            var challenges = challengeFaker.Generate(20);

            // Assert
            challenges.AssertStateIsValid();
        }

        [TestMethod]
        public void ChallengeViewModel_Mapping_ShouldBeValid()
        {
            // Arrange
            var challengeFaker = new ChallengeFaker();

            // Act
            var challenge = challengeFaker.Generate().ToViewModel();

            // Assert
            challenge.AssertMappingIsValid();
        }

        [DataTestMethod]
        [DataRow(1)]
        [DataRow(10)]
        [DataRow(100)]
        [DataRow(1000)]
        [DataRow(10000)]
        public void Generate_FromDifferentFakerWithSameSeed_ShouldBeEquals(int seed)
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
            challenge1.AssertStateIsValid();
            challenge2.AssertStateIsValid();
            challenge1.Should().Be(challenge2);
        }

        [DataTestMethod]
        [DataRow(1)]
        [DataRow(10)]
        [DataRow(100)]
        [DataRow(1000)]
        [DataRow(10000)]
        public void Generate_FromSameFaker_ShouldNotBeEquals(int seed)
        {
            // Arrange
            var challengeFaker = new ChallengeFaker();
            challengeFaker.UseSeed(seed);

            // Act
            var challenge1 = challengeFaker.Generate();
            var challenge2 = challengeFaker.Generate();

            // Assert
            challenge1.AssertStateIsValid();
            challenge2.AssertStateIsValid();
            challenge1.Should().NotBe(challenge2);
        }

        [DataTestMethod]
        [DataRow(1)]
        [DataRow(10)]
        [DataRow(100)]
        [DataRow(1000)]
        [DataRow(10000)]
        public void Generate_DistinctParticipants_ShouldHaveCount(int seed)
        {
            // Arrange
            var challengeFaker = new ChallengeFaker();
            challengeFaker.UseSeed(seed);
            var challenges = challengeFaker.Generate(10);

            // Act
            var participantIds = challenges.SelectMany(challenge => challenge.Participants).Select(participant => participant.Id).ToList();

            // Assert
            challenges.AssertStateIsValid();
            participantIds.Distinct().Should().HaveCount(participantIds.Count);
        }

        [DataTestMethod]
        [DataRow(1)]
        [DataRow(10)]
        [DataRow(100)]
        [DataRow(1000)]
        [DataRow(10000)]
        public void Generate_DistinctUserIds_ShouldNotHaveCount(int seed)
        {
            // Arrange
            var challengeFaker = new ChallengeFaker();
            challengeFaker.UseSeed(seed);
            var challenges = challengeFaker.Generate(10);

            // Act
            var participantUserIds = challenges.SelectMany(challenge => challenge.Participants).Select(participant => participant.UserId).ToList();

            // Assert
            challenges.AssertStateIsValid();
            participantUserIds.Distinct().Should().NotHaveCount(participantUserIds.Count);
        }
    }
}
