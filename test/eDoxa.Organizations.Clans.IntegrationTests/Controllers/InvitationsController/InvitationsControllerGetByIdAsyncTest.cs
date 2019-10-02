// Filename: InvitationsControllerGetByIdAsyncTest.cs
// Date Created: 2019-10-02
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
using eDoxa.Organizations.Clans.TestHelpers;
using eDoxa.Organizations.Clans.TestHelpers.Fixtures;
using eDoxa.Seedwork.Application.Extensions;
using eDoxa.Seedwork.Testing.Extensions;
using eDoxa.Seedwork.Testing.Http.Extensions;

using FluentAssertions;

using IdentityModel;

using Xunit;

namespace eDoxa.Organizations.Clans.IntegrationTests.Controllers.InvitationsController
{
    public sealed class InvitationsControllerGetByIdAsyncTest : IntegrationTest
    {
        public InvitationsControllerGetByIdAsyncTest(TestApiFixture testApi, TestMapperFixture testMapper) : base(testApi, testMapper)
        {
        }

        private HttpClient _httpClient;

        private async Task<HttpResponseMessage> ExecuteAsync(InvitationId invitationId)
        {
            return await _httpClient.GetAsync($"api/invitations/{invitationId}");
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
            using var response = await this.ExecuteAsync(new InvitationId());

            // Assert
            response.StatusCode.Should().Be(HttpStatusCode.NotFound);
        }

        [Fact]
        public async Task ShouldBeHttpStatusCodeOk()
        {
            // Arrange
            var userId = new UserId();
            var clan = new Clan("ClanName", new UserId());
            var invitation = new Invitation(userId, clan.Id);

            var factory = TestApi.WithClaims(new Claim(JwtClaimTypes.Subject, userId.ToString()));
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
            response.EnsureSuccessStatusCode();
            response.StatusCode.Should().Be(HttpStatusCode.OK);
            var invitationResponse = await response.DeserializeAsync<InvitationResponse>();
            invitationResponse!.Id.Should().Be(invitation.Id);
        }
    }
}
