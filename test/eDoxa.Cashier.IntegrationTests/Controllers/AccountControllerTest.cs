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
using eDoxa.Cashier.Application.ViewModels;
using eDoxa.Cashier.Domain.AggregateModels.UserAggregate;
using eDoxa.Cashier.Infrastructure;
using eDoxa.Cashier.IntegrationTests.Helpers;
using eDoxa.Testing.MSTest;
using eDoxa.Testing.MSTest.Extensions;

using FluentAssertions;

using IdentityModel;

using Microsoft.VisualStudio.TestTools.UnitTesting;

namespace eDoxa.Cashier.IntegrationTests.Controllers
{
    [TestClass]
    public sealed class AccountControllerTest
    {
        private static readonly Claim[] Claims = {new Claim(JwtClaimTypes.Subject, CashierTestConstants.TestUserId.ToString())};

        private HttpClient _httpClient;
        private CashierDbContext _dbContext;

        [TestInitialize]
        public void TestInitialize()
        {
            var factory = new CustomWebApplicationFactory<CashierDbContext, Startup>();

            _httpClient = factory.CreateClient();

            _dbContext = factory.DbContext;
        }

        public async Task<HttpResponseMessage> Execute()
        {
            return await _httpClient.DefaultRequestHeaders(Claims).GetAsync("api/account/balance/money");
        }

        [TestMethod]
        public async Task CashierScenario()
        {
            _dbContext.Users.Add(
                new User(CashierTestConstants.TestUserId, CashierTestConstants.TestStripeConnectAccountId, CashierTestConstants.TestStripeConnectAccountId)
            );

            await _dbContext.SaveChangesAsync();

            var response = await this.Execute();

            response.EnsureSuccessStatusCode();

            var balance = await response.DeserializeAsync<BalanceViewModel>();

            balance.Should().NotBeNull();
        }

        [TestCleanup]
        public async Task TestCleanup()
        {
            _dbContext.Users.RemoveRange(_dbContext.Users);

            await _dbContext.SaveChangesAsync();
        }
    }
}
