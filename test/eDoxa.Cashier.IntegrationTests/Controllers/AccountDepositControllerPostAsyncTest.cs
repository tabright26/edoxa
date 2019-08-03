// Filename: AccountDepositControllerPostAsyncTest.cs
// Date Created: 2019-07-05
// 
// ================================================
// Copyright © 2019, eDoxa. All rights reserved.

using System.Net.Http;
using System.Security.Claims;
using System.Threading.Tasks;

using eDoxa.Cashier.Api.Application.Requests;
using eDoxa.Cashier.Domain.AggregateModels;
using eDoxa.Cashier.Domain.AggregateModels.AccountAggregate;
using eDoxa.Cashier.Domain.Repositories;
using eDoxa.Seedwork.Application.Extensions;
using eDoxa.Seedwork.Security.Constants;
using eDoxa.Seedwork.Testing.Extensions;
using eDoxa.Seedwork.Testing.Helpers;

using FluentAssertions;

using IdentityModel;

using Microsoft.AspNetCore.Http;
using Microsoft.AspNetCore.TestHost;

using Xunit;

namespace eDoxa.Cashier.IntegrationTests.Controllers
{
    public sealed class AccountDepositControllerPostAsyncTest : IClassFixture<CashierWebApplicationFactory>
    {
        public AccountDepositControllerPostAsyncTest(CashierWebApplicationFactory cashierWebApplicationFactory)
        {
            _httpClient = cashierWebApplicationFactory.CreateClient();
            _testServer = cashierWebApplicationFactory.Server;
            _testServer.CleanupDbContext();
        }

        private readonly HttpClient _httpClient;
        private readonly TestServer _testServer;

        private async Task<HttpResponseMessage> ExecuteAsync(UserId userId, string customerId, DepositRequest request)
        {
            return await _httpClient
                .DefaultRequestHeaders(new[] {new Claim(JwtClaimTypes.Subject, userId.ToString()), new Claim(CustomClaimTypes.StripeCustomerId, customerId)})
                .PostAsync("api/account/deposit", new JsonContent(request));
        }

        [Fact]
        public async Task Money_InvalidAmount_ShouldBeStatus400BadRequest()
        {
            // Arrange
            var account = new Account(new UserId());

            await _testServer.UsingScopeAsync(
                async scope =>
                {
                    var accountRepository = scope.GetRequiredService<IAccountRepository>();
                    accountRepository.Create(account);
                    await accountRepository.CommitAsync();
                }
            );

            // Act
            using var response = await this.ExecuteAsync(account.UserId, "cus_test", new DepositRequest(Currency.Money.Name, 2.5M));

            // Assert
            response.StatusCode.Should().Be(StatusCodes.Status400BadRequest);
        }

        [Fact]
        public async Task Money_ValidAmount_ShouldBeStatus200OK()
        {
            // Arrange
            var account = new Account(new UserId());

            await _testServer.UsingScopeAsync(
                async scope =>
                {
                    var accountRepository = scope.GetRequiredService<IAccountRepository>();
                    accountRepository.Create(account);
                    await accountRepository.CommitAsync();
                }
            );

            // Act
            using var response = await this.ExecuteAsync(account.UserId, "cus_test", new DepositRequest(Currency.Money.Name, Money.Fifty));

            // Assert
            response.EnsureSuccessStatusCode();
            response.StatusCode.Should().Be(StatusCodes.Status200OK);
            var message = await response.DeserializeAsync<string>();
            message.Should().NotBeNull();
        }

        [Fact]
        public async Task Token_InvalidAmount_ShouldBeStatus400BadRequest()
        {
            // Arrange
            var account = new Account(new UserId());

            await _testServer.UsingScopeAsync(
                async scope =>
                {
                    var accountRepository = scope.GetRequiredService<IAccountRepository>();
                    accountRepository.Create(account);
                    await accountRepository.CommitAsync();
                }
            );

            // Act
            using var response = await this.ExecuteAsync(account.UserId, "cus_test", new DepositRequest(Currency.Token.Name, 2500M));

            // Assert
            response.StatusCode.Should().Be(StatusCodes.Status400BadRequest);
        }

        [Fact]
        public async Task Token_ValidAmount_ShouldBeStatus200OK()
        {
            // Arrange
            var account = new Account(new UserId());

            await _testServer.UsingScopeAsync(
                async scope =>
                {
                    var accountRepository = scope.GetRequiredService<IAccountRepository>();
                    accountRepository.Create(account);
                    await accountRepository.CommitAsync();
                }
            );

            // Act
            using var response = await this.ExecuteAsync(account.UserId, "cus_test", new DepositRequest(Currency.Token.Name, Token.TwoHundredFiftyThousand));

            // Assert
            response.EnsureSuccessStatusCode();
            response.StatusCode.Should().Be(StatusCodes.Status200OK);
            var message = await response.DeserializeAsync<string>();
            message.Should().NotBeNull();
        }
    }
}
