// Filename: RegisterParticipantCommandHandlerTest.cs
// Date Created: 2019-06-25
// 
// ================================================
// Copyright © 2019, eDoxa. All rights reserved.
// 
// This file is subject to the terms and conditions
// defined in file 'LICENSE.md', which is part of
// this source code package.

using System.Threading;
using System.Threading.Tasks;

using eDoxa.Arena.Challenges.Api.Application.Commands;
using eDoxa.Arena.Challenges.Api.Application.Commands.Handlers;
using eDoxa.Arena.Challenges.Domain.AggregateModels;
using eDoxa.Arena.Challenges.Domain.AggregateModels.ChallengeAggregate;
using eDoxa.Arena.Challenges.Domain.Services;
using eDoxa.Arena.Challenges.UnitTests.Helpers.Mocks;
using eDoxa.Commands.Extensions;
using eDoxa.Seedwork.Domain;

using Microsoft.VisualStudio.TestTools.UnitTesting;

using Moq;

namespace eDoxa.Arena.Challenges.UnitTests.Application.Commands.Handlers
{
    [TestClass]
    public sealed class RegisterParticipantCommandHandlerTest
    {
        private Mock<IChallengeService> _mockChallengeService;
        private MockHttpContextAccessor _mockHttpContextAccessor;

        [TestInitialize]
        public void TestInitialize()
        {
            _mockChallengeService = new Mock<IChallengeService>();
            _mockHttpContextAccessor = new MockHttpContextAccessor();
        }

        [TestMethod]
        public async Task HandleAsync_RegisterParticipantCommand_ShouldBeOfTypeOkObjectResult()
        {
            // Arrange
            _mockChallengeService.Setup(
                    challengeService => challengeService.RegisterParticipantAsync(
                        It.IsAny<ChallengeId>(),
                        It.IsAny<UserId>(),
                        It.IsAny<IDateTimeProvider>(),
                        It.IsAny<CancellationToken>()
                    )
                )
                .Returns(Task.CompletedTask)
                .Verifiable();

            var handler = new RegisterParticipantCommandHandler(_mockHttpContextAccessor.Object, _mockChallengeService.Object);

            // Act
            await handler.HandleAsync(new RegisterParticipantCommand(new ChallengeId()));

            // Assert
            _mockChallengeService.Verify(
                challengeService => challengeService.RegisterParticipantAsync(
                    It.IsAny<ChallengeId>(),
                    It.IsAny<UserId>(),
                    It.IsAny<IDateTimeProvider>(),
                    It.IsAny<CancellationToken>()
                ),
                Times.Once
            );
        }
    }
}
