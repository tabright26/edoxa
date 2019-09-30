// Filename: InvitationsControllerPostAsyncTest.cs
// Date Created: 2019-09-29
//
// ================================================
// Copyright © 2019, eDoxa. All rights reserved.

using System.Linq;
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

namespace eDoxa.Organizations.Clans.IntegrationTests.Controllers.InvitationsController
{
    public sealed class InvitationsControllerPostAsyncTest : IClassFixture<OrganizationsClansApiFactory>
    {
        public InvitationsControllerPostAsyncTest(OrganizationsClansApiFactory organizationsClansApiFactory)
        {
            _httpClient = organizationsClansApiFactory.CreateClient();
            _testServer = organizationsClansApiFactory.Server;
            _testServer.CleanupDbContext();
        }

        private readonly HttpClient _httpClient;
        private readonly TestServer _testServer;

        private async Task<HttpResponseMessage> ExecuteAsync(InvitationPostRequest invitationPostRequest)
        {
            return await _httpClient.PostAsync("api/invitations", new JsonContent(invitationPostRequest));
        }

        [Fact]
        public async Task ShouldBeHttpStatusCodeBadRequest() // Is the owner bad request
        {
            // Arrange
            var ownerId = new UserId();
            var clanId = new ClanId();

            await _testServer.UsingScopeAsync(
                async scope =>
                {
                    var clanRepository = scope.GetRequiredService<IClanRepository>();

                    clanRepository.Create(new Clan("TestClan", ownerId));
                    await clanRepository.CommitAsync();

                    var clans = await clanRepository.FetchClansAsync();
                    var clan = clans.SingleOrDefault();

                    if (clan != null)
                    {
                        clanId = clan.Id;
                    }
                });

            // Act
            using var response = await this.ExecuteAsync(new InvitationPostRequest(ownerId, clanId));

            // Assert
            response.EnsureSuccessStatusCode();
            response.StatusCode.Should().Be(HttpStatusCode.OK);
        }

        [Fact]
        public async Task ShouldBeHttpStatusCodeNotFound()
        {
            // Act
            using var response = await this.ExecuteAsync(new InvitationPostRequest(new UserId(), new ClanId()));

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
            using var response = await this.ExecuteAsync(new InvitationPostRequest(new UserId(), clanId));

            // Assert
            response.EnsureSuccessStatusCode();
            response.StatusCode.Should().Be(HttpStatusCode.OK);
        }
    }
}
