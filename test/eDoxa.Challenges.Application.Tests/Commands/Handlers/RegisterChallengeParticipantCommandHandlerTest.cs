// Filename: RegisterChallengeParticipantCommandHandlerTest.cs
// Date Created: 2019-04-21
// 
// ================================================
// Copyright © 2019, eDoxa. All rights reserved.
//  
// This file is subject to the terms and conditions
// defined in file 'LICENSE.md', which is part of
// this source code package.

using System;
using System.Threading;
using System.Threading.Tasks;

using eDoxa.Challenges.Application.Commands;
using eDoxa.Challenges.Application.Commands.Handlers;
using eDoxa.Challenges.Domain.AggregateModels;
using eDoxa.Challenges.Domain.AggregateModels.ChallengeAggregate;
using eDoxa.Challenges.Domain.Factories;
using eDoxa.Challenges.Domain.Repositories;

using FluentAssertions;

using Microsoft.AspNetCore.Mvc;
using Microsoft.VisualStudio.TestTools.UnitTesting;

using Moq;

namespace eDoxa.Challenges.Application.Tests.Commands.Handlers
{
    [TestClass]
    public sealed class RegisterChallengeParticipantCommandHandlerTest
    {
        private static readonly ChallengeAggregateFactory ChallengeAggregateFactory = ChallengeAggregateFactory.Instance;

        [TestMethod]
        public async Task HandleAsync_FindChallengeAsync_ShouldBeInvokedExactlyOneTime()
        {
            // Arrange
            var command = new RegisterChallengeParticipantCommand(new UserId())
            {
                LinkedAccount = new LinkedAccount(Guid.NewGuid())
            };

            var mockChallengeRepository = new Mock<IChallengeRepository>();

            mockChallengeRepository.Setup(repository => repository.FindChallengeAsync(It.IsAny<ChallengeId>()))
                .ReturnsAsync(ChallengeAggregateFactory.CreateChallenge())
                .Verifiable();

            mockChallengeRepository.Setup(repository => repository.UnitOfWork.CommitAndDispatchDomainEventsAsync(It.IsAny<CancellationToken>()))
                .Returns(Task.CompletedTask)
                .Verifiable();

            // Act
            var handler = new RegisterChallengeParticipantCommandHandler(mockChallengeRepository.Object);

            // Assert
            var result = await handler.Handle(command, default);

            result.Should().BeOfType<OkObjectResult>();

            mockChallengeRepository.Verify(repository => repository.FindChallengeAsync(It.IsAny<ChallengeId>()), Times.Once);

            mockChallengeRepository.Verify(repository => repository.UnitOfWork.CommitAndDispatchDomainEventsAsync(It.IsAny<CancellationToken>()), Times.Once);
        }
    }
}