// Filename: UserCreatedDomainEventHandlerTest.cs
// Date Created: 2019-04-09
// 
// ============================================================
// Copyright © 2019, Francis Quenneville
// All rights reserved.
// 
// This file is subject to the terms and conditions defined in file 'LICENSE.md', which is part of
// this source code package.

using System.Threading.Tasks;

using eDoxa.Cashier.Application.DomainEventHandlers;
using eDoxa.Cashier.Application.IntegrationEvents;
using eDoxa.Cashier.Domain.AggregateModels.UserAggregate.DomainEvents;
using eDoxa.Cashier.Domain.Factories;
using eDoxa.ServiceBus;

using Microsoft.VisualStudio.TestTools.UnitTesting;

using Moq;

namespace eDoxa.Cashier.Application.Tests.DomainEventHandlers
{
    [TestClass]
    public sealed class UserCreatedDomainEventHandlerTest
    {
        private readonly UserAggregateFactory _userAggregateFactory = UserAggregateFactory.Instance;

        [TestMethod]
        public async Task Handle_PublishAsync_ShouldBeInvokedExactlyOneTime()
        {
            // Arrange
            var user = _userAggregateFactory.CreateUser();

            var integrationEvent = new UserCreatedDomainEvent(user.Id, user.CustomerId);

            var mockIntegrationEventService = new Mock<IIntegrationEventService>();

            mockIntegrationEventService.Setup(service => service.PublishAsync(It.IsAny<UserClaimAddedIntegrationEvent>()))
                                       .Returns(Task.CompletedTask)
                                       .Verifiable();

            var handler = new UserCreatedDomainEventHandler(mockIntegrationEventService.Object);

            // Act
            await handler.Handle(integrationEvent, default);

            // Assert
            mockIntegrationEventService.Verify(service => service.PublishAsync(It.IsAny<UserClaimAddedIntegrationEvent>()), Times.Once);
        }
    }
}