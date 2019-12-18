// Filename: CandidatureAcceptedDomainEventHandler.cs
// Date Created: 2019-10-01
//
// ================================================
// Copyright © 2019, eDoxa. All rights reserved.

using System.Threading;
using System.Threading.Tasks;

using eDoxa.Clans.Api.Application.DomainEvents;
using eDoxa.Clans.Domain.DomainEvents;
using eDoxa.Clans.Domain.Models;
using eDoxa.Clans.Domain.Services;
using eDoxa.Clans.TestHelper;
using eDoxa.Clans.TestHelper.Fixtures;
using eDoxa.Grpc.Protos.Clans.IntegrationEvents;
using eDoxa.Seedwork.Domain.Misc;
using eDoxa.ServiceBus.Abstractions;

using Moq;

using Xunit;

namespace eDoxa.Clans.UnitTests.Application.DomainEvents
{
    public class InvitationCreatedDomainEventHandlerTest : UnitTest
    {
        public InvitationCreatedDomainEventHandlerTest(TestMapperFixture testMapper) : base(testMapper)
        {
        }

        [Fact]
        public async Task Handle()
        {
            // Arrange
            var mockClanService = new Mock<IClanService>();
            var mockServiceBus = new Mock<IServiceBusPublisher>();

            mockClanService.Setup(service => service.FindClanAsync(It.IsAny<ClanId>()))
                .ReturnsAsync(new Clan("test", new UserId()))
                .Verifiable();

            mockServiceBus.Setup(bus => bus.PublishAsync(It.IsAny<ClanInvitationSentIntegrationEvent>()))
                .Returns(Task.CompletedTask)
                .Verifiable();

            var domainEventHandler = new InvitationCreatedDomainEventHandler(mockClanService.Object, mockServiceBus.Object);
            var invitation = new Invitation(new UserId(), new ClanId());

            // Act
            await domainEventHandler.Handle(new InvitationCreatedDomainEvent(invitation), CancellationToken.None);

            // Assert
            mockClanService.Verify(service => service.FindClanAsync(It.IsAny<ClanId>()), Times.Once);
            mockServiceBus.Verify(bus => bus.PublishAsync(It.IsAny<ClanInvitationSentIntegrationEvent>()), Times.Once);
        }
    }
}
