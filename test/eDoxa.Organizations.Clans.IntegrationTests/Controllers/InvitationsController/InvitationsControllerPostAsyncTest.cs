// Filename: InvitationsControllerPostAsyncTest.cs
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
            var clan = new Clan("TestClan", ownerId);

            await _testServer.UsingScopeAsync(
                async scope =>
                {
                    var clanRepository = scope.GetRequiredService<IClanRepository>();
                    clanRepository.Create(clan);
                    await clanRepository.UnitOfWork.CommitAsync();
                });

            // Act
            using var response = await this.ExecuteAsync(new InvitationPostRequest(ownerId, clan.Id));

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
            var clan = new Clan("TestClan", new UserId());

            await _testServer.UsingScopeAsync(
                async scope =>
                {
                    var clanRepository = scope.GetRequiredService<IClanRepository>();
                    clanRepository.Create(clan);
                    await clanRepository.UnitOfWork.CommitAsync();
                });

            // Act
            using var response = await this.ExecuteAsync(new InvitationPostRequest(new UserId(), clan.Id));

            // Assert
            response.EnsureSuccessStatusCode();
            response.StatusCode.Should().Be(HttpStatusCode.OK);
        }
    }
}
