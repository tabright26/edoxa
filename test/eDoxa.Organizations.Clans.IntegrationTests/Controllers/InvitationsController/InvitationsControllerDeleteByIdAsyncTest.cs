// Filename: InvitationsControllerDeleteByIdAsyncTest.cs
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

namespace eDoxa.Organizations.Clans.IntegrationTests.Controllers.InvitationsController
{
    public sealed class InvitationsControllerDeleteByIdAsyncTest : IClassFixture<OrganizationsClansApiFactory>
    {
        public InvitationsControllerDeleteByIdAsyncTest(OrganizationsClansApiFactory organizationsClansApiFactory)
        {
            _httpClient = organizationsClansApiFactory.CreateClient();
            _testServer = organizationsClansApiFactory.Server;
            _testServer.CleanupDbContext();
        }

        private readonly HttpClient _httpClient;
        private readonly TestServer _testServer;

        private async Task<HttpResponseMessage> ExecuteAsync(InvitationId invitationId)
        {
            return await _httpClient.DeleteAsync($"api/invitations/{invitationId}");
        }

        // HOW TO TEST BAD REQUEST ?? MAYBE MAKE IT SO THAT ONLY OWNER CAN DECLINE CANDIDATURE ?
        // OTHERWISE, I THINK WE CAN REMOVE BAD REQUEST FROM THE CONTROLLER. CAUSE NO VALIDATION FAILURE IN SERVICE.

        [Fact]
        public async Task ShouldBeHttpStatusCodeBadRequest()
        {
            // Arrange
            var invitationId = new InvitationId();

            await _testServer.UsingScopeAsync(
                async scope =>
                {
                    var invitationRepository = scope.GetRequiredService<IInvitationRepository>();
                    invitationRepository.Create(new Invitation(new UserId(), new ClanId()));
                    await invitationRepository.CommitAsync();

                    var invitations = await invitationRepository.FetchAsync();
                    var invitation = invitations.SingleOrDefault();

                    if (invitation != null)
                    {
                        invitationId = invitation.Id;
                    }
                });

            // Act
            using var response = await this.ExecuteAsync(invitationId != null ? invitationId : new InvitationId());

            // Assert
            response.EnsureSuccessStatusCode();
            response.StatusCode.Should().Be(HttpStatusCode.BadRequest);
        }

        [Fact]
        public async Task ShouldBeHttpStatusCodeNotFound()
        {
            // Act
            using var response = await this.ExecuteAsync(new InvitationId());

            // Assert
            response.EnsureSuccessStatusCode();
            response.StatusCode.Should().Be(HttpStatusCode.NotFound);
        }

        [Fact]
        public async Task ShouldBeHttpStatusCodeOk()
        {
            // Arrange
            var invitationId = new InvitationId();

            await _testServer.UsingScopeAsync(
                async scope =>
                {
                    var invitationRepository = scope.GetRequiredService<IInvitationRepository>();
                    invitationRepository.Create(new Invitation(new UserId(), new ClanId()));

                    await invitationRepository.CommitAsync();

                    var invitations = await invitationRepository.FetchAsync();
                    var invitation = invitations.SingleOrDefault();

                    if (invitation != null)
                    {
                        invitationId = invitation.Id;
                    }
                });

            // Act
            using var response = await this.ExecuteAsync(invitationId != null ? invitationId : new InvitationId());

            // Assert
            response.EnsureSuccessStatusCode();
            response.StatusCode.Should().Be(HttpStatusCode.OK);
        }
    }
}
