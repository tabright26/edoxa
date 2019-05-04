// Filename: PublishChallengesCommandHandlerTest.cs
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
    public sealed class PublishChallengesCommandHandlerTest
    {
        [TestMethod]
        public async Task HandleAsync_PublishAsync_ShouldBeInvokedExactlyOneTime()
        {
            // Arrange
            var mockChallengePublisherService = new Mock<IChallengeMonthlyPublisherService>();

            mockChallengePublisherService.Setup(service => service.PublishAsync())
                                         .Returns(Task.CompletedTask)
                                         .Verifiable();

            // Act
            var handler = new PublishChallengesCommandHandler(mockChallengePublisherService.Object);

            // Assert
            await handler.HandleAsync(new PublishChallengesCommand());

            mockChallengePublisherService.Verify(service => service.PublishAsync(), Times.Once);
        }
    }
}