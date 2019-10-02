// Filename: InvitationsControllerGetAsyncTest.cs
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

namespace eDoxa.Organizations.Clans.IntegrationTests.Controllers.InvitationsController
{
    public sealed class InvitationsControllerGetAsyncTest : IClassFixture<OrganizationsClansApiFactory>
    {
        private readonly OrganizationsClansApiFactory _apiFactory;
        public InvitationsControllerGetAsyncTest(OrganizationsClansApiFactory apiFactory)
        {
            _apiFactory = apiFactory;
            _httpClient = new HttpClient();
        }
        private HttpClient _httpClient;

        private async Task<HttpResponseMessage> ExecuteAsync(UserId userId)
        {
            return await _httpClient.GetAsync($"api/invitations?userId={userId}");
        }

        private async Task<HttpResponseMessage> ExecuteAsync(ClanId clanId)
        {
            return await _httpClient.GetAsync($"api/invitations?clanId={clanId}");
        }

        [Fact]
        public async Task WithUserId_ShouldBeHttpStatusCodeNoContent()
        {
            // Arrange
            var factory = _apiFactory.WithClaims(new Claim(JwtClaimTypes.Subject, new UserId().ToString()));
            _httpClient = factory.CreateClient();
            var testServer = factory.Server;
            testServer.CleanupDbContext();

            // Act
            using var response = await this.ExecuteAsync(new UserId());

            // Assert
            response.EnsureSuccessStatusCode();
            response.StatusCode.Should().Be(HttpStatusCode.NoContent);
        }

        [Fact]
        public async Task WithClanId_ShouldBeHttpStatusCodeNoContent()
        {
            // Arrange
            var factory = _apiFactory.WithClaims(new Claim(JwtClaimTypes.Subject, new UserId().ToString()));
            _httpClient = factory.CreateClient();
            var testServer = factory.Server;
            testServer.CleanupDbContext();

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
            var userId = new UserId();
            var clan = new Clan("ClanName", new UserId());
            var invitation = new Invitation(userId, clan.Id);

            var factory = _apiFactory.WithClaims(new Claim(JwtClaimTypes.Subject, userId.ToString()));
            _httpClient = factory.CreateClient();
            var testServer = factory.Server;
            testServer.CleanupDbContext();

            await testServer.UsingScopeAsync(
                async scope =>
                {
                    var invitationRepository = scope.GetRequiredService<IInvitationRepository>();
                    invitationRepository.Create(invitation);
                    await invitationRepository.UnitOfWork.CommitAsync();
                });

            // Act
            using var response = await this.ExecuteAsync(clan.Id);

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
            var clan = new Clan("ClanName", new UserId());
            var invitation = new Invitation(userId, clan.Id);

            var factory = _apiFactory.WithClaims(new Claim(JwtClaimTypes.Subject, userId.ToString()));
            _httpClient = factory.CreateClient();
            var testServer = factory.Server;
            testServer.CleanupDbContext();

            await testServer.UsingScopeAsync(
                async scope =>
                {
                    var invitationRepository = scope.GetRequiredService<IInvitationRepository>();
                    invitationRepository.Create(invitation);
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
