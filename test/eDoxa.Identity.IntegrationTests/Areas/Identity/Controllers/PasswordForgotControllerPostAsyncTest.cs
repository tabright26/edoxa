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
using eDoxa.Identity.TestHelpers;
using eDoxa.Identity.TestHelpers.Fixtures;
using eDoxa.Seedwork.Application.Extensions;
using eDoxa.Seedwork.Testing.Extensions;
using eDoxa.Seedwork.Testing.Http;

using FluentAssertions;

using Xunit;

namespace eDoxa.Identity.IntegrationTests.Areas.Identity.Controllers
{
    public sealed class PasswordForgotControllerPostAsyncTest : IntegrationTest
    {
        public PasswordForgotControllerPostAsyncTest(TestApiFixture testApi, TestDataFixture testData, TestMapperFixture testMapper) : base(
            testApi,
            testData,
            testMapper)
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
            var users = TestData.FileStorage.GetUsers();
            var user = users.First();
            user.Informations = null;

            _httpClient = TestApi.CreateClient();
            var testServer = TestApi.Server;
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
