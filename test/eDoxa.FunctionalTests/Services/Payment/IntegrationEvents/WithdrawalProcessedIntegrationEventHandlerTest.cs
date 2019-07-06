// Filename: WithdrawalProcessedIntegrationEventHandlerTest.cs
// Date Created: 2019-07-05
// 
// ================================================
// Copyright © 2019, eDoxa. All rights reserved.
// 
// This file is subject to the terms and conditions
// defined in file 'LICENSE.md', which is part of
// this source code package.

using System.Threading.Tasks;

using eDoxa.Cashier.Api.Application.Fakers;
using eDoxa.Cashier.Api.IntegrationEvents;
using eDoxa.Cashier.Domain.AggregateModels;
using eDoxa.Cashier.Domain.AggregateModels.TransactionAggregate;
using eDoxa.Cashier.Domain.Repositories;
using eDoxa.FunctionalTests.Services.Cashier.Helpers;
using eDoxa.FunctionalTests.Services.Payment.Helpers;
using eDoxa.FunctionalTests.Services.Payment.Helpers.Startups;
using eDoxa.IntegrationEvents;
using eDoxa.Seedwork.Application.Extensions;
using eDoxa.Seedwork.Infrastructure.Abstractions;
using eDoxa.Seedwork.Testing.Extensions;

using FluentAssertions;

using JetBrains.Annotations;

using Microsoft.AspNetCore.TestHost;
using Microsoft.VisualStudio.TestTools.UnitTesting;

namespace eDoxa.FunctionalTests.Services.Payment.IntegrationEvents
{
    [TestClass]
    public sealed class WithdrawalProcessedIntegrationEventHandlerTest
    {
        [TestMethod]
        public async Task TransactionStatus_ShouldBeSucceded()
        {
            using (var cashierWebApplication = new TestCashierWebApplicationFactory<TestCashierStartup>())
            using (cashierWebApplication.CreateClient())
            using (var cashierTestServer = cashierWebApplication.Server)
            using (var paymentWebApplication = new TestPaymentWebApplicationFactory<TransactionStatusSuccededTestPaymentStartup>())
            using (paymentWebApplication.CreateClient())
            {
                await cashierTestServer.UsingScopeAsync(
                    async scope =>
                    {
                        var cashierDbContextData = scope.GetService<IDbContextData>();
                        await cashierDbContextData.CleanupAsync();
                    }
                );

                var accountFaker = new AccountFaker();
                accountFaker.UseSeed(1);
                var account = accountFaker.Generate();
                var moneyDepositTransaction = new MoneyDepositTransaction(Money.Fifty);
                account?.CreateTransaction(moneyDepositTransaction);

                await cashierTestServer.UsingScopeAsync(
                    async scope =>
                    {
                        var accountRepository = scope.GetService<IAccountRepository>();
                        accountRepository.Create(account);
                        await accountRepository.CommitAsync();
                    }
                );

                await cashierTestServer.UsingScopeAsync(
                    async scope =>
                    {
                        var integrationEventService = scope.GetService<IIntegrationEventService>();

                        await integrationEventService.PublishAsync(
                            new WithdrawalProcessedIntegrationEvent(moneyDepositTransaction.Id, moneyDepositTransaction.Description.Text, "acct_test", 5000)
                        );
                    }
                );

                var transaction = await TryGetPublishedTransaction(moneyDepositTransaction.Id, cashierTestServer);

                transaction.Should().NotBeNull();

                transaction?.Status.Should().Be(TransactionStatus.Succeded);
            }
        }

        [TestMethod]
        public async Task TransactionStatus_ShouldBeFailed()
        {
            using (var cashierWebApplication = new TestCashierWebApplicationFactory<TestCashierStartup>())
            using (cashierWebApplication.CreateClient())
            using (var cashierTestServer = cashierWebApplication.Server)
            using (var paymentWebApplication = new TestPaymentWebApplicationFactory<TransactionStatusFailedTestPaymentStartup>())
            using (paymentWebApplication.CreateClient())
            {
                await cashierTestServer.UsingScopeAsync(
                    async scope =>
                    {
                        var cashierDbContextData = scope.GetService<IDbContextData>();
                        await cashierDbContextData.CleanupAsync();
                    }
                );

                var accountFaker = new AccountFaker();
                accountFaker.UseSeed(1);
                var account = accountFaker.Generate();
                var moneyDepositTransaction = new MoneyDepositTransaction(Money.Fifty);
                account?.CreateTransaction(moneyDepositTransaction);

                await cashierTestServer.UsingScopeAsync(
                    async scope =>
                    {
                        var accountRepository = scope.GetService<IAccountRepository>();
                        accountRepository.Create(account);
                        await accountRepository.CommitAsync();
                    }
                );

                await cashierTestServer.UsingScopeAsync(
                    async scope =>
                    {
                        var integrationEventService = scope.GetService<IIntegrationEventService>();

                        await integrationEventService.PublishAsync(
                            new WithdrawalProcessedIntegrationEvent(moneyDepositTransaction.Id, moneyDepositTransaction.Description.Text, "acct_test", 5000)
                        );
                    }
                );

                var transaction = await TryGetPublishedTransaction(moneyDepositTransaction.Id, cashierTestServer);

                transaction.Should().NotBeNull();

                transaction?.Status.Should().Be(TransactionStatus.Failed);
            }
        }

        [ItemCanBeNull]
        private static async Task<ITransaction> TryGetPublishedTransaction(TransactionId transactionId, TestServer cashierTestServer)
        {
            var counter = 0;

            while (counter < 20)
            {
                var transaction = await cashierTestServer.UsingScopeAsync(
                    async scope =>
                    {
                        var transactionRepository = scope.GetService<ITransactionRepository>();

                        return await transactionRepository.FindTransactionAsync(transactionId);
                    }
                );

                if (transaction == null || transaction.Status == TransactionStatus.Pending)
                {
                    counter++;

                    await Task.Delay(100);

                    continue;
                }

                return transaction;
            }

            return null;
        }
    }
}
