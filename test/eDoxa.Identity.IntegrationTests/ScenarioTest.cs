// Filename: IdentityScenarioTest.cs
// Date Created: 2019-06-03
// 
// ================================================
// Copyright © 2019, eDoxa. All rights reserved.
// 
// This file is subject to the terms and conditions
// defined in file 'LICENSE.md', which is part of
// this source code package.

using System.Net.Http;
using System.Threading.Tasks;

using eDoxa.Identity.DTO;
using eDoxa.Identity.Infrastructure;
using eDoxa.Seedwork.Infrastructure.Extensions;
using eDoxa.Testing.MSTest.Extensions;

using FluentAssertions;

using Microsoft.AspNetCore.TestHost;
using Microsoft.VisualStudio.TestTools.UnitTesting;

namespace eDoxa.Identity.IntegrationTests
{
    [TestClass]
    public sealed class ScenarioTest
    {
        private readonly HttpClient _httpClient;
        private readonly TestServer _testServer;

        public ScenarioTest()
        {
            // The order of the method calls is sequential.
            var factory = new CustomWebApplicationFactory();

            _httpClient = factory.CreateClient();

            _testServer = factory.Server;
        }

        [TestMethod]
        public async Task IdentityScenario()
        {
            _testServer.MigrateDbContext<IdentityDbContext, ScenarioDbContextData>();

            var response = await _httpClient.GetAsync("api/users");

            response.EnsureSuccessStatusCode();

            var users = await response.DeserializeAsync<UserDTO[]>();

            users.Should().BeNull();
        }

        [TestCleanup]
        public void TestCleanup()
        {
            _testServer.CleanupDbContext<IdentityDbContext>();
        }
    }
}
