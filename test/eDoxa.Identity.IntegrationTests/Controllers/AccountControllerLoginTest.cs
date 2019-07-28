// Filename: AccountControllerLoginTest.cs
// Date Created: 2019-07-21
// 
// ================================================
// Copyright © 2019, eDoxa. All rights reserved.

using System.Net.Http;
using System.Threading.Tasks;

using Microsoft.VisualStudio.TestTools.UnitTesting;

namespace eDoxa.Identity.IntegrationTests.Controllers
{
    [TestClass]
    public sealed class AccountControllerLoginTest
    {
        private HttpClient _httpClient;

        public async Task<HttpResponseMessage> ExecuteAsync()
        {
            return await _httpClient.GetAsync("identity/account/login");
        }

        [TestInitialize]
        public void TestInitialize()
        {
            var identityWebApplicationFactory = new IdentityWebApplicationFactory();

            _httpClient = identityWebApplicationFactory.CreateClient();
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
