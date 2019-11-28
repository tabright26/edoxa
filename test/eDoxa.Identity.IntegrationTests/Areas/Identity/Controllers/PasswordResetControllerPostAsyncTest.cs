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

using eDoxa.Identity.Api.Services;
using eDoxa.Identity.Requests;
using eDoxa.Identity.TestHelper;
using eDoxa.Identity.TestHelper.Fixtures;
using eDoxa.Seedwork.Application.Extensions;
using eDoxa.Seedwork.TestHelper.Extensions;

using FluentAssertions;

using IdentityModel;

using Xunit;

namespace eDoxa.Identity.IntegrationTests.Areas.Identity.Controllers
{
    public sealed class PasswordResetControllerPostAsyncTest : IntegrationTest
    {
        public PasswordResetControllerPostAsyncTest(TestHostFixture testHost, TestDataFixture testData, TestMapperFixture testMapper) : base(
            testHost,
            testData,
            testMapper)
        {
        }

        private async Task<HttpResponseMessage> ExecuteAsync(ResetPasswordRequest request)
        {
            return await _httpClient.PostAsJsonAsync("api/password/reset", request);
        }

        private HttpClient _httpClient;

        [Fact(Skip = "Bearer authentication mock bug since .NET Core 3.0")]
        public async Task ShouldBeHttpStatusCodeOK()
        {
            var users = TestData.FileStorage.GetUsers();
            var user = users.First();

            var factory = TestHost.WithClaims(new Claim(JwtClaimTypes.Subject, user.Id.ToString()));
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
                    using var response = await this.ExecuteAsync(new ResetPasswordRequest("admin@edoxa.gg", "Pass@word1", code));

                    // Assert
                    response.EnsureSuccessStatusCode();

                    response.StatusCode.Should().Be(HttpStatusCode.OK);
                });
        }
    }
}
