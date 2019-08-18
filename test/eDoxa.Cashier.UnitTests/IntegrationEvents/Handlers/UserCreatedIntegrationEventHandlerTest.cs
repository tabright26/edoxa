// Filename: UserCreatedIntegrationEventHandlerTest.cs
// Date Created: 2019-06-25
// 
// ================================================
// Copyright © 2019, eDoxa. All rights reserved.

using System;
using System.Threading;
using System.Threading.Tasks;

using eDoxa.Cashier.Api.Application.Requests;
using eDoxa.Cashier.Api.IntegrationEvents;
using eDoxa.Cashier.Api.IntegrationEvents.Handlers;

using MediatR;

using Microsoft.VisualStudio.TestTools.UnitTesting;

using Moq;

namespace eDoxa.Cashier.UnitTests.IntegrationEvents.Handlers
{
    [TestClass]
    public sealed class UserCreatedIntegrationEventHandlerTest
    {
        [TestMethod]
        public async Task UserCreatedIntegrationEvent_ShouldBeCompletedTask()
        {
            // Arrange
            var mockMediator = new Mock<IMediator>();

            mockMediator.Setup(mediator => mediator.Send(It.IsAny<CreateUserRequest>(), It.IsAny<CancellationToken>())).Returns(Unit.Task).Verifiable();

            var handler = new UserCreatedIntegrationEventHandler(mockMediator.Object);

            var integrationEvent = new UserCreatedIntegrationEvent(Guid.NewGuid());

            // Act
            await handler.HandleAsync(integrationEvent);

            // Assert
            mockMediator.Verify(mediator => mediator.Send(It.IsAny<CreateUserRequest>(), It.IsAny<CancellationToken>()), Times.Once);
        }
    }
}
