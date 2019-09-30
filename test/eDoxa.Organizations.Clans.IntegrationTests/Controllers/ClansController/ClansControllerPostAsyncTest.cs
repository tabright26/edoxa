// Filename: ClansControllerPostAsyncTest.cs
// Date Created: 2019-09-30
// 
// ================================================
// Copyright © 2019, eDoxa. All rights reserved.

using System.Net;
using System.Net.Http;
using System.Threading.Tasks;

using eDoxa.Organizations.Clans.Api.Areas.Clans.Requests;
using eDoxa.Organizations.Clans.Domain.Models;
using eDoxa.Organizations.Clans.Domain.Repositories;
using eDoxa.Seedwork.Application.Extensions;
using eDoxa.Seedwork.Testing.Extensions;
using eDoxa.Seedwork.Testing.Http;

using FluentAssertions;

using Microsoft.AspNetCore.TestHost;

using Xunit;

namespace eDoxa.Organizations.Clans.IntegrationTests.Controllers.ClansController
{
    public sealed class ClansControllerPostAsyncTest : IClassFixture<OrganizationsClansApiFactory>
    {
        public ClansControllerPostAsyncTest(OrganizationsClansApiFactory organizationsClansApiFactory)
        {
            _httpClient = organizationsClansApiFactory.CreateClient();
            _testServer = organizationsClansApiFactory.Server;
            _testServer.CleanupDbContext();
        }

        private readonly HttpClient _httpClient;
        private readonly TestServer _testServer;

        private async Task<HttpResponseMessage> ExecuteAsync(ClanPostRequest clanPostRequest)
        {
            return await _httpClient.PostAsync("api/clans", new JsonContent(clanPostRequest));
        }

        [Fact]
        public async Task ShouldBeHttpStatusCodeBadRequest() // Already a clan named like that bad request
        {
            // Arrange
            await _testServer.UsingScopeAsync(
                async scope =>
                {
                    var clanRepository = scope.GetRequiredService<IClanRepository>();

                    clanRepository.Create(new Clan("TestClan", new UserId()));
                    await clanRepository.CommitAsync();
                });

            // Act
            using var response = await this.ExecuteAsync(new ClanPostRequest("TestClan", "This is a summary"));

            // Assert
            response.EnsureSuccessStatusCode();
            response.StatusCode.Should().Be(HttpStatusCode.OK);
        }

        [Fact]
        public async Task ShouldBeHttpStatusCodeOk()
        {
            // Act
            using var response = await this.ExecuteAsync(new ClanPostRequest("TestClan", "This is a summary"));

            // Assert
            response.EnsureSuccessStatusCode();
            response.StatusCode.Should().Be(HttpStatusCode.OK);
        }
    }
}
