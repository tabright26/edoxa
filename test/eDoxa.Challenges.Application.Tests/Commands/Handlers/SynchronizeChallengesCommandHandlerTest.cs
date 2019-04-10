// Filename: SynchronizeChallengesCommandHandlerTest.cs
// Date Created: 2019-03-22
// 
// ============================================================
// Copyright © 2019, Francis Quenneville
// All rights reserved.
// 
// This file is subject to the terms and conditions defined in file 'LICENSE.md', which is part of
// this source code package.

using System.Threading;
using System.Threading.Tasks;

using eDoxa.Challenges.Application.Commands;
using eDoxa.Challenges.Application.Commands.Handlers;
using eDoxa.Challenges.Domain.Services;

using Microsoft.VisualStudio.TestTools.UnitTesting;

using Moq;

namespace eDoxa.Challenges.Application.Tests.Commands.Handlers
{
    [TestClass]
    public sealed class SynchronizeChallengesCommandHandlerTest
    {
        [TestMethod]
        public async Task HandleAsync_SynchronizeAsync_ShouldBeInvokedExactlyOneTime()
        {
            // Arrange
            var mockChallengeSynchronizerService = new Mock<IChallengeSynchronizerService>();

            mockChallengeSynchronizerService.Setup(service => service.SynchronizeAsync()).Returns(Task.CompletedTask).Verifiable();

            // Act
            var handler = new SynchronizeChallengesCommandHandler(mockChallengeSynchronizerService.Object);

            // Assert
            await handler.HandleAsync(new SynchronizeChallengesCommand(), default(CancellationToken));

            mockChallengeSynchronizerService.Verify(service => service.SynchronizeAsync(), Times.Once);
        }
    }
}