// Filename: CredentialDeletedDomainEventHandlerTest.cs
// Date Created: 2019-12-26
// 
// ================================================
// Copyright © 2020, eDoxa. All rights reserved.

using System.Threading;
using System.Threading.Tasks;

using eDoxa.Games.Api.Application.DomainEvents.Handlers;
using eDoxa.Games.Domain.AggregateModels.GameAggregate;
using eDoxa.Games.Domain.DomainEvents;
using eDoxa.Games.TestHelper;
using eDoxa.Games.TestHelper.Fixtures;
using eDoxa.Grpc.Protos.Games.IntegrationEvents;
using eDoxa.Seedwork.Domain;
using eDoxa.Seedwork.Domain.Misc;

using Moq;

using Xunit;

namespace eDoxa.Games.UnitTests.Application.DomainEvents.Handlers
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

            var domainEvent = new CredentialDeletedDomainEvent(
                new Credential(
                    userId,
                    Game.LeagueOfLegends,
                    new PlayerId(),
                    new UtcNowDateTimeProvider()));

            TestMock.ServiceBusPublisher.Setup(serviceBus => serviceBus.PublishAsync(It.IsAny<UserGameCredentialRemovedIntegrationEvent>()))
                .Returns(Task.CompletedTask)
                .Verifiable();

            var credentialDeletedDomainEventHandler = new CredentialDeletedDomainEventHandler(TestMock.ServiceBusPublisher.Object);

            // Act
            await credentialDeletedDomainEventHandler.Handle(domainEvent, CancellationToken.None);

            // Assert
            TestMock.ServiceBusPublisher.Verify(serviceBus => serviceBus.PublishAsync(It.IsAny<UserGameCredentialRemovedIntegrationEvent>()), Times.Once);
        }
    }
}
