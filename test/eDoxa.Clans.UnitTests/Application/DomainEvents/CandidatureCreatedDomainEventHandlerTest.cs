// Filename: CandidatureCreatedDomainEventHandlerTest.cs
// Date Created: 2019-12-26
// 
// ================================================
// Copyright © 2020, eDoxa. All rights reserved.

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
    public class CandidatureCreatedDomainEventHandlerTest : UnitTest
    {
        public CandidatureCreatedDomainEventHandlerTest(TestMapperFixture testMapper) : base(testMapper)
        {
        }

        [Fact]
        public async Task Handle()
        {
            // Arrange
            var mockClanService = new Mock<IClanService>();
            var mockServiceBus = new Mock<IServiceBusPublisher>();

            mockClanService.Setup(service => service.FindClanAsync(It.IsAny<ClanId>())).ReturnsAsync(new Clan("test", new UserId())).Verifiable();

            mockServiceBus.Setup(bus => bus.PublishAsync(It.IsAny<ClanCandidatureSentIntegrationEvent>())).Returns(Task.CompletedTask).Verifiable();

            var domainEventHandler = new CandidatureCreatedDomainEventHandler(mockClanService.Object, mockServiceBus.Object);

            var candidature = new Candidature(new UserId(), new ClanId());

            // Act
            await domainEventHandler.Handle(new CandidatureCreatedDomainEvent(candidature), CancellationToken.None);

            // Assert
            mockClanService.Verify(service => service.FindClanAsync(It.IsAny<ClanId>()), Times.Once);
            mockServiceBus.Verify(bus => bus.PublishAsync(It.IsAny<ClanCandidatureSentIntegrationEvent>()), Times.Once);
        }
    }
}
