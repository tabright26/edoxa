﻿// Filename: SynchronizeCommandHandlerTest.cs
// Date Created: 2019-05-28
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
using eDoxa.Arena.Challenges.Domain.AggregateModels;
using eDoxa.Arena.Challenges.Services.Abstractions;
using eDoxa.Commands.Extensions;

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
            _mockChallengeService.Setup(mock => mock.SynchronizeAsync(It.IsAny<ChallengeId>(), It.IsAny<CancellationToken>()))
                .Returns(Task.CompletedTask)
                .Verifiable();

            var handler = new SynchronizeChallengeCommandHandler(_mockChallengeService.Object);

            // Act
            await handler.HandleAsync(new SynchronizeChallengeCommand(new ChallengeId()));

            // Assert
            _mockChallengeService.Verify(mock => mock.SynchronizeAsync(It.IsAny<ChallengeId>(), It.IsAny<CancellationToken>()), Times.Once);
        }
    }
}