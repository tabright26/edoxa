// Filename: LinkCredentialAsyncTest.cs
// Date Created: 2020-01-28
// 
// ================================================
// Copyright © 2020, eDoxa. All rights reserved.

using System;
using System.Net;
using System.Net.Http;
using System.Security.Claims;
using System.Threading.Tasks;

using Autofac;

using eDoxa.Games.Domain.AggregateModels.GameAggregate;
using eDoxa.Games.Domain.Repositories;
using eDoxa.Games.LeagueOfLegends;
using eDoxa.Games.LeagueOfLegends.Abstactions;
using eDoxa.Games.TestHelper;
using eDoxa.Games.TestHelper.Fixtures;
using eDoxa.Seedwork.Application.Extensions;
using eDoxa.Seedwork.Domain;
using eDoxa.Seedwork.Domain.Misc;
using eDoxa.Seedwork.TestHelper.Extensions;

using FluentAssertions;

using IdentityModel;

using Microsoft.AspNetCore.TestHost;

using Moq;

using RiotSharp.Endpoints.SummonerEndpoint;
using RiotSharp.Misc;

using Xunit;

namespace eDoxa.Games.IntegrationTests.Controllers.GameAuthenticationsController
{
    public sealed class LinkCredentialAsyncTest : IntegrationTest
    {
        public LinkCredentialAsyncTest(TestHostFixture testHost, TestDataFixture testData, TestMapperFixture testMapper) : base(testHost, testData, testMapper)
        {
        }

        private HttpClient _httpClient;

        private async Task<HttpResponseMessage> ExecuteAsync(Game game)
        {
            return await _httpClient.PutAsJsonAsync(
                $"api/games/{game}/authentications",
                new
                {
                });
        }

        [Fact]
        public async Task ShouldBeHttpStatusCodeBadRequest()
        {
            // Arrange
            var userId = new UserId();

            var credential = new Credential(
                userId,
                Game.LeagueOfLegends,
                new PlayerId(),
                new DateTimeProvider(DateTime.Now));

            var factory = TestHost.WithClaimsFromDefaultAuthentication(new Claim(JwtClaimTypes.Subject, userId.ToString()));

            _httpClient = factory.CreateClient();
            var testServer = factory.Server;
            testServer.CleanupDbContext();

            await testServer.UsingScopeAsync(
                async scope =>
                {
                    var credentialRepository = scope.GetRequiredService<IGameCredentialRepository>();
                    credentialRepository.CreateCredential(credential);
                    await credentialRepository.UnitOfWork.CommitAsync();
                });

            // Act
            using var response = await this.ExecuteAsync(Game.LeagueOfLegends);

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
                ProfileIconId = authentication.Factor.ExpectedSummonerProfileIconId
            };

            var factory = TestHost.WithClaimsFromDefaultAuthentication(new Claim(JwtClaimTypes.Subject, userId.ToString()))
                .WithWebHostBuilder(
                    builder => builder.ConfigureTestContainer<ContainerBuilder>(
                        container =>
                        {
                            var mockLeagueOfLegendsService = new Mock<ILeagueOfLegendsService>();

                            mockLeagueOfLegendsService
                                .Setup(leagueOfLegendsService => leagueOfLegendsService.Summoner.GetSummonerByAccountIdAsync(It.IsAny<Region>(), playerId))
                                .ReturnsAsync(summoner)
                                .Verifiable();

                            container.RegisterInstance(mockLeagueOfLegendsService.Object).As<ILeagueOfLegendsService>().SingleInstance();
                        }));

            _httpClient = factory.CreateClient();

            factory.Server.CleanupDbContext();

            await factory.Server.UsingScopeAsync(
                async scope =>
                {
                    var credentialRepository = scope.GetRequiredService<IGameAuthenticationRepository>();

                    await credentialRepository.AddAuthenticationAsync(userId, Game.LeagueOfLegends, authentication);
                });

            // Act
            using var response = await this.ExecuteAsync(Game.LeagueOfLegends);

            // Assert
            response.EnsureSuccessStatusCode();

            response.StatusCode.Should().Be(HttpStatusCode.OK);
        }
    }
}
