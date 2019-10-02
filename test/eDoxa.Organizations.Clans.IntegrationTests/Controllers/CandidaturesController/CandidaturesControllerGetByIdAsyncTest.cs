// Filename: CandidaturesControllerGetByIdAsyncTest.cs
// Date Created: 2019-09-30
//
// ================================================
// Copyright © 2019, eDoxa. All rights reserved.

using System.Net;
using System.Net.Http;
using System.Security.Claims;
using System.Threading.Tasks;

using eDoxa.Organizations.Clans.Api.Areas.Clans.Responses;
using eDoxa.Organizations.Clans.Domain.Models;
using eDoxa.Organizations.Clans.Domain.Repositories;
using eDoxa.Seedwork.Application.Extensions;
using eDoxa.Seedwork.Testing.Extensions;
using eDoxa.Seedwork.Testing.Http.Extensions;

using FluentAssertions;

using IdentityModel;

using Xunit;

namespace eDoxa.Organizations.Clans.IntegrationTests.Controllers.CandidaturesController
{
    public sealed class CandidaturesControllerGetByIdAsyncTest : IClassFixture<OrganizationsClansApiFactory>
    {
        private readonly OrganizationsClansApiFactory _apiFactory;
        public CandidaturesControllerGetByIdAsyncTest(OrganizationsClansApiFactory apiFactory)
        {
            _apiFactory = apiFactory;
            _httpClient = new HttpClient();
        }
        private HttpClient _httpClient;

        private async Task<HttpResponseMessage> ExecuteAsync(CandidatureId candidatureId)
        {
            return await _httpClient.GetAsync($"api/candidatures/{candidatureId}");
        }

        [Fact]
        public async Task ShouldBeHttpStatusCodeNotFound()
        {
            // Arrange
            var factory = _apiFactory.WithClaims(new Claim(JwtClaimTypes.Subject, new UserId().ToString()));
            _httpClient = factory.CreateClient();
            var testServer = factory.Server;
            testServer.CleanupDbContext();

            // Act
            using var response = await this.ExecuteAsync(new CandidatureId());

            // Assert
            response.StatusCode.Should().Be(HttpStatusCode.NotFound);
        }

        [Fact]
        public async Task ShouldBeHttpStatusCodeOk()
        {
            // Arrange
            var userId = new UserId();
            var clan = new Clan("ClanName", new UserId());
            var candidature = new Candidature(userId, clan.Id);

            var factory = _apiFactory.WithClaims(new Claim(JwtClaimTypes.Subject, userId.ToString()));
            _httpClient = factory.CreateClient();
            var testServer = factory.Server;
            testServer.CleanupDbContext();

            await testServer.UsingScopeAsync(
                async scope =>
                {
                    var candidatureRepository = scope.GetRequiredService<ICandidatureRepository>();
                    candidatureRepository.Create(candidature);
                    await candidatureRepository.UnitOfWork.CommitAsync();
                });

            // Act
            using var response = await this.ExecuteAsync(candidature.Id);

            // Assert
            response.EnsureSuccessStatusCode();
            response.StatusCode.Should().Be(HttpStatusCode.OK);
            var challengeResponse = await response.DeserializeAsync<CandidatureResponse>();
            challengeResponse!.Id.Should().Be(candidature.Id);
        }
    }
}
