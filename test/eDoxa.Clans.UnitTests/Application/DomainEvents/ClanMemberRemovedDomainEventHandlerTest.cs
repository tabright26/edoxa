// Filename: ClanMemberRemovedDomainEventHandlerTest.cs
// Date Created: 2019-12-26
// 
// ================================================
// Copyright © 2020, eDoxa. All rights reserved.

using System.Threading;
using System.Threading.Tasks;

using eDoxa.Clans.Api.Application.DomainEvents;
using eDoxa.Clans.Domain.DomainEvents;
using eDoxa.Clans.Domain.Models;
using eDoxa.Clans.TestHelper;
using eDoxa.Clans.TestHelper.Fixtures;
using eDoxa.Grpc.Protos.Clans.IntegrationEvents;
using eDoxa.Seedwork.Domain.Misc;

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
            TestMock.ClanService.Setup(clanService => clanService.FindClanAsync(It.IsAny<ClanId>()))
                .ReturnsAsync(new Clan("ClanName", new UserId()))
                .Verifiable();

            TestMock.ServiceBusPublisher.Setup(service => service.PublishAsync(It.IsAny<ClanMemberRemovedIntegrationEvent>()))
                .Returns(Task.CompletedTask)
                .Verifiable();

            var domainEventHandler = new ClanMemberRemovedDomainEventHandler(TestMock.ClanService.Object, TestMock.ServiceBusPublisher.Object);

            // Act
            await domainEventHandler.Handle(new ClanMemberRemovedDomainEvent(new UserId(), new ClanId()), CancellationToken.None);

            // Assert
            TestMock.ClanService.Verify(clanService => clanService.FindClanAsync(It.IsAny<ClanId>()), Times.Once);

            TestMock.ServiceBusPublisher.Verify(service => service.PublishAsync(It.IsAny<ClanMemberRemovedIntegrationEvent>()), Times.Once);
        }
    }
}
