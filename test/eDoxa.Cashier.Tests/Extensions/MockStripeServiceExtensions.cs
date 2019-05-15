// Filename: MockStripeServiceExtensions.cs
// Date Created: 2019-05-13
// 
// ================================================
// Copyright © 2019, eDoxa. All rights reserved.
// 
// This file is subject to the terms and conditions
// defined in file 'LICENSE.md', which is part of
// this source code package.

using System.Threading;
using System.Threading.Tasks;

using eDoxa.Cashier.Domain.Abstractions;
using eDoxa.Cashier.Domain.AggregateModels;
using eDoxa.Cashier.Domain.Services.Stripe.Abstractions;
using eDoxa.Cashier.Domain.Services.Stripe.Models;
using eDoxa.Cashier.Tests.Factories;
using eDoxa.Functional;

using Moq;

using Stripe;

namespace eDoxa.Cashier.Tests.Extensions
{
    public static class MockStripeServiceExtensions
    {
        private static readonly FakeStripeFactory FakeStripeFactory = FakeStripeFactory.Instance;

        public static void SetupMethods(this Mock<IStripeService> mockStripeService)
        {
            mockStripeService
                .Setup(mock => mock.CreateAccountAsync(It.IsAny<UserId>(), It.IsAny<string>(), It.IsAny<string>(), It.IsAny<string>(), It.IsAny<int>(),
                    It.IsAny<int>(), It.IsAny<int>(), It.IsAny<CancellationToken>()))
                .ReturnsAsync(FakeStripeFactory.CreateAccountId);

            mockStripeService
                .Setup(mock => mock.VerifyAccountAsync(It.IsAny<StripeAccountId>(), It.IsAny<string>(), It.IsAny<string>(), It.IsAny<string>(),
                    It.IsAny<string>(), It.IsAny<string>(), It.IsAny<CancellationToken>()))
                .Returns(Task.CompletedTask);

            mockStripeService
                .Setup(mock => mock.CreateBankAccountAsync(It.IsAny<StripeAccountId>(), It.IsAny<string>(), It.IsAny<CancellationToken>()))
                .ReturnsAsync(FakeStripeFactory.CreateBankAccountId);

            mockStripeService
                .Setup(mock => mock.DeleteBankAccountAsync(It.IsAny<StripeAccountId>(), It.IsAny<StripeBankAccountId>(), It.IsAny<CancellationToken>()))
                .Returns(Task.CompletedTask);

            mockStripeService
                .Setup(mock => mock.GetCardsAsync(It.IsAny<StripeCustomerId>()))
                .ReturnsAsync(FakeStripeFactory.CreateCards);

            mockStripeService
                .Setup(mock => mock.GetCardAsync(It.IsAny<StripeCustomerId>(), It.IsAny<StripeCardId>()))
                .ReturnsAsync(new Option<Card>(FakeStripeFactory.CreateCard()));

            mockStripeService
                .Setup(mock => mock.CreateCardAsync(It.IsAny<StripeCustomerId>(), It.IsAny<string>(), It.IsAny<CancellationToken>()))
                .Returns(Task.CompletedTask);

            mockStripeService
                .Setup(mock => mock.DeleteCardAsync(It.IsAny<StripeCustomerId>(), It.IsAny<StripeCardId>(), It.IsAny<CancellationToken>()))
                .Returns(Task.CompletedTask);

            mockStripeService
                .Setup(mock => mock.CreateCustomerAsync(It.IsAny<UserId>(), It.IsAny<StripeAccountId>(), It.IsAny<string>(), It.IsAny<CancellationToken>()))
                .ReturnsAsync(FakeStripeFactory.CreateCustomerId());

            mockStripeService
                .Setup(mock => mock.UpdateCardDefaultAsync(It.IsAny<StripeCustomerId>(), It.IsAny<StripeCardId>(), It.IsAny<CancellationToken>()))
                .Returns(Task.CompletedTask);

            mockStripeService
                .Setup(mock => mock.CreateInvoiceAsync(It.IsAny<StripeCustomerId>(), It.IsAny<IBundle>(), It.IsAny<ITransaction>(),
                    It.IsAny<CancellationToken>()))
                .Returns(Task.CompletedTask);

            mockStripeService
                .Setup(mock => mock.CreateTransferAsync(It.IsAny<StripeAccountId>(), It.IsAny<IBundle>(), It.IsAny<ITransaction>(),
                    It.IsAny<CancellationToken>()))
                .Returns(Task.CompletedTask);
        }
    }
}