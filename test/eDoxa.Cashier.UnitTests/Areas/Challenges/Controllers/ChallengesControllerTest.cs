// Filename: ChallengesControllerTest.cs
// Date Created: 2019-09-16
// 
// ================================================
// Copyright © 2019, eDoxa. All rights reserved.

using System.Threading.Tasks;

using eDoxa.Cashier.Api.Areas.Challenges.Controllers;
using eDoxa.Cashier.Domain.AggregateModels.ChallengeAggregate;
using eDoxa.Cashier.Domain.Queries;
using eDoxa.Cashier.UnitTests.TestHelpers;

using FluentAssertions;

using Microsoft.AspNetCore.Mvc;

using Moq;

using Xunit;

namespace eDoxa.Cashier.UnitTests.Areas.Challenges.Controllers
{
    public sealed class ChallengesControllerTest : UnitTestClass
    {
        public ChallengesControllerTest(TestDataFixture testData, TestMapperFixture testMapper) : base(testData, testMapper)
        {
        }

        [Fact]
        public async Task GetByIdAsync_ShouldBeOfTypeBadRequestObjectResult()
        {
            // Arrange
            var challenge = TestData.FakerFactory.CreateChallengeFaker(1000).FakeChallenge();

            var mockAccountQuery = new Mock<IChallengeQuery>();

            mockAccountQuery.Setup(accountQuery => accountQuery.FindChallengeAsync(It.IsAny<ChallengeId>())).ReturnsAsync(challenge).Verifiable();

            mockAccountQuery.SetupGet(accountQuery => accountQuery.Mapper).Verifiable();

            var controller = new ChallengesController(mockAccountQuery.Object);

            controller.ControllerContext.ModelState.AddModelError("error", "error");

            // Act
            var result = await controller.GetByIdAsync(new ChallengeId());

            // Assert
            result.Should().BeOfType<BadRequestObjectResult>();

            mockAccountQuery.Verify(accountQuery => accountQuery.FindChallengeAsync(It.IsAny<ChallengeId>()), Times.Never);

            mockAccountQuery.VerifyGet(accountQuery => accountQuery.Mapper, Times.Never);
        }

        [Fact]
        public async Task GetByIdAsync_ShouldBeOfTypeNotFoundObjectResult()
        {
            // Arrange
            var mockAccountQuery = new Mock<IChallengeQuery>();

            mockAccountQuery.Setup(accountQuery => accountQuery.FindChallengeAsync(It.IsAny<ChallengeId>())).Verifiable();

            mockAccountQuery.SetupGet(accountQuery => accountQuery.Mapper).Returns(TestMapper).Verifiable();

            var controller = new ChallengesController(mockAccountQuery.Object);

            // Act
            var result = await controller.GetByIdAsync(new ChallengeId());

            // Assert
            result.Should().BeOfType<NotFoundObjectResult>();

            mockAccountQuery.Verify(accountQuery => accountQuery.FindChallengeAsync(It.IsAny<ChallengeId>()), Times.Once);

            mockAccountQuery.VerifyGet(accountQuery => accountQuery.Mapper, Times.Once);
        }

        [Fact]
        public async Task GetByIdAsync_ShouldBeOfTypeOkObjectResult()
        {
            // Arrange
            var challenge = TestData.FakerFactory.CreateChallengeFaker(1000).FakeChallenge();

            var mockAccountQuery = new Mock<IChallengeQuery>();

            mockAccountQuery.Setup(accountQuery => accountQuery.FindChallengeAsync(It.IsAny<ChallengeId>())).ReturnsAsync(challenge).Verifiable();

            mockAccountQuery.SetupGet(accountQuery => accountQuery.Mapper).Returns(TestMapper).Verifiable();

            var controller = new ChallengesController(mockAccountQuery.Object);

            // Act
            var result = await controller.GetByIdAsync(new ChallengeId());

            // Assert
            result.Should().BeOfType<OkObjectResult>();

            mockAccountQuery.Verify(accountQuery => accountQuery.FindChallengeAsync(It.IsAny<ChallengeId>()), Times.Once);

            mockAccountQuery.VerifyGet(accountQuery => accountQuery.Mapper, Times.Once);
        }
    }
}
