// Filename: ClanGrpcServiceTest.cs
// Date Created: 2020-01-11
//
// ================================================
// Copyright © 2020, eDoxa. All rights reserved.

using System.Security.Claims;
using System.Threading.Tasks;

using eDoxa.Clans.Domain.Models;
using eDoxa.Clans.Domain.Repositories;
using eDoxa.Clans.TestHelper;
using eDoxa.Clans.TestHelper.Fixtures;
using eDoxa.Grpc.Protos.Clans.Requests;
using eDoxa.Grpc.Protos.Clans.Responses;
using eDoxa.Grpc.Protos.Clans.Services;
using eDoxa.Seedwork.Application.Extensions;
using eDoxa.Seedwork.Domain.Misc;
using eDoxa.Seedwork.TestHelper.Extensions;

using FluentAssertions;

using IdentityModel;

using Xunit;

namespace eDoxa.Clans.IntegrationTests.Services
{
    public sealed class ClanGrpcServiceTest : IntegrationTest
    {
        public ClanGrpcServiceTest(TestHostFixture testHost, TestMapperFixture testMapper) : base(testHost, testMapper)
        {
        }

        [Fact]
        public async Task FetchClans_ShouldBeOfTypeFetchClansResponse()
        {
            // Arrange
            var userId = new UserId();
            const string email = "test@edoxa.gg";

            var claims = new[] {new Claim(JwtClaimTypes.Subject, userId.ToString()), new Claim(JwtClaimTypes.Email, email)};
            var host = TestHost.WithClaimsFromBearerAuthentication(claims);
            host.Server.CleanupDbContext();

            await host.Server.UsingScopeAsync(
                async scope =>
                {
                    var clanRepo = scope.GetRequiredService<IClanRepository>();

                    var clan = new Clan("test", userId);

                    clanRepo.Create(clan);

                    await clanRepo.UnitOfWork.CommitAsync();
                });

            var request = new FetchClansRequest();

            var client = new ClanService.ClanServiceClient(host.CreateChannel());

            // Act
            var response = await client.FetchClansAsync(request);

            //Assert
            response.Should().BeOfType<FetchClansResponse>();
        }
    }
}
