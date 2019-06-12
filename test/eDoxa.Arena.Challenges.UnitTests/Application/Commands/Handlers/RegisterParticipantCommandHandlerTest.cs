// Filename: RegisterParticipantCommandHandlerTest.cs
// Date Created: 2019-06-01
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
using eDoxa.Arena.Challenges.Api.ViewModels;
using eDoxa.Arena.Challenges.Domain.Abstractions.Services;
using eDoxa.Arena.Challenges.Domain.AggregateModels.ChallengeAggregate;
using eDoxa.Arena.Challenges.Domain.AggregateModels.ParticipantAggregate;
using eDoxa.Arena.Challenges.Domain.Fakers;
using eDoxa.Arena.Challenges.UnitTests.Extensions;
using eDoxa.Commands.Extensions;
using eDoxa.Seedwork.Common.Enumerations;
using eDoxa.Seedwork.Common.ValueObjects;

using FluentAssertions;

using Microsoft.AspNetCore.Http;
using Microsoft.VisualStudio.TestTools.UnitTesting;

using Moq;

namespace eDoxa.Arena.Challenges.UnitTests.Application.Commands.Handlers
{
    [TestClass]
    public sealed class RegisterParticipantCommandHandlerTest
    {
        private ParticipantFaker _participantFaker;
        private Mock<IChallengeService> _mockChallengeService;
        private Mock<IHttpContextAccessor> _mockHttpContextAccessor;
        private Mock<IMapper> _mockMapper;

        [TestInitialize]
        public void TestInitialize()
        {
            _participantFaker = new ParticipantFaker();
            _mockChallengeService = new Mock<IChallengeService>();
            _mockHttpContextAccessor = new Mock<IHttpContextAccessor>();
            _mockHttpContextAccessor.SetupClaims();
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
                        It.IsAny<Func<Game, UserGameReference>>(),
                        It.IsAny<CancellationToken>()
                    )
                )
                .ReturnsAsync(_participantFaker.FakeParticipant(Game.LeagueOfLegends))
                .Verifiable();

            _mockMapper.Setup(x => x.Map<ParticipantViewModel>(It.IsAny<Participant>())).Returns(new ParticipantViewModel()).Verifiable();

            var handler = new RegisterParticipantCommandHandler(_mockHttpContextAccessor.Object, _mockChallengeService.Object, _mockMapper.Object);

            // Act
            var result = await handler.HandleAsync(new RegisterParticipantCommand(new ChallengeId()));

            // Assert
            result.Should().BeOfType<ParticipantViewModel>();
        }
    }
}
