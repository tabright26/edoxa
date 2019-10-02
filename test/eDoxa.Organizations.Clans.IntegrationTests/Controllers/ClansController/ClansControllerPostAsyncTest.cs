// Filename: ClansControllerPostAsyncTest.cs
// Date Created: 2019-09-30
//
// ================================================
// Copyright © 2019, eDoxa. All rights reserved.

using System.Net;
using System.Net.Http;
using System.Security.Claims;
using System.Threading.Tasks;

using eDoxa.Organizations.Clans.Api.Areas.Clans.Requests;
using eDoxa.Organizations.Clans.Domain.Models;
using eDoxa.Organizations.Clans.Domain.Repositories;
using eDoxa.Organizations.Clans.TestHelpers.Fixtures;
using eDoxa.Seedwork.Application.Extensions;
using eDoxa.Seedwork.Testing.Extensions;
using eDoxa.Seedwork.Testing.Http;

using FluentAssertions;

using IdentityModel;

using Xunit;

namespace eDoxa.Organizations.Clans.IntegrationTests.Controllers.ClansController
{
    public sealed class ClansControllerPostAsyncTest : IClassFixture<TestApiFixture>
    {
        private readonly TestApiFixture _apiFixture;

        public ClansControllerPostAsyncTest(TestApiFixture apiFixture)
        {
            _apiFixture = apiFixture;
            _httpClient = new HttpClient();
        }

        private HttpClient _httpClient;

        private async Task<HttpResponseMessage> ExecuteAsync(ClanPostRequest clanPostRequest)
        {
            return await _httpClient.PostAsync("api/clans", new JsonContent(clanPostRequest));
        }

        [Fact]
        public async Task ShouldBeHttpStatusCodeBadRequest() // Already a clan named like that bad request
        {
            // Arrange
            var userId = new UserId();
            var clan = new Clan("ClanName", new UserId());

            var factory = _apiFixture.WithClaims(new Claim(JwtClaimTypes.Subject, userId.ToString()));
            _httpClient = factory.CreateClient();
            var testServer = factory.Server;
            testServer.CleanupDbContext();

            await testServer.UsingScopeAsync(
                async scope =>
                {
                    var clanRepository = scope.GetRequiredService<IClanRepository>();
                    clanRepository.Create(clan);
                    await clanRepository.UnitOfWork.CommitAsync();
                });

            // Act
            using var response = await this.ExecuteAsync(new ClanPostRequest(clan.Name, "This is a summary"));

            // Assert
            response.StatusCode.Should().Be(HttpStatusCode.BadRequest);
        }

        [Fact]
        public async Task ShouldBeHttpStatusCodeOk()
        {
            // Arrange
            var userId = new UserId();

            var factory = _apiFixture.WithClaims(new Claim(JwtClaimTypes.Subject, userId.ToString()));
            _httpClient = factory.CreateClient();
            var testServer = factory.Server;
            testServer.CleanupDbContext();

            // Act
            using var response = await this.ExecuteAsync(new ClanPostRequest("TestClan", "This is a summary"));

            // Assert
            response.EnsureSuccessStatusCode();
            response.StatusCode.Should().Be(HttpStatusCode.OK);
        }
    }
}
