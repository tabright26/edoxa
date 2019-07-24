// Filename: WithdrawalProcessedIntegrationEventHandlerTest.cs
// Date Created: 2019-07-07
// 
// ================================================
// Copyright © 2019, eDoxa. All rights reserved.

using System;
using System.Threading;
using System.Threading.Tasks;

using Autofac;

using eDoxa.Cashier.Api.Infrastructure.Data.Fakers;
using eDoxa.Cashier.Api.IntegrationEvents;
using eDoxa.Cashier.Domain.AggregateModels;
using eDoxa.Cashier.Domain.AggregateModels.TransactionAggregate;
using eDoxa.Cashier.Domain.Repositories;
using eDoxa.FunctionalTests.Services.Cashier.Helpers;
using eDoxa.FunctionalTests.Services.Payment.Helpers;
using eDoxa.IntegrationEvents;
using eDoxa.Payment.Api.Providers.Stripe.Abstractions;
using eDoxa.Seedwork.Application.Extensions;
using eDoxa.Seedwork.Testing.Extensions;

using FluentAssertions;

using JetBrains.Annotations;

using Microsoft.VisualStudio.TestTools.UnitTesting;

using Moq;

using Stripe;

namespace eDoxa.FunctionalTests.Services.Payment.IntegrationEvents
{
    [TestClass]
    public sealed class WithdrawalProcessedIntegrationEventHandlerTest : TestCashierWebApplicationFactory
    {
        [TestInitialize]
        public async Task TestInitialize()
        {
            this.CreateClient();

            await this.TestCleanup();
        }

        [TestCleanup]
        public async Task TestCleanup()
        {
            await Server.CleanupDbContextAsync();
        }

        [TestMethod]
        public async Task TransactionStatus_ShouldBeSucceded()
        {
            using (var paymentWebApplication = new TestPaymentWebApplicationFactory())
            {
                paymentWebApplication.WithContainerBuilder(
                    builder =>
                    {
                        var mock = new Mock<IStripeService>();

                        mock.Setup(
                                stripeService => stripeService.CreateTransferAsync(
                                    It.IsAny<Guid>(),
                                    It.IsAny<string>(),
                                    It.IsAny<string>(),
                                    It.IsAny<long>(),
                                    It.IsAny<CancellationToken>()
                                )
                            )
                            .Returns(Task.CompletedTask);

                        builder.RegisterInstance(mock.Object).As<IStripeService>();
                    }
                );

                using (paymentWebApplication.CreateClient())
                {
                    var accountFaker = new AccountFaker();
                    accountFaker.UseSeed(23569854);
                    var account = accountFaker.Generate();
                    var moneyDepositTransaction = new MoneyDepositTransaction(Money.Fifty);
                    account?.CreateTransaction(moneyDepositTransaction);

                    await Server.UsingScopeAsync(
                        async scope =>
                        {
                            var accountRepository = scope.GetService<IAccountRepository>();
                            accountRepository.Create(account);
                            await accountRepository.CommitAsync();
                        }
                    );

                    await Server.UsingScopeAsync(
                        async scope =>
                        {
                            var integrationEventService = scope.GetService<IIntegrationEventService>();

                            await integrationEventService.PublishAsync(
                                new WithdrawalProcessedIntegrationEvent(moneyDepositTransaction.Id, moneyDepositTransaction.Description.Text, "acct_test", 5000)
                            );
                        }
                    );

                    var transaction = await this.TryGetPublishedTransaction(moneyDepositTransaction.Id);

                    transaction.Should().NotBeNull();

                    transaction?.Status.Should().Be(TransactionStatus.Succeded);
                }
            }
        }

        [TestMethod]
        public async Task TransactionStatus_ShouldBeFailed()
        {
            using (var paymentWebApplication = new TestPaymentWebApplicationFactory())
            {
                paymentWebApplication.WithContainerBuilder(
                    builder =>
                    {
                        var mock = new Mock<IStripeService>();

                        mock.Setup(
                                stripeService => stripeService.CreateTransferAsync(
                                    It.IsAny<Guid>(),
                                    It.IsAny<string>(),
                                    It.IsAny<string>(),
                                    It.IsAny<long>(),
                                    It.IsAny<CancellationToken>()
                                )
                            )
                            .Throws<StripeException>();

                        builder.RegisterInstance(mock.Object).As<IStripeService>();
                    }
                );

                using (paymentWebApplication.CreateClient())
                {
                    var accountFaker = new AccountFaker();
                    accountFaker.UseSeed(78589854);
                    var account = accountFaker.Generate();
                    var moneyDepositTransaction = new MoneyDepositTransaction(Money.Fifty);
                    account?.CreateTransaction(moneyDepositTransaction);

                    await Server.UsingScopeAsync(
                        async scope =>
                        {
                            var accountRepository = scope.GetService<IAccountRepository>();
                            accountRepository.Create(account);
                            await accountRepository.CommitAsync();
                        }
                    );

                    await Server.UsingScopeAsync(
                        async scope =>
                        {
                            var integrationEventService = scope.GetService<IIntegrationEventService>();

                            await integrationEventService.PublishAsync(
                                new WithdrawalProcessedIntegrationEvent(moneyDepositTransaction.Id, moneyDepositTransaction.Description.Text, "acct_test", 5000)
                            );
                        }
                    );

                    var transaction = await this.TryGetPublishedTransaction(moneyDepositTransaction.Id);

                    transaction.Should().NotBeNull();

                    transaction?.Status.Should().Be(TransactionStatus.Failed);
                }
            }
        }

        [ItemCanBeNull]
        private async Task<ITransaction> TryGetPublishedTransaction(TransactionId transactionId)
        {
            var counter = 0;

            while (counter < 20)
            {
                var transaction = await Server.UsingScopeAsync(
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
