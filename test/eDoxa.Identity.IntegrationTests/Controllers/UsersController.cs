// Filename: UsersControllerTest.cs
// Date Created: 2019-06-25
// 
// ================================================
// Copyright © 2019, eDoxa. All rights reserved.

using System.Linq;
using System.Net.Http;
using System.Threading.Tasks;

using eDoxa.Identity.Api.Areas.Identity.Services;
using eDoxa.Identity.Api.Areas.Identity.ViewModels;
using eDoxa.Identity.Api.Infrastructure.Data.Storage;
using eDoxa.Identity.IntegrationTests.Helpers;
using eDoxa.Seedwork.Application.Extensions;
using eDoxa.Seedwork.Testing.Extensions;

using FluentAssertions;

using Microsoft.AspNetCore.Http;
using Microsoft.VisualStudio.TestTools.UnitTesting;

namespace eDoxa.Identity.IntegrationTests.Controllers
{
    [TestClass]
    public sealed class UsersController : IdentityWebApplicationFactory
    {
        private HttpClient _httpClient;

        private async Task<HttpResponseMessage> ExecuteAsync()
        {
            return await _httpClient.GetAsync("api/users");
        }

        [TestInitialize]
        public async Task TestInitialize()
        {
            _httpClient = this.CreateClient();

            await this.TestCleanup();
        }

        [TestCleanup]
        public async Task TestCleanup()
        {
            await Server.CleanupDbContextAsync();
        }

        [TestMethod]
        public async Task ApiUsers_WithNinetyNineUsers_ShouldHaveCountOfNinetyNine()
        {
            // Arrange
            await Server.UsingScopeAsync(
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
