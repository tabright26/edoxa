// Filename: UsersControllerGetAsyncTest.cs
// Date Created: 2019-07-26
// 
// ================================================
// Copyright © 2019, eDoxa. All rights reserved.

using System.Linq;
using System.Net.Http;
using System.Threading.Tasks;

using eDoxa.Identity.Api.Areas.Identity.Services;
using eDoxa.Identity.Api.Areas.Identity.ViewModels;
using eDoxa.Identity.Api.Infrastructure.Data.Storage;
using eDoxa.Seedwork.Application.Extensions;
using eDoxa.Seedwork.Testing.Extensions;

using FluentAssertions;

using Microsoft.AspNetCore.Http;
using Microsoft.AspNetCore.TestHost;
using Microsoft.VisualStudio.TestTools.UnitTesting;

namespace eDoxa.Identity.IntegrationTests.Controllers
{
    [TestClass]
    public sealed class UsersControllerGetAsyncTest
    {
        private HttpClient _httpClient;
        private TestServer _testServer;

        private async Task<HttpResponseMessage> ExecuteAsync()
        {
            return await _httpClient.GetAsync("api/users");
        }

        [TestInitialize]
        public void TestInitialize()
        {
            var identityWebApplicationFactory = new IdentityWebApplicationFactory();

            _httpClient = identityWebApplicationFactory.CreateClient();

            _testServer = identityWebApplicationFactory.Server;

            _testServer.CleanupDbContext();
        }

        [TestMethod]
        public async Task ApiUsers_WithNinetyNineUsers_ShouldHaveCountOfNinetyNine()
        {
            // Arrange
            await _testServer.UsingScopeAsync(
                async scope =>
                {
                    var userManager = scope.GetService<CustomUserManager>();

                    foreach (var testUser in IdentityStorage.TestUsers.Take(100).ToList())
                    {
                        await userManager.CreateAsync(testUser);
                    }
                }
            );

            // Act
            var response = await this.ExecuteAsync();

            // Assert
            response.EnsureSuccessStatusCode();
            var users = await response.DeserializeAsync<UserViewModel[]>();
            users.Should().HaveCount(100);
        }

        [TestMethod]
        public async Task ApiUsers_WithNinetyNineUsers_ShouldBeNoContent()
        {
            // Act
            var response = await this.ExecuteAsync();

            // Assert
            response.StatusCode.Should().Be(StatusCodes.Status204NoContent);
        }
    }
}
