// Filename: ChallengeFakerTest.cs
// Date Created: 2019-06-15
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
using eDoxa.Arena.Challenges.Domain.AggregateModels.ChallengeAggregate;
using eDoxa.Arena.Challenges.UnitTests.Extensions;

using FluentAssertions;

using Microsoft.VisualStudio.TestTools.UnitTesting;

namespace eDoxa.Arena.Challenges.UnitTests.Domain.Fakers
{
    [TestClass]
    public sealed class ChallengeFakerTest
    {
        private static readonly Faker Faker = new Faker();

        private static IEnumerable<object[]> ChallengeStates => ChallengeState.GetEnumerations().Select(state => new object[] {state, Faker.Random.Int()});

        [TestMethod]
        public void Generate_FromDifferentFakerWithSameSeed_ShouldBeEquals()
        {
            // Arrange
            const int seed = 1;

            var challengeFaker1 = new ChallengeFaker();

            challengeFaker1.UseSeed(seed);

            var challengeFaker2 = new ChallengeFaker();

            challengeFaker2.UseSeed(seed);

            // Act
            var challenge1 = challengeFaker1.Generate();

            var challenge2 = challengeFaker2.Generate();

            // Assert
            challenge1.Should().Be(challenge2);
        }

        [TestMethod]
        public void Generate_FromSameFaker_ShouldNotBeEquals()
        {
            // Arrange
            var challengeFaker = new ChallengeFaker();

            // Act
            var challenge1 = challengeFaker.Generate();

            var challenge2 = challengeFaker.Generate();

            // Assert
            challenge1.Should().NotBe(challenge2);
        }

        [DataTestMethod]
        [DynamicData(nameof(ChallengeStates))]
        public void Generate_ChallengesWithAnyStateGeneratedByAnySeed_ShouldBeValidState(ChallengeState state, int seed)
        {
            // Arrange
            var challengeFaker = new ChallengeFaker(state: state);

            challengeFaker.UseSeed(seed);

            // Act
            var challenges = challengeFaker.Generate(20);

            // Assert
            challenges.AssertStateIsValid();
        }

        [TestMethod]
        public void Generate_ChallengeParticipants_ShouldBeUnique()
        {
            // Arrange
            var challengeFaker = new ChallengeFaker();

            // Act
            var challenge1 = challengeFaker.Generate();

            var challenge2 = challengeFaker.Generate();

            var challenge3 = challengeFaker.Generate();

            var participants1 = challenge1.Participants.OrderBy(participant => participant.Id).ToList();

            var participants2 = challenge2.Participants.OrderBy(participant => participant.Id).ToList();

            var participants3 = challenge3.Participants.OrderBy(participant => participant.Id).ToList();

            var participants = participants1.Union(participants2).Union(participants3).Distinct().ToList();

            // Assert
            participants.Should().HaveCount(participants1.Count + participants2.Count + participants3.Count);
        }
    }
}
