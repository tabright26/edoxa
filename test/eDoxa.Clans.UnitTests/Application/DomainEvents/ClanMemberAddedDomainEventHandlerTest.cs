// Filename: ClanMemberAddedDomainEventHandlerTest.cs
// Date Created: 2019-12-26
// 
// ================================================
// Copyright © 2020, eDoxa. All rights reserved.

using System;
using System.Threading;
using System.Threading.Tasks;

using eDoxa.Clans.Api.Application.DomainEvents;
using eDoxa.Clans.Domain.DomainEvents;
using eDoxa.Clans.Domain.Models;
using eDoxa.Clans.TestHelper;
using eDoxa.Clans.TestHelper.Fixtures;
using eDoxa.Grpc.Protos.Clans.IntegrationEvents;
using eDoxa.Seedwork.Domain.Misc;
using eDoxa.Seedwork.TestHelper.Mocks;

using Moq;

using Xunit;

namespace eDoxa.Clans.UnitTests.Application.DomainEvents
{
    public class ClanMemberAddedDomainEventHandlerTest : UnitTest
    {
        public ClanMemberAddedDomainEventHandlerTest(TestMapperFixture testMapper) : base(testMapper)
        {
        }

        [Fact]
        public async Task Handle_ShouldNotThrowException()
        {
            // Arrange
            var mockLogger = new MockLogger<ClanMemberAddedDomainEventHandler>();

            TestMock.ServiceBusPublisher.Setup(service => service.PublishAsync(It.IsAny<ClanMemberAddedIntegrationEvent>()))
                .Returns(Task.CompletedTask)
                .Verifiable();

            TestMock.ClanService.Setup(clanService => clanService.FindClanAsync(It.IsAny<ClanId>()))
                .ReturnsAsync(new Clan("ClanName", new UserId()))
                .Verifiable();

            TestMock.CandidatureService.Setup(service => service.DeleteCandidaturesAsync(It.IsAny<UserId>())).Returns(Task.CompletedTask).Verifiable();

            TestMock.InvitationService.Setup(service => service.DeleteInvitationsAsync(It.IsAny<UserId>())).Returns(Task.CompletedTask).Verifiable();

            var domainEventHandler = new ClanMemberAddedDomainEventHandler(
                TestMock.ClanService.Object,
                TestMock.InvitationService.Object,
                TestMock.CandidatureService.Object,
                TestMock.ServiceBusPublisher.Object,
                mockLogger.Object);

            // Act
            await domainEventHandler.Handle(new ClanMemberAddedDomainEvent(new UserId(), new ClanId()), CancellationToken.None);

            // Assert
            TestMock.ServiceBusPublisher.Verify(service => service.PublishAsync(It.IsAny<ClanMemberAddedIntegrationEvent>()), Times.Once);
            TestMock.ClanService.Verify(clanService => clanService.FindClanAsync(It.IsAny<ClanId>()), Times.Once);
            TestMock.CandidatureService.Verify(service => service.DeleteCandidaturesAsync(It.IsAny<UserId>()), Times.Once);
            TestMock.InvitationService.Verify(service => service.DeleteInvitationsAsync(It.IsAny<UserId>()), Times.Once);
        }

        [Fact]
        public async Task Handle_ShouldThrowExceptions()
        {
            // Arrange
            var mockLogger = new MockLogger<ClanMemberAddedDomainEventHandler>();

            TestMock.ServiceBusPublisher.Setup(service => service.PublishAsync(It.IsAny<ClanMemberAddedIntegrationEvent>()))
                .Returns(Task.CompletedTask)
                .Verifiable();

            TestMock.CandidatureService.Setup(service => service.DeleteCandidaturesAsync(It.IsAny<UserId>())).ThrowsAsync(new Exception("test")).Verifiable();

            TestMock.ClanService.Setup(clanService => clanService.FindClanAsync(It.IsAny<ClanId>()))
                .ReturnsAsync(new Clan("ClanName", new UserId()))
                .Verifiable();

            TestMock.InvitationService.Setup(service => service.DeleteInvitationsAsync(It.IsAny<UserId>())).ThrowsAsync(new Exception("test")).Verifiable();

            var domainEventHandler = new ClanMemberAddedDomainEventHandler(
                TestMock.ClanService.Object,
                TestMock.InvitationService.Object,
                TestMock.CandidatureService.Object,
                TestMock.ServiceBusPublisher.Object,
                mockLogger.Object);

            // Act
            await domainEventHandler.Handle(new ClanMemberAddedDomainEvent(new UserId(), new ClanId()), CancellationToken.None);

            // Assert
            TestMock.ServiceBusPublisher.Verify(service => service.PublishAsync(It.IsAny<ClanMemberAddedIntegrationEvent>()), Times.Once);
            TestMock.ClanService.Verify(clanService => clanService.FindClanAsync(It.IsAny<ClanId>()), Times.Once);
            TestMock.CandidatureService.Verify(service => service.DeleteCandidaturesAsync(It.IsAny<UserId>()), Times.Once);
            TestMock.InvitationService.Verify(service => service.DeleteInvitationsAsync(It.IsAny<UserId>()), Times.Once);
            mockLogger.Verify(Times.Exactly(2));
        }
    }
}
