// Filename: CredentialCreatedDomainEventHandlerTest.cs
// Date Created: 2019-11-01
//
// ================================================
// Copyright © 2019, eDoxa. All rights reserved.

using System.Threading;
using System.Threading.Tasks;

using eDoxa.Games.Abstractions.Factories;
using eDoxa.Games.Api.DomainEvents.Handlers;
using eDoxa.Games.Api.IntegrationEvents.Extensions;
using eDoxa.Games.Domain.AggregateModels.AuthFactorAggregate;
using eDoxa.Games.Domain.AggregateModels.CredentialAggregate;
using eDoxa.Games.Domain.DomainEvents;
using eDoxa.Games.Domain.Repositories;
using eDoxa.Games.Services;
using eDoxa.Games.TestHelper;
using eDoxa.Games.TestHelper.Fixtures;
using eDoxa.Seedwork.Domain;
using eDoxa.Seedwork.Domain.Miscs;
using eDoxa.ServiceBus.Abstractions;

using FluentValidation.Validators;

using Moq;

using Xunit;

namespace eDoxa.Games.UnitTests.DomainEvents.Handlers
{
    public sealed class CredentialCreatedDomainEventHandlerTest : UnitTest
    {
        public CredentialCreatedDomainEventHandlerTest(TestDataFixture testData, TestMapperFixture testMapper) : base(testData, testMapper)
        {
        }

        [Fact]
        public async Task Handle()
        {
            // Arrange
            var userId = new UserId();

            var domainEvent = new CredentialCreatedDomainEvent(new Credential(userId, Game.LeagueOfLegends, new PlayerId(), new UtcNowDateTimeProvider()));

            var mockServiceBusPublisher = new Mock<IServiceBusPublisher>();

            mockServiceBusPublisher
                .Setup(serviceBus => serviceBus.PublishAsync(It.IsAny<IIntegrationEvent>()))
                .Returns(Task.CompletedTask)
                .Verifiable();

            var credentialCreatedDomainEventHandler = new CredentialCreatedDomainEventHandler(mockServiceBusPublisher.Object);

            // Act
            await credentialCreatedDomainEventHandler.Handle(domainEvent, CancellationToken.None);

            // Assert
            mockServiceBusPublisher.Verify(serviceBus => serviceBus.PublishAsync(It.IsAny<IIntegrationEvent>()),
                Times.Once);
        }
    }
}
