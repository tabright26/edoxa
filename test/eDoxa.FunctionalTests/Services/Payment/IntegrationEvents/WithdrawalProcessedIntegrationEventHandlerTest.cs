// Filename: WithdrawalProcessedIntegrationEventHandlerTest.cs
// Date Created: 2019-08-18
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
using eDoxa.Cashier.Domain.AggregateModels.AccountAggregate;
using eDoxa.Cashier.Domain.AggregateModels.TransactionAggregate;
using eDoxa.Cashier.Domain.Repositories;
using eDoxa.FunctionalTests.Services.Cashier;
using eDoxa.Payment.Api.Providers.Stripe.Abstractions;
using eDoxa.Seedwork.Application.Extensions;
using eDoxa.Seedwork.Testing.Extensions;
using eDoxa.ServiceBus.Abstractions;

using FluentAssertions;

using Microsoft.AspNetCore.TestHost;

using Moq;

using Stripe;

using Xunit;

namespace eDoxa.FunctionalTests.Services.Payment.IntegrationEvents
{
    public sealed class WithdrawalProcessedIntegrationEventHandlerTest : IClassFixture<CashierApiFactory>
    {
        public WithdrawalProcessedIntegrationEventHandlerTest(CashierApiFactory cashierApiFactory)
        {
            cashierApiFactory.CreateClient();
            _testServer = cashierApiFactory.Server;
            _testServer.CleanupDbContext();
        }

        private readonly TestServer _testServer;

        // TODO: Create an helper method for functional assertions with a retry policy. (Maybe with Polly)
        private async Task<ITransaction> TryGetPublishedTransaction(TransactionId transactionId)
        {
            var counter = 0;

            while (counter < 20)
            {
                var transaction = await _testServer.UsingScopeAsync(
                    async scope =>
                    {
                        var transactionRepository = scope.GetRequiredService<ITransactionRepository>();

                        return await transactionRepository.FindTransactionAsync(transactionId);
                    }
                );

                if (transaction == null || transaction.Status == TransactionStatus.Pending)
                {
                    counter++;

                    await Task.Delay(2500);

                    continue;
                }

                return transaction;
            }

            return null;
        }

        [Fact]

        // TODO: The method name must be written as a test scenario.
        public async Task TransactionStatus_ShouldBeFailed()
        {
            using var paymentWebApplicationFactory = new PaymentApiFactory().WithWebHostBuilder(
                builder => builder.ConfigureTestContainer<ContainerBuilder>(
                    container =>
                    {
                        var mockStripeService = new Mock<IStripeService>();

                        mockStripeService.Setup(
                                stripeService => stripeService.CreateTransferAsync(
                                    It.IsAny<Guid>(),
                                    It.IsAny<string>(),
                                    It.IsAny<string>(),
                                    It.IsAny<long>(),
                                    It.IsAny<CancellationToken>()
                                )
                            )
                            .Throws<StripeException>();

                        container.RegisterInstance(mockStripeService.Object).As<IStripeService>();
                    }
                )
            );

            using (paymentWebApplicationFactory.CreateClient())
            {
                var account = new eDoxa.Cashier.Domain.AggregateModels.AccountAggregate.Account(new UserId());
                var moneyDepositTransaction = new MoneyDepositTransaction(Money.Fifty);
                account.CreateTransaction(moneyDepositTransaction);

                await _testServer.UsingScopeAsync(
                    async scope =>
                    {
                        var accountRepository = scope.GetRequiredService<IAccountRepository>();
                        accountRepository.Create(account);
                        await accountRepository.CommitAsync();
                    }
                );

                await _testServer.UsingScopeAsync(
                    async scope =>
                    {
                        var integrationEventService = scope.GetRequiredService<IServiceBusPublisher>();

                        await integrationEventService.PublishAsync(
                            new UserAccountWithdrawalIntegrationEvent(moneyDepositTransaction.Id, moneyDepositTransaction.Description.Text, "acct_test", 5000)
                        );
                    }
                );

                var transaction = await this.TryGetPublishedTransaction(moneyDepositTransaction.Id);

                transaction.Should().NotBeNull();

                transaction?.Status.Should().Be(TransactionStatus.Failed);
            }
        }

        [Fact]

        // TODO: The method name must be written as a test scenario.
        public async Task TransactionStatus_ShouldBeSucceded()
        {
            using var paymentWebApplicationFactory = new PaymentApiFactory().WithWebHostBuilder(
                builder => builder.ConfigureTestContainer<ContainerBuilder>(
                    container =>
                    {
                        var mockStripeService = new Mock<IStripeService>();

                        mockStripeService.Setup(
                                stripeService => stripeService.CreateTransferAsync(
                                    It.IsAny<Guid>(),
                                    It.IsAny<string>(),
                                    It.IsAny<string>(),
                                    It.IsAny<long>(),
                                    It.IsAny<CancellationToken>()
                                )
                            )
                            .Returns(Task.CompletedTask);

                        container.RegisterInstance(mockStripeService.Object).As<IStripeService>();
                    }
                )
            );

            using (paymentWebApplicationFactory.CreateClient())
            {
                var account = new eDoxa.Cashier.Domain.AggregateModels.AccountAggregate.Account(new UserId());
                var moneyDepositTransaction = new MoneyDepositTransaction(Money.Fifty);
                account.CreateTransaction(moneyDepositTransaction);

                await _testServer.UsingScopeAsync(
                    async scope =>
                    {
                        var accountRepository = scope.GetRequiredService<IAccountRepository>();
                        accountRepository.Create(account);
                        await accountRepository.CommitAsync();
                    }
                );

                await _testServer.UsingScopeAsync(
                    async scope =>
                    {
                        var integrationEventService = scope.GetRequiredService<IServiceBusPublisher>();

                        await integrationEventService.PublishAsync(
                            new UserAccountWithdrawalIntegrationEvent(moneyDepositTransaction.Id, moneyDepositTransaction.Description.Text, "acct_test", 5000)
                        );
                    }
                );

                var transaction = await this.TryGetPublishedTransaction(moneyDepositTransaction.Id);

                transaction.Should().NotBeNull();

                transaction?.Status.Should().Be(TransactionStatus.Succeded);
            }
        }
    }
}
