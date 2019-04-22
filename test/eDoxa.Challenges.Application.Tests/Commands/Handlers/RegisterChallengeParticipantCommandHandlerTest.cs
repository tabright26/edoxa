// Filename: RegisterChallengeParticipantCommandHandlerTest.cs
// Date Created: 2019-03-22
// 
// ============================================================
// Copyright © 2019, Francis Quenneville
// All rights reserved.
// 
// This file is subject to the terms and conditions defined in file 'LICENSE.md', which is part of
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
            var command = new RegisterChallengeParticipantCommand(new ChallengeId(), new UserId())
            {
                LinkedAccount = LinkedAccount.FromGuid(Guid.NewGuid())
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
            await handler.HandleAsync(command);

            mockChallengeRepository.Verify(repository => repository.FindChallengeAsync(It.IsAny<ChallengeId>()), Times.Once);

            mockChallengeRepository.Verify(repository => repository.UnitOfWork.CommitAndDispatchDomainEventsAsync(It.IsAny<CancellationToken>()), Times.Once);
        }
    }
}