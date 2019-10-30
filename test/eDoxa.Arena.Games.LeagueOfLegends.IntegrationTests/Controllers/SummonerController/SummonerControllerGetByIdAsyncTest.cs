// Filename: Test.cs
// Date Created: 2019-10-05
//
// ================================================
// Copyright © 2019, eDoxa. All rights reserved.

using System.Net;
using System.Net.Http;
using System.Threading.Tasks;

using eDoxa.Arena.Games.LeagueOfLegends.TestHelpers;
using eDoxa.Arena.Games.LeagueOfLegends.TestHelpers.Fixtures;
using eDoxa.Seedwork.Domain.Miscs;

using FluentAssertions;

using IdentityModel;

using Xunit;

using Claim = System.Security.Claims.Claim;

namespace eDoxa.Arena.Games.LeagueOfLegends.IntegrationTests.Controllers.SummonerController
{
    public sealed class SummonerControllerGetByIdAsyncTest : IntegrationTest
    {
        public SummonerControllerGetByIdAsyncTest(TestApiFixture testApi) : base(testApi)
        {
        }

        private HttpClient _httpClient;

        private async Task<HttpResponseMessage> ExecuteAsync(string summonerName)
        {
            return await _httpClient.GetAsync($"api/leagueoflegends/summoners/{summonerName}");
        }

        [Fact(Skip = "League of Legends service must be mock.")]
        public async Task ShouldBeHttpStatusCodeNotFound()
        {
            // Arrange
            var summonerName = "THISISNOTAVALIDSUMMONERNAME";

            var factory = TestApi.WithClaims(new Claim(JwtClaimTypes.Subject, new UserId().ToString()));
            _httpClient = factory.CreateClient();

            // Act
            using var response = await this.ExecuteAsync(summonerName);

            // Assert
            response.StatusCode.Should().Be(HttpStatusCode.NotFound);
        }

        [Fact(Skip = "League of Legends service must be mock.")]
        public async Task ShouldBeHttpStatusCodeOk()
        {
            // Arrange
            const string summonerName = "SWAGYOLOMLG";

            var factory = TestApi.WithClaims(new Claim(JwtClaimTypes.Subject, new UserId().ToString()));
            _httpClient = factory.CreateClient();

            // Act
            using var response = await this.ExecuteAsync(summonerName);

            // Assert
            response.EnsureSuccessStatusCode();
            response.StatusCode.Should().Be(HttpStatusCode.OK);
        }
    }
}
