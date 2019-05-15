// Filename: StripeServiceTest.cs
// Date Created: 2019-05-13
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
        private static readonly FakeStripeFactory FakeStripeFactory = FakeStripeFactory.Instance;
        private static readonly FakeCashierFactory FakeCashierFactory = FakeCashierFactory.Instance;
        private Mock<AccountService> _mockAccountService;
        private Mock<CardService> _mockCardService;
        private Mock<CustomerService> _mockCustomerService;
        private Mock<ExternalAccountService> _mockExternalAccountService;
        private Mock<InvoiceItemService> _mockInvoiceItemService;
        private Mock<InvoiceService> _mockInvoiceService;
        private Mock<TransferService> _mockTransferService;

        [TestInitialize]
        public void TestInitialize()
        {
            _mockAccountService = new Mock<AccountService>();
            _mockCardService = new Mock<CardService>();
            _mockCustomerService = new Mock<CustomerService>();
            _mockExternalAccountService = new Mock<ExternalAccountService>();
            _mockInvoiceService = new Mock<InvoiceService>();
            _mockInvoiceItemService = new Mock<InvoiceItemService>();
            _mockTransferService = new Mock<TransferService>();
        }

        [TestMethod]
        public async Task CreateAccountAsync()
        {
            // Arrange
            var account = FakeStripeFactory.CreateAccount();

            _mockAccountService.Setup(mock => mock.CreateAsync(It.IsAny<AccountCreateOptions>(), It.IsAny<RequestOptions>(), It.IsAny<CancellationToken>()))
                .ReturnsAsync(account)
                .Verifiable();

            var service = this.StripeService();

            // Act
            await service.CreateAccountAsync(FakeCashierFactory.CreateUserId(), account.Individual.Email, account.Individual.FirstName,
                account.Individual.LastName, 1, 1, 2000);

            // Assert
            _mockAccountService.Verify(mock => mock.CreateAsync(It.IsAny<AccountCreateOptions>(), It.IsAny<RequestOptions>(), It.IsAny<CancellationToken>()),
                Times.Once);
        }

        [TestMethod]
        public async Task VerifyAccountAsync()
        {
            // Arrange
            var account = FakeStripeFactory.CreateAccount();

            _mockAccountService.Setup(mock =>
                    mock.UpdateAsync(It.IsAny<string>(), It.IsAny<AccountUpdateOptions>(), It.IsAny<RequestOptions>(), It.IsAny<CancellationToken>()))
                .ReturnsAsync(account)
                .Verifiable();

            var service = this.StripeService();

            // Act
            await service.VerifyAccountAsync(FakeStripeFactory.CreateAccountId(), account.Individual.Address.Line1, account.Individual.Address.Line2,
                account.Individual.Address.City, account.Individual.Address.State, account.Individual.Address.PostalCode);

            // Assert
            _mockAccountService.Verify(
                mock => mock.UpdateAsync(It.IsAny<string>(), It.IsAny<AccountUpdateOptions>(), It.IsAny<RequestOptions>(), It.IsAny<CancellationToken>()),
                Times.Once);
        }

        [TestMethod]
        public async Task CreateBankAccountAsync()
        {
            // Arrange
            _mockExternalAccountService.Setup(mock => mock.CreateAsync(It.IsAny<string>(), It.IsAny<ExternalAccountCreateOptions>(), It.IsAny<RequestOptions>(),
                    It.IsAny<CancellationToken>()))
                .ReturnsAsync(FakeStripeFactory.CreateBankAccount)
                .Verifiable();

            var service = this.StripeService();

            // Act
            await service.CreateBankAccountAsync(FakeStripeFactory.CreateAccountId(), FakeStripeFactory.CreateSourceToken());

            // Assert
            _mockExternalAccountService.Verify(
                mock => mock.CreateAsync(It.IsAny<string>(), It.IsAny<ExternalAccountCreateOptions>(), It.IsAny<RequestOptions>(),
                    It.IsAny<CancellationToken>()), Times.Once);
        }

        [TestMethod]
        public async Task DeleteBankAccountAsync()
        {
            // Arrange

            _mockExternalAccountService.Setup(mock =>
                    mock.DeleteAsync(It.IsAny<string>(), It.IsAny<string>(), It.IsAny<RequestOptions>(), It.IsAny<CancellationToken>()))
                .ReturnsAsync(FakeStripeFactory.CreateBankAccount)
                .Verifiable();

            var service = this.StripeService();

            // Act
            await service.DeleteBankAccountAsync(FakeStripeFactory.CreateAccountId(), FakeStripeFactory.CreateBankAccountId());

            // Assert
            _mockExternalAccountService.Verify(
                mock => mock.DeleteAsync(It.IsAny<string>(), It.IsAny<string>(), It.IsAny<RequestOptions>(), It.IsAny<CancellationToken>()), Times.Once);
        }

        [TestMethod]
        public async Task ListCardsAsync()
        {
            // Arrange
            _mockCardService.Setup(mock =>
                    mock.ListAsync(It.IsAny<string>(), It.IsAny<CardListOptions>(), It.IsAny<RequestOptions>(), It.IsAny<CancellationToken>()))
                .ReturnsAsync(FakeStripeFactory.CreateCards())
                .Verifiable();

            var service = this.StripeService();

            // Act
            await service.GetCardsAsync(FakeStripeFactory.CreateCustomerId());

            // Assert
            _mockCardService.Verify(
                mock => mock.ListAsync(It.IsAny<string>(), It.IsAny<CardListOptions>(), It.IsAny<RequestOptions>(), It.IsAny<CancellationToken>()),
                Times.Once);
        }

        [TestMethod]
        public async Task GetCardAsync()
        {
            // Arrange
            _mockCardService.Setup(mock => mock.GetAsync(It.IsAny<string>(), It.IsAny<string>(), It.IsAny<RequestOptions>(), It.IsAny<CancellationToken>()))
                .ReturnsAsync(FakeStripeFactory.CreateCard)
                .Verifiable();

            var service = this.StripeService();

            // Act
            await service.GetCardAsync(FakeStripeFactory.CreateCustomerId(), FakeStripeFactory.CreateCardId());

            // Assert
            _mockCardService.Verify(
                mock => mock.GetAsync(It.IsAny<string>(), It.IsAny<string>(), It.IsAny<RequestOptions>(), It.IsAny<CancellationToken>()),
                Times.Once);
        }

        [TestMethod]
        public async Task CreateCardAsync()
        {
            // Arrange
            _mockCardService.Setup(mock =>
                    mock.CreateAsync(It.IsAny<string>(), It.IsAny<CardCreateOptions>(), It.IsAny<RequestOptions>(), It.IsAny<CancellationToken>()))
                .ReturnsAsync(FakeStripeFactory.CreateCard)
                .Verifiable();

            var service = this.StripeService();

            // Act
            await service.CreateCardAsync(FakeStripeFactory.CreateCustomerId(), FakeStripeFactory.CreateSourceToken());

            // Assert
            _mockCardService.Verify(
                mock => mock.CreateAsync(It.IsAny<string>(), It.IsAny<CardCreateOptions>(), It.IsAny<RequestOptions>(), It.IsAny<CancellationToken>()),
                Times.Once);
        }

        [TestMethod]
        public async Task DeleteCardAsync()
        {
            // Arrange
            _mockCardService.Setup(mock => mock.DeleteAsync(It.IsAny<string>(), It.IsAny<string>(), It.IsAny<RequestOptions>(), It.IsAny<CancellationToken>()))
                .ReturnsAsync(FakeStripeFactory.CreateCard)
                .Verifiable();

            var service = this.StripeService();

            // Act
            await service.DeleteCardAsync(FakeStripeFactory.CreateCustomerId(), FakeStripeFactory.CreateCardId());

            // Assert
            _mockCardService.Verify(mock => mock.DeleteAsync(It.IsAny<string>(), It.IsAny<string>(), It.IsAny<RequestOptions>(), It.IsAny<CancellationToken>()),
                Times.Once);
        }

        [TestMethod]
        public async Task CreateCustomerAsync()
        {
            // Arrange
            _mockCustomerService.Setup(mock => mock.CreateAsync(It.IsAny<CustomerCreateOptions>(), It.IsAny<RequestOptions>(), It.IsAny<CancellationToken>()))
                .ReturnsAsync(FakeStripeFactory.CreateCustomer)
                .Verifiable();

            var service = this.StripeService();

            // Act
            await service.CreateCustomerAsync(FakeCashierFactory.CreateUserId(), FakeStripeFactory.CreateAccountId(), FakeStripeFactory.CreateCustomer().Email);

            // Assert
            _mockCustomerService.Verify(mock => mock.CreateAsync(It.IsAny<CustomerCreateOptions>(), It.IsAny<RequestOptions>(), It.IsAny<CancellationToken>()),
                Times.Once);
        }

        [TestMethod]
        public async Task UpdateCardDefaultAsync()
        {
            // Arrange
            _mockCustomerService.Setup(mock =>
                    mock.UpdateAsync(It.IsAny<string>(), It.IsAny<CustomerUpdateOptions>(), It.IsAny<RequestOptions>(), It.IsAny<CancellationToken>()))
                .ReturnsAsync(FakeStripeFactory.CreateCustomer)
                .Verifiable();

            var service = this.StripeService();

            // Act
            await service.UpdateCardDefaultAsync(FakeStripeFactory.CreateCustomerId(), FakeStripeFactory.CreateCardId());

            // Assert
            _mockCustomerService.Verify(
                mock => mock.UpdateAsync(It.IsAny<string>(), It.IsAny<CustomerUpdateOptions>(), It.IsAny<RequestOptions>(), It.IsAny<CancellationToken>()),
                Times.Once);
        }

        [TestMethod]
        public async Task CreateInvoiceAsync()
        {
            // Arrange
            _mockInvoiceItemService.Setup(mock =>
                    mock.CreateAsync(It.IsAny<InvoiceItemCreateOptions>(), It.IsAny<RequestOptions>(), It.IsAny<CancellationToken>()))
                .ReturnsAsync(FakeStripeFactory.CreateInvoiceItem)
                .Verifiable();

            _mockInvoiceService.Setup(mock => mock.CreateAsync(It.IsAny<InvoiceCreateOptions>(), It.IsAny<RequestOptions>(), It.IsAny<CancellationToken>()))
                .ReturnsAsync(FakeStripeFactory.CreateInvoice)
                .Verifiable();

            var service = this.StripeService();

            // Act
            await service.CreateInvoiceAsync(FakeStripeFactory.CreateCustomerId(), FakeCashierFactory.CreateBundle(), FakeCashierFactory.CreateTransaction());

            // Assert
            _mockInvoiceItemService.Verify(
                mock => mock.CreateAsync(It.IsAny<InvoiceItemCreateOptions>(), It.IsAny<RequestOptions>(), It.IsAny<CancellationToken>()), Times.Once);

            _mockInvoiceService.Verify(mock => mock.CreateAsync(It.IsAny<InvoiceCreateOptions>(), It.IsAny<RequestOptions>(), It.IsAny<CancellationToken>()),
                Times.Once);
        }

        [TestMethod]
        public async Task CreatePayoutAsync()
        {
            // Arrange
            _mockTransferService.Setup(mock => mock.CreateAsync(It.IsAny<TransferCreateOptions>(), It.IsAny<RequestOptions>(), It.IsAny<CancellationToken>()))
                .ReturnsAsync(FakeStripeFactory.CreateTransfer)
                .Verifiable();

            var service = this.StripeService();

            // Act
            await service.CreateTransferAsync(FakeStripeFactory.CreateAccountId(), FakeCashierFactory.CreateBundle(), FakeCashierFactory.CreateTransaction());

            // Assert
            _mockTransferService.Verify(mock => mock.CreateAsync(It.IsAny<TransferCreateOptions>(), It.IsAny<RequestOptions>(), It.IsAny<CancellationToken>()),
                Times.Once);
        }

        private StripeService StripeService()
        {
            // TODO: WARNING! This implementation of the IConfiguration service can cause problems during automated VSTS testing with Docker support.
            var configuration = new ConfigurationBuilder()
                .SetBasePath(Path.Combine(Directory.GetCurrentDirectory(), "../../../../../src/eDoxa.Cashier.Api"))
                .AddJsonFile("appsettings.development.json", false, true)
                .AddEnvironmentVariables()
                .Build();

            return new StripeService(configuration, _mockAccountService.Object, _mockCardService.Object, _mockCustomerService.Object,
                _mockExternalAccountService.Object, _mockInvoiceService.Object, _mockInvoiceItemService.Object, _mockTransferService.Object);
        }
    }
}