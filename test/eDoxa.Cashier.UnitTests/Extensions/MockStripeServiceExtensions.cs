// Filename: MockStripeServiceExtensions.cs
// Date Created: 2019-06-01
// 
// ================================================
// Copyright © 2019, eDoxa. All rights reserved.
// 
// This file is subject to the terms and conditions
// defined in file 'LICENSE.md', which is part of
// this source code package.

using System;
using System.Threading;
using System.Threading.Tasks;

using eDoxa.Stripe.Abstractions;
using eDoxa.Stripe.Data.Fakers;
using eDoxa.Stripe.Extensions;
using eDoxa.Stripe.Models;

using Moq;

namespace eDoxa.Cashier.UnitTests.Extensions
{
    public static class MockStripeServiceExtensions
    {
        public static void SetupMethods(this Mock<IStripeService> mockStripeService)
        {
            var customerFaker = new CustomerFaker();

            var customer = customerFaker.FakeCustomer();

            var cardFaker = new CardFaker();

            var accountFaker = new AccountFaker();

            var account = accountFaker.FakeAccount();

            var bankAccountFaker = new BankAccountFaker();

            var bankAccount = bankAccountFaker.FakeBankAccount();

            mockStripeService.Setup(
                    mock => mock.CreateAccountAsync(
                        It.IsAny<Guid>(),
                        It.IsAny<string>(),
                        It.IsAny<string>(),
                        It.IsAny<string>(),
                        It.IsAny<int>(),
                        It.IsAny<int>(),
                        It.IsAny<int>(),
                        It.IsAny<CancellationToken>()
                    )
                )
                .ReturnsAsync(account.ToStripeId());

            mockStripeService.Setup(
                    mock => mock.VerifyAccountAsync(
                        It.IsAny<StripeConnectAccountId>(),
                        It.IsAny<string>(),
                        It.IsAny<string>(),
                        It.IsAny<string>(),
                        It.IsAny<string>(),
                        It.IsAny<string>(),
                        It.IsAny<CancellationToken>()
                    )
                )
                .Returns(Task.CompletedTask);

            mockStripeService.Setup(mock => mock.CreateBankAccountAsync(It.IsAny<StripeConnectAccountId>(), It.IsAny<string>(), It.IsAny<CancellationToken>()))
                .ReturnsAsync(bankAccount.ToStripeId);

            mockStripeService
                .Setup(mock => mock.DeleteBankAccountAsync(It.IsAny<StripeConnectAccountId>(), It.IsAny<StripeBankAccountId>(), It.IsAny<CancellationToken>()))
                .Returns(Task.CompletedTask);

            mockStripeService.Setup(mock => mock.GetCardsAsync(It.IsAny<StripeCustomerId>())).ReturnsAsync(cardFaker.FakeCards(5));

            mockStripeService.Setup(mock => mock.CreateCardAsync(It.IsAny<StripeCustomerId>(), It.IsAny<string>(), It.IsAny<CancellationToken>()))
                .Returns(Task.CompletedTask);

            mockStripeService.Setup(mock => mock.DeleteCardAsync(It.IsAny<StripeCustomerId>(), It.IsAny<StripeCardId>(), It.IsAny<CancellationToken>()))
                .Returns(Task.CompletedTask);

            mockStripeService
                .Setup(
                    mock => mock.CreateCustomerAsync(It.IsAny<Guid>(), It.IsAny<StripeConnectAccountId>(), It.IsAny<string>(), It.IsAny<CancellationToken>())
                )
                .ReturnsAsync(customer.ToStripeId);

            mockStripeService.Setup(mock => mock.UpdateCardDefaultAsync(It.IsAny<StripeCustomerId>(), It.IsAny<StripeCardId>(), It.IsAny<CancellationToken>()))
                .Returns(Task.CompletedTask);

            mockStripeService.Setup(
                    mock => mock.CreateInvoiceAsync(
                        It.IsAny<StripeCustomerId>(),
                        It.IsAny<long>(),
                        It.IsAny<Guid>(),
                        It.IsAny<string>(),
                        It.IsAny<CancellationToken>()
                    )
                )
                .Returns(Task.CompletedTask);

            mockStripeService.Setup(
                    mock => mock.CreateTransferAsync(
                        It.IsAny<StripeConnectAccountId>(),
                        It.IsAny<long>(),
                        It.IsAny<Guid>(),
                        It.IsAny<string>(),
                        It.IsAny<CancellationToken>()
                    )
                )
                .Returns(Task.CompletedTask);
        }
    }
}
