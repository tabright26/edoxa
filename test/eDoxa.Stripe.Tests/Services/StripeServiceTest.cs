// Filename: StripeServiceTest.cs
// Date Created: 2019-05-29
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

using eDoxa.Stripe.Services;
using eDoxa.Stripe.Tests.Utilities;

using Microsoft.Extensions.Configuration;
using Microsoft.VisualStudio.TestTools.UnitTesting;

using Moq;

using Stripe;

namespace eDoxa.Stripe.Tests.Services
{
    [TestClass]
    public sealed class StripeServiceTest
    {
        private static readonly StripeBuilder StripeBuilder = StripeBuilder.Instance;
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
            var account = StripeBuilder.CreateAccount();

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
            var account = StripeBuilder.CreateAccount();

            _mockAccountService
                .Setup(
                    mock => mock.UpdateAsync(It.IsAny<string>(), It.IsAny<AccountUpdateOptions>(), It.IsAny<RequestOptions>(), It.IsAny<CancellationToken>())
                )
                .ReturnsAsync(account)
                .Verifiable();

            var service = this.StripeService();

            // Act
            await service.VerifyAccountAsync(
                StripeBuilder.CreateAccountId(),
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
            _mockExternalAccountService
                .Setup(
                    mock => mock.CreateAsync(
                        It.IsAny<string>(),
                        It.IsAny<ExternalAccountCreateOptions>(),
                        It.IsAny<RequestOptions>(),
                        It.IsAny<CancellationToken>()
                    )
                )
                .ReturnsAsync(StripeBuilder.CreateBankAccount)
                .Verifiable();

            var service = this.StripeService();

            // Act
            await service.CreateBankAccountAsync(StripeBuilder.CreateAccountId(), StripeBuilder.CreateSourceToken());

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

            _mockExternalAccountService
                .Setup(mock => mock.DeleteAsync(It.IsAny<string>(), It.IsAny<string>(), It.IsAny<RequestOptions>(), It.IsAny<CancellationToken>()))
                .ReturnsAsync(StripeBuilder.CreateBankAccount)
                .Verifiable();

            var service = this.StripeService();

            // Act
            await service.DeleteBankAccountAsync(StripeBuilder.CreateAccountId(), StripeBuilder.CreateBankAccountId());

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
            _mockCardService
                .Setup(mock => mock.ListAsync(It.IsAny<string>(), It.IsAny<CardListOptions>(), It.IsAny<RequestOptions>(), It.IsAny<CancellationToken>()))
                .ReturnsAsync(StripeBuilder.CreateCards())
                .Verifiable();

            var service = this.StripeService();

            // Act
            await service.GetCardsAsync(StripeBuilder.CreateCustomerId());

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
            _mockCardService
                .Setup(mock => mock.CreateAsync(It.IsAny<string>(), It.IsAny<CardCreateOptions>(), It.IsAny<RequestOptions>(), It.IsAny<CancellationToken>()))
                .ReturnsAsync(StripeBuilder.CreateCard)
                .Verifiable();

            var service = this.StripeService();

            // Act
            await service.CreateCardAsync(StripeBuilder.CreateCustomerId(), StripeBuilder.CreateSourceToken());

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
            _mockCardService.Setup(mock => mock.DeleteAsync(It.IsAny<string>(), It.IsAny<string>(), It.IsAny<RequestOptions>(), It.IsAny<CancellationToken>()))
                .ReturnsAsync(StripeBuilder.CreateCard)
                .Verifiable();

            var service = this.StripeService();

            // Act
            await service.DeleteCardAsync(StripeBuilder.CreateCustomerId(), StripeBuilder.CreateCardId());

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
            _mockCustomerService.Setup(mock => mock.CreateAsync(It.IsAny<CustomerCreateOptions>(), It.IsAny<RequestOptions>(), It.IsAny<CancellationToken>()))
                .ReturnsAsync(StripeBuilder.CreateCustomer)
                .Verifiable();

            var service = this.StripeService();

            // Act
            await service.CreateCustomerAsync(Guid.NewGuid(), StripeBuilder.CreateAccountId(), StripeBuilder.CreateCustomer().Email);

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
            _mockCustomerService
                .Setup(
                    mock => mock.UpdateAsync(It.IsAny<string>(), It.IsAny<CustomerUpdateOptions>(), It.IsAny<RequestOptions>(), It.IsAny<CancellationToken>())
                )
                .ReturnsAsync(StripeBuilder.CreateCustomer)
                .Verifiable();

            var service = this.StripeService();

            // Act
            await service.UpdateCardDefaultAsync(StripeBuilder.CreateCustomerId(), StripeBuilder.CreateCardId());

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
            _mockInvoiceItemService
                .Setup(mock => mock.CreateAsync(It.IsAny<InvoiceItemCreateOptions>(), It.IsAny<RequestOptions>(), It.IsAny<CancellationToken>()))
                .ReturnsAsync(StripeBuilder.CreateInvoiceItem)
                .Verifiable();

            _mockInvoiceService.Setup(mock => mock.CreateAsync(It.IsAny<InvoiceCreateOptions>(), It.IsAny<RequestOptions>(), It.IsAny<CancellationToken>()))
                .ReturnsAsync(StripeBuilder.CreateInvoice)
                .Verifiable();

            var service = this.StripeService();

            // Act
            await service.CreateInvoiceAsync(StripeBuilder.CreateCustomerId(), 1000, Guid.NewGuid(), string.Empty);

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
            _mockTransferService.Setup(mock => mock.CreateAsync(It.IsAny<TransferCreateOptions>(), It.IsAny<RequestOptions>(), It.IsAny<CancellationToken>()))
                .ReturnsAsync(StripeBuilder.CreateTransfer)
                .Verifiable();

            var service = this.StripeService();

            // Act
            await service.CreateTransferAsync(StripeBuilder.CreateAccountId(), 1000, Guid.NewGuid(), string.Empty);

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
