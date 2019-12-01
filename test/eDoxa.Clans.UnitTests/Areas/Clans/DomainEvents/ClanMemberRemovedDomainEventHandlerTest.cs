// Filename: CandidatureAcceptedDomainEventHandler.cs
// Date Created: 2019-10-01
//
// ================================================
// Copyright © 2019, eDoxa. All rights reserved.

using System.Threading;
using System.Threading.Tasks;

using eDoxa.Clans.Api.Areas.Clans.DomainEvents;
using eDoxa.Clans.Domain.DomainEvents;
using eDoxa.Clans.TestHelper;
using eDoxa.Clans.TestHelper.Fixtures;
using eDoxa.Seedwork.Domain.Misc;
using eDoxa.ServiceBus.Abstractions;

using Moq;

using Xunit;

namespace eDoxa.Clans.UnitTests.Areas.Clans.DomainEvents
{
    public class ClanMemberRemovedDomainEventHandlerTest : UnitTest
    {
        public ClanMemberRemovedDomainEventHandlerTest(TestMapperFixture testMapper) : base(testMapper)
        {
        }

        [Fact]
        public async Task Handle()
        {
            // Arrange
            var mockServiceBus = new Mock<IServiceBusPublisher>();

            mockServiceBus.Setup(service => service.PublishAsync(It.IsAny<IIntegrationEvent>()))
                .Returns(Task.CompletedTask)
                .Verifiable();

            var domainEventHandler = new ClanMemberRemovedDomainEventHandler(mockServiceBus.Object);

            // Act
            await domainEventHandler.Handle(new ClanMemberRemovedDomainEvent(new UserId(), new ClanId()), CancellationToken.None);

            // Assert
            mockServiceBus.Verify(service => service.PublishAsync(It.IsAny<IIntegrationEvent>()), Times.Once);
        }
    }
}
