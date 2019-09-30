// Filename: CandidaturesControllerDeleteByIdAsyncTest.cs
// Date Created: 2019-09-29
// 
// ================================================
// Copyright © 2019, eDoxa. All rights reserved.

using System.Linq;
using System.Net;
using System.Net.Http;
using System.Threading.Tasks;

using eDoxa.Organizations.Clans.Domain.Models;
using eDoxa.Organizations.Clans.Domain.Repositories;
using eDoxa.Seedwork.Application.Extensions;
using eDoxa.Seedwork.Testing.Extensions;

using FluentAssertions;

using Microsoft.AspNetCore.TestHost;

using Xunit;

namespace eDoxa.Organizations.Clans.IntegrationTests.Controllers.CandidaturesController
{
    public sealed class CandidaturesControllerDeleteByIdAsyncTest : IClassFixture<OrganizationsClansApiFactory>
    {
        public CandidaturesControllerDeleteByIdAsyncTest(OrganizationsClansApiFactory organizationsClansApiFactory)
        {
            _httpClient = organizationsClansApiFactory.CreateClient();
            _testServer = organizationsClansApiFactory.Server;
            _testServer.CleanupDbContext();
        }

        private readonly HttpClient _httpClient;
        private readonly TestServer _testServer;

        private async Task<HttpResponseMessage> ExecuteAsync(CandidatureId candidatureId)
        {
            return await _httpClient.DeleteAsync($"api/candidatures/{candidatureId}");
        }

        // HOW TO TEST BAD REQUEST ?? MAYBE MAKE IT SO THAT ONLY OWNER CAN DECLINE CANDIDATURE ?
        // OTHERWISE, I THINK WE CAN REMOVE BAD REQUEST FROM THE CONTROLLER. CAUSE NO VALIDATION FAILURE IN SERVICE.

        [Fact]
        public async Task ShouldBeHttpStatusCodeBadRequest()
        {
            // Arrange
            var candidatureId = new CandidatureId();

            await _testServer.UsingScopeAsync(
                async scope =>
                {
                    var candidatureRepository = scope.GetRequiredService<ICandidatureRepository>();
                    candidatureRepository.Create(new Candidature(new UserId(), new ClanId()));
                    await candidatureRepository.CommitAsync();

                    var candidatures = await candidatureRepository.FetchAsync();
                    var candidature = candidatures.SingleOrDefault();

                    if (candidature != null)
                    {
                        candidatureId = candidature.Id;
                    }
                });

            // Act
            using var response = await this.ExecuteAsync(candidatureId != null ? candidatureId : new CandidatureId());

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
            var candidatureId = new CandidatureId();

            await _testServer.UsingScopeAsync(
                async scope =>
                {
                    var candidatureRepository = scope.GetRequiredService<ICandidatureRepository>();
                    candidatureRepository.Create(new Candidature(new UserId(), new ClanId()));

                    await candidatureRepository.CommitAsync();

                    var candidatures = await candidatureRepository.FetchAsync();
                    var candidature = candidatures.SingleOrDefault();

                    if (candidature != null)
                    {
                        candidatureId = candidature.Id;
                    }
                });

            // Act
            using var response = await this.ExecuteAsync(candidatureId != null ? candidatureId : new CandidatureId());

            // Assert
            response.EnsureSuccessStatusCode();
            response.StatusCode.Should().Be(HttpStatusCode.OK);
        }
    }
}
