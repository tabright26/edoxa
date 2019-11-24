// Filename: ClanMembersControllerDeleteByIdAsyncTest.cs
// Date Created: 2019-10-02
// 
// ================================================
// Copyright © 2019, eDoxa. All rights reserved.

using System.Linq;
using System.Net;
using System.Net.Http;
using System.Threading.Tasks;

using eDoxa.Clans.Domain.Models;
using eDoxa.Clans.Domain.Repositories;
using eDoxa.Clans.TestHelper;
using eDoxa.Clans.TestHelper.Fixtures;
using eDoxa.Seedwork.Application.Extensions;
using eDoxa.Seedwork.Domain.Miscs;
using eDoxa.Seedwork.TestHelper.Extensions;

using FluentAssertions;

using IdentityModel;

using Xunit;

using Claim = System.Security.Claims.Claim;

namespace eDoxa.Clans.IntegrationTests.Controllers.ClanMembersController
{
    public sealed class ClanMembersControllerDeleteByIdAsyncTest : IntegrationTest
    {
        public ClanMembersControllerDeleteByIdAsyncTest(TestHostFixture testHost, TestMapperFixture testMapper) : base(testHost, testMapper)
        {
        }

        private HttpClient _httpClient;

        private async Task<HttpResponseMessage> ExecuteAsync(ClanId clanId, MemberId memberId)
        {
            return await _httpClient.DeleteAsync($"api/clans/{clanId}/members/{memberId}");
        }

        [Fact]
        public async Task ShouldBeHttpStatusCodeBadRequest() // Is not owner bad request
        {
            // Arrange
            var userId = new UserId();
            var clan = new Clan("ClanName", userId);

            var factory = TestHost.WithClaims(new Claim(JwtClaimTypes.Subject, userId.ToString()));
            _httpClient = factory.CreateClient();
            var testServer = factory.Server;
            testServer.CleanupDbContext();

            await testServer.UsingScopeAsync(
                async scope =>
                {
                    var clanRepository = scope.GetRequiredService<IClanRepository>();
                    clanRepository.Create(clan);
                    await clanRepository.UnitOfWork.CommitAsync();
                });

            // Act
            using var response = await this.ExecuteAsync(clan.Id, new MemberId());

            // Assert
            response.StatusCode.Should().Be(HttpStatusCode.BadRequest);
        }

        [Fact]
        public async Task ShouldBeHttpStatusCodeNotFound()
        {
            // Arrange
            var factory = TestHost.WithClaims(new Claim(JwtClaimTypes.Subject, new UserId().ToString()));
            _httpClient = factory.CreateClient();
            var testServer = factory.Server;
            testServer.CleanupDbContext();

            // Act
            using var response = await this.ExecuteAsync(new ClanId(), new MemberId());

            // Assert
            response.StatusCode.Should().Be(HttpStatusCode.NotFound);
        }

        [Fact]
        public async Task ShouldBeHttpStatusCodeOk()
        {
            // Arrange
            var userId = new UserId();
            var clan = new Clan("ClanName", userId);

            //Todo: Would it be better to use the method instead ? Cuase the method calls a domain events.
            clan.Members.Add(new Member(clan.Id, new UserId()));

            var memberId = clan.Members.SingleOrDefault(member => member.UserId != userId)?.Id;

            var factory = TestHost.WithClaims(new Claim(JwtClaimTypes.Subject, userId.ToString()));
            _httpClient = factory.CreateClient();
            var testServer = factory.Server;
            testServer.CleanupDbContext();

            await testServer.UsingScopeAsync(
                async scope =>
                {
                    var clanRepository = scope.GetRequiredService<IClanRepository>();
                    clanRepository.Create(clan);
                    await clanRepository.UnitOfWork.CommitAsync();
                });

            // Act
            using var response = await this.ExecuteAsync(clan.Id, memberId ?? new MemberId()); // TODO: (VERY BAD) no condition in a test.

            // Assert
            response.EnsureSuccessStatusCode();
            response.StatusCode.Should().Be(HttpStatusCode.OK);
        }
    }
}
