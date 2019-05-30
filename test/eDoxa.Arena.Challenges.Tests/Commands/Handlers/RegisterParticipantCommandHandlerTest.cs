// Filename: RegisterParticipantCommandHandlerTest.cs
// Date Created: 2019-05-29
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

using eDoxa.Arena.Challenges.Application.Commands;
using eDoxa.Arena.Challenges.Application.Commands.Handlers;
using eDoxa.Arena.Challenges.Domain.AggregateModels;
using eDoxa.Arena.Challenges.Services.Abstractions;
using eDoxa.Arena.Challenges.Tests.Utilities.Fakes;
using eDoxa.Arena.Challenges.Tests.Utilities.Mocks.Extensions;
using eDoxa.Arena.Domain.ValueObjects;
using eDoxa.Commands.Extensions;
using eDoxa.Functional;
using eDoxa.Seedwork.Domain.Common;
using eDoxa.Seedwork.Domain.Common.Enumerations;

using FluentAssertions;

using FluentValidation.Results;

using Microsoft.AspNetCore.Http;
using Microsoft.VisualStudio.TestTools.UnitTesting;

using Moq;

namespace eDoxa.Arena.Challenges.Tests.Commands.Handlers
{
    [TestClass]
    public sealed class RegisterParticipantCommandHandlerTest
    {
        private static readonly FakeChallengeFactory FakeChallengeFactory = FakeChallengeFactory.Instance;
        private Mock<IChallengeService> _mockChallengeService;
        private Mock<IHttpContextAccessor> _mockHttpContextAccessor;

        [TestInitialize]
        public void TestInitialize()
        {
            _mockChallengeService = new Mock<IChallengeService>();
            _mockHttpContextAccessor = new Mock<IHttpContextAccessor>();
            _mockHttpContextAccessor.SetupClaims();
        }

        [TestMethod]
        public async Task HandleAsync_RegisterParticipantCommand_ShouldBeOfTypeOkObjectResult()
        {
            // Arrange
            _mockChallengeService.Setup(
                    mock => mock.RegisterParticipantAsync(
                        It.IsAny<ChallengeId>(),
                        It.IsAny<UserId>(),
                        It.IsAny<Func<Game, ExternalAccount>>(),
                        It.IsAny<CancellationToken>()
                    )
                )
                .ReturnsAsync(FakeChallengeFactory.CreateParticipant())
                .Verifiable();

            var handler = new RegisterParticipantCommandHandler(_mockHttpContextAccessor.Object, _mockChallengeService.Object);

            // Act
            var result = await handler.HandleAsync(new RegisterParticipantCommand(new ChallengeId()));

            // Assert
            result.Should().BeOfType<Either<ValidationResult, string>>();
        }
    }
}
