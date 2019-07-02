// Filename: MockStripeService.cs
// Date Created: 2019-07-01
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

namespace eDoxa.Cashier.UnitTests.Helpers.Mocks
{
    public sealed class MockStripeService : Mock<IStripeService>
    {
        private readonly CustomerFaker _customerFaker = new CustomerFaker();
        private readonly AccountFaker _accountFaker = new AccountFaker();
        private readonly BankAccountFaker _bankAccountFaker = new BankAccountFaker();
        private readonly CardFaker _cardFaker = new CardFaker();

        public MockStripeService()
        {
            this.Setup(
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
                .ReturnsAsync(_accountFaker.FakeAccount().ToStripeId());

            this.Setup(
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

            this.Setup(mock => mock.CreateBankAccountAsync(It.IsAny<StripeConnectAccountId>(), It.IsAny<string>(), It.IsAny<CancellationToken>()))
                .ReturnsAsync(_bankAccountFaker.FakeBankAccount().ToStripeId);

            this.Setup(mock => mock.DeleteBankAccountAsync(It.IsAny<StripeConnectAccountId>(), It.IsAny<StripeBankAccountId>(), It.IsAny<CancellationToken>()))
                .Returns(Task.CompletedTask);

            this.Setup(mock => mock.GetCardsAsync(It.IsAny<StripeCustomerId>())).ReturnsAsync(_cardFaker.FakeCards(5));

            this.Setup(mock => mock.CreateCardAsync(It.IsAny<StripeCustomerId>(), It.IsAny<string>(), It.IsAny<CancellationToken>()))
                .Returns(Task.CompletedTask);

            this.Setup(mock => mock.DeleteCardAsync(It.IsAny<StripeCustomerId>(), It.IsAny<StripeCardId>(), It.IsAny<CancellationToken>()))
                .Returns(Task.CompletedTask);

            this.Setup(
                    mock => mock.CreateCustomerAsync(It.IsAny<Guid>(), It.IsAny<StripeConnectAccountId>(), It.IsAny<string>(), It.IsAny<CancellationToken>())
                )
                .ReturnsAsync(_customerFaker.FakeCustomer().ToStripeId);

            this.Setup(mock => mock.UpdateCardDefaultAsync(It.IsAny<StripeCustomerId>(), It.IsAny<StripeCardId>(), It.IsAny<CancellationToken>()))
                .Returns(Task.CompletedTask);

            this.Setup(
                    mock => mock.CreateInvoiceAsync(
                        It.IsAny<StripeCustomerId>(),
                        It.IsAny<long>(),
                        It.IsAny<Guid>(),
                        It.IsAny<string>(),
                        It.IsAny<CancellationToken>()
                    )
                )
                .Returns(Task.CompletedTask);

            this.Setup(
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
