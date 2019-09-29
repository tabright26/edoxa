// Filename: PasswordResetControllerPostAsyncTest.cs
// Date Created: 2019-09-16
// 
// ================================================
// Copyright © 2019, eDoxa. All rights reserved.

using System.Linq;
using System.Net;
using System.Net.Http;
using System.Security.Claims;
using System.Threading.Tasks;

using eDoxa.Identity.Api.Areas.Identity.Requests;
using eDoxa.Identity.Api.Areas.Identity.Services;
using eDoxa.Identity.IntegrationTests.TestHelpers;
using eDoxa.Seedwork.Application.Extensions;
using eDoxa.Seedwork.Testing.Extensions;
using eDoxa.Seedwork.Testing.Http;

using FluentAssertions;

using IdentityModel;

using Xunit;

namespace eDoxa.Identity.IntegrationTests.Areas.Identity.Controllers
{

    public sealed class PasswordResetControllerPostAsyncTest : IntegrationTestClass
    {
        public PasswordResetControllerPostAsyncTest(IdentityApiFactory apiFactory, TestDataFixture testData) : base(apiFactory, testData)
        {
        }

        private async Task<HttpResponseMessage> ExecuteAsync(PasswordResetPostRequest request)
        {
            return await _httpClient.PostAsync("api/password/reset", new JsonContent(request));
        }

        private HttpClient _httpClient;

        [Fact]
        public async Task ShouldBeHttpStatusCodeOK()
        {
            var users = await TestData.FileStorage.GetUsersAsync();
            var user = users.First();

            var factory = ApiFactory.WithClaims(new Claim(JwtClaimTypes.Subject, user.Id.ToString()));
            _httpClient = factory.CreateClient();
            var testServer = factory.Server;
            testServer.CleanupDbContext();

            await testServer.UsingScopeAsync(
                async scope =>
                {
                    // Arrange
                    var userManager = scope.GetRequiredService<UserManager>();

                    var result = await userManager.CreateAsync(user);

                    result.Succeeded.Should().BeTrue();

                    var code = await userManager.GeneratePasswordResetTokenAsync(user);

                    // Act
                    using var response = await this.ExecuteAsync(new PasswordResetPostRequest("admin@edoxa.gg", "Pass@word1", code));

                    // Assert
                    response.EnsureSuccessStatusCode();

                    response.StatusCode.Should().Be(HttpStatusCode.OK);
                });
        }
    }
}
