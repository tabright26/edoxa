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
using eDoxa.Identity.Api.ViewModels;
using eDoxa.Identity.Domain.Fakers;
using eDoxa.Identity.Infrastructure;
using eDoxa.Seedwork.Testing.TestServer;
using eDoxa.Seedwork.Testing.TestServer.Extensions;

using FluentAssertions;

using Microsoft.AspNetCore.Http;
using Microsoft.VisualStudio.TestTools.UnitTesting;

namespace eDoxa.Identity.IntegrationTests.Controllers
{
    [TestClass]
    public sealed class UsersControllerTest
    {
        private HttpClient _httpClient;
        private IdentityDbContext _dbContext;

        [TestInitialize]
        public async Task TestInitialize()
        {
            var factory = new CustomWebApplicationFactory<IdentityDbContext, Startup>();

            _httpClient = factory.CreateClient();

            _dbContext = factory.DbContext;

            await this.TestCleanup();
        }

        [TestCleanup]
        public async Task TestCleanup()
        {
            _dbContext.Users.RemoveRange(_dbContext.Users);

            await _dbContext.SaveChangesAsync();
        }

        [TestMethod]
        public async Task ApiUsers_WithNinetyNineUsers_ShouldHaveCountOfNinetyNine()
        {
            // Arrange
            var userFaker = new UserFaker();

            _dbContext.AddRange(userFaker.FakeNewUsers(99));

            await _dbContext.SaveChangesAsync();

            // Act
            var response = await this.Execute();

            // Assert
            response.EnsureSuccessStatusCode();

            var users = await response.DeserializeAsync<UserViewModel[]>();

            users.Should().HaveCount(99);
        }

        [TestMethod]
        public async Task ApiUsers_WithNinetyNineUsers_ShouldBeNoContent()
        {
            // Act
            var response = await this.Execute();

            // Assert
            response.StatusCode.Should().Be(StatusCodes.Status204NoContent);
        }

        private async Task<HttpResponseMessage> Execute()
        {
            return await _httpClient.GetAsync("api/users");
        }
    }
}
