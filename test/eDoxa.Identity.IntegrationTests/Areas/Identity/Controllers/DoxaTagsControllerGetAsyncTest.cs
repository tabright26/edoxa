// Filename: DoxaTagsControllerGetAsyncTest.cs
// Date Created: 2019-09-16
// 
// ================================================
// Copyright © 2019, eDoxa. All rights reserved.

using System.Linq;
using System.Net;
using System.Net.Http;
using System.Threading.Tasks;

using eDoxa.Identity.Api.Areas.Identity.Responses;
using eDoxa.Identity.Api.Areas.Identity.Services;
using eDoxa.Identity.IntegrationTests.Helpers;
using eDoxa.Seedwork.Application.Extensions;
using eDoxa.Seedwork.Testing.Extensions;
using eDoxa.Seedwork.Testing.Http.Extensions;

using FluentAssertions;

using Xunit;

namespace eDoxa.Identity.IntegrationTests.Areas.Identity.Controllers
{
    [Collection(nameof(ControllerCollection))]
    public sealed class DoxaTagsControllerGetAsyncTest : ControllerTest
    {
        public DoxaTagsControllerGetAsyncTest(IdentityApiFactory apiFactory, TestDataFixture testData) : base(apiFactory, testData)
        {
        }

        private HttpClient _httpClient;

        private async Task<HttpResponseMessage> ExecuteAsync()
        {
            return await _httpClient.GetAsync("api/doxatags");
        }

        [Fact]
        public async Task ShouldBeHttpStatusCodeNoContent()
        {
            // Arrange
            _httpClient = ApiFactory.CreateClient();
            var testServer = ApiFactory.Server;
            testServer.CleanupDbContext();

            // Act
            using var response = await this.ExecuteAsync();

            // Assert
            response.EnsureSuccessStatusCode();

            response.StatusCode.Should().Be(HttpStatusCode.NoContent);
        }

        [Fact]
        public async Task ShouldBeHttpStatusCodeOK()
        {
            // Arrange
            _httpClient = ApiFactory.CreateClient();
            var testServer = ApiFactory.Server;
            testServer.CleanupDbContext();

            await testServer.UsingScopeAsync(
                async scope =>
                {
                    var userManager = scope.GetRequiredService<UserManager>();

                    var testUsers = await TestData.FileStorage.GetUsersAsync();

                    foreach (var testUser in testUsers.Take(100).ToList())
                    {
                        var result = await userManager.CreateAsync(testUser);

                        result.Succeeded.Should().BeTrue();
                    }
                });

            // Act
            using var response = await this.ExecuteAsync();

            // Assert
            response.EnsureSuccessStatusCode();

            response.StatusCode.Should().Be(HttpStatusCode.OK);

            var users = await response.DeserializeAsync<UserDoxaTagResponse[]>();

            users.Should().HaveCount(100);
        }
    }
}
