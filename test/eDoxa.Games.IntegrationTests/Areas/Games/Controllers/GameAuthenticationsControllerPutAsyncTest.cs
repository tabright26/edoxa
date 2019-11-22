﻿// Filename: GameCredentialsControllerPostByGameAsyncTest.cs
// Date Created: 2019-11-20
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
using eDoxa.Games.Domain.AggregateModels.GameAggregate;
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

namespace eDoxa.Games.IntegrationTests.Areas.Games.Controllers
{
    public sealed class GameAuthenticationsControllerPutAsyncTest : IntegrationTest
    {
        public GameAuthenticationsControllerPutAsyncTest(TestApiFixture testApi, TestDataFixture testData, TestMapperFixture testMapper) : base(
            testApi,
            testData,
            testMapper)
        {
        }

        private HttpClient _httpClient;

        private async Task<HttpResponseMessage> ExecuteAsync(Game game)
        {
            return await _httpClient.PutAsJsonAsync($"api/games/{game}/authentications", new {});
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

            var factory = TestApi.WithClaims(new Claim(JwtClaimTypes.Subject, userId.ToString()));

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

            var credential = new Credential(
                userId,
                Game.LeagueOfLegends,
                new PlayerId(),
                new DateTimeProvider(DateTime.Now));

            var factory = TestApi.WithClaims(new Claim(JwtClaimTypes.Subject, userId.ToString()))
                .WithWebHostBuilder(
                    builder => builder.ConfigureTestContainer<ContainerBuilder>(
                        container =>
                        {
                            var mockCredentialService = new Mock<IGameCredentialService>();

                            var validationFailure = new ValidationResult();
                            validationFailure.Errors.Add(new ValidationFailure("test", "validation failure test"));

                            mockCredentialService.Setup(credentialService => credentialService.FindCredentialAsync(It.IsAny<UserId>(), It.IsAny<Game>()))
                                .ReturnsAsync(credential)
                                .Verifiable();

                            mockCredentialService.Setup(credentialService => credentialService.LinkCredentialAsync(It.IsAny<UserId>(), It.IsAny<Game>()))
                                .ReturnsAsync(new ValidationResult())
                                .Verifiable();

                            container.RegisterInstance(mockCredentialService.Object).As<IGameCredentialService>().SingleInstance();
                        }));

            _httpClient = factory.CreateClient();
            var testServer = factory.Server;
            testServer.CleanupDbContext();

            // Act
            using var response = await this.ExecuteAsync(Game.LeagueOfLegends);

            // Assert
            response.EnsureSuccessStatusCode();
            response.StatusCode.Should().Be(HttpStatusCode.OK);
        }
    }
}
