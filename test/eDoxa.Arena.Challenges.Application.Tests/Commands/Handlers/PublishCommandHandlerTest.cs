﻿// Filename: PublishCommandHandlerTest.cs
// Date Created: 2019-05-03
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
using eDoxa.Arena.Challenges.Domain;
using eDoxa.Arena.Challenges.Domain.Services;
using eDoxa.Commands.Extensions;

using Microsoft.VisualStudio.TestTools.UnitTesting;

using Moq;

namespace eDoxa.Arena.Challenges.Application.Tests.Commands.Handlers
{
    [TestClass]
    public sealed class PublishCommandHandlerTest
    {
        private Mock<IChallengeService> _mockChallengeService;

        [TestInitialize]
        public void TestInitialize()
        {
            _mockChallengeService = new Mock<IChallengeService>();
        }

        [TestMethod]
        public async Task HandleAsync_PublishCommand_ShouldBeCompletedTask()
        {
            // Arrange
            _mockChallengeService.Setup(service => service.PublishAsync(It.IsAny<PublisherInterval>(), It.IsAny<CancellationToken>()))
                .Returns(Task.CompletedTask)
                .Verifiable();

            var handler = new PublishCommandHandler(_mockChallengeService.Object);

            // Act
            await handler.HandleAsync(new PublishCommand(PublisherInterval.Daily));

            // Assert
            _mockChallengeService.Verify(service => service.PublishAsync(It.IsAny<PublisherInterval>(), It.IsAny<CancellationToken>()), Times.Once);
        }
    }
}