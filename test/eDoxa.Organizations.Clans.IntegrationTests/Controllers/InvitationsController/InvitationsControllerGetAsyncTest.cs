// Filename: InvitationsControllerGetAsyncTest.cs
// Date Created: 2019-09-30
// 
// ================================================
// Copyright © 2019, eDoxa. All rights reserved.

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

namespace eDoxa.Organizations.Clans.IntegrationTests.Controllers.InvitationsController
{
    public sealed class InvitationsControllerGetAsyncTest : IClassFixture<OrganizationsClansApiFactory>
    {
        public InvitationsControllerGetAsyncTest(OrganizationsClansApiFactory organizationsClansApiFactory)
        {
            _httpClient = organizationsClansApiFactory.CreateClient();
            _testServer = organizationsClansApiFactory.Server;
            _testServer.CleanupDbContext();
        }

        private readonly HttpClient _httpClient;
        private readonly TestServer _testServer;

        private async Task<HttpResponseMessage> ExecuteAsync(UserId userId)
        {
            return await _httpClient.GetAsync($"api/invitations?userId={userId}");
        }

        private async Task<HttpResponseMessage> ExecuteAsync(ClanId clanId)
        {
            return await _httpClient.GetAsync($"api/invitations?clanId={clanId}");
        }

        [Fact]
        public async Task ShouldBeHttpStatusCodeNoContent()
        {
            // Act
            using var response = await this.ExecuteAsync(new ClanId());

            // Assert
            response.EnsureSuccessStatusCode();
            response.StatusCode.Should().Be(HttpStatusCode.NoContent);
        }

        [Fact]
        public async Task WithClanId_ShouldBeHttpStatusCodeOk()
        {
            // Arrange
            var clanId = new ClanId();

            await _testServer.UsingScopeAsync(
                async scope =>
                {
                    var invitationRepository = scope.GetRequiredService<IInvitationRepository>();
                    invitationRepository.Create(new Invitation(new UserId(), clanId));
                    await invitationRepository.UnitOfWork.CommitAsync();
                });

            // Act
            using var response = await this.ExecuteAsync(clanId);

            // Assert
            response.EnsureSuccessStatusCode();
            response.StatusCode.Should().Be(HttpStatusCode.OK);
            var challengeResponses = await response.DeserializeAsync<InvitationResponse[]>();
            challengeResponses.Should().HaveCount(1);
        }

        [Fact]
        public async Task WithUserId_ShouldBeHttpStatusCodeOk()
        {
            // Arrange
            var userId = new UserId();

            await _testServer.UsingScopeAsync(
                async scope =>
                {
                    var invitationRepository = scope.GetRequiredService<IInvitationRepository>();
                    invitationRepository.Create(new Invitation(userId, new ClanId()));
                    await invitationRepository.UnitOfWork.CommitAsync();
                });

            // Act
            using var response = await this.ExecuteAsync(userId);

            // Assert
            response.EnsureSuccessStatusCode();
            response.StatusCode.Should().Be(HttpStatusCode.OK);
            var challengeResponses = await response.DeserializeAsync<InvitationResponse[]>();
            challengeResponses.Should().HaveCount(1);
        }
    }
}
