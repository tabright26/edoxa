// Filename: Test.cs
// Date Created: 2019-10-05
//
// ================================================
// Copyright © 2019, eDoxa. All rights reserved.

using System.Net;
using System.Net.Http;
using System.Security.Claims;
using System.Threading.Tasks;

using eDoxa.Arena.Games.LeagueOfLegends.Api.Areas.Summoner.Services.Abstractions;
using eDoxa.Arena.Games.LeagueOfLegends.TestHelpers;
using eDoxa.Arena.Games.LeagueOfLegends.TestHelpers.Fixtures;
using eDoxa.Seedwork.Application.Extensions;
using eDoxa.Seedwork.Domain.Miscs;
using eDoxa.Seedwork.Testing.Extensions;
using eDoxa.Seedwork.Testing.Http;

using FluentAssertions;

using IdentityModel;

using Xunit;

namespace eDoxa.Arena.Games.LeagueOfLegends.IntegrationTests.Controllers.SummonerController
{
    public sealed class SummonerControllerPostByIdAsyncTest : IntegrationTest
    {
        public SummonerControllerPostByIdAsyncTest(TestApiFixture testApi) : base(testApi)
        {
        }

        private HttpClient _httpClient;

        private async Task<HttpResponseMessage> ExecuteAsync(string summonerName)
        {
            return await _httpClient.PostAsync($"api/leagueoflegends/summoners/{summonerName}", new JsonContent(""));
        }

        [Fact]
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
        public async Task ShouldBeHttpStatusCodeOk() //How to imitate a lol icon switch ?
        {
            // Arrange
            const string summonerName = "SWAGYOLOMLG";

            var factory = TestApi.WithClaims(new Claim(JwtClaimTypes.Subject, new UserId().ToString()));
            _httpClient = factory.CreateClient();
            var testServer = factory.Server;

            await testServer.UsingScopeAsync(
                async scope =>
                {
                    var summonerService = scope.GetRequiredService<ISummonerService>();

                    var summoner = await summonerService.FindSummonerAsync(summonerName);
                    await summonerService.GetSummonerValidationIcon(summoner!);
                });

            // Act
            using var response = await this.ExecuteAsync(summonerName);

            // Assert
            response.EnsureSuccessStatusCode();
            response.StatusCode.Should().Be(HttpStatusCode.OK);
        }

        [Fact(Skip = "League of Legends service must be mock.")]
        public async Task ShouldBeHttpStatusCodeBadRequest()
        {
            // Arrange
            const string summonerName = "SWAGYOLOMLG";

            var factory = TestApi.WithClaims(new Claim(JwtClaimTypes.Subject, new UserId().ToString()));
            _httpClient = factory.CreateClient();

            // Act
            using var response = await this.ExecuteAsync(summonerName);

            // Assert
            response.StatusCode.Should().Be(HttpStatusCode.BadRequest);
        }
    }
}
