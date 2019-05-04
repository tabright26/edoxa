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
using eDoxa.Challenges.Domain.AggregateModels.ChallengeAggregate;
using eDoxa.Challenges.Domain.AggregateModels.ParticipantAggregate;
using eDoxa.Challenges.Domain.Factories;
using eDoxa.Challenges.Domain.Repositories;
using eDoxa.Commands.Extensions;
using eDoxa.Security.Abstractions;
using eDoxa.Testing.MSTest.Extensions;

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
        private Mock<IChallengeRepository> _mockChallengeRepository;
        private Mock<IUserProfile> _mockUserProfile;

        [TestInitialize]
        public void TestInitialize()
        {
            _mockChallengeRepository = new Mock<IChallengeRepository>();
            _mockUserProfile = new Mock<IUserProfile>();
            _mockUserProfile.SetupProperties();
        }

        [TestMethod]
        public async Task HandleAsync_FindChallengeAsync_ShouldBeInvokedExactlyOneTime()
        {
            // Arrange
            var command = new RegisterChallengeParticipantCommand
            {
                LinkedAccount = new LinkedAccount(Guid.NewGuid())
            };

            _mockChallengeRepository.Setup(repository => repository.FindChallengeAsync(It.IsAny<ChallengeId>()))
                .ReturnsAsync(ChallengeAggregateFactory.CreateChallenge())
                .Verifiable();

            _mockChallengeRepository.Setup(repository => repository.UnitOfWork.CommitAndDispatchDomainEventsAsync(It.IsAny<CancellationToken>()))
                .Returns(Task.CompletedTask)
                .Verifiable();

            // Act
            var handler = new RegisterChallengeParticipantCommandHandler(_mockUserProfile.Object, _mockChallengeRepository.Object);

            // Assert
            var result = await handler.HandleAsync(command);

            result.Should().BeOfType<OkObjectResult>();

            _mockChallengeRepository.Verify(repository => repository.FindChallengeAsync(It.IsAny<ChallengeId>()), Times.Once);

            _mockChallengeRepository.Verify(repository => repository.UnitOfWork.CommitAndDispatchDomainEventsAsync(It.IsAny<CancellationToken>()), Times.Once);
        }
    }
}