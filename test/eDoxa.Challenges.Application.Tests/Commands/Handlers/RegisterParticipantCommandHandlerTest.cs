// Filename: RegisterParticipantCommandHandlerTest.cs
// Date Created: 2019-05-03
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
    public sealed class RegisterParticipantCommandHandlerTest
    {
        private static readonly ChallengeAggregateFactory ChallengeAggregateFactory = ChallengeAggregateFactory.Instance;
        private Mock<IChallengeRepository> _mockChallengeRepository;
        private Mock<IUserInfoService> _mockUserInfoService;
        private Mock<IUserLoginInfoService> _mockUserLoginInfoService;

        [TestInitialize]
        public void TestInitialize()
        {
            _mockChallengeRepository = new Mock<IChallengeRepository>();
            _mockUserInfoService = new Mock<IUserInfoService>();
            _mockUserInfoService.SetupGetProperties();
            _mockUserLoginInfoService = new Mock<IUserLoginInfoService>();
            _mockUserLoginInfoService.SetupGetProperties();
        }

        [TestMethod]
        public async Task HandleAsync_RegisterParticipantCommand_ShouldBeOfTypeOkObjectResult()
        {
            // Arrange
            var command = new RegisterParticipantCommand(new ChallengeId());

            _mockChallengeRepository.Setup(mock => mock.FindChallengeAsync(It.IsAny<ChallengeId>()))
                .ReturnsAsync(ChallengeAggregateFactory.CreateChallenge())
                .Verifiable();

            _mockChallengeRepository.Setup(mock => mock.UnitOfWork.CommitAndDispatchDomainEventsAsync(It.IsAny<CancellationToken>()))
                .Returns(Task.CompletedTask)
                .Verifiable();

            var handler = new RegisterParticipantCommandHandler(_mockUserInfoService.Object, _mockUserLoginInfoService.Object, _mockChallengeRepository.Object);

            // Act
            var result = await handler.HandleAsync(command);

            // Assert
            result.Should().BeOfType<OkObjectResult>();

            _mockChallengeRepository.Verify(mock => mock.FindChallengeAsync(It.IsAny<ChallengeId>()), Times.Once);

            _mockChallengeRepository.Verify(mock => mock.UnitOfWork.CommitAndDispatchDomainEventsAsync(It.IsAny<CancellationToken>()), Times.Once);
        }
    }
}