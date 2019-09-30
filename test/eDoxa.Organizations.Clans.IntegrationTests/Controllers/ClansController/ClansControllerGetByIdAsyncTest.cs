// Filename: ClansControllerGetByIdAsyncTest.cs
// Date Created: 2019-09-30
// 
// ================================================
// Copyright © 2019, eDoxa. All rights reserved.

using System.Linq;
using System.Net;
using System.Net.Http;
using System.Threading.Tasks;

using eDoxa.Organizations.Clans.Api.Areas.Clans.Responses;
using eDoxa.Organizations.Clans.Domain.Models;
using eDoxa.Organizations.Clans.Domain.Repositories;
using eDoxa.Seedwork.Application.Extensions;
using eDoxa.Seedwork.Testing.Extensions;
using eDoxa.Seedwork.Testing.Http.Extensions;

using FluentAssertions;

using Microsoft.AspNetCore.TestHost;

using Xunit;

namespace eDoxa.Organizations.Clans.IntegrationTests.Controllers.ClansController
{
    public sealed class ClansControllerGetByIdAsyncTest : IClassFixture<OrganizationsClansApiFactory>
    {
        public ClansControllerGetByIdAsyncTest(OrganizationsClansApiFactory organizationsClansApiFactory)
        {
            _httpClient = organizationsClansApiFactory.CreateClient();
            _testServer = organizationsClansApiFactory.Server;
            _testServer.CleanupDbContext();
        }

        private readonly HttpClient _httpClient;
        private readonly TestServer _testServer;

        private async Task<HttpResponseMessage> ExecuteAsync(ClanId clanId)
        {
            return await _httpClient.GetAsync($"api/clans/{clanId}");
        }

        [Fact]
        public async Task ShouldBeHttpStatusCodeNoContent()
        {
            // Act
            using var response = await this.ExecuteAsync(new ClanId());

            // Assert
            response.EnsureSuccessStatusCode();
            response.StatusCode.Should().Be(HttpStatusCode.NotFound);
        }

        [Fact]
        public async Task ShouldBeHttpStatusCodeOk()
        {
            // Arrange
            var clanId = new ClanId();

            await _testServer.UsingScopeAsync(
                async scope =>
                {
                    var clanRepository = scope.GetRequiredService<IClanRepository>();

                    clanRepository.Create(new Clan("TestClan", new UserId()));
                    await clanRepository.CommitAsync();

                    var clans = await clanRepository.FetchClansAsync();
                    var clan = clans.SingleOrDefault();

                    if (clan != null)
                    {
                        clanId = clan.Id;
                    }
                });

            // Act
            using var response = await this.ExecuteAsync(clanId != null ? clanId : new ClanId());

            // Assert
            response.EnsureSuccessStatusCode();
            response.StatusCode.Should().Be(HttpStatusCode.OK);
            var challengeResponses = await response.DeserializeAsync<InvitationResponse[]>();
            challengeResponses.Should().HaveCount(1);
        }
    }
}
