// Filename: GameAuthenticationsControllerPostAsyncTest.cs
// Date Created: 2019-12-26
// 
// ================================================
// Copyright © 2020, eDoxa. All rights reserved.

using System.Net;
using System.Net.Http;
using System.Security.Claims;
using System.Threading.Tasks;

using Autofac;

using eDoxa.Games.Domain.Services;
using eDoxa.Games.LeagueOfLegends;
using eDoxa.Games.LeagueOfLegends.Requests;
using eDoxa.Games.TestHelper;
using eDoxa.Games.TestHelper.Fixtures;
using eDoxa.Seedwork.Domain;
using eDoxa.Seedwork.Domain.Misc;
using eDoxa.Seedwork.TestHelper.Extensions;

using FluentAssertions;

using IdentityModel;

using Microsoft.AspNetCore.TestHost;

using Moq;

using Xunit;

namespace eDoxa.Games.IntegrationTests.Controllers
{
    public sealed class GameAuthenticationsControllerPostAsyncTest : IntegrationTest
    {
        public GameAuthenticationsControllerPostAsyncTest(TestHostFixture testHost, TestDataFixture testData, TestMapperFixture testMapper) : base(
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
                            var mockAuthFactorService = new Mock<IGameAuthenticationService>();

                            var validationFailure = new DomainValidationResult();
                            validationFailure.AddInvalidArgumentError("test", "validation failure test");

                            mockAuthFactorService
                                .Setup(
                                    authFactorService =>
                                        authFactorService.GenerateAuthenticationAsync(It.IsAny<UserId>(), It.IsAny<Game>(), It.IsAny<object>()))
                                .ReturnsAsync(validationFailure)
                                .Verifiable();

                            container.RegisterInstance(mockAuthFactorService.Object).As<IGameAuthenticationService>().SingleInstance();
                        }));

            _httpClient = factory.CreateClient();

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

            var authFactor = new LeagueOfLegendsGameAuthentication(
                new PlayerId(),
                new LeagueOfLegendsGameAuthenticationFactor(
                    1,
                    string.Empty,
                    2,
                    string.Empty));

            var factory = TestHost.WithClaimsFromDefaultAuthentication(new Claim(JwtClaimTypes.Subject, userId.ToString()))
                .WithWebHostBuilder(
                    builder => builder.ConfigureTestContainer<ContainerBuilder>(
                        container =>
                        {
                            var mockAuthFactorService = new Mock<IGameAuthenticationService>();

                            mockAuthFactorService
                                .Setup(
                                    authFactorService =>
                                        authFactorService.GenerateAuthenticationAsync(It.IsAny<UserId>(), It.IsAny<Game>(), It.IsAny<object>()))
                                .ReturnsAsync(new DomainValidationResult())
                                .Verifiable();

                            mockAuthFactorService.Setup(authFactorService => authFactorService.FindAuthenticationAsync(It.IsAny<UserId>(), It.IsAny<Game>()))
                                .ReturnsAsync(authFactor)
                                .Verifiable();

                            container.RegisterInstance(mockAuthFactorService.Object).As<IGameAuthenticationService>().SingleInstance();
                        }));

            _httpClient = factory.CreateClient();

            // Act
            using var response = await this.ExecuteAsync(Game.LeagueOfLegends, new LeagueOfLegendsRequest("SwagYoloMlg"));

            // Assert
            response.EnsureSuccessStatusCode();
            response.StatusCode.Should().Be(HttpStatusCode.OK);
        }
    }
}
