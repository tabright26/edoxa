// Filename: LoginAccountAsyncTest.cs
// Date Created: 2020-01-28
// 
// ================================================
// Copyright © 2020, eDoxa. All rights reserved.

using System.Linq;
using System.Net;
using System.Net.Http;
using System.Security.Claims;
using System.Threading.Tasks;

using eDoxa.Grpc.Protos.Identity.Requests;
using eDoxa.Identity.Domain.Services;
using eDoxa.Identity.TestHelper;
using eDoxa.Identity.TestHelper.Fixtures;
using eDoxa.Seedwork.Application.Extensions;
using eDoxa.Seedwork.TestHelper.Extensions;

using FluentAssertions;

using IdentityModel;

using Xunit;

namespace eDoxa.Identity.IntegrationTests.Controllers.AccountController
{
    public sealed class LoginAccountAsyncTest : IntegrationTest
    {
        private const string Password = "pass@word1";
        private HttpClient _httpClient;

        public LoginAccountAsyncTest(TestHostFixture testHost, TestDataFixture testData, TestMapperFixture testMapper) : base(testHost, testData, testMapper)
        {
        }

        private async Task<HttpResponseMessage> ExecuteAsync(LoginAccountRequest request)
        {
            return await _httpClient.PostAsJsonAsync("api/account/login", request);
        }

        [Fact(Skip = "Mocks needed.")]
        public async Task ShouldBeHttpStatusCodeBadRequest()
        {
            // Arrange
            var user = TestData.FileStorage.GetUsers().First();
            var factory = TestHost.WithClaimsFromDefaultAuthentication(new Claim(JwtClaimTypes.Subject, user.Id.ToString()));
            _httpClient = factory.CreateClient();
            var testServer = factory.Server;
            testServer.CleanupDbContext();

            await testServer.UsingScopeAsync(
                async scope =>
                {
                    var service = scope.GetRequiredService<IUserService>();

                    await service.CreateAsync(user, Password);
                });

            var request = new LoginAccountRequest
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

        [Fact(Skip = "Mocks needed.")]
        public async Task ShouldBeHttpStatusCodeOK()
        {
            // Arrange
            var user = TestData.FileStorage.GetUsers().First();
            var factory = TestHost;
            _httpClient = factory.CreateClient();
            var testServer = factory.Server;
            testServer.CleanupDbContext();

            await testServer.UsingScopeAsync(
                async scope =>
                {
                    var service = scope.GetRequiredService<IUserService>();

                    await service.CreateAsync(user, Password);
                });

            var request = new LoginAccountRequest
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

        [Fact(Skip = "Mocks needed.")]
        public async Task ShouldBeHttpStatusCodeUnauthorized()
        {
            // Arrange
            var user = TestData.FileStorage.GetUsers().First();
            var factory = TestHost;
            _httpClient = factory.CreateClient();
            var testServer = factory.Server;
            testServer.CleanupDbContext();

            var request = new LoginAccountRequest
            {
                Email = user.Email,
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
