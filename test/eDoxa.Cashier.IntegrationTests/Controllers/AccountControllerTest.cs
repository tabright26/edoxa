// Filename: AccountControllerTest.cs
// Date Created: 2019-06-06
// 
// ================================================
// Copyright © 2019, eDoxa. All rights reserved.
// 
// This file is subject to the terms and conditions
// defined in file 'LICENSE.md', which is part of
// this source code package.

using System.Net.Http;
using System.Security.Claims;
using System.Threading.Tasks;

using eDoxa.Cashier.Api;
using eDoxa.Cashier.Api.ViewModels;
using eDoxa.Cashier.Domain.AggregateModels.UserAggregate;
using eDoxa.Cashier.Infrastructure;
using eDoxa.Cashier.IntegrationTests.Helpers;
using eDoxa.Seedwork.Testing.TestServer;
using eDoxa.Seedwork.Testing.TestServer.Extensions;

using FluentAssertions;

using IdentityModel;

using Microsoft.AspNetCore.TestHost;
using Microsoft.VisualStudio.TestTools.UnitTesting;

namespace eDoxa.Cashier.IntegrationTests.Controllers
{
    [TestClass]
    public sealed class AccountControllerTest
    {
        private HttpClient _httpClient;
        private TestServer _testServer;

        public async Task<HttpResponseMessage> ExecuteAsync()
        {
            return await _httpClient.DefaultRequestHeaders(new[] { new Claim(JwtClaimTypes.Subject, CashierTestConstants.TestUserId.ToString()) }).GetAsync("api/account/balance/money");
        }

        [TestInitialize]
        public void TestInitialize()
        {
            var factory = new CustomWebApplicationFactory<CashierDbContext, Startup>();
            _httpClient = factory.CreateClient();
            _testServer = factory.Server;
        }
        
        [TestCleanup]
        public async Task TestCleanup()
        {
            var context = _testServer.GetService<CashierDbContext>();
            context.Users.RemoveRange(context.Users);
            await context.SaveChangesAsync();
        }

        [TestMethod]
        public async Task CashierScenario()
        {
            // Arrange
            var context = _testServer.GetService<CashierDbContext>();
            context.Users.Add(new User(CashierTestConstants.TestUserId, CashierTestConstants.TestStripeConnectAccountId, CashierTestConstants.TestStripeConnectAccountId));
            await context.SaveChangesAsync();

            // Act
            var response = await this.ExecuteAsync();

            // Assert
            response.EnsureSuccessStatusCode();
            var balance = await response.DeserializeAsync<BalanceViewModel>();
            balance.Should().NotBeNull();
        }
    }
}
