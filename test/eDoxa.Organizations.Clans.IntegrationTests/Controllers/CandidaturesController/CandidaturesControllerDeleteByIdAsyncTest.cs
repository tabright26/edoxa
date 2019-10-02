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
using eDoxa.Organizations.Clans.TestHelpers;
using eDoxa.Organizations.Clans.TestHelpers.Fixtures;
using eDoxa.Seedwork.Application.Extensions;
using eDoxa.Seedwork.Testing.Extensions;

using FluentAssertions;

using IdentityModel;

using Xunit;

namespace eDoxa.Organizations.Clans.IntegrationTests.Controllers.CandidaturesController
{
    public sealed class CandidaturesControllerDeleteByIdAsyncTest : IntegrationTest
    {

        private HttpClient _httpClient;

        private async Task<HttpResponseMessage> ExecuteAsync(CandidatureId candidatureId)
        {
            return await _httpClient.DeleteAsync($"api/candidatures/{candidatureId}");
        }

        [Fact]
        public async Task ShouldBeHttpStatusCodeBadRequest() //Bad request cause is not owner.
        {
            // Arrange
            var userId = new UserId();
            var clan = new Clan("ClanName", new UserId());
            var candidature = new Candidature(userId, clan.Id);

            var factory = TestApi.WithClaims(new Claim(JwtClaimTypes.Subject, userId.ToString()));
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

            response.StatusCode.Should().Be(HttpStatusCode.BadRequest);
        }

        [Fact]
        public async Task ShouldBeHttpStatusCodeNotFound()
        {
            // Arrange
            var factory = TestApi.WithClaims(new Claim(JwtClaimTypes.Subject, new UserId().ToString()));
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
            var ownerId = new UserId();
            var clan = new Clan("ClanName", ownerId);
            var candidature = new Candidature(new UserId(), clan.Id);

            var factory = TestApi.WithClaims(new Claim(JwtClaimTypes.Subject, ownerId.ToString()));
            _httpClient = factory.CreateClient();
            var testServer = factory.Server;
            testServer.CleanupDbContext();

            await testServer.UsingScopeAsync(
                async scope =>
                {
                    var candidatureRepository = scope.GetRequiredService<ICandidatureRepository>();
                    var clanRepository = scope.GetRequiredService<IClanRepository>();

                    candidatureRepository.Create(candidature);
                    await candidatureRepository.UnitOfWork.CommitAsync();

                    clanRepository.Create(clan);
                    await clanRepository.UnitOfWork.CommitAsync();
                });

            // Act
            using var response = await this.ExecuteAsync(candidature.Id);

            // Assert
            response.EnsureSuccessStatusCode();
            response.StatusCode.Should().Be(HttpStatusCode.OK);
        }

        public CandidaturesControllerDeleteByIdAsyncTest(TestApiFixture testApi, TestMapperFixture testMapper) : base(testApi, testMapper)
        {
        }
    }
}
