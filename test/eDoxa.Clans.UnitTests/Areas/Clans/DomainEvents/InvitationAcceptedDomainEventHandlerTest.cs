// Filename: CandidatureAcceptedDomainEventHandler.cs
// Date Created: 2019-10-01
//
// ================================================
// Copyright © 2019, eDoxa. All rights reserved.

using System.Threading;
using System.Threading.Tasks;

using eDoxa.Clans.Api.Areas.Clans.DomainEvents;
using eDoxa.Clans.Api.Areas.Clans.Services.Abstractions;
using eDoxa.Clans.Domain.DomainEvents;
using eDoxa.Clans.Domain.Models;
using eDoxa.Clans.TestHelper;
using eDoxa.Clans.TestHelper.Fixtures;
using eDoxa.Seedwork.Domain.Misc;

using Moq;

using Xunit;

using IMemberInfo = eDoxa.Clans.Domain.Models.IMemberInfo;

namespace eDoxa.Clans.UnitTests.Areas.Clans.DomainEvents
{
    public class InvitationAcceptedDomainEventHandlerTest : UnitTest
    {
        public InvitationAcceptedDomainEventHandlerTest(TestMapperFixture testMapper) : base(testMapper)
        {
        }

        [Fact]
        public async Task Handle()
        {
            // Arrange
            var mockClanService = new Mock<IClanService>();

            mockClanService.Setup(service => service.AddMemberToClanAsync(It.IsAny<ClanId>(), It.IsAny<IMemberInfo>()))
                .Returns(Task.CompletedTask)
                .Verifiable();

            var domainEventHandler = new InvitationAcceptedDomainEventHandler(mockClanService.Object);

            var invitation = new Invitation(new UserId(), new ClanId());

            // Act
            await domainEventHandler.Handle(new InvitationAcceptedDomainEvent(invitation), CancellationToken.None);

            // Assert
            mockClanService.Verify(service => service.AddMemberToClanAsync(It.IsAny<ClanId>(), It.IsAny<IMemberInfo>()), Times.Once);
        }
    }
}
