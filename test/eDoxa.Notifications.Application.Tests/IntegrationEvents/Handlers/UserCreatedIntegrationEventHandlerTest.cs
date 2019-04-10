// Filename: UserCreatedIntegrationEventHandlerTest.cs
// Date Created: 2019-03-26
// 
// ============================================================
// Copyright © 2019, Francis Quenneville
// All rights reserved.
// 
// This file is subject to the terms and conditions defined in file 'LICENSE.md', which is part of
// this source code package.

using System.Threading;
using System.Threading.Tasks;

using eDoxa.Notifications.Application.Commands;
using eDoxa.Notifications.Application.IntegrationEvents;
using eDoxa.Notifications.Application.IntegrationEvents.Handlers;
using eDoxa.Notifications.Domain.AggregateModels;

using MediatR;

using Microsoft.VisualStudio.TestTools.UnitTesting;

using Moq;

namespace eDoxa.Notifications.Application.Tests.IntegrationEvents.Handlers
{
    [TestClass]
    public sealed class UserCreatedIntegrationEventHandlerTest
    {
        [TestMethod]
        public async Task Handle_SendCommandAsync_ShouldBeInvokedExactlyOneTime()
        {
            // Arrange
            var mockMediator = new Mock<IMediator>();

            mockMediator.Setup(mediator => mediator.Send(It.IsAny<CreateUserCommand>(), It.IsAny<CancellationToken>()))
                        .ReturnsAsync(It.IsAny<Unit>())
                        .Verifiable();

            // Act
            var handler = new UserCreatedIntegrationEventHandler(mockMediator.Object);

            // Assert
            await handler.Handle(new UserCreatedIntegrationEvent(new UserId()));

            mockMediator.Verify(mediator => mediator.Send(It.IsAny<CreateUserCommand>(), It.IsAny<CancellationToken>()), Times.Once);
        }
    }
}