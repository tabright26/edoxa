﻿// Filename: DepositProcessedIntegrationEventHandlerTest.cs
// Date Created: 2019-11-25
// 
// ================================================
// Copyright © 2019, eDoxa. All rights reserved.

using System.Threading.Tasks;

using Autofac;

using eDoxa.Cashier.Api.IntegrationEvents.Extensions;
using eDoxa.Cashier.Domain.AggregateModels;
using eDoxa.Cashier.Domain.AggregateModels.AccountAggregate;
using eDoxa.Cashier.Domain.Repositories;
using eDoxa.Cashier.Domain.Services;
using eDoxa.FunctionalTests.Cashier;
using eDoxa.Payment.Domain.Stripe.Services;
using eDoxa.Seedwork.Application.Extensions;
using eDoxa.Seedwork.Domain.Misc;
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
    public sealed class DepositProcessedIntegrationEventHandlerTest : IClassFixture<CashierHostFactory>
    {
        public DepositProcessedIntegrationEventHandlerTest(CashierHostFactory cashierHostFactory)
        {
            cashierHostFactory.CreateClient();
            _testServer = cashierHostFactory.Server;
            _testServer.CleanupDbContext();
        }

        private readonly TestServer _testServer;

        // TODO: Create an helper method for functional assertions with a retry policy. (Maybe with Polly)
        private async Task<ITransaction> TryGetPublishedTransaction(UserId userId, TransactionId transactionId)
        {
            var counter = 0;

            while (counter < 20)
            {
                var transaction = await _testServer.UsingScopeAsync(
                    async scope =>
                    {
                        var transactionRepository = scope.GetRequiredService<IAccountService>();

                        var account = await transactionRepository.FindAccountAsync(userId);

                        return account.FindTransaction(transactionId);
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

        [Fact]

        // TODO: The method name must be written as a test scenario.
        public async Task TransactionStatus_ShouldBeFailed()
        {
            using var paymentWebApplicationFactory = new PaymentHostFactory().WithWebHostBuilder(
                builder => builder.ConfigureTestContainer<ContainerBuilder>(
                    container =>
                    {
                        var mockStripeCustomerSerivce = new Mock<IStripeCustomerService>();

                        mockStripeCustomerSerivce.Setup(stripeCustomerService => stripeCustomerService.GetCustomerIdAsync(It.IsAny<UserId>()))
                            .ReturnsAsync("CustomerId");

                        container.RegisterInstance(mockStripeCustomerSerivce.Object).As<IStripeCustomerService>();

                        var mockStripeService = new Mock<IStripeInvoiceService>();

                        mockStripeService.Setup(
                                stripeService => stripeService.CreateInvoiceAsync(
                                    It.IsAny<string>(),
                                    It.IsAny<TransactionId>(),
                                    It.IsAny<long>(),
                                    It.IsAny<string>()))
                            .Throws<StripeException>();

                        container.RegisterInstance(mockStripeService.Object).As<IStripeInvoiceService>();
                    }));

            using (paymentWebApplicationFactory.CreateClient())
            {
                var account = new Account(new UserId());
                var moneyAccount = new MoneyAccountDecorator(account);
                var depositTransaction = moneyAccount.Deposit(Money.Fifty);

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

                        await integrationEventService.PublishUserAccountDepositIntegrationEventAsync(
                            account.Id,
                            "noreply@edoxa.gg",
                            depositTransaction.Id,
                            depositTransaction.Description.Text,
                            5000);
                    });

                var transaction = await this.TryGetPublishedTransaction(account.Id, depositTransaction.Id);

                transaction.Should().NotBeNull();

                transaction?.Status.Should().Be(TransactionStatus.Failed);
            }
        }

        [Fact]

        // TODO: The method name must be written as a test scenario.
        public async Task TransactionStatus_ShouldBeSucceded()
        {
            using var paymentWebApplicationFactory = new PaymentHostFactory().WithWebHostBuilder(
                builder => builder.ConfigureTestContainer<ContainerBuilder>(
                    container =>
                    {
                        var mockStripeCustomerSerivce = new Mock<IStripeCustomerService>();

                        mockStripeCustomerSerivce.Setup(stripeCustomerService => stripeCustomerService.GetCustomerIdAsync(It.IsAny<UserId>()))
                            .ReturnsAsync("CustomerId");

                        mockStripeCustomerSerivce.Setup(stripeCustomerService => stripeCustomerService.HasDefaultPaymentMethodAsync(It.IsAny<string>()))
                            .ReturnsAsync(true);

                        container.RegisterInstance(mockStripeCustomerSerivce.Object).As<IStripeCustomerService>();

                        var mockStripeService = new Mock<IStripeInvoiceService>();

                        mockStripeService.Setup(
                                stripeService => stripeService.CreateInvoiceAsync(
                                    It.IsAny<string>(),
                                    It.IsAny<TransactionId>(),
                                    It.IsAny<long>(),
                                    It.IsAny<string>()))
                            .ReturnsAsync(new Invoice());

                        container.RegisterInstance(mockStripeService.Object).As<IStripeInvoiceService>();
                    }));

            using (paymentWebApplicationFactory.CreateClient())
            {
                var account = new Account(new UserId());
                var moneyAccount = new MoneyAccountDecorator(account);
                var depositTransaction = moneyAccount.Deposit(Money.Fifty);

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

                        await integrationEventService.PublishUserAccountDepositIntegrationEventAsync(
                            account.Id,
                            "noreply@edoxa.gg",
                            depositTransaction.Id,
                            depositTransaction.Description.Text,
                            5000);
                    });

                var transaction = await this.TryGetPublishedTransaction(account.Id, depositTransaction.Id);

                transaction.Should().NotBeNull();

                transaction?.Status.Should().Be(TransactionStatus.Succeded);
            }
        }
    }
}
