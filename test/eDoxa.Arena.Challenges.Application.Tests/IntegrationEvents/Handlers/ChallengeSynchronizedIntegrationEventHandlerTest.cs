﻿// Filename: ChallengeSynchronizedIntegrationEventHandlerTest.cs
// Date Created: 2019-05-03
// 
// ================================================
// Copyright © 2019, eDoxa. All rights reserved.
// 
// This file is subject to the terms and conditions
// defined in file 'LICENSE.md', which is part of
// this source code package.

using System.Threading;
using System.Threading.Tasks;

using eDoxa.Arena.Challenges.Application.Commands;
using eDoxa.Arena.Challenges.Application.IntegrationEvents;
using eDoxa.Arena.Challenges.Application.IntegrationEvents.Handlers;
using eDoxa.Seedwork.Domain.Enumerations;
using eDoxa.ServiceBus.Extensions;

using MediatR;

using Microsoft.VisualStudio.TestTools.UnitTesting;

using Moq;

namespace eDoxa.Arena.Challenges.Application.Tests.IntegrationEvents.Handlers
{
    [TestClass]
    public sealed class ChallengeSynchronizedIntegrationEventHandlerTest
    {
        private Mock<IMediator> _mockMediator;

        [TestInitialize]
        public void TestInitialize()
        {
            _mockMediator = new Mock<IMediator>();
        }

        [TestMethod]
        public async Task HandleAsync_ChallengeSynchronizedIntegrationEvent_ShouldBeCompletedTask()
        {
            // Arrange
            _mockMediator.Setup(mock => mock.Send(It.IsAny<SynchronizeCommand>(), It.IsAny<CancellationToken>()))
                .ReturnsAsync(It.IsAny<Unit>())
                .Verifiable();

            var handler = new ChallengeSynchronizedIntegrationEventHandler(_mockMediator.Object);

            // Act
            await handler.HandleAsync(new ChallengeSynchronizedIntegrationEvent(Game.LeagueOfLegends));

            // Assert
            _mockMediator.Verify(mock => mock.Send(It.IsAny<SynchronizeCommand>(), It.IsAny<CancellationToken>()), Times.Once);
        }
    }
}