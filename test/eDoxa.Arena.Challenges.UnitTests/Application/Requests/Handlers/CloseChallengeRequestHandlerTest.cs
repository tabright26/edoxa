// Filename: CloseChallengeRequestHandlerTest.cs
// Date Created: 2019-06-25
// 
// ================================================
// Copyright © 2019, eDoxa. All rights reserved.

using System.Threading;
using System.Threading.Tasks;

using eDoxa.Arena.Challenges.Api.Application.Requests;
using eDoxa.Arena.Challenges.Api.Application.Requests.Handlers;
using eDoxa.Arena.Challenges.Domain.AggregateModels.ChallengeAggregate;
using eDoxa.Arena.Challenges.Domain.Services;
using eDoxa.Seedwork.Application.Extensions;
using eDoxa.Seedwork.Domain;

using Microsoft.VisualStudio.TestTools.UnitTesting;

using Moq;

namespace eDoxa.Arena.Challenges.UnitTests.Application.Requests.Handlers
{
    [TestClass]
    public sealed class CloseChallengeRequestHandlerTest
    {
        private Mock<IChallengeService> _mockChallengeService;

        [TestInitialize]
        public void TestInitialize()
        {
            _mockChallengeService = new Mock<IChallengeService>();
        }

        [TestMethod]
        public async Task CloseChallengesRequest_ShouldBeCompletedTask()
        {
            // Arrange
            _mockChallengeService.Setup(mock => mock.CloseAsync(It.IsAny<IDateTimeProvider>(), It.IsAny<CancellationToken>()))
                .Returns(Task.CompletedTask)
                .Verifiable();

            var handler = new CloseChallengesRequestHandler(_mockChallengeService.Object);

            // Act
            await handler.HandleAsync(new CloseChallengesRequest(new ChallengeId()));

            // Assert
            _mockChallengeService.Verify(mock => mock.CloseAsync(It.IsAny<IDateTimeProvider>(), It.IsAny<CancellationToken>()), Times.Once);
        }
    }
}
