// Filename: ClanMembersControllerDeleteByIdAsyncTest.cs
// Date Created: 2019-09-30
// 
// ================================================
// Copyright © 2019, eDoxa. All rights reserved.

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

namespace eDoxa.Organizations.Clans.IntegrationTests.Controllers.ClanMembersController
{
    public sealed class ClanMembersControllerDeleteByIdAsyncTest : IClassFixture<OrganizationsClansApiFactory>
    {
        public ClanMembersControllerDeleteByIdAsyncTest(OrganizationsClansApiFactory organizationsClansApiFactory)
        {
            _httpClient = organizationsClansApiFactory.CreateClient();
            _testServer = organizationsClansApiFactory.Server;
            _testServer.CleanupDbContext();
        }

        private readonly HttpClient _httpClient;
        private readonly TestServer _testServer;

        private async Task<HttpResponseMessage> ExecuteAsync(ClanId clanId, MemberId memberId)
        {
            return await _httpClient.DeleteAsync($"api/clans/{clanId}/members/{memberId}");
        }

        [Fact]
        public async Task ShouldBeHttpStatusCodeBadRequest() // Is not owner bad request
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
            using var response = await this.ExecuteAsync(clan.Id, new MemberId());

            // Assert
            response.EnsureSuccessStatusCode();
            response.StatusCode.Should().Be(HttpStatusCode.OK);
        }

        [Fact]
        public async Task ShouldBeHttpStatusCodeNotFound()
        {
            // Act
            using var response = await this.ExecuteAsync(new ClanId(), new MemberId());

            // Assert
            response.EnsureSuccessStatusCode();
            response.StatusCode.Should().Be(HttpStatusCode.NotFound);
        }

        [Fact]
        public async Task ShouldBeHttpStatusCodeOk() // How can I use the same http context user id, otherwise not owner.
        {
            // Arrange
            var userId = new UserId();
            var clan = new Clan("TestClan", new UserId());
            clan.AddMember(new Invitation(userId, clan.Id));
            var member = clan.FindMember(userId);

            await _testServer.UsingScopeAsync(
                async scope =>
                {
                    var clanRepository = scope.GetRequiredService<IClanRepository>();
                    clanRepository.Create(clan);
                    await clanRepository.UnitOfWork.CommitAsync();
                });

            // Act
            using var response = await this.ExecuteAsync(clan.Id, member.Id);

            // Assert
            response.EnsureSuccessStatusCode();
            response.StatusCode.Should().Be(HttpStatusCode.OK);
        }
    }
}
