// Filename: InvitationsControllerPostByIdAsyncTest.cs
// Date Created: 2019-09-30
//
// ================================================
// Copyright © 2019, eDoxa. All rights reserved.

using System.Net;
using System.Net.Http;
using System.Security.Claims;
using System.Threading.Tasks;

using eDoxa.Organizations.Clans.Domain.Models;
using eDoxa.Organizations.Clans.Domain.Repositories;
using eDoxa.Organizations.Clans.TestHelpers.Fixtures;
using eDoxa.Seedwork.Application.Extensions;
using eDoxa.Seedwork.Testing.Extensions;
using eDoxa.Seedwork.Testing.Http;
using eDoxa.Seedwork.Testing.Http.Extensions;

using FluentAssertions;

using IdentityModel;

using Xunit;

namespace eDoxa.Organizations.Clans.IntegrationTests.Controllers.InvitationsController
{
    public sealed class InvitationsControllerPostByIdAsyncTest : IClassFixture<TestApiFixture>
    {
        private readonly TestApiFixture _apiFixture;

        public InvitationsControllerPostByIdAsyncTest(TestApiFixture apiFixture)
        {
            _apiFixture = apiFixture;
            _httpClient = new HttpClient();
        }
        private HttpClient _httpClient;

        private async Task<HttpResponseMessage> ExecuteAsync(InvitationId invitationId)
        {
            return await _httpClient.PostAsync($"api/invitations/{invitationId}", JsonContent.EmptyBody);
        }

        // Do I need to test out all single bad request possible ?

        [Fact]
        public async Task ShouldBeHttpStatusCodeBadRequest() //Clan does not exist bad request.
        {
            // Arrange
            var invitation = new Invitation(new UserId(), new ClanId());

            var factory = _apiFixture.WithClaims(new Claim(JwtClaimTypes.Subject, new UserId().ToString()));
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
            using var response = await this.ExecuteAsync(invitation.Id);

            // Assert
            response.StatusCode.Should().Be(HttpStatusCode.BadRequest);
        }

        [Fact]
        public async Task ShouldBeHttpStatusCodeNotFound()
        {
            // Arrange
            var factory = _apiFixture.WithClaims(new Claim(JwtClaimTypes.Subject, new UserId().ToString()));
            _httpClient = factory.CreateClient();
            var testServer = factory.Server;
            testServer.CleanupDbContext();

            // Act
            using var response = await this.ExecuteAsync(new InvitationId());

            // Assert
            response.StatusCode.Should().Be(HttpStatusCode.NotFound);
        }

        [Fact]
        public async Task ShouldBeHttpStatusCodeOk()
        {
            // Arrange
            var clan = new Clan("ClanName", new UserId());
            var userId = new UserId();
            var invitation = new Invitation(userId, clan.Id);

            var factory = _apiFixture.WithClaims(new Claim(JwtClaimTypes.Subject, userId.ToString()));
            _httpClient = factory.CreateClient();
            var testServer = factory.Server;
            testServer.CleanupDbContext();

            await testServer.UsingScopeAsync(
                async scope =>
                {
                    var invitationRepository = scope.GetRequiredService<IInvitationRepository>();
                    var clanRepository = scope.GetRequiredService<IClanRepository>();

                    invitationRepository.Create(invitation);
                    await invitationRepository.UnitOfWork.CommitAsync();

                    clanRepository.Create(clan);
                    await clanRepository.UnitOfWork.CommitAsync();
                });

            // Act
            using var response = await this.ExecuteAsync(invitation.Id);

            // Assert
            var test = await response.DeserializeAsync<dynamic>();
            
            response.EnsureSuccessStatusCode();
            response.StatusCode.Should().Be(HttpStatusCode.OK);
        }
    }
}
