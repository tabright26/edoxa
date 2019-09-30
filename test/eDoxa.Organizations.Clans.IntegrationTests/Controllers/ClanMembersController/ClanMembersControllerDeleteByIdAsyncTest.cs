// Filename: ClanMembersControllerDeleteByIdAsyncTest.cs
// Date Created: 2019-09-30
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
            using var response = await this.ExecuteAsync(clanId != null ? clanId : new ClanId(), new MemberId());

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
            var clanId = new ClanId();
            var memberId = new MemberId();

            await _testServer.UsingScopeAsync(
                async scope =>
                {
                    var clanRepository = scope.GetRequiredService<IClanRepository>();
                    clanRepository.Create(new Clan("TestClan", new UserId()));

                    await clanRepository.CommitAsync();

                    var clans = await clanRepository.FetchClansAsync();
                    var clan = clans.SingleOrDefault();

                    var userId = new UserId();

                    if (clan != null)
                    {
                        clanId = clan.Id;
                        clan.AddMember(new Invitation(userId, clanId));
                    }

                    await clanRepository.CommitAsync();

                    var members = await clanRepository.FetchMembersAsync(clanId);
                    var target = members.SingleOrDefault(member => member.UserId == userId);

                    if (target != null)
                    {
                        memberId = target.Id;
                    }
                });

            // Act
            using var response = await this.ExecuteAsync(clanId != null ? clanId : new ClanId(), memberId);

            // Assert
            response.EnsureSuccessStatusCode();
            response.StatusCode.Should().Be(HttpStatusCode.OK);
        }
    }
}
