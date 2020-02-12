// Filename: LogoutAccountAsyncTest.cs
// Date Created: 2020-01-28
//
// ================================================
// Copyright © 2020, eDoxa. All rights reserved.

using System;
using System.Net;
using System.Net.Http;
using System.Security.Claims;
using System.Threading.Tasks;

using eDoxa.Grpc.Protos.Identity.Requests;
using eDoxa.Identity.Domain.AggregateModels.UserAggregate;
using eDoxa.Identity.Domain.Services;
using eDoxa.Identity.TestHelper;
using eDoxa.Identity.TestHelper.Fixtures;
using eDoxa.Seedwork.Application.Extensions;
using eDoxa.Seedwork.Domain.Misc;
using eDoxa.Seedwork.TestHelper.Extensions;

using FluentAssertions;

using IdentityModel;

using Xunit;

namespace eDoxa.Identity.IntegrationTests.Controllers.AccountController
{
    public sealed class LogoutAccountAsyncTest : IntegrationTest
    {
        public LogoutAccountAsyncTest(TestHostFixture testHost, TestDataFixture testData, TestMapperFixture testMapper) : base(testHost, testData, testMapper)
        {
        }

        private HttpClient _httpClient;
        private const string Password = "pass@word1";


        private async Task<HttpResponseMessage> ExecuteAsync()
        {
            return await _httpClient.GetAsync($"api/account/logout");
        }

        private static User GenerateUser()
        {
            return new User()
            {
                AccessFailedCount = 0,
                ConcurrencyStamp = string.Empty,
                Country = Country.Canada,
                Dob = new UserDob(1990,01,01),
                Email = "test@edoxa.gg",
                NormalizedEmail = "test@edoxa.gg",
                EmailConfirmed = true,
                Id = new Guid(),
                LockoutEnabled = false,
                UserName = "test",
                NormalizedUserName = "test",
                PhoneNumber = "",
                PasswordHash = Password
            };
        }

        [Fact]
        public async Task ShouldBeHttpStatusCodeOK()
        {
            var user = GenerateUser();
            var factory = TestHost.WithClaimsFromDefaultAuthentication(new Claim(JwtClaimTypes.Subject, user.Id.ToString()));
            _httpClient = factory.CreateClient();
            var testServer = factory.Server;
            testServer.CleanupDbContext();

            await testServer.UsingScopeAsync(
                async scope =>
                {
                    var service = scope.GetRequiredService<IUserService>();

                    await service.CreateAsync(user);
                });

            // Act
            using var response = await this.ExecuteAsync();

            // Assert
            response.StatusCode.Should().Be(HttpStatusCode.OK);
        }
    }
}
