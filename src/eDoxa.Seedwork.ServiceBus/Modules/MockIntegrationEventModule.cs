// Filename: MockIntegrationEventModule.cs
// Date Created: --
// 
// ================================================
// Copyright © 2019, eDoxa. All rights reserved.

using System.Threading.Tasks;

using Autofac;

using Moq;

namespace eDoxa.Seedwork.ServiceBus.Modules
{
    public sealed class MockIntegrationEventModule : Module
    {
        protected override void Load(ContainerBuilder builder)
        {
            var mockIntegrationEventPublisher = new Mock<IIntegrationEventPublisher>();

            mockIntegrationEventPublisher.Setup(integrationEventService => integrationEventService.PublishAsync(It.IsAny<IntegrationEvent>()))
                .Returns(Task.CompletedTask);

            builder.RegisterInstance(mockIntegrationEventPublisher.Object).As<IIntegrationEventPublisher>().SingleInstance();
        }
    }
}
