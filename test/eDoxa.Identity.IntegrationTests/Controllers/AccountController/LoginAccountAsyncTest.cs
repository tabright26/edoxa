// Filename: LoginAccountAsyncTest.cs
// Date Created: 2020-01-28
//
// ================================================
// Copyright © 2020, eDoxa. All rights reserved.

using System;
using System.Linq;
using System.Net;
using System.Net.Http;
using System.Security.Claims;
using System.Threading.Tasks;

using eDoxa.Grpc.Protos.Identity.Requests;
using eDoxa.Identity.Api.Application.Services;
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
    public sealed class LoginAccountAsyncTest : IntegrationTest
    {
        public LoginAccountAsyncTest(TestHostFixture testHost, TestDataFixture testData, TestMapperFixture testMapper) : base(testHost, testData, testMapper)
        {
        }

        private HttpClient _httpClient;
        private const string Password = "pass@word1";


        private async Task<HttpResponseMessage> ExecuteAsync(LoginAccountRequest request)
        {
            return await _httpClient.PostAsJsonAsync($"api/account/login", request);
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
                // Francis: Ca je crois vraiment pas que c'est bon, comment je peux savoir le password pour imiter un vrai login ?
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

            var request = new LoginAccountRequest()
            {
                Email = user.Email,
                Password = Password,
                RememberMe = false,
                ReturnUrl = "/"
            };

            // Act
            using var response = await this.ExecuteAsync(request);

            // Assert
            response.StatusCode.Should().Be(HttpStatusCode.OK);

            // Francis: Est-ce que je dois vérifier si il est connecté ???
        }

        [Fact]
        public async Task ShouldBeHttpStatusCodeBadRequest()
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

            var request = new LoginAccountRequest()
            {
                Email = user.Email,
                Password = "badPassword",
                RememberMe = false,
                ReturnUrl = "/"
            };

            // Act
            using var response = await this.ExecuteAsync(request);

            // Assert
            response.StatusCode.Should().Be(HttpStatusCode.BadRequest);
            // Francis: Est-ce que je devrais checker si le message derreur est le bon ???
        }

        [Fact]
        public async Task ShouldBeHttpStatusCodeUnauthorized()
        {
            _httpClient = TestHost.CreateClient();
            var testServer = TestHost.Server;
            testServer.CleanupDbContext();

            var request = new LoginAccountRequest()
            {
                Email = "test@edoxa.gg",
                Password = Password,
                RememberMe = false,
                ReturnUrl = "/"
            };

            // Act
            using var response = await this.ExecuteAsync(request);

            // Assert
            response.StatusCode.Should().Be(HttpStatusCode.Unauthorized);
        }
    }
}
