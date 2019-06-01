﻿// Filename: ChallengePayoutDomainEventTest.cs
// Date Created: 2019-05-29
// 
// ================================================
// Copyright © 2019, eDoxa. All rights reserved.
// 
// This file is subject to the terms and conditions
// defined in file 'LICENSE.md', which is part of
// this source code package.

using System.Threading.Tasks;

using eDoxa.Arena.Challenges.Application.DomainEventHandlers;
using eDoxa.Arena.Challenges.Application.IntegrationEvents;
using eDoxa.Arena.Challenges.Domain.AggregateModels.ChallengeAggregate;
using eDoxa.Arena.Challenges.Domain.DomainEvents;
using eDoxa.Arena.Challenges.Tests.Utilities.Fakes;
using eDoxa.Seedwork.Application.Extensions;
using eDoxa.ServiceBus;

using Microsoft.VisualStudio.TestTools.UnitTesting;

using Moq;

namespace eDoxa.Arena.Challenges.Tests.DomainEventHandlers
{
    [TestClass]
    public sealed class ChallengePayoutDomainEventTest
    {
        private static readonly FakeChallengeFactory FakeChallengeFactory = FakeChallengeFactory.Instance;
        private Mock<IIntegrationEventService> _mockIntegrationEventService;

        [TestInitialize]
        public void TestInitialize()
        {
            _mockIntegrationEventService = new Mock<IIntegrationEventService>();
        }

        [TestMethod]
        public async Task HandleAsync_PayoutProcessedDomainEvent_ShouldBeCompletedTask()
        {
            // Arrange
            var challenge = FakeChallengeFactory.CreateChallenge(ChallengeState.Closed);

            var userPrizes = challenge.Payout.GetParticipantPrizes(challenge.Scoreboard);

            _mockIntegrationEventService.Setup(mock => mock.PublishAsync(It.IsAny<ChallengePayoutIntegrationEvent>())).Returns(Task.CompletedTask).Verifiable();

            var handler = new ChallengePayoutDomainEventHandler(_mockIntegrationEventService.Object);

            // Act
            await handler.HandleAsync(new ChallengePayoutDomainEvent(challenge.Id, userPrizes));

            // Assert
            _mockIntegrationEventService.Verify(mock => mock.PublishAsync(It.IsAny<ChallengePayoutIntegrationEvent>()), Times.Once);
        }
    }
}