// Filename: UsersControllerTest.cs
// Date Created: 2019-06-25
// 
// ================================================
// Copyright © 2019, eDoxa. All rights reserved.

using System.Net.Http;
using System.Threading.Tasks;

using eDoxa.Identity.Api.Infrastructure.Data.Fakers;
using eDoxa.Identity.Api.ViewModels;
using eDoxa.Identity.Infrastructure;
using eDoxa.Identity.IntegrationTests.Helpers;
using eDoxa.Seedwork.Application.Extensions;
using eDoxa.Seedwork.Testing.Extensions;

using FluentAssertions;

using Microsoft.AspNetCore.Http;
using Microsoft.AspNetCore.TestHost;
using Microsoft.VisualStudio.TestTools.UnitTesting;

namespace eDoxa.Identity.IntegrationTests.Controllers
{
    [TestClass]
    public sealed class UsersControllerTest
    {
        private HttpClient _httpClient;
        private TestServer _testServer;

        private async Task<HttpResponseMessage> ExecuteAsync()
        {
            return await _httpClient.GetAsync("api/users");
        }

        [TestInitialize]
        public async Task TestInitialize()
        {
            var factory = new TestIdentityWebApplicationFactory<TestIdentityStartup>();
            _httpClient = factory.CreateClient();
            _testServer = factory.Server;
            await this.TestCleanup();
        }

        [TestCleanup]
        public async Task TestCleanup()
        {
            await _testServer.CleanupDbContextAsync();
        }

        [TestMethod]
        public async Task ApiUsers_WithNinetyNineUsers_ShouldHaveCountOfNinetyNine()
        {
            // Arrange
            var userFaker = new UserFaker();
            userFaker.UseSeed(1);
            var fakeUsers = userFaker.FakeTestUsers(99);

            await _testServer.UsingScopeAsync(
                async scope =>
                {
                    var context = scope.GetService<IdentityDbContext>();
                    context.AddRange(fakeUsers);
                    await context.SaveChangesAsync();
                }
            );

            // Act
            var response = await this.ExecuteAsync();

            // Assert
            response.EnsureSuccessStatusCode();
            var users = await response.DeserializeAsync<UserViewModel[]>();
            users.Should().HaveCount(99);
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
