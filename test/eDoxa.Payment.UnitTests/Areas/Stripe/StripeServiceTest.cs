// Filename: StripeServiceTest.cs
// Date Created: 2019-10-02
// 
// ================================================
// Copyright © 2019, eDoxa. All rights reserved.

using System;
using System.Linq;
using System.Threading;
using System.Threading.Tasks;

using eDoxa.Payment.Api.Areas.Stripe;
using eDoxa.Payment.Api.Areas.Stripe.Fakers;
using eDoxa.Payment.TestHelpers;
using eDoxa.Payment.TestHelpers.Fixtures;

using Microsoft.Extensions.Options;

using Moq;

using Stripe;

using Xunit;

namespace eDoxa.Payment.UnitTests.Areas.Stripe
{
    public sealed class StripeServiceTest : UnitTest
    {
        public StripeServiceTest(TestMapperFixture testMapper) : base(testMapper)
        {
            _mockAccountService = new Mock<AccountService>();
            _mockCustomerService = new Mock<CustomerService>();
            _mockInvoiceService = new Mock<InvoiceService>();
            _mockInvoiceItemService = new Mock<InvoiceItemService>();
            _mockTransferService = new Mock<TransferService>();
        }

        private readonly Mock<AccountService> _mockAccountService;
        private readonly Mock<CustomerService> _mockCustomerService;
        private readonly Mock<InvoiceItemService> _mockInvoiceItemService;
        private readonly Mock<InvoiceService> _mockInvoiceService;
        private readonly Mock<TransferService> _mockTransferService;

        private StripeService CreateStripeService()
        {
            var mockOptionsSnapshot = new Mock<IOptionsSnapshot<StripeOptions>>();

            mockOptionsSnapshot.Setup(snapshot => snapshot.Value)
                .Returns(
                    new StripeOptions
                    {
                        Currency = "cad",
                        TaxRateIds = Array.Empty<string>().ToList()
                    });

            return new StripeService(
                mockOptionsSnapshot.Object,
                _mockAccountService.Object,
                _mockCustomerService.Object,
                _mockInvoiceService.Object,
                _mockInvoiceItemService.Object,
                _mockTransferService.Object);
        }

        [Fact]
        public async Task CreateAccountAsync_WhenValid_ShouldBeCompletedTask()
        {
            // Arrange
            var accountFaker = new AccountFaker();

            var account = accountFaker.FakeAccount();

            _mockAccountService.Setup(mock => mock.CreateAsync(It.IsAny<AccountCreateOptions>(), It.IsAny<RequestOptions>(), It.IsAny<CancellationToken>()))
                .ReturnsAsync(account)
                .Verifiable();

            var service = this.CreateStripeService();

            // Act
            await service.CreateAccountAsync(
                Guid.NewGuid(),
                account.Individual.Email,
                account.Individual.FirstName,
                account.Individual.LastName,
                1,
                1,
                2000);

            // Assert
            _mockAccountService.Verify(
                mock => mock.CreateAsync(It.IsAny<AccountCreateOptions>(), It.IsAny<RequestOptions>(), It.IsAny<CancellationToken>()),
                Times.Once);
        }

        [Fact]
        public async Task CreateCustomerAsync_WhenValid_ShouldBeCompletedTask()
        {
            // Arrange
            var accountFaker = new AccountFaker();

            var account = accountFaker.FakeAccount();

            var customerFaker = new CustomerFaker();

            var customer = customerFaker.FakeCustomer();

            _mockCustomerService.Setup(mock => mock.CreateAsync(It.IsAny<CustomerCreateOptions>(), It.IsAny<RequestOptions>(), It.IsAny<CancellationToken>()))
                .ReturnsAsync(customer)
                .Verifiable();

            var service = this.CreateStripeService();

            // Act
            await service.CreateCustomerAsync(Guid.NewGuid(), account.Id, customer.Email);

            // Assert
            _mockCustomerService.Verify(
                mock => mock.CreateAsync(It.IsAny<CustomerCreateOptions>(), It.IsAny<RequestOptions>(), It.IsAny<CancellationToken>()),
                Times.Once);
        }

        [Fact]
        public async Task CreateInvoiceAsync_WhenValid_ShouldBeCompletedTask()
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

            var service = this.CreateStripeService();

            // Act
            await service.CreateInvoiceAsync(
                Guid.NewGuid(),
                string.Empty,
                customer.Id,
                1000);

            // Assert
            _mockInvoiceItemService.Verify(
                mock => mock.CreateAsync(It.IsAny<InvoiceItemCreateOptions>(), It.IsAny<RequestOptions>(), It.IsAny<CancellationToken>()),
                Times.Once);

            _mockInvoiceService.Verify(
                mock => mock.CreateAsync(It.IsAny<InvoiceCreateOptions>(), It.IsAny<RequestOptions>(), It.IsAny<CancellationToken>()),
                Times.Once);
        }

        [Fact]
        public async Task CreateTransferAsync_WhenValid_ShouldBeCompletedTask()
        {
            // Arrange
            var transferFaker = new TransferFaker();

            var accountFaker = new AccountFaker();

            var account = accountFaker.FakeAccount();

            _mockTransferService.Setup(mock => mock.CreateAsync(It.IsAny<TransferCreateOptions>(), It.IsAny<RequestOptions>(), It.IsAny<CancellationToken>()))
                .ReturnsAsync(transferFaker.FakeTransfer)
                .Verifiable();

            var service = this.CreateStripeService();

            // Act
            await service.CreateTransferAsync(
                Guid.NewGuid(),
                string.Empty,
                account.Id,
                1000);

            // Assert
            _mockTransferService.Verify(
                mock => mock.CreateAsync(It.IsAny<TransferCreateOptions>(), It.IsAny<RequestOptions>(), It.IsAny<CancellationToken>()),
                Times.Once);
        }
    }
}
