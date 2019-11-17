// Filename: ChallengeGameMatchesControllerTest.cs
// Date Created: 2019-11-11
// 
// ================================================
// Copyright © 2019, eDoxa. All rights reserved.

using System.Threading.Tasks;

using eDoxa.Games.Abstractions.Services;
using eDoxa.Games.Api.Areas.Challenges.Controllers;
using eDoxa.Games.TestHelper;
using eDoxa.Games.TestHelper.Fixtures;
using eDoxa.Games.TestHelper.Mocks;
using eDoxa.Seedwork.Domain.Miscs;

using FluentAssertions;

using Microsoft.AspNetCore.Mvc;

using Moq;

using Xunit;

namespace eDoxa.Games.UnitTests.Areas.Challenges.Controllers
{
    public sealed class ChallengeGameMatchesControllerTest : UnitTest
    {
        public ChallengeGameMatchesControllerTest(TestDataFixture testData, TestMapperFixture testMapper) : base(testData, testMapper)
        {
        }

        [Fact]
        public async Task GetAsync_ShouldBeOfTypeOkObjectResult()
        {
            // Arrange
            var mockChallengeService = new Mock<IChallengeService>();

            var challengeGameMatchesController = new ChallengeGameMatchesController(mockChallengeService.Object);

            var mockHttpContextAccessor = new MockHttpContextAccessor();

            challengeGameMatchesController.ControllerContext.HttpContext = mockHttpContextAccessor.Object.HttpContext;

            // Act
            var result = await challengeGameMatchesController.GetAsync(
                Game.LeagueOfLegends,
                new PlayerId(),
                null,
                null);

            // Assert
            result.Should().BeOfType<OkObjectResult>();
        }
    }
}
