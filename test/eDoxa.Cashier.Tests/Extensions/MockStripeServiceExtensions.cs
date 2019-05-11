// Filename: MockStripeServiceExtensions.cs
// Date Created: 2019-05-10
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

using Moq;

namespace eDoxa.Cashier.Tests.Extensions
{
    public static class MockStripeServiceExtensions
    {
        private static readonly FakeCashierFactory FakeCashierFactory = FakeCashierFactory.Instance;

        public static void SetupMethods(this Mock<IStripeService> mockStripeService)
        {
            mockStripeService
                .Setup(mock => mock.CreateBankAccountAsync(It.IsAny<CustomerId>(), It.IsAny<string>(), It.IsAny<CancellationToken>()))
                .ReturnsAsync(FakeCashierFactory.CreateBankAccountId());

            mockStripeService
                .Setup(mock => mock.DeleteBankAccountAsync(It.IsAny<CustomerId>(), It.IsAny<BankAccountId>(), It.IsAny<CancellationToken>()))
                .Returns(Task.CompletedTask);

            mockStripeService
                .Setup(mock => mock.CreateCardAsync(It.IsAny<CustomerId>(), It.IsAny<string>(), It.IsAny<bool>(), It.IsAny<CancellationToken>()))
                .Returns(Task.CompletedTask);

            mockStripeService
                .Setup(mock => mock.DeleteCardAsync(It.IsAny<CustomerId>(), It.IsAny<CardId>(), It.IsAny<CancellationToken>()))
                .Returns(Task.CompletedTask);

            mockStripeService
                .Setup(mock => mock.CreateCustomerAsync(It.IsAny<UserId>(), It.IsAny<string>(), It.IsAny<CancellationToken>()))
                .ReturnsAsync(FakeCashierFactory.CreateCustomerId());

            mockStripeService
                .Setup(mock => mock.UpdateCustomerEmailAsync(It.IsAny<CustomerId>(), It.IsAny<string>(), It.IsAny<CancellationToken>()))
                .Returns(Task.CompletedTask);

            mockStripeService
                .Setup(mock => mock.UpdateCustomerDefaultSourceAsync(It.IsAny<CustomerId>(), It.IsAny<CardId>(), It.IsAny<CancellationToken>()))
                .Returns(Task.CompletedTask);

            mockStripeService
                .Setup(mock => mock.CreateInvoiceAsync(It.IsAny<CustomerId>(), It.IsAny<IBundle>(), It.IsAny<ITransaction>(), It.IsAny<CancellationToken>()))
                .Returns(Task.CompletedTask);

            mockStripeService
                .Setup(mock => mock.CreatePayoutAsync(It.IsAny<CustomerId>(), It.IsAny<IBundle>(), It.IsAny<ITransaction>(), It.IsAny<CancellationToken>()))
                .Returns(Task.CompletedTask);
        }
    }
}