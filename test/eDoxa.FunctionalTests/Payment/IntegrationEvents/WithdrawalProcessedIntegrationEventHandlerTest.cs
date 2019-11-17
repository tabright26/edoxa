// Filename: WithdrawalProcessedIntegrationEventHandlerTest.cs
// Date Created: 2019-11-02
// 
// ================================================
// Copyright © 2019, eDoxa. All rights reserved.

using System.Threading.Tasks;

using Autofac;

using eDoxa.Cashier.Api.IntegrationEvents;
using eDoxa.Cashier.Domain.AggregateModels;
using eDoxa.Cashier.Domain.AggregateModels.TransactionAggregate;
using eDoxa.Cashier.Domain.Repositories;
using eDoxa.FunctionalTests.Cashier;
using eDoxa.Payment.Domain.Stripe.Services;
using eDoxa.Seedwork.Application.Extensions;
using eDoxa.Seedwork.Domain.Miscs;
using eDoxa.Seedwork.TestHelper.Extensions;
using eDoxa.ServiceBus.Abstractions;

using FluentAssertions;

using Microsoft.AspNetCore.TestHost;

using Moq;

using Stripe;

using Xunit;

using Account = eDoxa.Cashier.Domain.AggregateModels.AccountAggregate.Account;

namespace eDoxa.FunctionalTests.Payment.IntegrationEvents
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
                    });

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

        // TODO: The method name must be written as a test scenario.
        [Fact]
        public async Task TransactionStatus_ShouldBeFailed()
        {
            using var paymentWebApplicationFactory = new PaymentApiFactory().WithWebHostBuilder(
                builder => builder.ConfigureTestContainer<ContainerBuilder>(
                    container =>
                    {
                        var mockStripeCustomerSerivce = new Mock<IStripeAccountService>();

                        mockStripeCustomerSerivce.Setup(stripeCustomerService => stripeCustomerService.GetAccountIdAsync(It.IsAny<UserId>()))
                            .ReturnsAsync("ConnectAccountId");

                        container.RegisterInstance(mockStripeCustomerSerivce.Object).As<IStripeAccountService>();

                        var mockStripeService = new Mock<IStripeTransferService>();

                        mockStripeService.Setup(
                                stripeService => stripeService.CreateTransferAsync(
                                    It.IsAny<string>(),
                                    It.IsAny<TransactionId>(),
                                    It.IsAny<long>(),
                                    It.IsAny<string>()))
                            .Throws<StripeException>();

                        container.RegisterInstance(mockStripeService.Object).As<IStripeTransferService>();
                    }));

            using (paymentWebApplicationFactory.CreateClient())
            {
                var account = new Account(new UserId());
                var moneyDepositTransaction = new MoneyDepositTransaction(Money.Fifty);
                account.CreateTransaction(moneyDepositTransaction);

                await _testServer.UsingScopeAsync(
                    async scope =>
                    {
                        var accountRepository = scope.GetRequiredService<IAccountRepository>();
                        accountRepository.Create(account);
                        await accountRepository.CommitAsync();
                    });

                await _testServer.UsingScopeAsync(
                    async scope =>
                    {
                        var integrationEventService = scope.GetRequiredService<IServiceBusPublisher>();

                        await integrationEventService.PublishAsync(
                            new UserAccountWithdrawalIntegrationEvent(
                                account.UserId,
                                "noreply@edoxa.gg",
                                moneyDepositTransaction.Id,
                                moneyDepositTransaction.Description.Text,
                                5000));
                    });

                var transaction = await this.TryGetPublishedTransaction(moneyDepositTransaction.Id);

                transaction.Should().NotBeNull();

                transaction?.Status.Should().Be(TransactionStatus.Failed);
            }
        }

        // TODO: The method name must be written as a test scenario.
        [Fact]
        public async Task TransactionStatus_ShouldBeSucceded()
        {
            using var paymentWebApplicationFactory = new PaymentApiFactory().WithWebHostBuilder(
                builder => builder.ConfigureTestContainer<ContainerBuilder>(
                    container =>
                    {
                        var mockStripeAccountSerivce = new Mock<IStripeAccountService>();

                        mockStripeAccountSerivce.Setup(stripeAccountService => stripeAccountService.HasAccountVerifiedAsync(It.IsAny<string>()))
                            .ReturnsAsync(true);

                        mockStripeAccountSerivce.Setup(stripeAccountService => stripeAccountService.GetAccountIdAsync(It.IsAny<UserId>()))
                            .ReturnsAsync("AccountId");

                        container.RegisterInstance(mockStripeAccountSerivce.Object).As<IStripeAccountService>();

                        var mockStripeService = new Mock<IStripeTransferService>();

                        mockStripeService.Setup(
                                stripeService => stripeService.CreateTransferAsync(
                                    It.IsAny<string>(),
                                    It.IsAny<TransactionId>(),
                                    It.IsAny<long>(),
                                    It.IsAny<string>()))
                            .Returns(Task.CompletedTask);

                        container.RegisterInstance(mockStripeService.Object).As<IStripeTransferService>();
                    }));

            using (paymentWebApplicationFactory.CreateClient())
            {
                var account = new Account(new UserId());
                var moneyDepositTransaction = new MoneyDepositTransaction(Money.Fifty);
                account.CreateTransaction(moneyDepositTransaction);

                await _testServer.UsingScopeAsync(
                    async scope =>
                    {
                        var accountRepository = scope.GetRequiredService<IAccountRepository>();
                        accountRepository.Create(account);
                        await accountRepository.CommitAsync();
                    });

                await _testServer.UsingScopeAsync(
                    async scope =>
                    {
                        var integrationEventService = scope.GetRequiredService<IServiceBusPublisher>();

                        await integrationEventService.PublishAsync(
                            new UserAccountWithdrawalIntegrationEvent(
                                account.UserId,
                                "noreply@edoxa.gg",
                                moneyDepositTransaction.Id,
                                moneyDepositTransaction.Description.Text,
                                5000));
                    });

                var transaction = await this.TryGetPublishedTransaction(moneyDepositTransaction.Id);

                transaction.Should().NotBeNull();

                transaction?.Status.Should().Be(TransactionStatus.Succeded);
            }
        }
    }
}
