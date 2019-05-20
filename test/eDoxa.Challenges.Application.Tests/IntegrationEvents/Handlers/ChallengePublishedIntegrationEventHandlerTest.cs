// Filename: ChallengePublishedIntegrationEventHandlerTest.cs
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

using eDoxa.Challenges.Application.Commands;
using eDoxa.Challenges.Application.IntegrationEvents;
using eDoxa.Challenges.Application.IntegrationEvents.Handlers;
using eDoxa.Challenges.Domain;
using eDoxa.ServiceBus.Extensions;

using MediatR;

using Microsoft.VisualStudio.TestTools.UnitTesting;

using Moq;

namespace eDoxa.Challenges.Application.Tests.IntegrationEvents.Handlers
{
    [TestClass]
    public sealed class ChallengePublishedIntegrationEventHandlerTest
    {
        private Mock<IMediator> _mockMediator;

        [TestInitialize]
        public void TestInitialize()
        {
            _mockMediator = new Mock<IMediator>();
        }

        [TestMethod]
        public async Task HandleAsync_ChallengePublishedIntegrationEvent_ShouldBeCompletedTask()
        {
            // Arrange
            _mockMediator.Setup(mock => mock.Send(It.IsAny<PublishCommand>(), It.IsAny<CancellationToken>()))
                .ReturnsAsync(It.IsAny<Unit>())
                .Verifiable();

            var handler = new ChallengePublishedIntegrationEventHandler(_mockMediator.Object);

            // Act
            await handler.HandleAsync(new ChallengePublishedIntegrationEvent(PublisherInterval.Daily));

            // Assert
            _mockMediator.Verify(mock => mock.Send(It.IsAny<PublishCommand>(), It.IsAny<CancellationToken>()), Times.Once);
        }
    }
}