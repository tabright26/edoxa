// Filename: AccountController.cs
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

using eDoxa.Identity.Infrastructure;
using eDoxa.Seedwork.Testing;

using Microsoft.VisualStudio.TestTools.UnitTesting;

namespace eDoxa.IdentityServer.IntegrationTests.Controllers
{
    [TestClass]
    public sealed class AccountController
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
            return await _httpClient.GetAsync("identity/account/login");
        }

        [TestMethod]
        public async Task IdentityScenario()
        {
            var response = await this.Execute();

            response.EnsureSuccessStatusCode();
        }

        [TestCleanup]
        public async Task TestCleanup()
        {
            _dbContext.Users.RemoveRange(_dbContext.Users);

            await _dbContext.SaveChangesAsync();
        }
    }
}
