// Filename: AccountControllerLoginTest.cs
// Date Created: 2019-08-10
// 
// ================================================
// Copyright © 2019, eDoxa. All rights reserved.

using System.Net.Http;
using System.Threading.Tasks;

using Xunit;

namespace eDoxa.Identity.IntegrationTests.Areas.Identity.Pages
{
    public sealed class AccountControllerLoginTest : IClassFixture<IdentityWebApiFactory>
    {
        public AccountControllerLoginTest(IdentityWebApiFactory identityWebApiFactory)
        {
            _httpClient = identityWebApiFactory.CreateClient();
        }

        private readonly HttpClient _httpClient;

        private async Task<HttpResponseMessage> ExecuteAsync()
        {
            return await _httpClient.GetAsync("identity/account/login");
        }

        [Fact]
        public async Task IdentityScenario()
        {
            // Act
            using var response = await this.ExecuteAsync();

            // Assert
            response.EnsureSuccessStatusCode();
        }
    }
}
