// Filename: AccountController.cs
// Date Created: 2019-06-25
// 
// ================================================
// Copyright © 2019, eDoxa. All rights reserved.

using System.Net.Http;
using System.Threading.Tasks;

using eDoxa.Identity.Api.Infrastructure;
using eDoxa.Identity.IntegrationTests.Helpers;
using eDoxa.Seedwork.Application.Extensions;
using eDoxa.Seedwork.Testing.Extensions;

using Microsoft.VisualStudio.TestTools.UnitTesting;

namespace eDoxa.Identity.IntegrationTests.Controllers
{
    [TestClass]
    public sealed class AccountController : IdentityWebApplicationFactory
    {
        private HttpClient _httpClient;

        public async Task<HttpResponseMessage> ExecuteAsync()
        {
            return await _httpClient.GetAsync("identity/account/login");
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
            await Server.UsingScopeAsync(
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
