// Filename: ChallengesSynchronizedIntegrationEventHandlerTest.cs
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
using eDoxa.Challenges.Application.IntegrationEvents;
using eDoxa.Challenges.Application.IntegrationEvents.Handlers;

using MediatR;

using Microsoft.VisualStudio.TestTools.UnitTesting;

using Moq;

namespace eDoxa.Challenges.Application.Tests.IntegrationEvents.Handlers
{
    [TestClass]
    public sealed class ChallengesSynchronizedIntegrationEventHandlerTest
    {
        [TestMethod]
        public async Task Handle_SendCommandAsync_ShouldBeInvokedExactlyOneTime()
        {
            // Arrange
            var mockMediator = new Mock<IMediator>();

            mockMediator.Setup(mediator => mediator.Send(It.IsAny<PublishChallengesCommand>(), It.IsAny<CancellationToken>()))
                        .ReturnsAsync(It.IsAny<Unit>())
                        .Verifiable();

            // Act
            var handler = new ChallengesSynchronizedIntegrationEventHandler(mockMediator.Object);

            // Assert
            await handler.Handle(new ChallengesSynchronizedIntegrationEvent());

            mockMediator.Verify(mediator => mediator.Send(It.IsAny<SynchronizeChallengesCommand>(), It.IsAny<CancellationToken>()), Times.Once);
        }
    }
}