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
using eDoxa.Seedwork.Domain.Misc;

using Moq;

using Xunit;

using IMemberInfo = eDoxa.Clans.Domain.Models.IMemberInfo;

namespace eDoxa.Clans.UnitTests.Areas.Clans.DomainEvents
{
    public class CandidatureAcceptedDomainEventHandlerTest : UnitTest
    {
        public CandidatureAcceptedDomainEventHandlerTest(TestMapperFixture testMapper) : base(testMapper)
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

            var domainEventHandler = new CandidatureAcceptedDomainEventHandler(mockClanService.Object);

            var candidature = new Candidature(new UserId(), new ClanId());

            // Act
            await domainEventHandler.Handle(new CandidatureAcceptedDomainEvent(candidature), CancellationToken.None);

            // Assert
            mockClanService.Verify(service => service.AddMemberToClanAsync(It.IsAny<ClanId>(), It.IsAny<IMemberInfo>()), Times.Once);
        }
    }
}
