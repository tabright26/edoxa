// Filename: GameCredentialControllerDeleteByGameAsyncTest.cs
// Date Created: 2019-11-01
//
// ================================================
// Copyright © 2019, eDoxa. All rights reserved.

using System;
using System.Net;
using System.Net.Http;
using System.Security.Claims;
using System.Threading.Tasks;

using Autofac;

using eDoxa.Games.Abstractions.Services;
using eDoxa.Games.Domain.Repositories;
using eDoxa.Games.TestHelper;
using eDoxa.Games.TestHelper.Fixtures;
using eDoxa.Seedwork.Application.Extensions;
using eDoxa.Seedwork.Domain;
using eDoxa.Seedwork.Domain.Miscs;
using eDoxa.Seedwork.TestHelper.Extensions;

using FluentAssertions;

using FluentValidation.Results;

using IdentityModel;

using Microsoft.AspNetCore.TestHost;

using Moq;

using Xunit;

namespace eDoxa.Games.IntegrationTests.Areas.Credential.Controllers
{
    public sealed class GameCredentialControllerDeleteByGameAsyncTest : IntegrationTest
    {
        public GameCredentialControllerDeleteByGameAsyncTest(TestApiFixture testApi, TestDataFixture testData, TestMapperFixture testMapper) : base(
            testApi,
            testData,
            testMapper)
        {
        }

        private HttpClient _httpClient;

        private async Task<HttpResponseMessage> ExecuteAsync(Game game)
        {
            return await _httpClient.DeleteAsync($"api/{game}/credential");
        }

        [Fact]
        public async Task ShouldBeHttpStatusCodeOk()
        {
            // Arrange
            var userId = new UserId();

            var credential = new Domain.AggregateModels.CredentialAggregate.Credential(
                userId,
                Game.LeagueOfLegends,
                new PlayerId(),
                new DateTimeProvider(DateTime.Now));


            var factory = TestApi.WithClaims(new Claim(JwtClaimTypes.Subject, userId.ToString()));

            _httpClient = factory.CreateClient();
            var testServer = factory.Server;
            testServer.CleanupDbContext();
            await testServer.UsingScopeAsync(
                async scope =>
                {
                    var credentialRepository = scope.GetRequiredService<ICredentialRepository>();
                    credentialRepository.CreateCredential(credential);
                    await credentialRepository.UnitOfWork.CommitAsync();
                });

            // Act
            using var response = await this.ExecuteAsync(Game.LeagueOfLegends);

            // Assert
            response.EnsureSuccessStatusCode();
            response.StatusCode.Should().Be(HttpStatusCode.OK);
        }

        [Fact]
        public async Task ShouldBeHttpStatusCodeNotFound()
        {
            // Arrange
            var userId = new UserId();

            var factory = TestApi.WithClaims(new Claim(JwtClaimTypes.Subject, userId.ToString()));

            _httpClient = factory.CreateClient();
            var testServer = factory.Server;
            testServer.CleanupDbContext();

            // Act
            using var response = await this.ExecuteAsync(Game.LeagueOfLegends);

            // Assert
            response.StatusCode.Should().Be(HttpStatusCode.NotFound);
        }

        [Fact]
        public async Task ShouldBeHttpStatusCodeBadRequest()
        {
            // Arrange
            var userId = new UserId();

            var credential = new Domain.AggregateModels.CredentialAggregate.Credential(
                userId,
                Game.LeagueOfLegends,
                new PlayerId(),
                new DateTimeProvider(DateTime.Now));


            var factory = TestApi.WithClaims(new Claim(JwtClaimTypes.Subject, userId.ToString()))
                .WithWebHostBuilder(builder => builder.ConfigureTestContainer<ContainerBuilder>(
                    container =>
                    {
                        var mockCredentialService = new Mock<ICredentialService>();

                        var validationFailure = new ValidationResult();
                        validationFailure.Errors.Add(new ValidationFailure("test", "validation failure test"));

                        mockCredentialService
                            .Setup(credentialService => credentialService.FindCredentialAsync(It.IsAny<UserId>(), It.IsAny<Game>()))
                            .ReturnsAsync(credential)
                            .Verifiable();

                        mockCredentialService
                            .Setup(credentialService => credentialService.UnlinkCredentialAsync(It.IsAny<Domain.AggregateModels.CredentialAggregate.Credential>()))
                            .ReturnsAsync(validationFailure)
                            .Verifiable();

                        container.RegisterInstance(mockCredentialService.Object).As<ICredentialService>().SingleInstance();
                    }));

            _httpClient = factory.CreateClient();
            var testServer = factory.Server;
            testServer.CleanupDbContext();

            // Act
            using var response = await this.ExecuteAsync(Game.LeagueOfLegends);

            // Assert
            response.StatusCode.Should().Be(HttpStatusCode.BadRequest);
        }
    }
}
