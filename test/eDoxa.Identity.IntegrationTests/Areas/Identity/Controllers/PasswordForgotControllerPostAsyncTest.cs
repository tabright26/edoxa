// Filename: PasswordForgotControllerPostAsyncTest.cs
// Date Created: 2019-09-16
// 
// ================================================
// Copyright © 2019, eDoxa. All rights reserved.

using System.Linq;
using System.Net;
using System.Net.Http;
using System.Threading.Tasks;

using eDoxa.Identity.Api.Areas.Identity.Requests;
using eDoxa.Identity.Api.Areas.Identity.Services;
using eDoxa.Identity.IntegrationTests.Helpers;
using eDoxa.Seedwork.Application.Extensions;
using eDoxa.Seedwork.Testing.Extensions;
using eDoxa.Seedwork.Testing.Http;

using FluentAssertions;

using Xunit;

namespace eDoxa.Identity.IntegrationTests.Areas.Identity.Controllers
{
    [Collection(nameof(ControllerCollection))]
    public sealed class PasswordForgotControllerPostAsyncTest : ControllerTest
    {
        public PasswordForgotControllerPostAsyncTest(IdentityApiFactory apiFactory, TestDataFixture testData) : base(apiFactory, testData)
        {
        }

        private async Task<HttpResponseMessage> ExecuteAsync(PasswordForgotPostRequest request)
        {
            return await _httpClient.PostAsync("api/password/forgot", new JsonContent(request));
        }

        private HttpClient _httpClient;

        [Fact]
        public async Task ShouldBeHttpStatusCodeOK()
        {
            var users = await TestData.FileStorage.GetUsersAsync();
            var user = users.First();
            user.PersonalInfo = null;

            _httpClient = ApiFactory.CreateClient();
            var testServer = ApiFactory.Server;
            testServer.CleanupDbContext();

            await testServer.UsingScopeAsync(
                async scope =>
                {
                    var userManager = scope.GetRequiredService<UserManager>();

                    var result = await userManager.CreateAsync(user);

                    result.Succeeded.Should().BeTrue();
                });

            // Act
            using var response = await this.ExecuteAsync(new PasswordForgotPostRequest("admin@edoxa.gg"));

            // Assert
            response.EnsureSuccessStatusCode();

            response.StatusCode.Should().Be(HttpStatusCode.OK);
        }
    }
}
