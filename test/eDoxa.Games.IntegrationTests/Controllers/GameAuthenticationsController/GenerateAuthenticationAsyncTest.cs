// Filename: GenerateAuthenticationAsyncTest.cs
// Date Created: 2020-01-28
// 
// ================================================
// Copyright © 2020, eDoxa. All rights reserved.

using System.Net;
using System.Net.Http;
using System.Security.Claims;
using System.Threading.Tasks;

using Autofac;

using eDoxa.Games.LeagueOfLegends;
using eDoxa.Games.LeagueOfLegends.Abstactions;
using eDoxa.Games.LeagueOfLegends.Requests;
using eDoxa.Games.TestHelper;
using eDoxa.Games.TestHelper.Fixtures;
using eDoxa.Seedwork.Domain.Misc;
using eDoxa.Seedwork.TestHelper.Extensions;

using FluentAssertions;

using IdentityModel;

using Microsoft.AspNetCore.TestHost;

using Moq;

using RiotSharp;
using RiotSharp.Endpoints.SummonerEndpoint;
using RiotSharp.Misc;

using Xunit;

namespace eDoxa.Games.IntegrationTests.Controllers.GameAuthenticationsController
{
    public sealed class GenerateAuthenticationAsyncTest : IntegrationTest
    {
        public GenerateAuthenticationAsyncTest(TestHostFixture testHost, TestDataFixture testData, TestMapperFixture testMapper) : base(
            testHost,
            testData,
            testMapper)
        {
        }

        private HttpClient _httpClient;

        private async Task<HttpResponseMessage> ExecuteAsync(Game game, LeagueOfLegendsRequest request)
        {
            return await _httpClient.PostAsJsonAsync($"api/games/{game}/authentications", request);
        }

        [Fact]
        public async Task ShouldBeHttpStatusCodeBadRequest()
        {
            // Arrange
            var userId = new UserId();

            var factory = TestHost.WithClaimsFromDefaultAuthentication(new Claim(JwtClaimTypes.Subject, userId.ToString()))
                .WithWebHostBuilder(
                    builder => builder.ConfigureTestContainer<ContainerBuilder>(
                        container =>
                        {
                            var mockLeagueOfLegendsService = new Mock<ILeagueOfLegendsService>();

                            mockLeagueOfLegendsService
                                .Setup(
                                    leagueOfLegendsService =>
                                        leagueOfLegendsService.Summoner.GetSummonerByNameAsync(It.IsAny<Region>(), It.IsAny<string>()))
                                .ThrowsAsync(new RiotSharpException("Summoner's name not found", HttpStatusCode.BadRequest));

                            container.RegisterInstance(mockLeagueOfLegendsService.Object).As<ILeagueOfLegendsService>().SingleInstance();
                        }));

            _httpClient = factory.CreateClient();

            factory.Server.CleanupDbContext();

            // Act
            using var response = await this.ExecuteAsync(Game.LeagueOfLegends, new LeagueOfLegendsRequest("SwagYoloMlg"));

            // Assert
            response.StatusCode.Should().Be(HttpStatusCode.BadRequest);
        }

        [Fact]
        public async Task ShouldBeHttpStatusCodeOk()
        {
            // Arrange
            var userId = new UserId();
            var playerId = new PlayerId();

            var authentication = new LeagueOfLegendsGameAuthentication(
                playerId,
                new LeagueOfLegendsGameAuthenticationFactor(
                    1,
                    string.Empty,
                    2,
                    string.Empty));

            var summoner = new Summoner
            {
                AccountId = playerId,
                ProfileIconId = authentication.Factor.CurrentSummonerProfileIconId
            };

            var request = new LeagueOfLegendsRequest("SwagYoloMlg");

            var factory = TestHost.WithClaimsFromDefaultAuthentication(new Claim(JwtClaimTypes.Subject, userId.ToString()))
                .WithWebHostBuilder(
                    builder => builder.ConfigureTestContainer<ContainerBuilder>(
                        container =>
                        {
                            var mockLeagueOfLegendsService = new Mock<ILeagueOfLegendsService>();

                            mockLeagueOfLegendsService
                                .Setup(leagueOfLegendsService => leagueOfLegendsService.Summoner.GetSummonerByNameAsync(It.IsAny<Region>(), request.SummonerName))
                                .ReturnsAsync(summoner)
                                .Verifiable();

                            container.RegisterInstance(mockLeagueOfLegendsService.Object).As<ILeagueOfLegendsService>().SingleInstance();
                        }));

            _httpClient = factory.CreateClient();

            factory.Server.CleanupDbContext();

            // Act
            using var response = await this.ExecuteAsync(Game.LeagueOfLegends, request);

            // Assert
            response.EnsureSuccessStatusCode();

            response.StatusCode.Should().Be(HttpStatusCode.OK);
        }
    }
}
