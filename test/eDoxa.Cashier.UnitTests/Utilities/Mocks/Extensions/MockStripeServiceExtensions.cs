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
using eDoxa.Stripe.Models;
using eDoxa.Stripe.UnitTests.Utilities;

using Moq;

namespace eDoxa.Cashier.UnitTests.Utilities.Mocks.Extensions
{
    public static class MockStripeServiceExtensions
    {
        private static readonly StripeBuilder StripeBuilder = StripeBuilder.Instance;

        public static void SetupMethods(this Mock<IStripeService> mockStripeService)
        {
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
                .ReturnsAsync(StripeBuilder.CreateAccountId);

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
                .ReturnsAsync(StripeBuilder.CreateBankAccountId);

            mockStripeService
                .Setup(mock => mock.DeleteBankAccountAsync(It.IsAny<StripeConnectAccountId>(), It.IsAny<StripeBankAccountId>(), It.IsAny<CancellationToken>()))
                .Returns(Task.CompletedTask);

            mockStripeService.Setup(mock => mock.GetCardsAsync(It.IsAny<StripeCustomerId>())).ReturnsAsync(StripeBuilder.CreateCards);

            mockStripeService.Setup(mock => mock.CreateCardAsync(It.IsAny<StripeCustomerId>(), It.IsAny<string>(), It.IsAny<CancellationToken>()))
                .Returns(Task.CompletedTask);

            mockStripeService.Setup(mock => mock.DeleteCardAsync(It.IsAny<StripeCustomerId>(), It.IsAny<StripeCardId>(), It.IsAny<CancellationToken>()))
                .Returns(Task.CompletedTask);

            mockStripeService
                .Setup(
                    mock => mock.CreateCustomerAsync(It.IsAny<Guid>(), It.IsAny<StripeConnectAccountId>(), It.IsAny<string>(), It.IsAny<CancellationToken>())
                )
                .ReturnsAsync(StripeBuilder.CreateCustomerId);

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
