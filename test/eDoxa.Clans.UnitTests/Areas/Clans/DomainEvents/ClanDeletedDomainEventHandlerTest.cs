// Filename: CandidatureAcceptedDomainEventHandler.cs
// Date Created: 2019-10-01
//
// ================================================
// Copyright © 2019, eDoxa. All rights reserved.

using System;
using System.Threading;
using System.Threading.Tasks;

using eDoxa.Clans.Api.Areas.Clans.DomainEvents;
using eDoxa.Clans.Api.Areas.Clans.Services.Abstractions;
using eDoxa.Clans.Domain.DomainEvents;
using eDoxa.Clans.TestHelper;
using eDoxa.Clans.TestHelper.Fixtures;
using eDoxa.Seedwork.Domain.Miscs;
using eDoxa.Seedwork.TestHelper.Mocks;

using Moq;

using Xunit;

namespace eDoxa.Clans.UnitTests.Areas.Clans.DomainEvents
{
    public class ClanDeletedDomainEventHandlerTest : UnitTest
    {
        public ClanDeletedDomainEventHandlerTest(TestMapperFixture testMapper) : base(testMapper)
        {
        }

        [Fact]
        public async Task Handle_ShouldNotThrowException()
        {
            // Arrange
            var mockClanService = new Mock<IClanService>();
            var mockCandidationService = new Mock<ICandidatureService>();
            var mockInvitationService = new Mock<IInvitationService>();
            var mockLogger = new MockLogger<ClanDeletedDomainEventHandler>();


            mockClanService.Setup(service => service.DeleteLogoAsync(It.IsAny<ClanId>()))
                .Returns(Task.CompletedTask)
                .Verifiable();

            mockCandidationService.Setup(service => service.DeleteCandidaturesAsync(It.IsAny<ClanId>()))
                .Returns(Task.CompletedTask)
                .Verifiable();

            mockInvitationService.Setup(service => service.DeleteInvitationsAsync(It.IsAny<ClanId>()))
                .Returns(Task.CompletedTask)
                .Verifiable();

            var domainEventHandler = new ClanDeletedDomainEventHandler(mockClanService.Object, mockCandidationService.Object, mockInvitationService.Object, mockLogger.Object);

            // Act
            await domainEventHandler.Handle(new ClanDeletedDomainEvent(new ClanId()), CancellationToken.None);

            // Assert
            mockClanService.Verify(service => service.DeleteLogoAsync(It.IsAny<ClanId>()), Times.Once);
            mockCandidationService.Verify(service => service.DeleteCandidaturesAsync(It.IsAny<ClanId>()), Times.Once);
            mockInvitationService.Verify(service => service.DeleteInvitationsAsync(It.IsAny<ClanId>()), Times.Once);
        }

        [Fact]
        public async Task Handle_ShouldThrowExceptions()
        {
            // Arrange
            var mockClanService = new Mock<IClanService>();
            var mockCandidationService = new Mock<ICandidatureService>();
            var mockInvitationService = new Mock<IInvitationService>();
            var mockLogger = new MockLogger<ClanDeletedDomainEventHandler>();


            mockClanService.Setup(service => service.DeleteLogoAsync(It.IsAny<ClanId>()))
                .Returns(Task.CompletedTask)
                .Verifiable();

            mockCandidationService.Setup(service => service.DeleteCandidaturesAsync(It.IsAny<ClanId>()))
                .ThrowsAsync(new Exception("test"))
                .Verifiable();

            mockInvitationService.Setup(service => service.DeleteInvitationsAsync(It.IsAny<ClanId>()))
                .ThrowsAsync(new Exception("test"))
                .Verifiable();

            var domainEventHandler = new ClanDeletedDomainEventHandler(mockClanService.Object, mockCandidationService.Object, mockInvitationService.Object, mockLogger.Object);

            // Act
            await domainEventHandler.Handle(new ClanDeletedDomainEvent(new ClanId()), CancellationToken.None);

            // Assert
            mockClanService.Verify(service => service.DeleteLogoAsync(It.IsAny<ClanId>()), Times.Once);
            mockCandidationService.Verify(service => service.DeleteCandidaturesAsync(It.IsAny<ClanId>()), Times.Once);
            mockInvitationService.Verify(service => service.DeleteInvitationsAsync(It.IsAny<ClanId>()), Times.Once);

            mockLogger.Verify(Times.Exactly(2));
        }
    }
}
