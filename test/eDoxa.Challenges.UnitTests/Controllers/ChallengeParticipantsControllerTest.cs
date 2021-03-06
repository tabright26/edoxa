﻿// Filename: ChallengeParticipantsControllerTest.cs
// Date Created: 2019-12-26
// 
// ================================================
// Copyright © 2020, eDoxa. All rights reserved.

using System;
using System.Threading.Tasks;

using eDoxa.Challenges.Api.Controllers;
using eDoxa.Challenges.Domain.AggregateModels.ChallengeAggregate;
using eDoxa.Challenges.TestHelper;
using eDoxa.Challenges.TestHelper.Fixtures;
using eDoxa.Seedwork.Domain.Misc;

using FluentAssertions;

using Microsoft.AspNetCore.Mvc;

using Moq;

using Xunit;

namespace eDoxa.Challenges.UnitTests.Controllers
{
    public sealed class ChallengeParticipantsControllerTest : UnitTest
    {
        public ChallengeParticipantsControllerTest(TestDataFixture testData, TestMapperFixture testMapper, TestValidator validator) : base(
            testData,
            testMapper,
            validator)
        {
        }

        [Fact]
        public async Task FetchChallengeParticipantsAsync_ShouldBeNoContentResult()
        {
            // Arrange
            TestMock.ParticipantQuery.Setup(queries => queries.FetchChallengeParticipantsAsync(It.IsAny<ChallengeId>()))
                .ReturnsAsync(Array.Empty<Participant>())
                .Verifiable();

            var controller = new ChallengeParticipantsController(TestMock.ParticipantQuery.Object, TestMapper);

            // Act
            var result = await controller.FetchChallengeParticipantsAsync(new ChallengeId());

            // Assert
            result.Should().BeOfType<NoContentResult>();

            TestMock.ParticipantQuery.Verify(participantQuery => participantQuery.FetchChallengeParticipantsAsync(It.IsAny<ChallengeId>()), Times.Once);
        }

        [Fact]
        public async Task FetchChallengeParticipantsAsync_ShouldBeOkObjectResult()
        {
            // Arrange
            var challengeFaker = TestData.FakerFactory.CreateChallengeFaker(25392992, Game.LeagueOfLegends);

            var challenge = challengeFaker.FakeChallenge();

            TestMock.ParticipantQuery.Setup(participantQuery => participantQuery.FetchChallengeParticipantsAsync(It.IsAny<ChallengeId>()))
                .ReturnsAsync(challenge.Participants)
                .Verifiable();

            var controller = new ChallengeParticipantsController(TestMock.ParticipantQuery.Object, TestMapper);

            // Act
            var result = await controller.FetchChallengeParticipantsAsync(new ChallengeId());

            // Assert
            result.Should().BeOfType<OkObjectResult>();

            TestMock.ParticipantQuery.Verify(participantQuery => participantQuery.FetchChallengeParticipantsAsync(It.IsAny<ChallengeId>()), Times.Once);
        }
    }
}
