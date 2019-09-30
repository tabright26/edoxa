// Filename: InvitationsControllerGetByIdAsyncTest.cs
// Date Created: 2019-09-29
// 
// ================================================
// Copyright © 2019, eDoxa. All rights reserved.

using System.Collections.Generic;
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

namespace eDoxa.Organizations.Clans.IntegrationTests.Controllers.InvitationsController
{
    public sealed class InvitationsControllerGetByIdAsyncTest : IClassFixture<OrganizationsClansApiFactory>
    {
        public InvitationsControllerGetByIdAsyncTest(OrganizationsClansApiFactory organizationsClansApiFactory)
        {
            _httpClient = organizationsClansApiFactory.CreateClient();
            _testServer = organizationsClansApiFactory.Server;
            _testServer.CleanupDbContext();
        }

        private readonly HttpClient _httpClient;
        private readonly TestServer _testServer;

        private async Task<HttpResponseMessage> ExecuteAsync(InvitationId invitationId)
        {
            return await _httpClient.GetAsync($"api/invitations/{invitationId}");
        }

        [Fact]
        public async Task ShouldBeHttpStatusCodeNoContent()
        {
            // Act
            using var response = await this.ExecuteAsync(new InvitationId());

            // Assert
            response.EnsureSuccessStatusCode();
            response.StatusCode.Should().Be(HttpStatusCode.NoContent);
        }

        [Fact]
        public async Task ShouldBeHttpStatusCodeOk()
        {
            // Arrange

            var invitation = new List<Invitation>();

            await _testServer.UsingScopeAsync(
                async scope =>
                {
                    var invitationRepository = scope.GetRequiredService<IInvitationRepository>();
                    invitationRepository.Create(new Invitation(new UserId(), new ClanId()));
                    await invitationRepository.CommitAsync();

                    var invitations = await invitationRepository.FetchAsync();
                    invitation = invitation.ToList();
                });

            // Act
            using var response = await this.ExecuteAsync(invitation.FirstOrDefault() != null ? invitation.First().Id : new InvitationId());

            // Assert
            response.EnsureSuccessStatusCode();
            response.StatusCode.Should().Be(HttpStatusCode.OK);
            var challengeResponses = await response.DeserializeAsync<InvitationResponse[]>();
            challengeResponses.Should().HaveCount(1);
        }
    }
}
