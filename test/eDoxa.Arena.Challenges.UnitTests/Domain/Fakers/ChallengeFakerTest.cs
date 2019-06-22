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

using AutoMapper;

using Bogus;

using eDoxa.Arena.Challenges.Api.Infrastructure.Data.Fakers;
using eDoxa.Arena.Challenges.Api.Infrastructure.Data.Fakers.Extensions;
using eDoxa.Arena.Challenges.Domain.AggregateModels.ChallengeAggregate;
using eDoxa.Arena.Challenges.UnitTests.Extensions;
using eDoxa.Arena.Challenges.UnitTests.Utilities;

using FluentAssertions;

using Microsoft.VisualStudio.TestTools.UnitTesting;

namespace eDoxa.Arena.Challenges.UnitTests.Domain.Fakers
{
    [TestClass]
    public sealed class ChallengeFakerTest
    {
        private static readonly IMapper Mapper = MapperBuilder.CreateMapper();

        private static readonly Faker Faker = new Faker();

        private static IEnumerable<object[]> ChallengeStates => ChallengeState.GetEnumerations().Select(state => new object[] {state, Faker.Random.Int()});

        [TestMethod]
        public void M()
        {
            var challengeFaker = new ChallengeFaker();

            var challenge = challengeFaker.GenerateViewModel();
        }

        [TestMethod]
        public void FakeChallenges_ShouldNotThrow1()
        {
            // Arrange
            var challengeFaker1 = new ChallengeFaker();

            var challengeFaker2 = new ChallengeFaker();

            // Act
            var challenge1 = challengeFaker1.GenerateEntity();

            var challenge2 = challengeFaker2.GenerateEntity();

            // Assert
            challenge1.Should().Be(challenge2);
        }

        [TestMethod]
        public void FakeChallenges_ShouldNotThrow2()
        {
            // Arrange
            var challengeFaker = new ChallengeFaker();

            // Act
            var challenge1 = challengeFaker.GenerateEntity();

            var challenge2 = challengeFaker.GenerateEntity();

            // Assert
            challenge1.Should().NotBe(challenge2);
        }

        [DataTestMethod]
        [DynamicData(nameof(ChallengeStates))]
        public void Generate_ChallengesWithAnyStateGeneratedByAnySeed_ShouldBeValidObjectState(ChallengeState state, int seed)
        {
            // Arrange
            var challengeFaker = new ChallengeFaker(state: state);

            challengeFaker.UseSeed(seed);

            // Act
            var challenges = challengeFaker.GenerateEntities(20);

            // Assert
            challenges.ShouldBeValidObjectState();
        }

        //[Ignore("This feature is temporairy disabled.")]
        //public void FakeChallenges_ShouldNotThrow3()
        //{
        //    // Arrange
        //    var challengeFaker = new ChallengeFaker();

        //    // Act
        //    var challenge1 = challengeFaker.Generate();

        //    //challengeFaker.ParticipantFaker = new ParticipantFaker();

        //    var challenge2 = challengeFaker.Generate();

        //    //challengeFaker.ParticipantFaker = new ParticipantFaker();

        //    var challenge3 = challengeFaker.Generate();

        //    var participants1 = challenge1.Participants.OrderBy(x => x.Id).ToList();

        //    var participants2 = challenge2.Participants.OrderBy(x => x.Id).ToList();

        //    var participants3 = challenge3.Participants.OrderBy(x => x.Id).ToList();

        //    var r = participants1.Union(participants2).Union(participants3).Distinct().ToList();

        //    // Assert
        //    r.Should().HaveCount(200);
        //}

        //[Ignore("This feature is temporairy disabled.")]
        //public void FakeChallenges_ShouldNotThrow4()
        //{
        //    // Arrange
        //    var challengeFaker = new ChallengeFaker();

        //    // Act
        //    var challenge1 = challengeFaker.Generate();

        //    var challenge2 = challengeFaker.Generate();

        //    var challenge3 = challengeFaker.Generate();

        //    var participants1 = challenge1.Participants.OrderBy(participant => participant.Id).ToList();

        //    var participants2 = challenge2.Participants.OrderBy(participant => participant.Id).ToList();

        //    var participants3 = challenge3.Participants.OrderBy(participant => participant.Id).ToList();

        //    var participants = participants1.Union(participants2).Union(participants3).Distinct().ToList();

        //    // Assert
        //    participants.Should().HaveCount(participants1.Count + participants2.Count + participants3.Count);
        //}
    }
}
