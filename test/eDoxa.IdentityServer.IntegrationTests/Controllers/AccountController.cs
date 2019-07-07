// Filename: AccountController.cs
// Date Created: 2019-06-25
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
using eDoxa.IdentityServer.IntegrationTests.Helpers;
using eDoxa.Seedwork.Application.Extensions;
using eDoxa.Seedwork.Testing.Extensions;

using Microsoft.AspNetCore.TestHost;
using Microsoft.VisualStudio.TestTools.UnitTesting;

namespace eDoxa.IdentityServer.IntegrationTests.Controllers
{
    [TestClass]
    public sealed class AccountController
    {
        private HttpClient _httpClient;
        private TestServer _testServer;

        public async Task<HttpResponseMessage> ExecuteAsync()
        {
            return await _httpClient.GetAsync("identity/account/login");
        }

        [TestInitialize]
        public async Task TestInitialize()
        {
            var factory = new TestIdentityServerWebApplicationFactory<Startup>();
            _httpClient = factory.CreateClient();
            _testServer = factory.Server;
            await this.TestCleanup();
        }

        [TestCleanup]
        public async Task TestCleanup()
        {
            await _testServer.UsingScopeAsync(
                async scope =>
                {
                    var context = scope.GetService<IdentityDbContext>();
                    context.Users.RemoveRange(context.Users);
                    await context.SaveChangesAsync();
                }
            );
        }

        [TestMethod]
        public async Task IdentityScenario()
        {
            // Act
            var response = await this.ExecuteAsync();

            // Assert
            response.EnsureSuccessStatusCode();
        }
    }
}
