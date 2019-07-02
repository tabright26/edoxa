// Filename: UsersControllerTest.cs
// Date Created: 2019-06-06
// 
// ================================================
// Copyright © 2019, eDoxa. All rights reserved.
// 
// This file is subject to the terms and conditions
// defined in file 'LICENSE.md', which is part of
// this source code package.

using System.Net.Http;
using System.Threading.Tasks;

using eDoxa.Identity.Api;
using eDoxa.Identity.Api.Application.Fakers;
using eDoxa.Identity.Domain.ViewModels;
using eDoxa.Identity.Infrastructure;
using eDoxa.Seedwork.Testing.TestServer;
using eDoxa.Seedwork.Testing.TestServer.Extensions;

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
            var factory = new CustomWebApplicationFactory<IdentityDbContext, Startup>();
            _httpClient = factory.CreateClient();
            _testServer = factory.Server;
            await this.TestCleanup();
        }

        [TestCleanup]
        public async Task TestCleanup()
        {
            var context = _testServer.GetService<IdentityDbContext>();
            context.Users.RemoveRange(context.Users);
            await context.SaveChangesAsync();
        }

        [TestMethod]
        public async Task ApiUsers_WithNinetyNineUsers_ShouldHaveCountOfNinetyNine()
        {
            // Arrange
            var userFaker = new UserFaker();
            userFaker.UseSeed(1);
            var context = _testServer.GetService<IdentityDbContext>();
            context.AddRange(userFaker.FakeTestUsers(99));
            await context.SaveChangesAsync();

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
