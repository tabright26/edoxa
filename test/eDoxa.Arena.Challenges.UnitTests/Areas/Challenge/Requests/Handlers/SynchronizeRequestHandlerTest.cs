// Filename: SynchronizeRequestHandlerTest.cs
// Date Created: 2019-07-26
// 
// ================================================
// Copyright © 2019, eDoxa. All rights reserved.

using System;
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
    public sealed class SynchronizeRequestHandlerTest
    {
        private Mock<IChallengeService> _mockChallengeService;

        [TestInitialize]
        public void TestInitialize()
        {
            _mockChallengeService = new Mock<IChallengeService>();
        }

        [TestMethod]
        public async Task SynchronizeChallengesRequest_ShouldBeCompletedTask()
        {
            // Arrange
            _mockChallengeService
                .Setup(
                    mock => mock.SynchronizeAsync(It.IsAny<ChallengeGame>(), It.IsAny<TimeSpan>(), It.IsAny<IDateTimeProvider>(), It.IsAny<CancellationToken>())
                )
                .Returns(Task.CompletedTask)
                .Verifiable();

            var handler = new SynchronizeChallengesRequestHandler(_mockChallengeService.Object);

            // Act
            await handler.HandleAsync(new SynchronizeChallengesRequest(new ChallengeId()));

            // Assert
            _mockChallengeService.Verify(
                mock => mock.SynchronizeAsync(It.IsAny<ChallengeGame>(), It.IsAny<TimeSpan>(), It.IsAny<IDateTimeProvider>(), It.IsAny<CancellationToken>()),
                Times.Once
            );
        }
    }
}
