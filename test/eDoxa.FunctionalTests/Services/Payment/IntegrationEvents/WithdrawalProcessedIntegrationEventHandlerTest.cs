// Filename: WithdrawalProcessedIntegrationEventHandlerTest.cs
// Date Created: 2019-07-26
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
    public sealed class WithdrawalProcessedIntegrationEventHandlerTest : IClassFixture<CashierWebApplicationFactory>
    {
        public WithdrawalProcessedIntegrationEventHandlerTest(CashierWebApplicationFactory cashierWebApplicationFactory)
        {
            cashierWebApplicationFactory.CreateClient();
            _testServer = cashierWebApplicationFactory.Server;
            _testServer.CleanupDbContext();
        }

        private readonly TestServer _testServer;

        private async Task<ITransaction?> TryGetPublishedTransaction(TransactionId transactionId)
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

                    await Task.Delay(1000);

                    continue;
                }

                return transaction;
            }

            return null;
        }

        [Fact]
        public async Task TransactionStatus_ShouldBeFailed()
        {
            using (var paymentWebApplicationFactory = new PaymentWebApplicationFactory().WithWebHostBuilder(
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
            ))
            {
                using (paymentWebApplicationFactory.CreateClient())
                {
                    var accountFaker = new AccountFaker();
                    accountFaker.UseSeed(78589854);
                    var account = accountFaker.Generate();
                    var moneyDepositTransaction = new MoneyDepositTransaction(Money.Fifty);
                    account?.CreateTransaction(moneyDepositTransaction);

                    await _testServer.UsingScopeAsync(
                        async scope =>
                        {
                            var accountRepository = scope.GetRequiredService<IAccountRepository>();
                            accountRepository.Create(account);
                            await accountRepository.CommitAsync();
                        }
                    );

                    _testServer.UsingScope(
                        scope =>
                        {
                            var integrationEventService = scope.GetRequiredService<IServiceBusPublisher>();

                            integrationEventService.Publish(
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

        [Fact]
        public async Task TransactionStatus_ShouldBeSucceded()
        {
            using (var paymentWebApplicationFactory = new PaymentWebApplicationFactory().WithWebHostBuilder(
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
            ))
            {
                using (paymentWebApplicationFactory.CreateClient())
                {
                    var accountFaker = new AccountFaker();
                    accountFaker.UseSeed(23569854);
                    var account = accountFaker.Generate();
                    var moneyDepositTransaction = new MoneyDepositTransaction(Money.Fifty);
                    account?.CreateTransaction(moneyDepositTransaction);

                    await _testServer.UsingScopeAsync(
                        async scope =>
                        {
                            var accountRepository = scope.GetRequiredService<IAccountRepository>();
                            accountRepository.Create(account);
                            await accountRepository.CommitAsync();
                        }
                    );

                    _testServer.UsingScope(
                        scope =>
                        {
                            var integrationEventService = scope.GetRequiredService<IServiceBusPublisher>();

                            integrationEventService.Publish(
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
    }
}
