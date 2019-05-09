// Filename: ChallengeCompletedIntegrationEventHandlerTest.cs
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
using eDoxa.ServiceBus.Extensions;

using MediatR;

using Microsoft.VisualStudio.TestTools.UnitTesting;

using Moq;

namespace eDoxa.Challenges.Application.Tests.IntegrationEvents.Handlers
{
    [TestClass]
    public sealed class ChallengeCompletedIntegrationEventHandlerTest
    {
        private Mock<IMediator> _mockMediator;

        [TestInitialize]
        public void TestInitialize()
        {
            _mockMediator = new Mock<IMediator>();
        }

        [TestMethod]
        public async Task HandleAsync_ChallengeCompletedIntegrationEvent_ShouldBeCompletedTask()
        {
            // Arrange
            _mockMediator.Setup(mock => mock.Send(It.IsAny<CompleteCommand>(), It.IsAny<CancellationToken>()))
                .ReturnsAsync(It.IsAny<Unit>())
                .Verifiable();

            var handler = new ChallengeCompletedIntegrationEventHandler(_mockMediator.Object);

            // Act
            await handler.HandleAsync(new ChallengeCompletedIntegrationEvent());

            // Assert
            _mockMediator.Verify(mock => mock.Send(It.IsAny<CompleteCommand>(), It.IsAny<CancellationToken>()), Times.Once);
        }
    }
}