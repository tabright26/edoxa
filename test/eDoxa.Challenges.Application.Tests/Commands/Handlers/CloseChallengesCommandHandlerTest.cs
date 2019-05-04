// Filename: CloseChallengesCommandHandlerTest.cs
// Date Created: 2019-03-22
// 
// ============================================================
// Copyright © 2019, Francis Quenneville
// All rights reserved.
// 
// This file is subject to the terms and conditions defined in file 'LICENSE.md', which is part of
// this source code package.

using System.Threading.Tasks;

using eDoxa.Challenges.Application.Commands;
using eDoxa.Challenges.Application.Commands.Handlers;
using eDoxa.Challenges.Domain.Services;
using eDoxa.Commands.Extensions;

using Microsoft.VisualStudio.TestTools.UnitTesting;

using Moq;

namespace eDoxa.Challenges.Application.Tests.Commands.Handlers
{
    [TestClass]
    public sealed class CloseChallengesCommandHandlerTest
    {
        [TestMethod]
        public async Task HandleAsync_CloseAsync_ShouldBeInvokedExactlyOneTime()
        {
            // Arrange
            var mockChallengeCloserService = new Mock<IChallengeCloserService>();

            mockChallengeCloserService.Setup(service => service.CloseAsync()).Returns(Task.CompletedTask).Verifiable();

            // Act
            var handler = new CloseChallengesCommandHandler(mockChallengeCloserService.Object);

            // Assert
            await handler.HandleAsync(new CloseChallengesCommand());

            mockChallengeCloserService.Verify(service => service.CloseAsync(), Times.Once);
        }
    }
}