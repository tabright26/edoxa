// Filename: ChallengeGameMatchesControllerGetAsyncTest.cs
// Date Created: 2019-11-01
//
// ================================================
// Copyright © 2019, eDoxa. All rights reserved.

using eDoxa.Games.TestHelper;
using eDoxa.Games.TestHelper.Fixtures;

namespace eDoxa.Games.IntegrationTests.Areas.Challenge.Controllers
{
    public sealed class ChallengeGameMatchesControllerGetAsyncTest : IntegrationTest // TODO: Integration test
    {
        public ChallengeGameMatchesControllerGetAsyncTest(TestApiFixture testApi, TestDataFixture testData, TestMapperFixture testMapper) : base(
            testApi,
            testData,
            testMapper)
        {
        }

        //private HttpClient _httpClient;

        //private async Task<HttpResponseMessage> ExecuteAsync(Game game)
        //{
        //    return await _httpClient.GetAsync($"api/challenge/games/{game}/matches");
        //}

        //[Fact]
        //public async Task ShouldBeHttpStatusCodeOk()
        //{
        //    // Arrange
        //    var userId = new UserId();
        //    var factory = TestApi.WithClaims(new Claim(JwtClaimTypes.Subject, userId.ToString()));
        //    _httpClient = factory.CreateClient();

        //    // Act
        //    using var response = await this.ExecuteAsync(Game.LeagueOfLegends);

        //    // Assert
        //    response.EnsureSuccessStatusCode();
        //    response.StatusCode.Should().Be(HttpStatusCode.OK);
        //}
    }
}
