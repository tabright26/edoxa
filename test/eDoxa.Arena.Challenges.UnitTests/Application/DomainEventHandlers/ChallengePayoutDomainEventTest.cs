// Filename: ChallengePayoutDomainEventTest.cs
// Date Created: 2019-06-09
// 
// ================================================
// Copyright © 2019, eDoxa. All rights reserved.
// 
// This file is subject to the terms and conditions
// defined in file 'LICENSE.md', which is part of
// this source code package.

using System.Threading.Tasks;
using eDoxa.Arena.Challenges.Api.Application.DomainEvents.Extensions;
using eDoxa.Arena.Challenges.Api.Application.DomainEvents.Handlers;
using eDoxa.Arena.Challenges.Api.Application.Fakers;
using eDoxa.Arena.Challenges.Api.IntegrationEvents;
using eDoxa.Arena.Challenges.Domain.AggregateModels.ChallengeAggregate;
using eDoxa.Arena.Challenges.Domain.DomainEvents;
using eDoxa.IntegrationEvents;

using Microsoft.VisualStudio.TestTools.UnitTesting;

using Moq;

namespace eDoxa.Arena.Challenges.UnitTests.Application.DomainEventHandlers
{
    [TestClass]
    public sealed class ChallengePayoutDomainEventTest
    {
        private ChallengeFaker _challengeFaker;
        private Mock<IIntegrationEventService> _mockIntegrationEventService;

        [TestInitialize]
        public void TestInitialize()
        {
            _challengeFaker = new ChallengeFaker(state: ChallengeState.Ended);
            _mockIntegrationEventService = new Mock<IIntegrationEventService>();
        }

        [TestMethod]
        public async Task HandleAsync_PayoutProcessedDomainEvent_ShouldBeCompletedTask()
        {
            // Arranges
            var challenge = _challengeFaker.Generate();
            
            _mockIntegrationEventService.Setup(mock => mock.PublishAsync(It.IsAny<ChallengePayoutIntegrationEvent>())).Returns(Task.CompletedTask).Verifiable();

            var handler = new ChallengePayoutDomainEventHandler(_mockIntegrationEventService.Object);

            // Act
            await handler.HandleAsync(new ChallengePayoutDomainEvent(challenge.Id, challenge.Payout.GetParticipantPrizes(challenge.Scoreboard)));

            // Assert
            _mockIntegrationEventService.Verify(mock => mock.PublishAsync(It.IsAny<ChallengePayoutIntegrationEvent>()), Times.Once);
        }
    }
}
