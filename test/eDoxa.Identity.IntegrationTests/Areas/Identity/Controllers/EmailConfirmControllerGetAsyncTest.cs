// Filename: EmailConfirmControllerGetAsyncTest.cs
// Date Created: 2019-09-01
// 
// ================================================
// Copyright © 2019, eDoxa. All rights reserved.

using System.Linq;
using System.Net;
using System.Net.Http;
using System.Security.Claims;
using System.Threading.Tasks;

using eDoxa.Identity.Api.Areas.Identity.Services;
using eDoxa.Identity.Api.Infrastructure.Data.Storage;
using eDoxa.Identity.IntegrationTests.Collections;
using eDoxa.Seedwork.Application.Extensions;
using eDoxa.Seedwork.Testing.Extensions;
using eDoxa.Storage.Azure.File;

using FluentAssertions;

using IdentityModel;

using Xunit;

namespace eDoxa.Identity.IntegrationTests.Areas.Identity.Controllers
{
    [Collection(nameof(TestDataFixture.TestData))]
    public sealed class EmailConfirmControllerGetAsyncTest : IClassFixture<IdentityApiFactory>
    {
        public EmailConfirmControllerGetAsyncTest(IdentityApiFactory identityApiFactory, TestDataFixture testData)
        {
            _identityApiFactory = identityApiFactory;
            _testData = testData;
        }

        private readonly IdentityApiFactory _identityApiFactory;
        private readonly TestDataFixture _testData;

        private HttpClient _httpClient;

        private async Task<HttpResponseMessage> ExecuteAsync(string userId, string code)
        {
            return await _httpClient.GetAsync($"api/email/confirm?userId={userId}&code={code}");
        }

        [Fact]
        public async Task ShouldBeHttpStatusCodeOK()
        {
            var users = await _testData.TestData.GetUsersAsync();
            var user = users.First();

            var factory = _identityApiFactory.WithClaims(new Claim(JwtClaimTypes.Subject, user.Id.ToString()));
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
