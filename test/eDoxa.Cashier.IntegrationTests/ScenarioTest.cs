// Filename: ScenarioTest.cs
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

using eDoxa.Cashier.DTO;
using eDoxa.Cashier.Infrastructure;
using eDoxa.Seedwork.Infrastructure.Extensions;
using eDoxa.Testing.MSTest.Extensions;

using FluentAssertions;

using IdentityModel;

using Microsoft.AspNetCore.TestHost;
using Microsoft.VisualStudio.TestTools.UnitTesting;

namespace eDoxa.Cashier.IntegrationTests
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

            _httpClient.DefaultRequestHeaders.Add(JwtClaimTypes.Subject, ScenarioConstants.TestUserId.ToString());

            _testServer = factory.Server;
        }

        [TestMethod]
        public async Task CashierScenario()
        {
            _testServer.MigrateDbContext<CashierDbContext, ScenarioDbContextData>();

            var response = await _httpClient.GetAsync("api/account/balance/money");

            response.EnsureSuccessStatusCode();

            var balance = await response.DeserializeAsync<BalanceDTO>();

            balance.Should().NotBeNull();
        }

        [TestCleanup]
        public void TestCleanup()
        {
            _testServer.CleanupDbContext<CashierDbContext>();
        }
    }
}
