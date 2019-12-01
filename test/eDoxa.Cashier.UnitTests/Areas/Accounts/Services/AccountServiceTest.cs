// Filename: AccountServiceTest.cs
// Date Created: 2019-11-20
// 
// ================================================
// Copyright © 2019, eDoxa. All rights reserved.

using System;
using System.Collections.Generic;
using System.Collections.Immutable;
using System.Threading;
using System.Threading.Tasks;

using eDoxa.Cashier.Api.Areas.Accounts.Services;
using eDoxa.Cashier.Api.Areas.Accounts.Services.Abstractions;
using eDoxa.Cashier.Api.IntegrationEvents;
using eDoxa.Cashier.Domain.AggregateModels;
using eDoxa.Cashier.Domain.AggregateModels.AccountAggregate;
using eDoxa.Cashier.Domain.AggregateModels.TransactionAggregate;
using eDoxa.Cashier.Domain.Repositories;
using eDoxa.Cashier.TestHelper;
using eDoxa.Cashier.TestHelper.Fixtures;
using eDoxa.Seedwork.Domain;
using eDoxa.Seedwork.Domain.Misc;
using eDoxa.ServiceBus.Abstractions;

using FluentAssertions;

using FluentValidation.Results;

using Moq;

using Xunit;

namespace eDoxa.Cashier.UnitTests.Areas.Accounts.Services
{
    public sealed class AccountServiceTest : UnitTest
    {
        public AccountServiceTest(TestDataFixture testData, TestMapperFixture testMapper) : base(testData, testMapper)
        {
        }

        [Fact]
        public async Task CreateAccountAsync_ShouldBeOfTypeValidationResult()
        {
            // Arrange
            var mockAccountRepository = new Mock<IAccountRepository>();
            var mockBundlesService = new Mock<IBundlesService>();
            var mockServiceBusPublisher = new Mock<IServiceBusPublisher>();

            var userId = new UserId();

            mockAccountRepository.Setup(accountRepository => accountRepository.Create(It.IsAny<Account>())).Verifiable();

            mockAccountRepository.Setup(accountRepository => accountRepository.CommitAsync(It.IsAny<CancellationToken>()))
                .Returns(Task.CompletedTask)
                .Verifiable();

            var service = new AccountService(mockAccountRepository.Object, mockBundlesService.Object, mockServiceBusPublisher.Object);

            // Act
            await service.CreateAccountAsync(userId);

            // Assert
            mockAccountRepository.Verify(accountRepository => accountRepository.Create(It.IsAny<Account>()), Times.Once);

            mockAccountRepository.Verify(accountRepository => accountRepository.CommitAsync(It.IsAny<CancellationToken>()), Times.Once);
        }

        [Fact]
        public async Task CreateMoneyTransactionAsync_WithTypeCharge_ShouldBeOfTypeValidationResult()
        {
            // Arrange
            var mockAccountRepository = new Mock<IAccountRepository>();
            var mockBundlesService = new Mock<IBundlesService>();
            var mockServiceBusPublisher = new Mock<IServiceBusPublisher>();

            var account = new Account(new UserId());

            var transaction = new Transaction(
                Money.OneHundred,
                new TransactionDescription("test"),
                TransactionType.Deposit,
                new UtcNowDateTimeProvider());

            transaction.MarkAsSucceded();

            account.CreateTransaction(transaction);

            var moneyAccount = new MoneyAccount(account);

            mockAccountRepository.Setup(accountRepository => accountRepository.CommitAsync(It.IsAny<CancellationToken>()))
                .Returns(Task.CompletedTask)
                .Verifiable();

            var service = new AccountService(mockAccountRepository.Object, mockBundlesService.Object, mockServiceBusPublisher.Object);

            // Act
            var result = await service.CreateTransactionAsync(
                moneyAccount,
                Money.Twenty,
                new TransactionId(),
                TransactionType.Charge);

            // Assert
            result.Should().BeOfType<ValidationResult>();

            mockAccountRepository.Verify(accountRepository => accountRepository.CommitAsync(It.IsAny<CancellationToken>()), Times.Once);
        }

        [Fact]
        public async Task CreateMoneyTransactionAsync_WithTypeChargeWithEmptyFunds_ShouldBeOfTypeValidationResultWithErrors()
        {
            // Arrange
            var mockAccountRepository = new Mock<IAccountRepository>();
            var mockBundlesService = new Mock<IBundlesService>();
            var mockServiceBusPublisher = new Mock<IServiceBusPublisher>();

            var account = new Account(new UserId());

            var moneyAccount = new MoneyAccount(account);

            var service = new AccountService(mockAccountRepository.Object, mockBundlesService.Object, mockServiceBusPublisher.Object);

            // Act
            var result = await service.CreateTransactionAsync(
                moneyAccount,
                Money.OneHundred,
                new TransactionId(),
                TransactionType.Charge);

            // Assert
            result.Should().BeOfType<ValidationResult>();
            result.Errors.Should().NotBeEmpty();
        }

        [Fact]
        public async Task CreateTokenTransactionAsync_WithTypeCharge_ShouldBeOfTypeValidationResult()
        {
            // Arrange
            var mockAccountRepository = new Mock<IAccountRepository>();
            var mockBundlesService = new Mock<IBundlesService>();
            var mockServiceBusPublisher = new Mock<IServiceBusPublisher>();

            var account = new Account(new UserId());

            var transaction = new Transaction(
                Token.TwoHundredFiftyThousand,
                new TransactionDescription("test"),
                TransactionType.Deposit,
                new UtcNowDateTimeProvider());

            transaction.MarkAsSucceded();

            account.CreateTransaction(transaction);

            var tokenAccount = new TokenAccount(account);

            mockAccountRepository.Setup(accountRepository => accountRepository.CommitAsync(It.IsAny<CancellationToken>()))
                .Returns(Task.CompletedTask)
                .Verifiable();

            var service = new AccountService(mockAccountRepository.Object, mockBundlesService.Object, mockServiceBusPublisher.Object);

            // Act
            var result = await service.CreateTransactionAsync(
                tokenAccount,
                Token.FiftyThousand,
                new TransactionId(),
                TransactionType.Charge);

            // Assert
            result.Should().BeOfType<ValidationResult>();

            mockAccountRepository.Verify(accountRepository => accountRepository.CommitAsync(It.IsAny<CancellationToken>()), Times.Once);
        }

        [Fact]
        public async Task CreateTokenTransactionAsync_WithTypeChargeWithEmptyFunds_ShouldBeOfTypeValidationResultWithErrors()
        {
            // Arrange
            var mockAccountRepository = new Mock<IAccountRepository>();
            var mockBundlesService = new Mock<IBundlesService>();
            var mockServiceBusPublisher = new Mock<IServiceBusPublisher>();

            var account = new Account(new UserId());

            var tokenAccount = new TokenAccount(account);

            var service = new AccountService(mockAccountRepository.Object, mockBundlesService.Object, mockServiceBusPublisher.Object);

            // Act
            var result = await service.CreateTransactionAsync(
                tokenAccount,
                Token.FiftyThousand,
                new TransactionId(),
                TransactionType.Charge);

            // Assert
            result.Should().BeOfType<ValidationResult>();
            result.Errors.Should().NotBeEmpty();
        }

        [Fact]
        public async Task CreateTransactionAsync_WithCurrencyAll_ShouldBeOfTypeValidationResultWithErrors()
        {
            // Arrange
            var mockAccountRepository = new Mock<IAccountRepository>();
            var mockBundlesService = new Mock<IBundlesService>();
            var mockServiceBusPublisher = new Mock<IServiceBusPublisher>();

            var account = new Account(new UserId());

            var service = new AccountService(mockAccountRepository.Object, mockBundlesService.Object, mockServiceBusPublisher.Object);

            // Act
            var result = await service.CreateTransactionAsync(
                account,
                20,
                Currency.All,
                new TransactionId(),
                TransactionType.Deposit);

            // Assert
            result.Should().BeOfType<ValidationResult>();
            result.Errors.Should().NotBeEmpty();
        }

        [Fact]
        public async Task CreateTransactionAsync_WithCurrencyMoney_ShouldBeOfTypeValidationResult()
        {
            // Arrange
            var mockAccountRepository = new Mock<IAccountRepository>();
            var mockBundlesService = new Mock<IBundlesService>();
            var mockServiceBusPublisher = new Mock<IServiceBusPublisher>();

            var account = new Account(new UserId());

            var service = new AccountService(mockAccountRepository.Object, mockBundlesService.Object, mockServiceBusPublisher.Object);

            // Act
            var result = await service.CreateTransactionAsync(
                account,
                20,
                Currency.Money,
                new TransactionId(),
                TransactionType.Deposit);

            // Assert
            result.Should().BeOfType<ValidationResult>();
        }

        [Fact]
        public async Task CreateTransactionAsync_WithCurrencyToken_ShouldBeOfTypeValidationResult()
        {
            // Arrange
            var mockAccountRepository = new Mock<IAccountRepository>();
            var mockBundlesService = new Mock<IBundlesService>();
            var mockServiceBusPublisher = new Mock<IServiceBusPublisher>();

            var account = new Account(new UserId());

            var service = new AccountService(mockAccountRepository.Object, mockBundlesService.Object, mockServiceBusPublisher.Object);

            // Act
            var result = await service.CreateTransactionAsync(
                account,
                20,
                Currency.Token,
                new TransactionId(),
                TransactionType.Deposit);

            // Assert
            result.Should().BeOfType<ValidationResult>();
        }

        [Fact] //TODO: Validation with TryGetValue
        public async Task CreateTransactionAsync_WithWrongAmount_ShouldBeOfTypeValidationResultWithErrors()
        {
            // Arrange
            var mockAccountRepository = new Mock<IAccountRepository>();
            var mockBundlesService = new Mock<IBundlesService>();
            var mockServiceBusPublisher = new Mock<IServiceBusPublisher>();

            var account = new Account(new UserId());
            var service = new AccountService(mockAccountRepository.Object, mockBundlesService.Object, mockServiceBusPublisher.Object);

            // Act
            var result = await service.CreateTransactionAsync(
                account,
                0,
                Currency.All,
                new TransactionId(),
                TransactionType.Deposit);

            // Assert
            result.Should().BeOfType<ValidationResult>();
            result.Errors.Should().NotBeEmpty();
        }

        [Fact]
        public async Task DepositAsync_WithCurrencyMoney_ShouldBeOfTypeValidationResult()
        {
            // Arrange
            var mockAccountRepository = new Mock<IAccountRepository>();
            var mockBundlesService = new Mock<IBundlesService>();
            var mockServiceBusPublisher = new Mock<IServiceBusPublisher>();

            var account = new Account(new UserId());

            mockAccountRepository.Setup(accountRepository => accountRepository.CommitAsync(It.IsAny<CancellationToken>()))
                .Returns(Task.CompletedTask)
                .Verifiable();

            mockServiceBusPublisher.Setup(serviceBus => serviceBus.PublishAsync(It.IsAny<UserAccountDepositIntegrationEvent>()))
                .Returns(Task.CompletedTask)
                .Verifiable();

            var bundle = new List<Bundle>
            {
                new Bundle(new Money(100), new Price(new Money(100)))
            };

            mockBundlesService.Setup(bundleService => bundleService.FetchDepositMoneyBundles()).Returns(bundle.ToImmutableHashSet()).Verifiable();

            var service = new AccountService(mockAccountRepository.Object, mockBundlesService.Object, mockServiceBusPublisher.Object);

            // Act
            var result = await service.DepositAsync(account, Money.OneHundred, "gabriel@edoxa.gg");

            // Assert
            result.Should().BeOfType<ValidationResult>();

            mockBundlesService.Verify(bundleService => bundleService.FetchDepositMoneyBundles(), Times.Once());

            mockAccountRepository.Verify(accountRepository => accountRepository.CommitAsync(It.IsAny<CancellationToken>()), Times.Once());

            mockServiceBusPublisher.Verify(serviceBus => serviceBus.PublishAsync(It.IsAny<UserAccountDepositIntegrationEvent>()), Times.Once());
        }

        [Fact]
        public async Task DepositAsync_WithCurrencyMoneyWithDepositPresent_ShouldBeOfTypeValidationResultWithErrors()
        {
            // Arrange
            var mockAccountRepository = new Mock<IAccountRepository>();
            var mockBundlesService = new Mock<IBundlesService>();
            var mockServiceBusPublisher = new Mock<IServiceBusPublisher>();

            var account = new Account(new UserId());

            var transaction = new Transaction(
                Money.OneHundred,
                new TransactionDescription("test"),
                TransactionType.Deposit,
                new UtcNowDateTimeProvider());

            transaction.MarkAsSucceded();

            account.CreateTransaction(transaction);

            var bundle = new List<Bundle>
            {
                new Bundle(new Money(100), new Price(new Money(100)))
            };

            mockBundlesService.Setup(bundleService => bundleService.FetchDepositMoneyBundles()).Returns(bundle.ToImmutableHashSet()).Verifiable();

            var service = new AccountService(mockAccountRepository.Object, mockBundlesService.Object, mockServiceBusPublisher.Object);

            // Act
            var result = await service.DepositAsync(account, Money.OneHundred, "gabriel@edoxa.gg");

            // Assert
            result.Should().BeOfType<ValidationResult>();
            result.Errors.Should().NotBeEmpty();

            mockBundlesService.Verify(bundleService => bundleService.FetchDepositMoneyBundles(), Times.Once());
        }

        [Fact]
        public async Task DepositAsync_WithCurrencyMoneyWithWrongBundle_ShouldBeOfTypeValidationResultWithErrors()
        {
            // Arrange
            var mockAccountRepository = new Mock<IAccountRepository>();
            var mockBundlesService = new Mock<IBundlesService>();
            var mockServiceBusPublisher = new Mock<IServiceBusPublisher>();

            var account = new Account(new UserId());

            var bundle = new List<Bundle>
            {
                new Bundle(new Money(20), new Price(new Money(100)))
            };

            mockBundlesService.Setup(bundleService => bundleService.FetchDepositMoneyBundles()).Returns(bundle.ToImmutableHashSet()).Verifiable();
            var service = new AccountService(mockAccountRepository.Object, mockBundlesService.Object, mockServiceBusPublisher.Object);

            // Act
            var result = await service.DepositAsync(account, Money.OneHundred, "gabriel@edoxa.gg");

            // Assert
            result.Should().BeOfType<ValidationResult>();

            result.Errors.Should().NotBeEmpty();

            mockBundlesService.Verify(bundleService => bundleService.FetchDepositMoneyBundles(), Times.Once());
        }

        [Fact]
        public void DepositAsync_WithCurrencyNull_ShouldThrowInvalidOperationException()
        {
            // Arrange
            var mockAccountRepository = new Mock<IAccountRepository>();
            var mockBundlesService = new Mock<IBundlesService>();
            var mockServiceBusPublisher = new Mock<IServiceBusPublisher>();

            var account = new Account(new UserId());

            var service = new AccountService(mockAccountRepository.Object, mockBundlesService.Object, mockServiceBusPublisher.Object);

            // Act
            var action = new Func<Task<ValidationResult>>(async () => await service.DepositAsync(account, null, "gabriel@edoxa.gg"));

            // Assert
            action.Should().Throw<InvalidOperationException>();
        }

        [Fact]
        public async Task DepositAsync_WithCurrencyToken_ShouldBeOfTypeValidationResult()
        {
            // Arrange
            var mockAccountRepository = new Mock<IAccountRepository>();
            var mockBundlesService = new Mock<IBundlesService>();
            var mockServiceBusPublisher = new Mock<IServiceBusPublisher>();

            var account = new Account(new UserId());

            mockAccountRepository.Setup(accountRepository => accountRepository.CommitAsync(It.IsAny<CancellationToken>()))
                .Returns(Task.CompletedTask)
                .Verifiable();

            mockServiceBusPublisher.Setup(serviceBus => serviceBus.PublishAsync(It.IsAny<UserAccountDepositIntegrationEvent>()))
                .Returns(Task.CompletedTask)
                .Verifiable();

            var bundle = new List<Bundle>
            {
                new Bundle(new Token(50000), new Price(new Money(100)))
            };

            mockBundlesService.Setup(bundleService => bundleService.FetchDepositTokenBundles()).Returns(bundle.ToImmutableHashSet()).Verifiable();

            var service = new AccountService(mockAccountRepository.Object, mockBundlesService.Object, mockServiceBusPublisher.Object);

            // Act
            var result = await service.DepositAsync(account, Token.FiftyThousand, "gabriel@edoxa.gg");

            // Assert
            result.Should().BeOfType<ValidationResult>();
            mockBundlesService.Verify(bundleService => bundleService.FetchDepositTokenBundles(), Times.Once());
            mockAccountRepository.Verify(accountRepository => accountRepository.CommitAsync(It.IsAny<CancellationToken>()), Times.Once());
            mockServiceBusPublisher.Verify(serviceBus => serviceBus.PublishAsync(It.IsAny<UserAccountDepositIntegrationEvent>()), Times.Once());
        }

        [Fact]
        public async Task DepositAsync_WithCurrencyTokenWithDepositPresent_ShouldBeOfTypeValidationResultWithErrors()
        {
            // Arrange
            var mockAccountRepository = new Mock<IAccountRepository>();
            var mockBundlesService = new Mock<IBundlesService>();
            var mockServiceBusPublisher = new Mock<IServiceBusPublisher>();

            var account = new Account(new UserId());

            var transaction = new Transaction(
                Token.FiftyThousand,
                new TransactionDescription("test"),
                TransactionType.Deposit,
                new UtcNowDateTimeProvider());

            transaction.MarkAsSucceded();
            account.CreateTransaction(transaction);

            var bundle = new List<Bundle>
            {
                new Bundle(new Token(50000), new Price(new Money(100)))
            };

            mockBundlesService.Setup(bundleService => bundleService.FetchDepositTokenBundles()).Returns(bundle.ToImmutableHashSet()).Verifiable();

            var service = new AccountService(mockAccountRepository.Object, mockBundlesService.Object, mockServiceBusPublisher.Object);

            // Act
            var result = await service.DepositAsync(account, Token.FiftyThousand, "gabriel@edoxa.gg");

            // Assert
            result.Should().BeOfType<ValidationResult>();
            result.Errors.Should().NotBeEmpty();

            mockBundlesService.Verify(bundleService => bundleService.FetchDepositTokenBundles(), Times.Once());
        }

        [Fact]
        public async Task DepositAsync_WithCurrencyTokenWithWrongBundle_ShouldBeOfTypeValidationResultWithErrors()
        {
            // Arrange
            var mockAccountRepository = new Mock<IAccountRepository>();
            var mockBundlesService = new Mock<IBundlesService>();
            var mockServiceBusPublisher = new Mock<IServiceBusPublisher>();

            var account = new Account(new UserId());

            var bundle = new List<Bundle>
            {
                new Bundle(new Token(1000), new Price(new Money(100)))
            };

            mockBundlesService.Setup(bundleService => bundleService.FetchDepositTokenBundles()).Returns(bundle.ToImmutableHashSet()).Verifiable();

            var service = new AccountService(mockAccountRepository.Object, mockBundlesService.Object, mockServiceBusPublisher.Object);

            // Act
            var result = await service.DepositAsync(account, Token.FiftyThousand, "gabriel@edoxa.gg");

            // Assert
            result.Should().BeOfType<ValidationResult>();
            result.Errors.Should().NotBeEmpty();

            mockBundlesService.Verify(bundleService => bundleService.FetchDepositTokenBundles(), Times.Once());
        }

        [Fact]
        public async Task FindUserAccountAsync_ShouldBeOfTypeValidationResult()
        {
            // Arrange
            var mockAccountRepository = new Mock<IAccountRepository>();
            var mockBundlesService = new Mock<IBundlesService>();
            var mockServiceBusPublisher = new Mock<IServiceBusPublisher>();

            var userId = new UserId();
            var account = new Account(userId);

            mockAccountRepository.Setup(accountRepository => accountRepository.FindUserAccountAsync(It.IsAny<UserId>())).ReturnsAsync(account).Verifiable();

            var service = new AccountService(mockAccountRepository.Object, mockBundlesService.Object, mockServiceBusPublisher.Object);

            // Act
            var result = await service.FindUserAccountAsync(userId);

            // Assert
            result.Should().BeOfType<Account>();

            mockAccountRepository.Verify(accountRepository => accountRepository.FindUserAccountAsync(It.IsAny<UserId>()), Times.Once);
        }

        [Fact]
        public async Task WithdrawalAsync_WithCurrencyMoney_ShouldBeOfTypeValidationResult()
        {
            // Arrange
            var mockAccountRepository = new Mock<IAccountRepository>();
            var mockBundlesService = new Mock<IBundlesService>();
            var mockServiceBusPublisher = new Mock<IServiceBusPublisher>();

            var account = new Account(new UserId());

            var transaction = new Transaction(
                Money.OneHundred,
                new TransactionDescription("test"),
                TransactionType.Deposit,
                new UtcNowDateTimeProvider());

            transaction.MarkAsSucceded();

            account.CreateTransaction(transaction);

            mockAccountRepository.Setup(accountRepository => accountRepository.CommitAsync(It.IsAny<CancellationToken>()))
                .Returns(Task.CompletedTask)
                .Verifiable();

            mockServiceBusPublisher.Setup(serviceBus => serviceBus.PublishAsync(It.IsAny<UserAccountWithdrawalIntegrationEvent>()))
                .Returns(Task.CompletedTask)
                .Verifiable();

            var bundle = new List<Bundle>
            {
                new Bundle(new Money(20), new Price(new Money(100)))
            };

            mockBundlesService.Setup(bundleService => bundleService.FetchWithdrawalMoneyBundles()).Returns(bundle.ToImmutableHashSet()).Verifiable();

            var service = new AccountService(mockAccountRepository.Object, mockBundlesService.Object, mockServiceBusPublisher.Object);

            // Act
            var result = await service.WithdrawalAsync(account, Money.Twenty, "gabriel@edoxa.gg");

            // Assert
            result.Should().BeOfType<ValidationResult>();

            mockBundlesService.Verify(bundleService => bundleService.FetchWithdrawalMoneyBundles(), Times.Once());

            mockAccountRepository.Verify(accountRepository => accountRepository.CommitAsync(It.IsAny<CancellationToken>()), Times.Once());

            mockServiceBusPublisher.Verify(serviceBus => serviceBus.PublishAsync(It.IsAny<UserAccountWithdrawalIntegrationEvent>()), Times.Once());
        }

        [Fact]
        public async Task WithdrawalAsync_WithCurrencyToken_ShouldBeOfTypeValidationResultWithErrors()
        {
            // Arrange
            var mockAccountRepository = new Mock<IAccountRepository>();

            var mockBundlesService = new Mock<IBundlesService>();

            var mockServiceBusPublisher = new Mock<IServiceBusPublisher>();

            var account = new Account(new UserId());

            var service = new AccountService(mockAccountRepository.Object, mockBundlesService.Object, mockServiceBusPublisher.Object);

            // Act
            var result = await service.WithdrawalAsync(account, Token.FiftyThousand, "gabriel@edoxa.gg");

            // Assert
            result.Should().BeOfType<ValidationResult>();
            result.Errors.Should().NotBeEmpty();
        }

        [Fact]
        public async Task WithdrawalAsync_WithEmptyAccountBalance_ShouldBeOfTypeValidationResultWithErrors()
        {
            // Arrange
            var mockAccountRepository = new Mock<IAccountRepository>();

            var mockBundlesService = new Mock<IBundlesService>();

            var mockServiceBusPublisher = new Mock<IServiceBusPublisher>();

            var account = new Account(new UserId());

            var bundle = new List<Bundle>
            {
                new Bundle(new Money(20), new Price(new Money(100)))
            };

            mockBundlesService.Setup(bundleService => bundleService.FetchWithdrawalMoneyBundles()).Returns(bundle.ToImmutableHashSet()).Verifiable();

            var service = new AccountService(mockAccountRepository.Object, mockBundlesService.Object, mockServiceBusPublisher.Object);

            // Act
            var result = await service.WithdrawalAsync(account, Money.Twenty, "gabriel@edoxa.gg");

            // Assert
            result.Should().BeOfType<ValidationResult>();
            result.Errors.Should().NotBeEmpty();

            mockBundlesService.Verify(bundleService => bundleService.FetchWithdrawalMoneyBundles(), Times.Once());
        }

        [Fact]
        public async Task WithdrawalAsync_WithWithdrawlPresent_ShouldBeOfTypeValidationResultWithErrors()
        {
            // Arrange
            var mockAccountRepository = new Mock<IAccountRepository>();
            var mockBundlesService = new Mock<IBundlesService>();
            var mockServiceBusPublisher = new Mock<IServiceBusPublisher>();

            var account = new Account(new UserId());

            var transaction = new Transaction(
                Money.OneHundred,
                new TransactionDescription("test"),
                TransactionType.Withdrawal,
                new UtcNowDateTimeProvider());

            transaction.MarkAsSucceded();

            account.CreateTransaction(transaction);

            var bundle = new List<Bundle>
            {
                new Bundle(new Money(20), new Price(new Money(100)))
            };

            mockBundlesService.Setup(bundleService => bundleService.FetchWithdrawalMoneyBundles()).Returns(bundle.ToImmutableHashSet()).Verifiable();

            var service = new AccountService(mockAccountRepository.Object, mockBundlesService.Object, mockServiceBusPublisher.Object);

            // Act
            var result = await service.WithdrawalAsync(account, Money.Twenty, "gabriel@edoxa.gg");

            // Assert
            result.Should().BeOfType<ValidationResult>();
            result.Errors.Should().NotBeEmpty();

            mockBundlesService.Verify(bundleService => bundleService.FetchWithdrawalMoneyBundles(), Times.Once());
        }

        [Fact]
        public async Task WithdrawalAsync_WithWrongBundle_ShouldBeOfTypeValidationResultWithErrors()
        {
            // Arrange
            var mockAccountRepository = new Mock<IAccountRepository>();
            var mockBundlesService = new Mock<IBundlesService>();
            var mockServiceBusPublisher = new Mock<IServiceBusPublisher>();

            var account = new Account(new UserId());

            var bundle = new List<Bundle>
            {
                new Bundle(new Money(100), new Price(new Money(100)))
            };

            mockBundlesService.Setup(bundleService => bundleService.FetchWithdrawalMoneyBundles()).Returns(bundle.ToImmutableHashSet()).Verifiable();

            var service = new AccountService(mockAccountRepository.Object, mockBundlesService.Object, mockServiceBusPublisher.Object);

            // Act
            var result = await service.WithdrawalAsync(account, Money.Twenty, "gabriel@edoxa.gg");

            // Assert
            result.Should().BeOfType<ValidationResult>();
            result.Errors.Should().NotBeEmpty();

            mockBundlesService.Verify(bundleService => bundleService.FetchWithdrawalMoneyBundles(), Times.Once());
        }
    }
}
