// Filename: CandidaturesControllerPostByIdAsyncTest.cs
// Date Created: 2019-09-29
// 
// ================================================
// Copyright © 2019, eDoxa. All rights reserved.

using System.Net;
using System.Net.Http;
using System.Threading.Tasks;

using eDoxa.Organizations.Clans.Domain.Models;
using eDoxa.Organizations.Clans.Domain.Repositories;
using eDoxa.Seedwork.Application.Extensions;
using eDoxa.Seedwork.Testing.Extensions;
using eDoxa.Seedwork.Testing.Http;

using FluentAssertions;

using Microsoft.AspNetCore.TestHost;

using Xunit;

namespace eDoxa.Organizations.Clans.IntegrationTests.Controllers.CandidaturesController
{
    public sealed class CandidaturesControllerPostByIdAsyncTest : IClassFixture<OrganizationsClansApiFactory>
    {
        public CandidaturesControllerPostByIdAsyncTest(OrganizationsClansApiFactory organizationsClansApiFactory)
        {
            _httpClient = organizationsClansApiFactory.CreateClient();
            _testServer = organizationsClansApiFactory.Server;
            _testServer.CleanupDbContext();
        }

        private readonly HttpClient _httpClient;
        private readonly TestServer _testServer;

        private async Task<HttpResponseMessage> ExecuteAsync(CandidatureId candidatureId)
        {
            return await _httpClient.PostAsync($"api/candidatures/{candidatureId}", new JsonContent(""));
        }

        // Do I need to test out all single bad request possible ?

        [Fact]
        public async Task ShouldBeHttpStatusCodeBadRequest() //Clan does not exist bad request.
        {
            // Arrange
            var candidature = new Candidature(new UserId(), new ClanId());

            await _testServer.UsingScopeAsync(
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
            var clan = new Clan("TestClan", ownerId);
            var candidature = new Candidature(new UserId(), clan.Id);

            await _testServer.UsingScopeAsync(
                async scope =>
                {
                    var candidatureRepository = scope.GetRequiredService<ICandidatureRepository>();
                    var clanRepository = scope.GetRequiredService<IClanRepository>();
                    clanRepository.Create(clan);
                    await clanRepository.UnitOfWork.CommitAsync();
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
