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
    public class ClanMemberRemovedDomainEventHandlerTest : UnitTest
    {
        public ClanMemberRemovedDomainEventHandlerTest(TestMapperFixture testMapper) : base(testMapper)
        {
        }

        [Fact]
        public async Task Handle()
        {
            // Arrange
            var mockClanService = new Mock<IClanService>();

            mockClanService.Setup(clanService => clanService.FindClanAsync(It.IsAny<ClanId>())).ReturnsAsync(new Clan("ClanName", new UserId())).Verifiable();

            var mockServiceBus = new Mock<IServiceBusPublisher>();

            mockServiceBus.Setup(service => service.PublishAsync(It.IsAny<ClanMemberRemovedIntegrationEvent>()))
                .Returns(Task.CompletedTask)
                .Verifiable();

            var domainEventHandler = new ClanMemberRemovedDomainEventHandler(mockClanService.Object, mockServiceBus.Object);

            // Act
            await domainEventHandler.Handle(new ClanMemberRemovedDomainEvent(new UserId(), new ClanId()), CancellationToken.None);

            // Assert
            mockClanService.Verify(clanService => clanService.FindClanAsync(It.IsAny<ClanId>()), Times.Once);

            mockServiceBus.Verify(service => service.PublishAsync(It.IsAny<ClanMemberRemovedIntegrationEvent>()), Times.Once);
        }
    }
}
