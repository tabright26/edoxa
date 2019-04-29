// Filename: ChallengeUserPrizesSnapshottedDomainEventHandlerTest.cs
// Date Created: 2019-03-22
// 
// ============================================================
// Copyright © 2019, Francis Quenneville
// All rights reserved.
// 
// This file is subject to the terms and conditions defined in file 'LICENSE.md', which is part of
// this source code package.

using System.Threading;
using System.Threading.Tasks;

using eDoxa.Challenges.Application.DomainEventHandlers;
using eDoxa.Challenges.Application.IntegrationEvents;
using eDoxa.Challenges.Domain.AggregateModels.ChallengeAggregate;
using eDoxa.Challenges.Domain.AggregateModels.ChallengeAggregate.DomainEvent;
using eDoxa.Challenges.Domain.Factories;
using eDoxa.ServiceBus;

using Microsoft.VisualStudio.TestTools.UnitTesting;

using Moq;

namespace eDoxa.Challenges.Application.Tests.DomainEventHandlers
{
    [TestClass]
    public sealed class ChallengePaidOffDomainEventHandlerTest
    {
        private static readonly ChallengeAggregateFactory ChallengeAggregateFactory = ChallengeAggregateFactory.Instance;

        [TestMethod]
        public async Task Handle_PublishAsync_ShouldBeInvokedExactlyOneTime()
        {
            // Arrange
            var challenge = ChallengeAggregateFactory.CreateChallenge(ChallengeState1.Closed);

            var userPrizes = challenge.Payout.Payoff(challenge.Scoreboard);

            var mockIntegrationEventService = new Mock<IIntegrationEventService>();

            mockIntegrationEventService.Setup(service => service.PublishAsync(It.IsAny<ChallengeUserPrizesSnapshottedIntegrationEvent>()))
                                       .Returns(Task.CompletedTask)
                                       .Verifiable();

            // Act
            var handler = new ChallengePaidOffDomainEventHandler(mockIntegrationEventService.Object);

            // Assert
            await handler.Handle(new ChallengePaidOffDomainEvent(challenge.Id, userPrizes), It.IsAny<CancellationToken>());

            mockIntegrationEventService.Verify(service => service.PublishAsync(It.IsAny<ChallengeUserPrizesSnapshottedIntegrationEvent>()), Times.Once);
        }
    }
}