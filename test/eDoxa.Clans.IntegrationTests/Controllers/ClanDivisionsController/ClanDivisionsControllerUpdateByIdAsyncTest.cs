// Filename: ClansControllerPostAsyncTest.cs
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
using eDoxa.Clans.Requests;
using eDoxa.Clans.TestHelper;
using eDoxa.Clans.TestHelper.Fixtures;
using eDoxa.Seedwork.Application.Extensions;
using eDoxa.Seedwork.Domain.Misc;
using eDoxa.Seedwork.TestHelper.Extensions;

using FluentAssertions;

using IdentityModel;

using Xunit;

using Claim = System.Security.Claims.Claim;

namespace eDoxa.Clans.IntegrationTests.Controllers.ClanDivisionsController
{
    public sealed class ClanDivisionsControllerUpdateByIdAsyncTest : IntegrationTest
    {
        public ClanDivisionsControllerUpdateByIdAsyncTest(TestHostFixture testHost, TestMapperFixture testMapper) : base(testHost, testMapper)
        {
        }

        private HttpClient _httpClient;

        private async Task<HttpResponseMessage> ExecuteAsync(ClanId clanId, DivisionId divisionId, DivisionPostRequest request)
        {
            return await _httpClient.PostAsJsonAsync($"api/clans/{clanId}/divisions/{divisionId}", request);
        }

        [Fact]
        public async Task ShouldBeHttpStatusCodeBadRequest() // Not the owner bad request
        {
            // Arrange
            var userId = new UserId();
            var clan = new Clan("ClanName", new UserId());

            clan.CreateDivision("test", "description");
            var divisionId = clan.Divisions.FirstOrDefault()?.Id;

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
            using var response = await this.ExecuteAsync(clan.Id, divisionId, new DivisionPostRequest("test", "division"));

            // Assert
            response.StatusCode.Should().Be(HttpStatusCode.BadRequest);
        }

        [Fact]
        public async Task ShouldBeHttpStatusCodeNotFound()
        {
            // Arrange
            var userId = new UserId();
            var clan = new Clan("ClanName", new UserId());

            var factory = TestHost.WithClaims(new Claim(JwtClaimTypes.Subject, userId.ToString()));
            _httpClient = factory.CreateClient();
            var testServer = factory.Server;
            testServer.CleanupDbContext();

            // Act
            using var response = await this.ExecuteAsync(clan.Id, new DivisionId(), new DivisionPostRequest("test", "division"));

            // Assert
            response.StatusCode.Should().Be(HttpStatusCode.NotFound);
        }

        [Fact(Skip = "Need a database fix.")]
        public async Task ShouldBeHttpStatusCodeOk()
        {
            // Arrange
            var userId = new UserId();
            var clan = new Clan("ClanName", userId);

            clan.CreateDivision("test", "description");
            var divisionId = clan.Divisions.FirstOrDefault()?.Id;

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
            using var response = await this.ExecuteAsync(clan.Id, new DivisionId(), new DivisionPostRequest("test", "division"));

            // Assert
            response.EnsureSuccessStatusCode();
            response.StatusCode.Should().Be(HttpStatusCode.OK);
        }
    }
}
