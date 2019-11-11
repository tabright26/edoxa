// Filename: ChallengeGameScoringControllerTest.cs
// Date Created: 2019-11-01
//
// ================================================
// Copyright © 2019, eDoxa. All rights reserved.

using System.Threading.Tasks;

using eDoxa.Games.Api.Areas.Challenge.Controllers;
using eDoxa.Games.TestHelper;
using eDoxa.Games.TestHelper.Fixtures;
using eDoxa.Games.TestHelper.Mocks;

using FluentAssertions;

using Microsoft.AspNetCore.Mvc;

using Xunit;

namespace eDoxa.Games.UnitTests.Areas.Challenge.Controllers
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
            var challengeGameScoringController = new ChallengeGameScoringController();

            var mockHttpContextAccessor = new MockHttpContextAccessor();

            challengeGameScoringController.ControllerContext.HttpContext = mockHttpContextAccessor.Object.HttpContext;

            // Act
            var result = await challengeGameScoringController.GetAsync();

            // Assert
            result.Should().BeOfType<OkResult>();
        }
    }
}

