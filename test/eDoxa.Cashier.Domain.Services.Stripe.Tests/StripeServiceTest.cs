// Filename: StripeServiceTest.cs
// Date Created: 2019-05-10
// 
// ================================================
// Copyright © 2019, eDoxa. All rights reserved.
// 
// This file is subject to the terms and conditions
// defined in file 'LICENSE.md', which is part of
// this source code package.

using System.IO;
using System.Threading;
using System.Threading.Tasks;

using eDoxa.Cashier.Tests.Factories;

using Microsoft.Extensions.Configuration;
using Microsoft.VisualStudio.TestTools.UnitTesting;

using Moq;

using Stripe;

namespace eDoxa.Cashier.Domain.Services.Stripe.Tests
{
    [TestClass]
    public sealed class StripeServiceTest
    {
        private static readonly FakeCashierFactory FakeCashierFactory = FakeCashierFactory.Instance;
        private Mock<BankAccountService> _mockBankAccountService;
        private Mock<CardService> _mockCardService;
        private Mock<CustomerService> _mockCustomerService;
        private Mock<InvoiceItemService> _mockInvoiceItemService;
        private Mock<InvoiceService> _mockInvoiceService;

        [TestInitialize]
        public void TestInitialize()
        {
            _mockBankAccountService = new Mock<BankAccountService>();
            _mockCardService = new Mock<CardService>();
            _mockCustomerService = new Mock<CustomerService>();
            _mockInvoiceService = new Mock<InvoiceService>();
            _mockInvoiceItemService = new Mock<InvoiceItemService>();
        }

        [TestMethod]
        public async Task CreateBankAccountAsync()
        {
            // Arrange
            _mockBankAccountService.Setup(mock =>
                    mock.CreateAsync(It.IsAny<string>(), It.IsAny<BankAccountCreateOptions>(), It.IsAny<RequestOptions>(), It.IsAny<CancellationToken>()))
                .ReturnsAsync(FakeCashierFactory.CreateBankAccount)
                .Verifiable();

            var service = this.StripeService();

            // Act
            await service.CreateBankAccountAsync(FakeCashierFactory.CreateCustomerId(), FakeCashierFactory.CreateSourceToken());

            // Assert
            _mockBankAccountService.Verify(
                mock => mock.CreateAsync(It.IsAny<string>(), It.IsAny<BankAccountCreateOptions>(), It.IsAny<RequestOptions>(), It.IsAny<CancellationToken>()),
                Times.Once);
        }

        [TestMethod]
        public async Task DeleteBankAccountAsync()
        {
            // Arrange
            _mockBankAccountService.Setup(mock =>
                    mock.ListAsync(It.IsAny<string>(), It.IsAny<BankAccountListOptions>(), It.IsAny<RequestOptions>(), It.IsAny<CancellationToken>()))
                .ReturnsAsync(FakeCashierFactory.CreateBankAccounts)
                .Verifiable();

            _mockBankAccountService.Setup(mock =>
                    mock.DeleteAsync(It.IsAny<string>(), It.IsAny<string>(), It.IsAny<RequestOptions>(), It.IsAny<CancellationToken>()))
                .ReturnsAsync(FakeCashierFactory.CreateBankAccount)
                .Verifiable();

            var service = this.StripeService();

            // Act
            await service.DeleteBankAccountAsync(FakeCashierFactory.CreateCustomerId());

            // Assert
            _mockBankAccountService.Verify(
                mock => mock.ListAsync(It.IsAny<string>(), It.IsAny<BankAccountListOptions>(), It.IsAny<RequestOptions>(), It.IsAny<CancellationToken>()),
                Times.Once);

            _mockBankAccountService.Verify(
                mock => mock.DeleteAsync(It.IsAny<string>(), It.IsAny<string>(), It.IsAny<RequestOptions>(), It.IsAny<CancellationToken>()), Times.Once);
        }

        [TestMethod]
        public async Task CreateCardAsync()
        {
            // Arrange
            _mockCardService.Setup(mock =>
                    mock.CreateAsync(It.IsAny<string>(), It.IsAny<CardCreateOptions>(), It.IsAny<RequestOptions>(), It.IsAny<CancellationToken>()))
                .ReturnsAsync(FakeCashierFactory.CreateCard)
                .Verifiable();

            _mockCustomerService.Setup(mock =>
                    mock.UpdateAsync(It.IsAny<string>(), It.IsAny<CustomerUpdateOptions>(), It.IsAny<RequestOptions>(), It.IsAny<CancellationToken>()))
                .ReturnsAsync(FakeCashierFactory.CreateCustomer)
                .Verifiable();

            var service = this.StripeService();

            // Act
            await service.CreateCardAsync(FakeCashierFactory.CreateCustomerId(), FakeCashierFactory.CreateSourceToken(), true);

            // Assert
            _mockCardService.Verify(
                mock => mock.CreateAsync(It.IsAny<string>(), It.IsAny<CardCreateOptions>(), It.IsAny<RequestOptions>(), It.IsAny<CancellationToken>()),
                Times.Once);

            _mockCustomerService.Verify(
                mock => mock.UpdateAsync(It.IsAny<string>(), It.IsAny<CustomerUpdateOptions>(), It.IsAny<RequestOptions>(), It.IsAny<CancellationToken>()),
                Times.Once);
        }

        [TestMethod]
        public async Task DeleteCardAsync()
        {
            // Arrange
            _mockCardService.Setup(mock => mock.DeleteAsync(It.IsAny<string>(), It.IsAny<string>(), It.IsAny<RequestOptions>(), It.IsAny<CancellationToken>()))
                .ReturnsAsync(FakeCashierFactory.CreateCard)
                .Verifiable();

            var service = this.StripeService();

            // Act
            await service.DeleteCardAsync(FakeCashierFactory.CreateCustomerId(), FakeCashierFactory.CreateCardId());

            // Assert
            _mockCardService.Verify(mock => mock.DeleteAsync(It.IsAny<string>(), It.IsAny<string>(), It.IsAny<RequestOptions>(), It.IsAny<CancellationToken>()),
                Times.Once);
        }

        [TestMethod]
        public async Task CreateCustomerAsync()
        {
            // Arrange
            _mockCustomerService.Setup(mock => mock.CreateAsync(It.IsAny<CustomerCreateOptions>(), It.IsAny<RequestOptions>(), It.IsAny<CancellationToken>()))
                .ReturnsAsync(FakeCashierFactory.CreateCustomer)
                .Verifiable();
            
            var service = this.StripeService();

            // Act
            await service.CreateCustomerAsync(FakeCashierFactory.CreateUserId(), FakeCashierFactory.CreateCustomer().Email);

            // Assert
            _mockCustomerService.Verify(mock => mock.CreateAsync(It.IsAny<CustomerCreateOptions>(), It.IsAny<RequestOptions>(), It.IsAny<CancellationToken>()), Times.Once);
        }

        [TestMethod]
        public async Task UpdateCustomerDefaultSourceAsync()
        {
            // Arrange
            _mockCustomerService.Setup(mock =>
                    mock.UpdateAsync(It.IsAny<string>(), It.IsAny<CustomerUpdateOptions>(), It.IsAny<RequestOptions>(), It.IsAny<CancellationToken>()))
                .ReturnsAsync(FakeCashierFactory.CreateCustomer)
                .Verifiable();

            var service = this.StripeService();

            // Act
            await service.UpdateCustomerDefaultSourceAsync(FakeCashierFactory.CreateCustomerId(), FakeCashierFactory.CreateCardId());

            // Assert
            _mockCustomerService.Verify(
                mock => mock.UpdateAsync(It.IsAny<string>(), It.IsAny<CustomerUpdateOptions>(), It.IsAny<RequestOptions>(), It.IsAny<CancellationToken>()),
                Times.Once);
        }

        [TestMethod]
        public async Task CreateInvoiceAsync()
        {
            // Arrange
            _mockCustomerService.Setup(mock => mock.GetAsync(It.IsAny<string>(), It.IsAny<RequestOptions>(), It.IsAny<CancellationToken>()))
                .ReturnsAsync(FakeCashierFactory.CreateCustomer)
                .Verifiable();

            _mockInvoiceItemService.Setup(mock =>
                    mock.CreateAsync(It.IsAny<InvoiceItemCreateOptions>(), It.IsAny<RequestOptions>(), It.IsAny<CancellationToken>()))
                .ReturnsAsync(FakeCashierFactory.CreateInvoiceItem)
                .Verifiable();

            _mockInvoiceService.Setup(mock => mock.CreateAsync(It.IsAny<InvoiceCreateOptions>(), It.IsAny<RequestOptions>(), It.IsAny<CancellationToken>()))
                .ReturnsAsync(FakeCashierFactory.CreateInvoice)
                .Verifiable();

            var service = this.StripeService();

            // Act
            await service.CreateInvoiceAsync(FakeCashierFactory.CreateCustomerId(), "test@edoxa.gg", FakeCashierFactory.CreateBundle(),
                FakeCashierFactory.CreateTransaction());

            // Assert
            _mockCustomerService.Verify(mock => mock.GetAsync(It.IsAny<string>(), It.IsAny<RequestOptions>(), It.IsAny<CancellationToken>()), Times.Once);

            _mockInvoiceItemService.Verify(
                mock => mock.CreateAsync(It.IsAny<InvoiceItemCreateOptions>(), It.IsAny<RequestOptions>(), It.IsAny<CancellationToken>()), Times.Once);

            _mockInvoiceService.Verify(mock => mock.CreateAsync(It.IsAny<InvoiceCreateOptions>(), It.IsAny<RequestOptions>(), It.IsAny<CancellationToken>()),
                Times.Once);
        }

        //[TestMethod]
        //public async Task CreatePayoutAsync()
        //{
        //    // Arrange
        //    _mockCustomerService.Setup(mock => mock.GetAsync(It.IsAny<string>(), It.IsAny<RequestOptions>(), It.IsAny<CancellationToken>()))
        //        .ReturnsAsync(FakeCashierFactory.CreateCustomer)
        //        .Verifiable();

        //    _mockPayoutService.Setup(mock => mock.CreateAsync(It.IsAny<PayoutCreateOptions>(), It.IsAny<RequestOptions>(), It.IsAny<CancellationToken>()))
        //        .ReturnsAsync(FakeCashierFactory.CreatePayout)
        //        .Verifiable();

        //    var service = this.StripeService();

        //    // Act
        //    await service.CreatePayoutAsync(FakeCashierFactory.CreateCustomerId(), FakeCashierFactory.CreateBundle(), FakeCashierFactory.CreateTransaction());

        //    // Assert
        //    _mockCustomerService.Verify(mock => mock.GetAsync(It.IsAny<string>(), It.IsAny<RequestOptions>(), It.IsAny<CancellationToken>()), Times.Once);

        //    _mockPayoutService.Verify(mock => mock.CreateAsync(It.IsAny<PayoutCreateOptions>() ,It.IsAny<RequestOptions>(), It.IsAny<CancellationToken>()), Times.Once);
        //}

        private StripeService StripeService()
        {
            var configuration = new ConfigurationBuilder()
                .SetBasePath(Path.Combine(Directory.GetCurrentDirectory(), "../../../../../src/eDoxa.Cashier.Api"))
                .AddJsonFile("appsettings.development.json", false, true)
                .AddEnvironmentVariables()
                .Build();

            return new StripeService(
                _mockBankAccountService.Object,
                _mockCardService.Object,
                _mockCustomerService.Object,
                _mockInvoiceService.Object,
                _mockInvoiceItemService.Object,
                configuration);
        }
    }
}