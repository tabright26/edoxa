﻿// Filename: PayoutProcessedDomainEventHandlerTest.cs
// Date Created: 2019-05-28
// 
// ================================================
// Copyright © 2019, eDoxa. All rights reserved.
// 
// This file is subject to the terms and conditions
// defined in file 'LICENSE.md', which is part of
// this source code package.

namespace eDoxa.Arena.Challenges.Tests.DomainEventHandlers
{
    //[TestClass]
    public sealed class PayoutProcessedDomainEventHandlerTest
    {
        //private static readonly ChallengeAggregateFactory ChallengeAggregateFactory = ChallengeAggregateFactory.Instance;
        //private Mock<IIntegrationEventService> _mockIntegrationEventService;

        //[TestInitialize]
        //public void TestInitialize()
        //{
        //    _mockIntegrationEventService = new Mock<IIntegrationEventService>();
        //}

        //[TestMethod]
        //public async Task HandleAsync_PayoutProcessedDomainEvent_ShouldBeCompletedTask()
        //{
        //    // Arrange
        //    var challenge = ChallengeAggregateFactory.CreateChallenge(ChallengeState1.Closed);

        //    var userPrizes = challenge.Payout.Payoff(challenge.Scoreboard);

        //    _mockIntegrationEventService.Setup(mock => mock.PublishAsync(It.IsAny<ChallengePayoutProcessedIntegrationEvent>()))
        //        .Returns(Task.CompletedTask)
        //        .Verifiable();

        //    var handler = new PayoutProcessedDomainEventHandler(_mockIntegrationEventService.Object);

        //    // Act
        //    await handler.HandleAsync(new PayoutProcessedDomainEvent(challenge.Id, userPrizes));

        //    // Assert
        //    _mockIntegrationEventService.Verify(mock => mock.PublishAsync(It.IsAny<ChallengePayoutProcessedIntegrationEvent>()), Times.Once);
        //}
    }
}