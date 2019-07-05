﻿// Filename: RegisterParticipantCommandHandlerTest.cs
// Date Created: 2019-06-25
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

using AutoMapper;

using eDoxa.Arena.Challenges.Api.Application.Commands;
using eDoxa.Arena.Challenges.Api.Application.Commands.Handlers;
using eDoxa.Arena.Challenges.Domain.AggregateModels.ChallengeAggregate;
using eDoxa.Arena.Challenges.Domain.Services;
using eDoxa.Arena.Challenges.Domain.ViewModels;
using eDoxa.Arena.Challenges.UnitTests.Helpers.Mocks;
using eDoxa.Commands.Extensions;
using eDoxa.Seedwork.Common.ValueObjects;

using Microsoft.VisualStudio.TestTools.UnitTesting;

using Moq;

namespace eDoxa.Arena.Challenges.UnitTests.Application.Commands.Handlers
{
    [TestClass]
    public sealed class RegisterParticipantCommandHandlerTest
    {
        private Mock<IChallengeService> _mockChallengeService;
        private MockHttpContextAccessor _mockHttpContextAccessor;
        private Mock<IMapper> _mockMapper;

        [TestInitialize]
        public void TestInitialize()
        {
            _mockChallengeService = new Mock<IChallengeService>();
            _mockHttpContextAccessor = new MockHttpContextAccessor();
            _mockMapper = new Mock<IMapper>();
        }

        [TestMethod]
        public async Task HandleAsync_RegisterParticipantCommand_ShouldBeOfTypeOkObjectResult()
        {
            // Arrange
            _mockChallengeService.Setup(
                    mock => mock.RegisterParticipantAsync(
                        It.IsAny<ChallengeId>(),
                        It.IsAny<UserId>(),
                        It.IsAny<Func<ChallengeGame, GameAccountId>>(),
                        It.IsAny<CancellationToken>()
                    )
                )
                .Returns(Task.CompletedTask)
                .Verifiable();

            _mockMapper.Setup(mapper => mapper.Map<ParticipantViewModel>(It.IsAny<Participant>())).Returns(new ParticipantViewModel()).Verifiable();

            var handler = new RegisterParticipantCommandHandler(_mockHttpContextAccessor.Object, _mockChallengeService.Object);

            // Act
            await handler.HandleAsync(new RegisterParticipantCommand(new ChallengeId()));

            // Assert
            _mockChallengeService.Verify(
                mock => mock.RegisterParticipantAsync(
                    It.IsAny<ChallengeId>(),
                    It.IsAny<UserId>(),
                    It.IsAny<Func<ChallengeGame, GameAccountId>>(),
                    It.IsAny<CancellationToken>()
                ),
                Times.Once
            );
        }
    }
}