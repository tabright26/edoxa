// Filename: StripeServiceTest.cs
// Date Created: 2019-06-01
// 
// ================================================
// Copyright © 2019, eDoxa. All rights reserved.
// 
// This file is subject to the terms and conditions
// defined in file 'LICENSE.md', which is part of
// this source code package.

using System;
using System.IO;
using System.Threading;
using System.Threading.Tasks;

using eDoxa.Stripe.Data.Fakers;
using eDoxa.Stripe.Extensions;
using eDoxa.Stripe.Services;

using Microsoft.Extensions.Configuration;
using Microsoft.VisualStudio.TestTools.UnitTesting;

using Moq;

using Stripe;

namespace eDoxa.Stripe.UnitTests.Services
{
    [TestClass]
    public sealed class StripeServiceTest
    {
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
        public async Task VerifyAccountAsync()
        {
            // Arrange
            var accountFaker = new AccountFaker();

            var account = accountFaker.FakeAccount();

            _mockAccountService
                .Setup(
                    mock => mock.UpdateAsync(It.IsAny<string>(), It.IsAny<AccountUpdateOptions>(), It.IsAny<RequestOptions>(), It.IsAny<CancellationToken>())
                )
                .ReturnsAsync(account)
                .Verifiable();

            var service = this.StripeService();

            // Act
            await service.VerifyAccountAsync(
                account.ToStripeId(),
                account.Individual.Address.Line1,
                account.Individual.Address.Line2,
                account.Individual.Address.City,
                account.Individual.Address.State,
                account.Individual.Address.PostalCode
            );

            // Assert
            _mockAccountService.Verify(
                mock => mock.UpdateAsync(It.IsAny<string>(), It.IsAny<AccountUpdateOptions>(), It.IsAny<RequestOptions>(), It.IsAny<CancellationToken>()),
                Times.Once
            );
        }

        [TestMethod]
        public async Task CreateBankAccountAsync()
        {
            // Arrange
            var bankAccountFaker = new BankAccountFaker();

            var accountFaker = new AccountFaker();

            var account = accountFaker.FakeAccount();

            _mockExternalAccountService
                .Setup(
                    mock => mock.CreateAsync(
                        It.IsAny<string>(),
                        It.IsAny<ExternalAccountCreateOptions>(),
                        It.IsAny<RequestOptions>(),
                        It.IsAny<CancellationToken>()
                    )
                )
                .ReturnsAsync(bankAccountFaker.FakeBankAccount)
                .Verifiable();

            var service = this.StripeService();

            // Act
            await service.CreateBankAccountAsync(account.ToStripeId(), "qwe23rwr2r12rqwe123qwsda241qweasd");

            // Assert
            _mockExternalAccountService.Verify(
                mock => mock.CreateAsync(
                    It.IsAny<string>(),
                    It.IsAny<ExternalAccountCreateOptions>(),
                    It.IsAny<RequestOptions>(),
                    It.IsAny<CancellationToken>()
                ),
                Times.Once
            );
        }

        [TestMethod]
        public async Task DeleteBankAccountAsync()
        {
            // Arrange
            var bankAccountFaker = new BankAccountFaker();

            var bankAccount = bankAccountFaker.FakeBankAccount();

            var accountFaker = new AccountFaker();

            var account = accountFaker.FakeAccount();

            _mockExternalAccountService
                .Setup(mock => mock.DeleteAsync(It.IsAny<string>(), It.IsAny<string>(), It.IsAny<RequestOptions>(), It.IsAny<CancellationToken>()))
                .ReturnsAsync(bankAccountFaker.FakeBankAccount)
                .Verifiable();

            var service = this.StripeService();

            // Act
            await service.DeleteBankAccountAsync(account.ToStripeId(), bankAccount.ToStripeId());

            // Assert
            _mockExternalAccountService.Verify(
                mock => mock.DeleteAsync(It.IsAny<string>(), It.IsAny<string>(), It.IsAny<RequestOptions>(), It.IsAny<CancellationToken>()),
                Times.Once
            );
        }

        [TestMethod]
        public async Task ListCardsAsync()
        {
            // Arrange
            var customerFaker = new CustomerFaker();

            var customer = customerFaker.FakeCustomer();

            var cardFaker = new CardFaker();

            _mockCardService
                .Setup(mock => mock.ListAsync(It.IsAny<string>(), It.IsAny<CardListOptions>(), It.IsAny<RequestOptions>(), It.IsAny<CancellationToken>()))
                .ReturnsAsync(cardFaker.FakeCards(5))
                .Verifiable();

            var service = this.StripeService();

            // Act
            await service.GetCardsAsync(customer.ToStripeId());

            // Assert
            _mockCardService.Verify(
                mock => mock.ListAsync(It.IsAny<string>(), It.IsAny<CardListOptions>(), It.IsAny<RequestOptions>(), It.IsAny<CancellationToken>()),
                Times.Once
            );
        }

        [TestMethod]
        public async Task CreateCardAsync()
        {
            // Arrange
            var customerFaker = new CustomerFaker();

            var customer = customerFaker.FakeCustomer();

            var cardFaker = new CardFaker();

            var card = cardFaker.FakeCard();

            _mockCardService
                .Setup(mock => mock.CreateAsync(It.IsAny<string>(), It.IsAny<CardCreateOptions>(), It.IsAny<RequestOptions>(), It.IsAny<CancellationToken>()))
                .ReturnsAsync(card)
                .Verifiable();

            var service = this.StripeService();

            // Act
            await service.CreateCardAsync(customer.ToStripeId(), "qwe23rwr2r12rqwe123qwsda241qweasd");

            // Assert
            _mockCardService.Verify(
                mock => mock.CreateAsync(It.IsAny<string>(), It.IsAny<CardCreateOptions>(), It.IsAny<RequestOptions>(), It.IsAny<CancellationToken>()),
                Times.Once
            );
        }

        [TestMethod]
        public async Task DeleteCardAsync()
        {
            // Arrange
            var customerFaker = new CustomerFaker();

            var customer = customerFaker.FakeCustomer();

            var cardFaker = new CardFaker();

            var card = cardFaker.FakeCard();

            _mockCardService.Setup(mock => mock.DeleteAsync(It.IsAny<string>(), It.IsAny<string>(), It.IsAny<RequestOptions>(), It.IsAny<CancellationToken>()))
                .ReturnsAsync(card)
                .Verifiable();

            var service = this.StripeService();

            // Act
            await service.DeleteCardAsync(customer.ToStripeId(), card.ToStripeId());

            // Assert
            _mockCardService.Verify(
                mock => mock.DeleteAsync(It.IsAny<string>(), It.IsAny<string>(), It.IsAny<RequestOptions>(), It.IsAny<CancellationToken>()),
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
            await service.CreateCustomerAsync(Guid.NewGuid(), account.ToStripeId(), customer.Email);

            // Assert
            _mockCustomerService.Verify(
                mock => mock.CreateAsync(It.IsAny<CustomerCreateOptions>(), It.IsAny<RequestOptions>(), It.IsAny<CancellationToken>()),
                Times.Once
            );
        }

        [TestMethod]
        public async Task UpdateCardDefaultAsync()
        {
            // Arrange
            var customerFaker = new CustomerFaker();

            var customer = customerFaker.FakeCustomer();

            var cardFaker = new CardFaker();

            var card = cardFaker.FakeCard();

            _mockCustomerService
                .Setup(
                    mock => mock.UpdateAsync(It.IsAny<string>(), It.IsAny<CustomerUpdateOptions>(), It.IsAny<RequestOptions>(), It.IsAny<CancellationToken>())
                )
                .ReturnsAsync(customer)
                .Verifiable();

            var service = this.StripeService();

            // Act
            await service.UpdateCardDefaultAsync(customer.ToStripeId(), card.ToStripeId());

            // Assert
            _mockCustomerService.Verify(
                mock => mock.UpdateAsync(It.IsAny<string>(), It.IsAny<CustomerUpdateOptions>(), It.IsAny<RequestOptions>(), It.IsAny<CancellationToken>()),
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
            await service.CreateInvoiceAsync(customer.ToStripeId(), 1000, Guid.NewGuid(), string.Empty);

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
            await service.CreateTransferAsync(account.ToStripeId(), 1000, Guid.NewGuid(), string.Empty);

            // Assert
            _mockTransferService.Verify(
                mock => mock.CreateAsync(It.IsAny<TransferCreateOptions>(), It.IsAny<RequestOptions>(), It.IsAny<CancellationToken>()),
                Times.Once
            );
        }

        private StripeService StripeService()
        {
            var configuration = new ConfigurationBuilder().SetBasePath(Path.Combine(Directory.GetCurrentDirectory(), "../../../../../src/eDoxa.Cashier.Api"))
                .AddJsonFile("appsettings.json", false, true)
                .AddEnvironmentVariables()
                .Build();

            return new StripeService(
                configuration,
                _mockAccountService.Object,
                _mockCardService.Object,
                _mockCustomerService.Object,
                _mockExternalAccountService.Object,
                _mockInvoiceService.Object,
                _mockInvoiceItemService.Object,
                _mockTransferService.Object
            );
        }
    }
}
