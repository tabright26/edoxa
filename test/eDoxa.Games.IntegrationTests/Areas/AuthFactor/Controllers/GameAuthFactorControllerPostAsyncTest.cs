// Filename: GameAuthFactorControllerPostAsyncTest.cs
// Date Created: 2019-11-01
//
// ================================================
// Copyright © 2019, eDoxa. All rights reserved.

using System.Net;
using System.Net.Http;
using System.Security.Claims;
using System.Threading.Tasks;

using Autofac;

using eDoxa.Games.Abstractions.Services;
using eDoxa.Games.Domain.AggregateModels.GameAggregate;
using eDoxa.Games.LeagueOfLegends;
using eDoxa.Games.LeagueOfLegends.Requests;
using eDoxa.Games.TestHelper;
using eDoxa.Games.TestHelper.Fixtures;
using eDoxa.Seedwork.Domain.Miscs;
using eDoxa.Seedwork.TestHelper.Http;

using FluentAssertions;

using FluentValidation.Results;

using IdentityModel;

using Microsoft.AspNetCore.TestHost;

using Moq;

using Xunit;

namespace eDoxa.Games.IntegrationTests.Areas.AuthFactor.Controllers
{
    public sealed class GameAuthFactorControllerPostAsyncTest : IntegrationTest
    {
        public GameAuthFactorControllerPostAsyncTest(TestApiFixture testApi, TestDataFixture testData, TestMapperFixture testMapper) : base(
            testApi,
            testData,
            testMapper)
        {
        }

        private HttpClient _httpClient;

        private async Task<HttpResponseMessage> ExecuteAsync(Game game, LeagueOfLegendsRequest request)
        {
            return await _httpClient.PostAsync($"api/{game}/auth-factor", new JsonContent(request));
        }

        [Fact]
        public async Task ShouldBeHttpStatusCodeOk()
        {
            // Arrange
            var userId = new UserId();

            var authFactor = new GameAuthentication<LeagueOfLegendsGameAuthenticationFactor>(new PlayerId(), new LeagueOfLegendsGameAuthenticationFactor(1, string.Empty, 2, string.Empty));

            var factory = TestApi.WithClaims(new Claim(JwtClaimTypes.Subject, userId.ToString()))
                .WithWebHostBuilder(builder => builder.ConfigureTestContainer<ContainerBuilder>(
                    container =>
                    {
                        var mockAuthFactorService = new Mock<IAuthenticationService>();

                        mockAuthFactorService
                            .Setup(authFactorService => authFactorService.GenerateAuthenticationAsync(It.IsAny<UserId>(), It.IsAny<Game>(), It.IsAny<object>()))
                            .ReturnsAsync(new ValidationResult())
                            .Verifiable();

                        mockAuthFactorService
                            .Setup(authFactorService => authFactorService.FindAuthenticationAsync<LeagueOfLegendsGameAuthenticationFactor>(It.IsAny<UserId>(), It.IsAny<Game>()))
                            .ReturnsAsync(authFactor)
                            .Verifiable();

                        container.RegisterInstance(mockAuthFactorService.Object).As<IAuthenticationService>().SingleInstance();
                    }));
            _httpClient = factory.CreateClient();

            // Act
            using var response = await this.ExecuteAsync(Game.LeagueOfLegends, new LeagueOfLegendsRequest("SwagYoloMlg"));

            // Assert
            response.EnsureSuccessStatusCode();
            response.StatusCode.Should().Be(HttpStatusCode.OK);
        }

        [Fact]
        public async Task ShouldBeHttpStatusCodeBadRequest()
        {
            // Arrange
            var userId = new UserId();
            var factory = TestApi.WithClaims(new Claim(JwtClaimTypes.Subject, userId.ToString()))
                .WithWebHostBuilder(builder => builder.ConfigureTestContainer<ContainerBuilder>(
                    container =>
                    {
                        var mockAuthFactorService = new Mock<IAuthenticationService>();

                        var validationFailure = new ValidationResult();
                        validationFailure.Errors.Add(new ValidationFailure("test", "validation failure test"));

                        mockAuthFactorService
                            .Setup(authFactorService => authFactorService.GenerateAuthenticationAsync(It.IsAny<UserId>(), It.IsAny<Game>(), It.IsAny<object>()))
                            .ReturnsAsync(validationFailure)
                            .Verifiable();

                        container.RegisterInstance(mockAuthFactorService.Object).As<IAuthenticationService>().SingleInstance();
                    }));
            _httpClient = factory.CreateClient();

            // Act
            using var response = await this.ExecuteAsync(Game.LeagueOfLegends, new LeagueOfLegendsRequest("SwagYoloMlg"));

            // Assert
            response.StatusCode.Should().Be(HttpStatusCode.BadRequest);
        }

    }
}
