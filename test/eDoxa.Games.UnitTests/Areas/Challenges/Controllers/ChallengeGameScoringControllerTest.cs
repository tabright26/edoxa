// Filename: ChallengeGameScoringControllerTest.cs
// Date Created: 2019-11-11
// 
// ================================================
// Copyright © 2019, eDoxa. All rights reserved.

using System.Threading.Tasks;

using eDoxa.Games.Api.Areas.Challenges.Controllers;
using eDoxa.Games.Domain.Services;
using eDoxa.Games.TestHelper;
using eDoxa.Games.TestHelper.Fixtures;
using eDoxa.Games.TestHelper.Mocks;
using eDoxa.Seedwork.Domain.Misc;

using FluentAssertions;

using Microsoft.AspNetCore.Mvc;

using Moq;

using Xunit;

namespace eDoxa.Games.UnitTests.Areas.Challenges.Controllers
{
    public sealed class ChallengeGameScoringControllerTest : UnitTest
    {
        public ChallengeGameScoringControllerTest(TestDataFixture testData, TestMapperFixture testMapper) : base(testData, testMapper)
        {
        }

        [Fact]
        public async Task GetAsync_ShouldBeOfTypeOkObjectResult()
        {
            // Arrange
            var mockChallengeService = new Mock<IChallengeService>();

            var challengeGameScoringController = new ChallengeGameScoringController(mockChallengeService.Object);

            var mockHttpContextAccessor = new MockHttpContextAccessor();

            challengeGameScoringController.ControllerContext.HttpContext = mockHttpContextAccessor.Object.HttpContext;

            // Act
            var result = await challengeGameScoringController.GetAsync(Game.LeagueOfLegends);

            // Assert
            result.Should().BeOfType<OkObjectResult>();
        }
    }
}
