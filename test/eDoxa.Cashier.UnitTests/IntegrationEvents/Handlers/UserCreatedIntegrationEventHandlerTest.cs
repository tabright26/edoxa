﻿// Filename: UserCreatedIntegrationEventHandlerTest.cs
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
using eDoxa.Seedwork.Testing.TestConstructor;

using MediatR;

using Microsoft.VisualStudio.TestTools.UnitTesting;

using Moq;

namespace eDoxa.Cashier.UnitTests.IntegrationEvents.Handlers
{
    [TestClass]
    public sealed class UserCreatedIntegrationEventHandlerTest
    {
        private Mock<IMediator> _mockMediator;

        [TestInitialize]
        public void TestInitialize()
        {
            _mockMediator = new Mock<IMediator>();
        }

        [TestMethod]
        public void Constructor_Tests()
        {
            TestConstructor<UserCreatedIntegrationEventHandler>.ForParameters(typeof(IMediator)).WithClassName("UserCreatedIntegrationEventHandler").Assert();
        }

        [TestMethod]
        public async Task UserCreatedIntegrationEvent_ShouldBeCompletedTask()
        {
            // Arrange
            var integrationEvent = new UserCreatedIntegrationEvent(Guid.NewGuid());

            _mockMediator.Setup(mock => mock.Send(It.IsAny<CreateUserRequest>(), It.IsAny<CancellationToken>())).Returns(Unit.Task).Verifiable();

            var handler = new UserCreatedIntegrationEventHandler(_mockMediator.Object);

            // Act
            await handler.HandleAsync(integrationEvent);

            // Assert
            _mockMediator.Verify(mock => mock.Send(It.IsAny<CreateUserRequest>(), It.IsAny<CancellationToken>()), Times.Once);
        }
    }
}
