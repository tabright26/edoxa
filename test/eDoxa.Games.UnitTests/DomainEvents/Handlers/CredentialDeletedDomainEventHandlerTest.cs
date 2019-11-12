// Filename: CredentialDeletedDomainEventHandlerTest.cs
// Date Created: 2019-11-01
//
// ================================================
// Copyright © 2019, eDoxa. All rights reserved.

using System.Threading;
using System.Threading.Tasks;

using eDoxa.Games.Api.DomainEvents.Handlers;
using eDoxa.Games.Domain.AggregateModels.CredentialAggregate;
using eDoxa.Games.Domain.DomainEvents;
using eDoxa.Games.TestHelper;
using eDoxa.Games.TestHelper.Fixtures;
using eDoxa.Seedwork.Domain;
using eDoxa.Seedwork.Domain.Miscs;
using eDoxa.ServiceBus.Abstractions;

using Moq;

using Xunit;

namespace eDoxa.Games.UnitTests.DomainEvents.Handlers
{
    public sealed class CredentialDeletedDomainEventHandlerTest : UnitTest
    {
        public CredentialDeletedDomainEventHandlerTest(TestDataFixture testData, TestMapperFixture testMapper) : base(testData, testMapper)
        {
        }

        [Fact]
        public async Task Handle()
        {
            // Arrange
            var userId = new UserId();

            var domainEvent = new CredentialDeletedDomainEvent(new Credential(userId, Game.LeagueOfLegends, new PlayerId(), new UtcNowDateTimeProvider()));

            var mockServiceBusPublisher = new Mock<IServiceBusPublisher>();

            mockServiceBusPublisher
                .Setup(serviceBus => serviceBus.PublishAsync(It.IsAny<IIntegrationEvent>()))
                .Returns(Task.CompletedTask)
                .Verifiable();

            var credentialDeletedDomainEventHandler = new CredentialDeletedDomainEventHandler(mockServiceBusPublisher.Object);

            // Act
            await credentialDeletedDomainEventHandler.Handle(domainEvent, CancellationToken.None);

            // Assert
            mockServiceBusPublisher.Verify(serviceBus => serviceBus.PublishAsync(It.IsAny<IIntegrationEvent>()),
                Times.Once);
        }
    }
}
