// Filename: ChallengeHistoryControllerTest.cs
// Date Created: 2019-09-29
// 
// ================================================
// Copyright © 2019, eDoxa. All rights reserved.

using System.Collections.ObjectModel;
using System.Threading.Tasks;

using eDoxa.Arena.Challenges.Api.Areas.Challenges.Controllers;
using eDoxa.Arena.Challenges.Domain.AggregateModels;
using eDoxa.Arena.Challenges.Domain.AggregateModels.ChallengeAggregate;
using eDoxa.Arena.Challenges.Domain.Queries;
using eDoxa.Arena.Challenges.TestHelpers;
using eDoxa.Arena.Challenges.TestHelpers.Fixtures;

using FluentAssertions;

using Microsoft.AspNetCore.Mvc;

using Moq;

using Xunit;

namespace eDoxa.Arena.Challenges.UnitTests.Areas.Challenges.Controllers
{
    public sealed class ChallengeHistoryControllerTest : UnitTest
    {
        public ChallengeHistoryControllerTest(TestDataFixture testData, TestMapperFixture testMapper) : base(testData, testMapper)
        {
        }

        [Fact]
        public async Task GetAsync_ShouldBeNoContentResult()
        {
            // Arrange
            var mockChallengeQuery = new Mock<IChallengeQuery>();

            mockChallengeQuery.Setup(queries => queries.FetchUserChallengeHistoryAsync(It.IsAny<ChallengeGame>(), It.IsAny<ChallengeState>()))
                .ReturnsAsync(new Collection<IChallenge>())
                .Verifiable();

            mockChallengeQuery.SetupGet(challengeQuery => challengeQuery.Mapper).Returns(TestMapper).Verifiable();

            var controller = new ChallengeHistoryController(mockChallengeQuery.Object);

            // Act
            var result = await controller.GetAsync();

            // Assert
            result.Should().BeOfType<NoContentResult>();

            mockChallengeQuery.Verify(
                challengeQuery => challengeQuery.FetchUserChallengeHistoryAsync(It.IsAny<ChallengeGame>(), It.IsAny<ChallengeState>()),
                Times.Once);

            mockChallengeQuery.VerifyGet(challengeQuery => challengeQuery.Mapper, Times.Once);
        }

        [Fact]
        public async Task GetAsync_ShouldBeOkObjectResult()
        {
            // Arrange
            var challengeFaker = TestData.FakerFactory.CreateChallengeFaker(48392992);

            var challenges = challengeFaker.FakeChallenges(2);

            var mockChallengeQuery = new Mock<IChallengeQuery>();

            mockChallengeQuery.Setup(challengeQuery => challengeQuery.FetchUserChallengeHistoryAsync(It.IsAny<ChallengeGame>(), It.IsAny<ChallengeState>()))
                .ReturnsAsync(challenges)
                .Verifiable();

            mockChallengeQuery.SetupGet(challengeQuery => challengeQuery.Mapper).Returns(TestMapper).Verifiable();

            var controller = new ChallengeHistoryController(mockChallengeQuery.Object);

            // Act
            var result = await controller.GetAsync();

            // Assert
            result.Should().BeOfType<OkObjectResult>();

            mockChallengeQuery.Verify(
                challengeQuery => challengeQuery.FetchUserChallengeHistoryAsync(It.IsAny<ChallengeGame>(), It.IsAny<ChallengeState>()),
                Times.Once);

            mockChallengeQuery.VerifyGet(challengeQuery => challengeQuery.Mapper, Times.Once);
        }
    }
}
