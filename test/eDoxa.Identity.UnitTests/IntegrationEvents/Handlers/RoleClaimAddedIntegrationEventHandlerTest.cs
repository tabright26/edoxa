// Filename: UserCreatedIntegrationEventHandlerTest.cs
// Date Created: 2019-06-25
// 
// ================================================
// Copyright © 2019, eDoxa. All rights reserved.

using System;
using System.Threading;
using System.Threading.Tasks;

using eDoxa.Identity.Api.Areas.Identity.Services;
using eDoxa.Identity.Api.IntegrationEvents;
using eDoxa.Identity.Api.IntegrationEvents.Handlers;

using MediatR;

using Microsoft.VisualStudio.TestTools.UnitTesting;

using Moq;

namespace eDoxa.Cashier.UnitTests.IntegrationEvents.Handlers
{
    [TestClass]
    public sealed class RoleClaimAddedIntegrationEventHandlerTest
    {
        [TestMethod]
        public async Task RoleClaimAddedIntegrationEvent_ShouldBeCompletedTask()
        {
            // Arrange
            var mockRole = new Mock<RoleManager>();

            mockRole.Setup(role => role);

            var handler = new RoleClaimAddedIntegrationEventHandler(mockRole.Object);

            var integrationEvent = new RoleClaimAddedIntegrationEvent("admin", "test", "allow");

            // Act
            await handler.HandleAsync(integrationEvent);

            // Assert
            mockRole.Verify(role => role);
        }
    }
}
