// Filename: RoleClaimAddedIntegrationEventHandlerTest.cs
// Date Created: 2019-09-16
// 
// ================================================
// Copyright © 2019, eDoxa. All rights reserved.

using System;
using System.Threading.Tasks;

using eDoxa.Grpc.Protos.Notifications.IntegrationEvents;
using eDoxa.Notifications.Api.IntegrationEvents;
using eDoxa.Notifications.Api.IntegrationEvents.Handlers;
using eDoxa.Notifications.Domain.Services;
using eDoxa.Seedwork.Domain.Misc;

using Moq;

using Xunit;

namespace eDoxa.Notifications.UnitTests.IntegrationEvents.Handlers
{
    public sealed class RoleClaimAddedIntegrationEventHandlerTest
    {
        [Fact]
        public async Task RoleClaimAddedIntegrationEvent_ShouldBeCompletedTask()
        {
            // Arrange
            var mockEmailService = new Mock<IEmailService>();

            mockEmailService.Setup(emailService => emailService.SendEmailAsync(It.IsAny<string>(), It.IsAny<string>(), It.IsAny<string>())).Returns(Task.CompletedTask).Verifiable();

            var handler = new EmailSentIntegrationEventHandler(mockEmailService.Object);

            var integrationEvent = new EmailSentIntegrationEvent
            {
                UserId = Guid.NewGuid().ToString(),
                Email =  "gabriel@edoxa.gg",
                Subject = "mange Dla Baloney",
                HtmlMessage = "Mah man"
            };

            // Act
            await handler.HandleAsync(integrationEvent);

            // Assert
            mockEmailService.Verify(emailService => emailService.SendEmailAsync(It.IsAny<string>(), It.IsAny<string>(), It.IsAny<string>()), Times.Once);
        }
    }
}
