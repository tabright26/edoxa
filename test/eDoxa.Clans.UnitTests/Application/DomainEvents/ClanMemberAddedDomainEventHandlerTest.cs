// Filename: CandidatureAcceptedDomainEventHandler.cs
// Date Created: 2019-10-01
//
// ================================================
// Copyright © 2019, eDoxa. All rights reserved.

using System;
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
using eDoxa.Seedwork.TestHelper.Mocks;
using eDoxa.ServiceBus.Abstractions;

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
            var mockServiceBus = new Mock<IServiceBusPublisher>();
            var mockClanService = new Mock<IClanService>();
            var mockCandidationService = new Mock<ICandidatureService>();
            var mockInvitationService = new Mock<IInvitationService>();
            var mockLogger = new MockLogger<ClanMemberAddedDomainEventHandler>();

            mockServiceBus.Setup(service => service.PublishAsync(It.IsAny<ClanMemberAddedIntegrationEvent>()))
                .Returns(Task.CompletedTask)
                .Verifiable();

            mockClanService.Setup(clanService => clanService.FindClanAsync(It.IsAny<ClanId>())).ReturnsAsync(new Clan("ClanName", new UserId())).Verifiable();

            mockCandidationService.Setup(service => service.DeleteCandidaturesAsync(It.IsAny<UserId>()))
                .Returns(Task.CompletedTask)
                .Verifiable();

            mockInvitationService.Setup(service => service.DeleteInvitationsAsync(It.IsAny<UserId>()))
                .Returns(Task.CompletedTask)
                .Verifiable();

            var domainEventHandler = new ClanMemberAddedDomainEventHandler(mockClanService.Object, mockInvitationService.Object, mockCandidationService.Object, mockServiceBus.Object, mockLogger.Object);

            // Act
            await domainEventHandler.Handle(new ClanMemberAddedDomainEvent(new UserId(), new ClanId()), CancellationToken.None);

            // Assert
            mockServiceBus.Verify(service => service.PublishAsync(It.IsAny<ClanMemberAddedIntegrationEvent>()), Times.Once);
            mockClanService.Verify(clanService => clanService.FindClanAsync(It.IsAny<ClanId>()), Times.Once);
            mockCandidationService.Verify(service => service.DeleteCandidaturesAsync(It.IsAny<UserId>()), Times.Once);
            mockInvitationService.Verify(service => service.DeleteInvitationsAsync(It.IsAny<UserId>()), Times.Once);
        }

        [Fact]
        public async Task Handle_ShouldThrowExceptions()
        {
            // Arrange
            var mockServiceBus = new Mock<IServiceBusPublisher>();
            var mockClanService = new Mock<IClanService>();
            var mockCandidationService = new Mock<ICandidatureService>();
            var mockInvitationService = new Mock<IInvitationService>();
            var mockLogger = new MockLogger<ClanMemberAddedDomainEventHandler>();

            mockServiceBus.Setup(service => service.PublishAsync(It.IsAny<ClanMemberAddedIntegrationEvent>()))
                .Returns(Task.CompletedTask)
                .Verifiable();

            mockCandidationService.Setup(service => service.DeleteCandidaturesAsync(It.IsAny<UserId>()))
                .ThrowsAsync(new Exception("test"))
                .Verifiable();

            mockClanService.Setup(clanService => clanService.FindClanAsync(It.IsAny<ClanId>())).ReturnsAsync(new Clan("ClanName", new UserId())).Verifiable();

            mockInvitationService.Setup(service => service.DeleteInvitationsAsync(It.IsAny<UserId>()))
                .ThrowsAsync(new Exception("test"))
                .Verifiable();

            var domainEventHandler = new ClanMemberAddedDomainEventHandler(mockClanService.Object, mockInvitationService.Object, mockCandidationService.Object, mockServiceBus.Object, mockLogger.Object);

            // Act
            await domainEventHandler.Handle(new ClanMemberAddedDomainEvent(new UserId(), new ClanId()), CancellationToken.None);

            // Assert
            mockServiceBus.Verify(service => service.PublishAsync(It.IsAny<ClanMemberAddedIntegrationEvent>()), Times.Once);
            mockClanService.Verify(clanService => clanService.FindClanAsync(It.IsAny<ClanId>()), Times.Once);
            mockCandidationService.Verify(service => service.DeleteCandidaturesAsync(It.IsAny<UserId>()), Times.Once);
            mockInvitationService.Verify(service => service.DeleteInvitationsAsync(It.IsAny<UserId>()), Times.Once);
            mockLogger.Verify(Times.Exactly(2));
        }
    }
}
