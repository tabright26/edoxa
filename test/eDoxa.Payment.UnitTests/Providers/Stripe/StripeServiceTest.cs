// Filename: StripeServiceTest.cs
// Date Created: 2019-07-03
// 
// ================================================
// Copyright © 2019, eDoxa. All rights reserved.
// 
// This file is subject to the terms and conditions
// defined in file 'LICENSE.md', which is part of
// this source code package.

using System;
using System.Linq;
using System.Threading;
using System.Threading.Tasks;

using eDoxa.Payment.Api.Providers.Stripe;
using eDoxa.Payment.Api.Providers.Stripe.Fakers;

using Microsoft.Extensions.Options;
using Microsoft.VisualStudio.TestTools.UnitTesting;

using Moq;

using Stripe;

namespace eDoxa.Payment.UnitTests.Providers.Stripe
{
    [TestClass]
    public sealed class StripeServiceTest
    {
        private Mock<AccountService> _mockAccountService;
        private Mock<CustomerService> _mockCustomerService;
        private Mock<InvoiceItemService> _mockInvoiceItemService;
        private Mock<InvoiceService> _mockInvoiceService;
        private Mock<TransferService> _mockTransferService;

        [TestInitialize]
        public void TestInitialize()
        {
            _mockAccountService = new Mock<AccountService>();
            _mockCustomerService = new Mock<CustomerService>();
            _mockInvoiceService = new Mock<InvoiceService>();
            _mockInvoiceItemService = new Mock<InvoiceItemService>();
            _mockTransferService = new Mock<TransferService>();
        }

        [TestMethod]
        public async Task CreateAccountAsync()
        {
            // Arrange
            var accountFaker = new AccountFaker();

            var account = accountFaker.FakeAccount();

            _mockAccountService.Setup(mock => mock.CreateAsync(It.IsAny<AccountCreateOptions>(), It.IsAny<RequestOptions>(), It.IsAny<CancellationToken>()))
                .ReturnsAsync(account)
                .Verifiable();

            var service = this.StripeService();

            // Act
            await service.CreateAccountAsync(
                Guid.NewGuid(),
                account.Individual.Email,
                account.Individual.FirstName,
                account.Individual.LastName,
                1,
                1,
                2000
            );

            // Assert
            _mockAccountService.Verify(
                mock => mock.CreateAsync(It.IsAny<AccountCreateOptions>(), It.IsAny<RequestOptions>(), It.IsAny<CancellationToken>()),
                Times.Once
            );
        }

        [TestMethod]
        public async Task CreateCustomerAsync()
        {
            // Arrange
            var accountFaker = new AccountFaker();

            var account = accountFaker.FakeAccount();

            var customerFaker = new CustomerFaker();

            var customer = customerFaker.FakeCustomer();

            _mockCustomerService.Setup(mock => mock.CreateAsync(It.IsAny<CustomerCreateOptions>(), It.IsAny<RequestOptions>(), It.IsAny<CancellationToken>()))
                .ReturnsAsync(customer)
                .Verifiable();

            var service = this.StripeService();

            // Act
            await service.CreateCustomerAsync(Guid.NewGuid(), account.Id, customer.Email);

            // Assert
            _mockCustomerService.Verify(
                mock => mock.CreateAsync(It.IsAny<CustomerCreateOptions>(), It.IsAny<RequestOptions>(), It.IsAny<CancellationToken>()),
                Times.Once
            );
        }

        [TestMethod]
        public async Task CreateInvoiceAsync()
        {
            // Arrange
            var invoiceFaker = new InvoiceFaker();

            var invoiceItemFaker = new InvoiceItemFaker();

            var customerFaker = new CustomerFaker();

            var customer = customerFaker.FakeCustomer();

            _mockInvoiceItemService
                .Setup(mock => mock.CreateAsync(It.IsAny<InvoiceItemCreateOptions>(), It.IsAny<RequestOptions>(), It.IsAny<CancellationToken>()))
                .ReturnsAsync(invoiceItemFaker.FakeInvoiceItem)
                .Verifiable();

            _mockInvoiceService.Setup(mock => mock.CreateAsync(It.IsAny<InvoiceCreateOptions>(), It.IsAny<RequestOptions>(), It.IsAny<CancellationToken>()))
                .ReturnsAsync(invoiceFaker.FakeInvoice)
                .Verifiable();

            var service = this.StripeService();

            // Act
            await service.CreateInvoiceAsync(Guid.NewGuid(), string.Empty, customer.Id, 1000);

            // Assert
            _mockInvoiceItemService.Verify(
                mock => mock.CreateAsync(It.IsAny<InvoiceItemCreateOptions>(), It.IsAny<RequestOptions>(), It.IsAny<CancellationToken>()),
                Times.Once
            );

            _mockInvoiceService.Verify(
                mock => mock.CreateAsync(It.IsAny<InvoiceCreateOptions>(), It.IsAny<RequestOptions>(), It.IsAny<CancellationToken>()),
                Times.Once
            );
        }

        [TestMethod]
        public async Task CreatePayoutAsync()
        {
            // Arrange
            var transferFaker = new TransferFaker();

            var accountFaker = new AccountFaker();

            var account = accountFaker.FakeAccount();

            _mockTransferService.Setup(mock => mock.CreateAsync(It.IsAny<TransferCreateOptions>(), It.IsAny<RequestOptions>(), It.IsAny<CancellationToken>()))
                .ReturnsAsync(transferFaker.FakeTransfer)
                .Verifiable();

            var service = this.StripeService();

            // Act
            await service.CreateTransferAsync(Guid.NewGuid(), string.Empty, account.Id, 1000);

            // Assert
            _mockTransferService.Verify(
                mock => mock.CreateAsync(It.IsAny<TransferCreateOptions>(), It.IsAny<RequestOptions>(), It.IsAny<CancellationToken>()),
                Times.Once
            );
        }

        private StripeService StripeService()
        {
            var mockOptionsSnapshot = new Mock<IOptionsSnapshot<StripeOptions>>();

            mockOptionsSnapshot.Setup(snapshot => snapshot.Value)
                .Returns(
                    new StripeOptions
                    {
                        Currency = "cad",
                        TaxRateIds = Array.Empty<string>().ToList()
                    }
                );

            return new StripeService(
                mockOptionsSnapshot.Object,
                _mockAccountService.Object,
                _mockCustomerService.Object,
                _mockInvoiceService.Object,
                _mockInvoiceItemService.Object,
                _mockTransferService.Object
            );
        }
    }
}
