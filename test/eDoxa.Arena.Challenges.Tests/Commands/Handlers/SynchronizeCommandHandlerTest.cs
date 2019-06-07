// Filename: SynchronizeCommandHandlerTest.cs
// Date Created: 2019-05-29
// 
// ================================================
// Copyright © 2019, eDoxa. All rights reserved.
// 
// This file is subject to the terms and conditions
// defined in file 'LICENSE.md', which is part of
// this source code package.

using System.Threading;
using System.Threading.Tasks;

using eDoxa.Arena.Challenges.Application.Commands;
using eDoxa.Arena.Challenges.Application.Commands.Handlers;
using eDoxa.Arena.Challenges.Domain.AggregateModels.ChallengeAggregate;
using eDoxa.Arena.Challenges.Services.Abstractions;
using eDoxa.Seedwork.Application.Commands.Extensions;
using eDoxa.Seedwork.Domain.Common.Enumerations;

using Microsoft.VisualStudio.TestTools.UnitTesting;

using Moq;

namespace eDoxa.Arena.Challenges.Tests.Commands.Handlers
{
    [TestClass]
    public sealed class SynchronizeCommandHandlerTest
    {
        private Mock<IChallengeService> _mockChallengeService;

        [TestInitialize]
        public void TestInitialize()
        {
            _mockChallengeService = new Mock<IChallengeService>();
        }

        [TestMethod]
        public async Task HandleAsync_SynchronizeCommand_ShouldBeCompletedTask()
        {
            // Arrange
            _mockChallengeService.Setup(mock => mock.SynchronizeAsync(It.IsAny<Game>(), It.IsAny<CancellationToken>()))
                .Returns(Task.CompletedTask)
                .Verifiable();

            var handler = new SynchronizeChallengesCommandHandler(_mockChallengeService.Object);

            // Act
            await handler.HandleAsync(new SynchronizeChallengesCommand(new ChallengeId()));

            // Assert
            _mockChallengeService.Verify(mock => mock.SynchronizeAsync(It.IsAny<Game>(), It.IsAny<CancellationToken>()), Times.Once);
        }
    }
}
