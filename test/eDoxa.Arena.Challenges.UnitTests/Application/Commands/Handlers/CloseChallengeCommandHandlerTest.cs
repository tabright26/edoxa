// Filename: CloseChallengeCommandHandlerTest.cs
// Date Created: 2019-06-25
// 
// ================================================
// Copyright © 2019, eDoxa. All rights reserved.
// 
// This file is subject to the terms and conditions
// defined in file 'LICENSE.md', which is part of
// this source code package.

using System.Threading;
using System.Threading.Tasks;

using eDoxa.Arena.Challenges.Api.Application.Commands;
using eDoxa.Arena.Challenges.Api.Application.Commands.Handlers;
using eDoxa.Arena.Challenges.Domain.AggregateModels.ChallengeAggregate;
using eDoxa.Arena.Challenges.Domain.Services;
using eDoxa.Commands.Extensions;
using eDoxa.Seedwork.Domain;

using Microsoft.VisualStudio.TestTools.UnitTesting;

using Moq;

namespace eDoxa.Arena.Challenges.UnitTests.Application.Commands.Handlers
{
    [TestClass]
    public sealed class CloseChallengeCommandHandlerTest
    {
        private Mock<IChallengeService> _mockChallengeService;

        [TestInitialize]
        public void TestInitialize()
        {
            _mockChallengeService = new Mock<IChallengeService>();
        }

        [TestMethod]
        public async Task HandleAsync_CompleteCommand_ShouldBeCompletedTask()
        {
            // Arrange
            _mockChallengeService.Setup(mock => mock.CloseAsync(It.IsAny<IDateTimeProvider>(), It.IsAny<CancellationToken>()))
                .Returns(Task.CompletedTask)
                .Verifiable();

            var handler = new CloseChallengesCommandHandler(_mockChallengeService.Object);

            // Act
            await handler.HandleAsync(new CloseChallengesCommand(new ChallengeId()));

            // Assert
            _mockChallengeService.Verify(mock => mock.CloseAsync(It.IsAny<IDateTimeProvider>(), It.IsAny<CancellationToken>()), Times.Once);
        }
    }
}
