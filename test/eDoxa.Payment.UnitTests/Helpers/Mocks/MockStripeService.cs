// Filename: MockStripeService.cs
// Date Created: 2019-07-03
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

using eDoxa.Payment.Api.Providers.Stripe.Abstractions;
using eDoxa.Payment.Api.Providers.Stripe.Fakers;

using Moq;

namespace eDoxa.Payment.UnitTests.Helpers.Mocks
{
    public sealed class MockStripeService : Mock<IStripeService>
    {
        private readonly CustomerFaker _customerFaker = new CustomerFaker();
        private readonly AccountFaker _accountFaker = new AccountFaker();

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
                .ReturnsAsync(_accountFaker.FakeAccount().Id);

            this.Setup(mock => mock.CreateCustomerAsync(It.IsAny<Guid>(), It.IsAny<string>(), It.IsAny<string>(), It.IsAny<CancellationToken>()))
                .ReturnsAsync(_customerFaker.FakeCustomer().Id);

            this.Setup(
                    mock => mock.CreateInvoiceAsync(
                        It.IsAny<Guid>(),
                        It.IsAny<string>(),
                        It.IsAny<string>(),
                        It.IsAny<long>(),
                        It.IsAny<CancellationToken>()
                    )
                )
                .Returns(Task.CompletedTask);

            this.Setup(
                    mock => mock.CreateTransferAsync(
                        It.IsAny<Guid>(),
                        It.IsAny<string>(),
                        It.IsAny<string>(),
                        It.IsAny<long>(),
                        It.IsAny<CancellationToken>()
                    )
                )
                .Returns(Task.CompletedTask);
        }
    }
}
