﻿// Filename: AccountWithdrawalControllerPostAsyncTest.cs
// Date Created: 2019-07-04
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

using eDoxa.Cashier.Api.Application.Commands;
using eDoxa.Cashier.Domain.AggregateModels;
using eDoxa.Cashier.Domain.AggregateModels.AccountAggregate;
using eDoxa.Cashier.Domain.AggregateModels.TransactionAggregate;
using eDoxa.Cashier.Domain.Repositories;
using eDoxa.Cashier.Infrastructure;
using eDoxa.Cashier.IntegrationTests.Helpers;
using eDoxa.Seedwork.Common.ValueObjects;
using eDoxa.Seedwork.Security.Constants;
using eDoxa.Seedwork.Testing.Extensions;
using eDoxa.Seedwork.Testing.Helpers;

using FluentAssertions;

using IdentityModel;

using Microsoft.AspNetCore.Http;
using Microsoft.AspNetCore.TestHost;
using Microsoft.VisualStudio.TestTools.UnitTesting;

namespace eDoxa.Cashier.IntegrationTests.Controllers
{
    [TestClass]
    public sealed class AccountWithdrawalControllerPostAsyncTest
    {
        private HttpClient _httpClient;
        private TestServer _testServer;

        public async Task<HttpResponseMessage> ExecuteAsync(UserId userId, string connectAccountId, WithdrawalCommand command)
        {
            return await _httpClient
                .DefaultRequestHeaders(
                    new[] {new Claim(JwtClaimTypes.Subject, userId.ToString()), new Claim(CustomClaimTypes.StripeConnectAccountId, connectAccountId)}
                )
                .PostAsync("api/account/withdrawal", new JsonContent(command));
        }

        [TestInitialize]
        public async Task TestInitialize()
        {
            var factory = new WebApplicationFactory<TestStartup>();
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
                    var context = scope.GetService<CashierDbContext>();
                    context.Accounts.RemoveRange(context.Accounts);
                    await context.SaveChangesAsync();
                }
            );
        }

        [TestMethod]
        public async Task Money_ValidAmount_ShouldBeStatus200OK()
        {
            // Arrange
            var account = new Account(new UserId());

            ITransaction transaction = new MoneyDepositTransaction(Money.Fifty);

            account.CreateTransaction(transaction);

            await _testServer.UsingScopeAsync(
                async scope =>
                {
                    var accountRepository = scope.GetService<IAccountRepository>();
                    accountRepository.Create(account);
                    await accountRepository.CommitAsync();
                }
            );

            await _testServer.UsingScopeAsync(
                async scope =>
                {
                    var transactionRepository = scope.GetService<ITransactionRepository>();
                    transaction = await transactionRepository.FindTransactionAsync(transaction.Id);
                    transaction?.MarkAsSucceded();
                    await transactionRepository.CommitAsync();
                }
            );

            // Act
            var response = await this.ExecuteAsync(account.UserId, "acct_test", new WithdrawalCommand(Money.Fifty));

            // Assert
            response.EnsureSuccessStatusCode();
            response.StatusCode.Should().Be(StatusCodes.Status200OK);
            var message = await response.DeserializeAsync<string>();
            message.Should().NotBeNull();
        }

        [TestMethod]
        public async Task Money_InvalidAmount_ShouldBeStatus400BadRequest()
        {
            // Arrange
            var account = new Account(new UserId());

            await _testServer.UsingScopeAsync(
                async scope =>
                {
                    var accountRepository = scope.GetService<IAccountRepository>();
                    accountRepository.Create(account);
                    await accountRepository.CommitAsync();
                }
            );

            // Act
            var response = await this.ExecuteAsync(account.UserId, "acct_test", new WithdrawalCommand(2.5M));

            // Assert
            response.StatusCode.Should().Be(StatusCodes.Status400BadRequest);
        }

        [TestMethod]
        public async Task Money_InsufficientFunds_ShouldBeStatus400BadRequest()
        {
            // Arrange
            var account = new Account(new UserId());

            await _testServer.UsingScopeAsync(
                async scope =>
                {
                    var accountRepository = scope.GetService<IAccountRepository>();
                    accountRepository.Create(account);
                    await accountRepository.CommitAsync();
                }
            );

            // Act
            var response = await this.ExecuteAsync(account.UserId, "acct_test", new WithdrawalCommand(Money.Fifty));

            // Assert
            response.StatusCode.Should().Be(StatusCodes.Status400BadRequest);
        }

        [TestMethod]
        public async Task User_WithoutAccount_ShouldBeStatus404NotFound()
        {
            // Act
            var response = await this.ExecuteAsync(new UserId(), "acct_test", new WithdrawalCommand(Money.Fifty));

            // Assert
            response.StatusCode.Should().Be(StatusCodes.Status400BadRequest);
        }
    }
}