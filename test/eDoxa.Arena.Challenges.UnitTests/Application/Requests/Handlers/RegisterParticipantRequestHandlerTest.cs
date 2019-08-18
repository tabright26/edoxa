// Filename: RegisterParticipantRequestHandlerTest.cs
// Date Created: 2019-07-26
// 
// ================================================
// Copyright © 2019, eDoxa. All rights reserved.

using System.Threading;
using System.Threading.Tasks;

using eDoxa.Arena.Challenges.Api.Application.Requests;
using eDoxa.Arena.Challenges.Api.Application.Requests.Handlers;
using eDoxa.Arena.Challenges.Domain.AggregateModels;
using eDoxa.Arena.Challenges.Domain.AggregateModels.ChallengeAggregate;
using eDoxa.Arena.Challenges.Domain.Services;
using eDoxa.Arena.Challenges.UnitTests.Helpers.Mocks;
using eDoxa.Seedwork.Application.Extensions;
using eDoxa.Seedwork.Domain;

using Microsoft.VisualStudio.TestTools.UnitTesting;

using Moq;

namespace eDoxa.Arena.Challenges.UnitTests.Application.Requests.Handlers
{
    [TestClass]
    public sealed class RegisterParticipantRequestHandlerTest
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
        public async Task RegisterParticipantRequest_ShouldBeOfTypeOkObjectResult()
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

            var handler = new RegisterParticipantRequestHandler(_mockHttpContextAccessor.Object, _mockChallengeService.Object);

            // Act
            await handler.HandleAsync(new RegisterParticipantRequest(new ChallengeId()));

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
