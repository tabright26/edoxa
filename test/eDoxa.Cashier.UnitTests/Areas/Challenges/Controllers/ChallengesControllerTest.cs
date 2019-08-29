// Filename: ChallengesControllerTest.cs
// Date Created: 2019-08-28
// 
// ================================================
// Copyright © 2019, eDoxa. All rights reserved.

using System.Threading.Tasks;

using eDoxa.Cashier.Api.Areas.Challenges.Controllers;
using eDoxa.Cashier.Api.Infrastructure.Data.Fakers;
using eDoxa.Cashier.Domain.AggregateModels.ChallengeAggregate;
using eDoxa.Cashier.Domain.Queries;
using eDoxa.Cashier.UnitTests.Helpers.Extensions;

using FluentAssertions;

using Microsoft.AspNetCore.Mvc;
using Microsoft.VisualStudio.TestTools.UnitTesting;

using Moq;

namespace eDoxa.Cashier.UnitTests.Areas.Challenges.Controllers
{
    [TestClass]
    public sealed class ChallengesControllerTest
    {
        [TestMethod]
        public async Task GetByIdAsync_ShouldBeOfTypeOkObjectResult()
        {
            // Arrange
            var challengeFaker = new ChallengeFaker();

            challengeFaker.UseSeed(1000);

            var challenge = challengeFaker.Generate();

            var mockAccountQuery = new Mock<IChallengeQuery>();

            mockAccountQuery.Setup(accountQuery => accountQuery.FindChallengeAsync(It.IsAny<ChallengeId>())).ReturnsAsync(challenge).Verifiable();

            mockAccountQuery.SetupGet(accountQuery => accountQuery.Mapper).Returns(MapperExtensions.Mapper).Verifiable();

            var controller = new ChallengesController(mockAccountQuery.Object);

            // Act
            var result = await controller.GetByIdAsync(new ChallengeId());

            // Assert
            result.Should().BeOfType<OkObjectResult>();

            mockAccountQuery.Verify(accountQuery => accountQuery.FindChallengeAsync(It.IsAny<ChallengeId>()), Times.Once);

            mockAccountQuery.VerifyGet(accountQuery => accountQuery.Mapper, Times.Once);
        }

        [TestMethod]
        public async Task GetByIdAsync_ShouldBeOfTypeNotFoundObjectResult()
        {
            // Arrange
            var challengeFaker = new ChallengeFaker();

            challengeFaker.UseSeed(1000);

            var mockAccountQuery = new Mock<IChallengeQuery>();

            mockAccountQuery.Setup(accountQuery => accountQuery.FindChallengeAsync(It.IsAny<ChallengeId>())).Verifiable();

            mockAccountQuery.SetupGet(accountQuery => accountQuery.Mapper).Returns(MapperExtensions.Mapper).Verifiable();

            var controller = new ChallengesController(mockAccountQuery.Object);

            // Act
            var result = await controller.GetByIdAsync(new ChallengeId());

            // Assert
            result.Should().BeOfType<NotFoundObjectResult>();

            mockAccountQuery.Verify(accountQuery => accountQuery.FindChallengeAsync(It.IsAny<ChallengeId>()), Times.Once);

            mockAccountQuery.VerifyGet(accountQuery => accountQuery.Mapper, Times.Once);
        }

        [TestMethod]
        public async Task GetByIdAsync_ShouldBeOfTypeBadRequestObjectResult()
        {
            // Arrange
            var challengeFaker = new ChallengeFaker();

            challengeFaker.UseSeed(1000);

            var mockAccountQuery = new Mock<IChallengeQuery>();

            mockAccountQuery.Setup(accountQuery => accountQuery.FindChallengeAsync(It.IsAny<ChallengeId>())).Verifiable();

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
    }
}
