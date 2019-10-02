// Filename: CandidaturesControllerDeleteByIdAsyncTest.cs
// Date Created: 2019-09-29
// 
// ================================================
// Copyright © 2019, eDoxa. All rights reserved.

using System.Net;
using System.Net.Http;
using System.Security.Claims;
using System.Threading.Tasks;

using eDoxa.Organizations.Clans.Domain.Models;
using eDoxa.Organizations.Clans.Domain.Repositories;
using eDoxa.Seedwork.Application.Extensions;
using eDoxa.Seedwork.Testing.Extensions;

using FluentAssertions;

using IdentityModel;

using Xunit;

namespace eDoxa.Organizations.Clans.IntegrationTests.Controllers.CandidaturesController
{
    public sealed class CandidaturesControllerDeleteByIdAsyncTest : IClassFixture<OrganizationsClansApiFactory>
    {
        private readonly OrganizationsClansApiFactory _apiFactory;

        public CandidaturesControllerDeleteByIdAsyncTest(OrganizationsClansApiFactory apiFactory)
        {
            _apiFactory = apiFactory;
        }

        private HttpClient _httpClient;

        private async Task<HttpResponseMessage> ExecuteAsync(CandidatureId candidatureId)
        {
            return await _httpClient.DeleteAsync($"api/candidatures/{candidatureId}");
        }

        [Fact]
        public async Task ShouldBeHttpStatusCodeBadRequest()
        {
            // Arrange
            var ownerId = new UserId();
            var clan = new Clan("ClanName", ownerId);
            var candidature = new Candidature(new UserId(), clan.Id);

            var factory = _apiFactory.WithClaims(new Claim(JwtClaimTypes.Subject, ownerId.ToString()));
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
            response.StatusCode.Should().Be(HttpStatusCode.BadRequest);
        }

        [Fact]
        public async Task ShouldBeHttpStatusCodeNotFound()
        {
            // Act
            using var response = await this.ExecuteAsync(new CandidatureId());

            // Assert
            response.EnsureSuccessStatusCode();
            response.StatusCode.Should().Be(HttpStatusCode.NotFound);
        }

        [Fact]
        public async Task ShouldBeHttpStatusCodeOk()
        {
            // Arrange
            var ownerId = new UserId();
            var clan = new Clan("ClanName", ownerId);
            var candidature = new Candidature(new UserId(), clan.Id);

            var factory = _apiFactory.WithClaims(new Claim(JwtClaimTypes.Subject, ownerId.ToString()));
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
        }
    }
}
