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
using eDoxa.Identity.Application.ViewModels;
using eDoxa.Identity.Infrastructure;
using eDoxa.Testing.MSTest;
using eDoxa.Testing.MSTest.Extensions;

using FluentAssertions;

using Microsoft.VisualStudio.TestTools.UnitTesting;

namespace eDoxa.Identity.IntegrationTests.Controllers
{
    [TestClass]
    public sealed class UsersControllerTest
    {
        private HttpClient _httpClient;
        private IdentityDbContext _dbContext;

        [TestInitialize]
        public void TestInitialize()
        {
            var factory = new CustomWebApplicationFactory<IdentityDbContext, Startup>();

            _httpClient = factory.CreateClient();

            _dbContext = factory.DbContext;
        }

        public async Task<HttpResponseMessage> Execute()
        {
            return await _httpClient.GetAsync("api/users");
        }

        [TestMethod]
        public async Task IdentityScenario()
        {
            var response = await this.Execute();

            response.EnsureSuccessStatusCode();

            var users = await response.DeserializeAsync<UserViewModel[]>();

            users.Should().BeNull();
        }

        [TestCleanup]
        public async Task TestCleanup()
        {
            _dbContext.Users.RemoveRange(_dbContext.Users);

            await _dbContext.SaveChangesAsync();
        }
    }
}
