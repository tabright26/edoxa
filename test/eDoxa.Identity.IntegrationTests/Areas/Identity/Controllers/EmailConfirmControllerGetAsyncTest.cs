// Filename: EmailConfirmControllerGetAsyncTest.cs
// Date Created: 2019-09-16
// 
// ================================================
// Copyright © 2019, eDoxa. All rights reserved.

using System.Linq;
using System.Net;
using System.Net.Http;
using System.Security.Claims;
using System.Threading.Tasks;

using eDoxa.Identity.Api.Areas.Identity.Services;
using eDoxa.Identity.TestHelpers;
using eDoxa.Identity.TestHelpers.Fixtures;
using eDoxa.Seedwork.Application.Extensions;
using eDoxa.Seedwork.Testing.Extensions;

using FluentAssertions;

using IdentityModel;

using Xunit;

namespace eDoxa.Identity.IntegrationTests.Areas.Identity.Controllers
{
    public sealed class EmailConfirmControllerGetAsyncTest : IntegrationTest
    {
        public EmailConfirmControllerGetAsyncTest(TestApiFixture testApi, TestDataFixture testData, TestMapperFixture testMapper) : base(
            testApi,
            testData,
            testMapper)
        {
        }

        private HttpClient _httpClient;

        private async Task<HttpResponseMessage> ExecuteAsync(string userId, string code)
        {
            return await _httpClient.GetAsync($"api/email/confirm?userId={userId}&code={code}");
        }

        [Fact]
        public async Task ShouldBeHttpStatusCodeOK()
        {
            var users = TestData.FileStorage.GetUsers();
            var user = users.First();

            var factory = TestApi.WithClaims(new Claim(JwtClaimTypes.Subject, user.Id.ToString()));
            _httpClient = factory.CreateClient();
            var testServer = factory.Server;
            testServer.CleanupDbContext();

            await testServer.UsingScopeAsync(
                async scope =>
                {
                    var userManager = scope.GetRequiredService<UserManager>();

                    var result = await userManager.CreateAsync(user);

                    result.Succeeded.Should().BeTrue();

                    var code = await userManager.GenerateEmailConfirmationTokenAsync(user);

                    // Act
                    using var response = await this.ExecuteAsync(user.Id.ToString(), code);

                    // Assert
                    response.EnsureSuccessStatusCode();

                    response.StatusCode.Should().Be(HttpStatusCode.OK);
                });
        }
    }
}
