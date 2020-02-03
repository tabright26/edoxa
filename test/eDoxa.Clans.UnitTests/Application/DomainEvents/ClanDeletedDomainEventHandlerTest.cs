// Filename: ClanDeletedDomainEventHandlerTest.cs
// Date Created: 2019-12-26
// 
// ================================================
// Copyright © 2020, eDoxa. All rights reserved.

using System;
using System.Threading;
using System.Threading.Tasks;

using eDoxa.Clans.Api.Application.DomainEvents;
using eDoxa.Clans.Domain.DomainEvents;
using eDoxa.Clans.TestHelper;
using eDoxa.Clans.TestHelper.Fixtures;
using eDoxa.Seedwork.Domain.Misc;
using eDoxa.Seedwork.TestHelper.Mocks;

using Moq;

using Xunit;

namespace eDoxa.Clans.UnitTests.Application.DomainEvents
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
            var mockLogger = new MockLogger<ClanDeletedDomainEventHandler>();

            TestMock.ClanService.Setup(service => service.DeleteLogoAsync(It.IsAny<ClanId>())).Returns(Task.CompletedTask).Verifiable();

            TestMock.CandidatureService.Setup(service => service.DeleteCandidaturesAsync(It.IsAny<ClanId>())).Returns(Task.CompletedTask).Verifiable();

            TestMock.InvitationService.Setup(service => service.DeleteInvitationsAsync(It.IsAny<ClanId>())).Returns(Task.CompletedTask).Verifiable();

            var domainEventHandler = new ClanDeletedDomainEventHandler(
                TestMock.ClanService.Object,
                TestMock.CandidatureService.Object,
                TestMock.InvitationService.Object,
                mockLogger.Object);

            // Act
            await domainEventHandler.Handle(new ClanDeletedDomainEvent(new ClanId()), CancellationToken.None);

            // Assert
            TestMock.ClanService.Verify(service => service.DeleteLogoAsync(It.IsAny<ClanId>()), Times.Once);
            TestMock.CandidatureService.Verify(service => service.DeleteCandidaturesAsync(It.IsAny<ClanId>()), Times.Once);
            TestMock.InvitationService.Verify(service => service.DeleteInvitationsAsync(It.IsAny<ClanId>()), Times.Once);
        }

        [Fact]
        public async Task Handle_ShouldThrowExceptions()
        {
            // Arrange
            var mockLogger = new MockLogger<ClanDeletedDomainEventHandler>();

            TestMock.ClanService.Setup(service => service.DeleteLogoAsync(It.IsAny<ClanId>())).Returns(Task.CompletedTask).Verifiable();

            TestMock.CandidatureService.Setup(service => service.DeleteCandidaturesAsync(It.IsAny<ClanId>())).ThrowsAsync(new Exception()).Verifiable();

            TestMock.InvitationService.Setup(service => service.DeleteInvitationsAsync(It.IsAny<ClanId>())).ThrowsAsync(new Exception()).Verifiable();

            var domainEventHandler = new ClanDeletedDomainEventHandler(
                TestMock.ClanService.Object,
                TestMock.CandidatureService.Object,
                TestMock.InvitationService.Object,
                mockLogger.Object);

            // Act
            await domainEventHandler.Handle(new ClanDeletedDomainEvent(new ClanId()), CancellationToken.None);

            // Assert
            TestMock.ClanService.Verify(service => service.DeleteLogoAsync(It.IsAny<ClanId>()), Times.Once);
            TestMock.CandidatureService.Verify(service => service.DeleteCandidaturesAsync(It.IsAny<ClanId>()), Times.Once);
            TestMock.InvitationService.Verify(service => service.DeleteInvitationsAsync(It.IsAny<ClanId>()), Times.Once);

            mockLogger.Verify(Times.Exactly(2));
        }
    }
}
