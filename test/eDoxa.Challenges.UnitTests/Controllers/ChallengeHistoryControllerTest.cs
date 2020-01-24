// Filename: ChallengeHistoryControllerTest.cs
// Date Created: 2019-12-26
// 
// ================================================
// Copyright © 2020, eDoxa. All rights reserved.

using System.Collections.ObjectModel;
using System.Threading.Tasks;

using eDoxa.Challenges.Api.Controllers;
using eDoxa.Challenges.Domain.AggregateModels;
using eDoxa.Challenges.Domain.AggregateModels.ChallengeAggregate;
using eDoxa.Challenges.Domain.Queries;
using eDoxa.Challenges.TestHelper;
using eDoxa.Challenges.TestHelper.Fixtures;
using eDoxa.Challenges.TestHelper.Mocks;
using eDoxa.Seedwork.Domain.Misc;

using FluentAssertions;

using Microsoft.AspNetCore.Mvc;

using Moq;

using Xunit;

namespace eDoxa.Challenges.UnitTests.Controllers
{
    public sealed class ChallengeHistoryControllerTest : UnitTest
    {
        public ChallengeHistoryControllerTest(TestDataFixture testData, TestMapperFixture testMapper, TestValidator validator) : base(
            testData,
            testMapper,
            validator)
        {
        }

        [Fact]
        public async Task GetAsync_ShouldBeNoContentResult()
        {
            // Arrange
            var mockHttpContextAccessor = new MockHttpContextAccessor(); 

            var mockChallengeQuery = new Mock<IChallengeQuery>();

            mockChallengeQuery.Setup(queries => queries.FetchUserChallengeHistoryAsync(It.IsAny<UserId>(), It.IsAny<Game>(), It.IsAny<ChallengeState>()))
                .ReturnsAsync(new Collection<IChallenge>())
                .Verifiable();

            var controller = new ChallengeHistoryController(mockChallengeQuery.Object, TestMapper)
            {
                ControllerContext =
                {
                    HttpContext = mockHttpContextAccessor.Object.HttpContext
                }
            };

            // Act
            var result = await controller.GetAsync();

            // Assert
            result.Should().BeOfType<NoContentResult>();

            mockChallengeQuery.Verify(
                challengeQuery => challengeQuery.FetchUserChallengeHistoryAsync(It.IsAny<UserId>(), It.IsAny<Game>(), It.IsAny<ChallengeState>()),
                Times.Once);
        }

        [Fact]
        public async Task GetAsync_ShouldBeOkObjectResult()
        {
            // Arrange
            var mockHttpContextAccessor = new MockHttpContextAccessor();

            var challengeFaker = TestData.FakerFactory.CreateChallengeFaker(48392992, Game.LeagueOfLegends);

            var challenges = challengeFaker.FakeChallenges(2);

            var mockChallengeQuery = new Mock<IChallengeQuery>();

            mockChallengeQuery
                .Setup(challengeQuery => challengeQuery.FetchUserChallengeHistoryAsync(It.IsAny<UserId>(), It.IsAny<Game>(), It.IsAny<ChallengeState>()))
                .ReturnsAsync(challenges)
                .Verifiable();

            var controller = new ChallengeHistoryController(mockChallengeQuery.Object, TestMapper)
            {
                ControllerContext =
                {
                    HttpContext = mockHttpContextAccessor.Object.HttpContext
                }
            };

            // Act
            var result = await controller.GetAsync();

            // Assert
            result.Should().BeOfType<OkObjectResult>();

            mockChallengeQuery.Verify(
                challengeQuery => challengeQuery.FetchUserChallengeHistoryAsync(It.IsAny<UserId>(), It.IsAny<Game>(), It.IsAny<ChallengeState>()),
                Times.Once);
        }
    }
}
