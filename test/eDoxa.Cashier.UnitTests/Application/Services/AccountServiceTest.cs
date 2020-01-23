// Filename: AccountServiceTest.cs
// Date Created: 2019-12-26
// 
// ================================================
// Copyright © 2020, eDoxa. All rights reserved.

using System.Threading;
using System.Threading.Tasks;

using eDoxa.Cashier.Api.Application.Services;
using eDoxa.Cashier.Domain.AggregateModels;
using eDoxa.Cashier.Domain.AggregateModels.AccountAggregate;
using eDoxa.Cashier.Domain.Repositories;
using eDoxa.Cashier.TestHelper;
using eDoxa.Cashier.TestHelper.Fixtures;
using eDoxa.Grpc.Protos.Cashier.Options;
using eDoxa.Seedwork.Domain;
using eDoxa.Seedwork.Domain.Misc;

using FluentAssertions;

using Microsoft.Extensions.Options;

using Moq;

using Xunit;

namespace eDoxa.Cashier.UnitTests.Application.Services
{
    public sealed class AccountServiceTest : UnitTest
    {
        public AccountServiceTest(TestDataFixture testData, TestMapperFixture testMapper, TestValidator testValidator) : base(
            testData,
            testMapper,
            testValidator)
        {
        }

        [Fact]
        public async Task CreateAccountAsync_ShouldBeOfTypeValidationResult()
        {
            // Arrange
            var mockAccountRepository = new Mock<IAccountRepository>();
            var mockOptionsSnapshot = new Mock<IOptionsSnapshot<CashierApiOptions>>();

            var userId = new UserId();

            mockAccountRepository.Setup(accountRepository => accountRepository.Create(It.IsAny<Account>())).Verifiable();

            mockAccountRepository.Setup(accountRepository => accountRepository.CommitAsync(true, It.IsAny<CancellationToken>()))
                .Returns(Task.CompletedTask)
                .Verifiable();

            mockOptionsSnapshot.Setup(x => x.Value).Returns(new CashierApiOptions());

            var service = new AccountService(mockAccountRepository.Object, mockOptionsSnapshot.Object);

            // Act
            await service.CreateAccountAsync(userId);

            // Assert
            mockAccountRepository.Verify(accountRepository => accountRepository.Create(It.IsAny<Account>()), Times.Once);

            mockAccountRepository.Verify(accountRepository => accountRepository.CommitAsync(true, It.IsAny<CancellationToken>()), Times.Once);
        }

        [Fact]
        public async Task CreateMoneyTransactionAsync_WithTypeCharge_ShouldBeOfTypeValidationResult()
        {
            // Arrange
            var mockAccountRepository = new Mock<IAccountRepository>();
            var mockOptionsSnapshot = new Mock<IOptionsSnapshot<CashierApiOptions>>();

            var account = new Account(new UserId());
            var moneyAccount = new MoneyAccountDecorator(account);
            var transaction = moneyAccount.Deposit(Money.OneHundred);

            transaction.MarkAsSucceeded();

            mockAccountRepository.Setup(accountRepository => accountRepository.CommitAsync(true, It.IsAny<CancellationToken>()))
                .Returns(Task.CompletedTask)
                .Verifiable();

            mockOptionsSnapshot.Setup(x => x.Value).Returns(new CashierApiOptions());

            var service = new AccountService(mockAccountRepository.Object, mockOptionsSnapshot.Object);

            // Act
            var result = await service.CreateTransactionAsync(
                moneyAccount,
                Money.Twenty.Amount,
                Currency.Money,
                TransactionType.Charge);

            // Assert
            result.Should().BeOfType<DomainValidationResult>();

            mockAccountRepository.Verify(accountRepository => accountRepository.CommitAsync(true, It.IsAny<CancellationToken>()), Times.Once);
        }

        [Fact]
        public async Task CreateMoneyTransactionAsync_WithTypeChargeWithEmptyFunds_ShouldBeOfTypeValidationResultWithErrors()
        {
            // Arrange
            var mockAccountRepository = new Mock<IAccountRepository>();
            var mockOptionsSnapshot = new Mock<IOptionsSnapshot<CashierApiOptions>>();

            var account = new Account(new UserId());

            var service = new AccountService(mockAccountRepository.Object, mockOptionsSnapshot.Object);

            // Act
            var result = await service.CreateTransactionAsync(
                account,
                Money.Twenty.Amount,
                Currency.Money,
                TransactionType.Charge);

            // Assert
            result.Should().BeOfType<DomainValidationResult>();
            result.Errors.Should().NotBeEmpty();
        }

        [Fact]
        public async Task CreateTokenTransactionAsync_WithTypeCharge_ShouldBeOfTypeValidationResult()
        {
            // Arrange
            var mockAccountRepository = new Mock<IAccountRepository>();
            var mockOptionsSnapshot = new Mock<IOptionsSnapshot<CashierApiOptions>>();

            var account = new Account(new UserId());
            var tokenAccount = new TokenAccountDecorator(account);
            var transaction = tokenAccount.Deposit(Token.TwoHundredFiftyThousand);

            transaction.MarkAsSucceeded();

            mockAccountRepository.Setup(accountRepository => accountRepository.CommitAsync(true, It.IsAny<CancellationToken>()))
                .Returns(Task.CompletedTask)
                .Verifiable();

            mockOptionsSnapshot.Setup(x => x.Value).Returns(new CashierApiOptions());

            var service = new AccountService(mockAccountRepository.Object, mockOptionsSnapshot.Object);

            // Act
            var result = await service.CreateTransactionAsync(
                tokenAccount,
                Token.FiftyThousand.Amount,
                Currency.Token,
                TransactionType.Charge);

            // Assert
            result.Should().BeOfType<DomainValidationResult>();

            mockAccountRepository.Verify(accountRepository => accountRepository.CommitAsync(true, It.IsAny<CancellationToken>()), Times.Once);
        }

        [Fact]
        public async Task CreateTokenTransactionAsync_WithTypeChargeWithEmptyFunds_ShouldBeOfTypeValidationResultWithErrors()
        {
            // Arrange
            var mockAccountRepository = new Mock<IAccountRepository>();
            var mockOptionsSnapshot = new Mock<IOptionsSnapshot<CashierApiOptions>>();

            var account = new Account(new UserId());

            mockOptionsSnapshot.Setup(x => x.Value).Returns(new CashierApiOptions());

            var service = new AccountService(mockAccountRepository.Object, mockOptionsSnapshot.Object);

            // Act
            var result = await service.CreateTransactionAsync(
                account,
                Token.FiftyThousand.Amount,
                Currency.Token,
                TransactionType.Charge);

            // Assert
            result.Should().BeOfType<DomainValidationResult>();
            result.Errors.Should().NotBeEmpty();
        }

        [Fact]
        public async Task CreateTransactionAsync_WithCurrencyAll_ShouldBeOfTypeValidationResultWithErrors()
        {
            // Arrange
            var mockAccountRepository = new Mock<IAccountRepository>();
            var mockOptionsSnapshot = new Mock<IOptionsSnapshot<CashierApiOptions>>();

            var account = new Account(new UserId());

            mockOptionsSnapshot.Setup(x => x.Value).Returns(new CashierApiOptions());

            var service = new AccountService(mockAccountRepository.Object, mockOptionsSnapshot.Object);

            // Act
            var result = await service.CreateTransactionAsync(
                account,
                20,
                Currency.All,
                TransactionType.Deposit);

            // Assert
            result.Should().BeOfType<DomainValidationResult>();
            result.Errors.Should().NotBeEmpty();
        }

        [Fact]
        public async Task CreateTransactionAsync_WithCurrencyMoney_ShouldBeOfTypeValidationResult()
        {
            // Arrange
            var mockAccountRepository = new Mock<IAccountRepository>();
            var mockOptionsSnapshot = new Mock<IOptionsSnapshot<CashierApiOptions>>();

            mockOptionsSnapshot.Setup(x => x.Value)
                .Returns(
                    new CashierApiOptions
                    {
                        Static = new CashierApiOptions.Types.StaticOptions
                        {
                            Transaction = new CashierApiOptions.Types.StaticOptions.Types.TransactionOptions()
                        }
                    });

            var account = new Account(new UserId());

            var service = new AccountService(mockAccountRepository.Object, mockOptionsSnapshot.Object);

            // Act
            var result = await service.CreateTransactionAsync(
                account,
                20,
                Currency.Money,
                TransactionType.Deposit);

            // Assert
            result.Should().BeOfType<DomainValidationResult>();
        }

        [Fact]
        public async Task CreateTransactionAsync_WithCurrencyToken_ShouldBeOfTypeValidationResult()
        {
            // Arrange
            var mockAccountRepository = new Mock<IAccountRepository>();
            var mockOptionsSnapshot = new Mock<IOptionsSnapshot<CashierApiOptions>>();

            mockOptionsSnapshot.Setup(x => x.Value)
                .Returns(
                    new CashierApiOptions
                    {
                        Static = new CashierApiOptions.Types.StaticOptions
                        {
                            Transaction = new CashierApiOptions.Types.StaticOptions.Types.TransactionOptions()
                        }
                    });

            var account = new Account(new UserId());

            var service = new AccountService(mockAccountRepository.Object, mockOptionsSnapshot.Object);

            // Act
            var result = await service.CreateTransactionAsync(
                account,
                20,
                Currency.Token,
                TransactionType.Deposit);

            // Assert
            result.Should().BeOfType<DomainValidationResult>();
        }

        [Fact]
        public async Task CreateTransactionAsync_WithWrongAmount_ShouldBeOfTypeValidationResultWithErrors()
        {
            // Arrange
            var mockAccountRepository = new Mock<IAccountRepository>();
            var mockOptionsSnapshot = new Mock<IOptionsSnapshot<CashierApiOptions>>();
            mockOptionsSnapshot.Setup(x => x.Value).Returns(new CashierApiOptions());
            var account = new Account(new UserId());

            var service = new AccountService(mockAccountRepository.Object, mockOptionsSnapshot.Object);

            // Act
            var result = await service.CreateTransactionAsync(
                account,
                0,
                Currency.All,
                TransactionType.Deposit);

            // Assert
            result.Should().BeOfType<DomainValidationResult>();

            result.Errors.Should().NotBeEmpty();
        }

        [Fact]
        public async Task DepositAsync_WithCurrencyMoney_ShouldBeOfTypeValidationResult()
        {
            // Arrange
            var mockAccountRepository = new Mock<IAccountRepository>();

            var account = new Account(new UserId());

            mockAccountRepository.Setup(accountRepository => accountRepository.CommitAsync(true, It.IsAny<CancellationToken>()))
                .Returns(Task.CompletedTask)
                .Verifiable();

            var service = new AccountService(mockAccountRepository.Object, TestValidator);

            // Act
            var result = await service.CreateTransactionAsync(
                account,
                Money.OneHundred.Amount,
                Currency.Money,
                TransactionType.Deposit);

            // Assert
            result.Should().BeOfType<DomainValidationResult>();

            mockAccountRepository.Verify(accountRepository => accountRepository.CommitAsync(true, It.IsAny<CancellationToken>()), Times.Once());
        }

        [Fact]
        public async Task DepositAsync_WithCurrencyMoneyWithDepositPresent_ShouldBeOfTypeValidationResultWithErrors()
        {
            // Arrange
            var mockAccountRepository = new Mock<IAccountRepository>();

            var account = new Account(new UserId());
            var moneyAccount = new MoneyAccountDecorator(account);
            var transaction = moneyAccount.Deposit(Money.OneHundred);

            transaction.MarkAsSucceeded();

            var service = new AccountService(mockAccountRepository.Object, TestValidator);

            // Act
            var result = await service.CreateTransactionAsync(
                moneyAccount,
                Money.OneHundred.Amount,
                Currency.Money,
                TransactionType.Deposit);

            // Assert
            result.Should().BeOfType<DomainValidationResult>();

            result.Errors.Should().NotBeEmpty();
        }

        //[Fact]
        //public async Task DepositAsync_WithCurrencyMoneyWithWrongBundle_ShouldBeOfTypeValidationResultWithErrors()
        //{
        //    // Arrange
        //    var mockAccountRepository = new Mock<IAccountRepository>();

        //    var account = new Account(new UserId());

        //    var service = new AccountService(mockAccountRepository.Object, TestValidator);

        //    // Act
        //    var result = await service.CreateTransactionAsync(
        //        account,
        //        Money.OneHundred.Amount,
        //        Currency.Money,
        //        TransactionType.Deposit);

        //    // Assert
        //    result.Should().BeOfType<DomainValidationResult>();

        //    result.Errors.Should().NotBeEmpty();
        //}

        [Fact]
        public async Task DepositAsync_WithCurrencyToken_ShouldBeOfTypeValidationResult()
        {
            // Arrange
            var mockAccountRepository = new Mock<IAccountRepository>();

            var account = new Account(new UserId());

            //var tokenAccount = new TokenAccountDecorator(account);

            //tokenAccount.Deposit(Token.OneMillion);

            mockAccountRepository.Setup(accountRepository => accountRepository.CommitAsync(true, It.IsAny<CancellationToken>()))
                .Returns(Task.CompletedTask)
                .Verifiable();

            var accountService = new AccountService(mockAccountRepository.Object, TestValidator);

            // Act
            var result = await accountService.CreateTransactionAsync(
                account,
                Token.OneMillion.Amount,
                Currency.Token,
                TransactionType.Deposit);

            // Assert
            result.Should().BeOfType<DomainValidationResult>();

            mockAccountRepository.Verify(accountRepository => accountRepository.CommitAsync(true, It.IsAny<CancellationToken>()), Times.Once());
        }

        [Fact]
        public async Task DepositAsync_WithCurrencyTokenWithDepositPresent_ShouldBeOfTypeValidationResultWithErrors()
        {
            // Arrange
            var mockAccountRepository = new Mock<IAccountRepository>();

            var account = new Account(new UserId());
            var tokenAccount = new TokenAccountDecorator(account);
            var transaction = tokenAccount.Deposit(Token.FiftyThousand);

            transaction.MarkAsSucceeded();

            var service = new AccountService(mockAccountRepository.Object, TestValidator);

            // Act
            var result = await service.CreateTransactionAsync(
                tokenAccount,
                Token.FiftyThousand.Amount,
                Currency.Token,
                TransactionType.Deposit);

            // Assert
            result.Should().BeOfType<DomainValidationResult>();

            result.Errors.Should().NotBeEmpty();
        }

        //[Fact]
        //public async Task DepositAsync_WithCurrencyTokenWithWrongBundle_ShouldBeOfTypeValidationResultWithErrors()
        //{
        //    // Arrange
        //    var mockAccountRepository = new Mock<IAccountRepository>();

        //    var account = new Account(new UserId());

        //    var service = new AccountService(mockAccountRepository.Object, TestValidator);

        //    // Act
        //    var result = await service.CreateTransactionAsync(
        //        account,
        //        Token.FiftyThousand.Amount,
        //        Currency.Token,
        //        TransactionType.Deposit);

        //    // Assert
        //    result.Should().BeOfType<DomainValidationResult>();

        //    result.Errors.Should().NotBeEmpty();
        //}

        [Fact]
        public async Task FindUserAccountAsync_ShouldBeOfTypeValidationResult()
        {
            // Arrange
            var mockAccountRepository = new Mock<IAccountRepository>();
            var mockOptionsSnapshot = new Mock<IOptionsSnapshot<CashierApiOptions>>();
            mockOptionsSnapshot.Setup(x => x.Value).Returns(new CashierApiOptions());
            var userId = new UserId();
            var account = new Account(userId);

            mockAccountRepository.Setup(accountRepository => accountRepository.FindAccountOrNullAsync(It.IsAny<UserId>())).ReturnsAsync(account).Verifiable();

            var service = new AccountService(mockAccountRepository.Object, mockOptionsSnapshot.Object);

            // Act
            var result = await service.FindAccountOrNullAsync(userId);

            // Assert
            result.Should().BeOfType<Account>();

            mockAccountRepository.Verify(accountRepository => accountRepository.FindAccountOrNullAsync(It.IsAny<UserId>()), Times.Once);
        }

        [Fact]
        public async Task WithdrawalAsync_WithCurrencyMoney_ShouldBeOfTypeValidationResult()
        {
            // Arrange
            var mockAccountRepository = new Mock<IAccountRepository>();

            var account = new Account(new UserId());
            var moneyAccount = new MoneyAccountDecorator(account);
            var transaction = moneyAccount.Deposit(Money.OneHundred);

            transaction.MarkAsSucceeded();

            mockAccountRepository.Setup(accountRepository => accountRepository.CommitAsync(true, It.IsAny<CancellationToken>()))
                .Returns(Task.CompletedTask)
                .Verifiable();

            var metadata = new TransactionMetadata
            {
                {"UserId", account.Id.ToString()},
                {"Email", "gabriel@edoxa.gg"}
            };

            var service = new AccountService(mockAccountRepository.Object, TestValidator);

            // Act
            var result = await service.CreateTransactionAsync(
                moneyAccount,
                Money.OneHundred.Amount,
                Currency.Money,
                TransactionType.Withdrawal,
                metadata);

            // Assert
            result.Should().BeOfType<DomainValidationResult>();

            mockAccountRepository.Verify(accountRepository => accountRepository.CommitAsync(true, It.IsAny<CancellationToken>()), Times.Once());
        }

        //[Fact]
        //public async Task WithdrawalAsync_WithCurrencyToken_ShouldBeOfTypeValidationResultWithErrors()
        //{
        //    // Arrange
        //    var mockAccountRepository = new Mock<IAccountRepository>();
        //    var mockOptionsSnapshot = new Mock<IOptionsSnapshot<CashierApiOptions>>();
        //    mockOptionsSnapshot.Setup(x => x.Value).Returns(new CashierApiOptions());
        //    var account = new Account(new UserId());

        //    var metadata = new TransactionMetadata
        //    {
        //        {"UserId", account.Id.ToString()},
        //        {"Email", "gabriel@edoxa.gg"}
        //    };

        //    var service = new AccountService(mockAccountRepository.Object, mockOptionsSnapshot.Object);

        //    // Act
        //    var result = await service.CreateTransactionAsync(
        //        account,
        //        Money.Twenty.Amount,
        //        Currency.Money,
        //        TransactionType.Withdrawal,
        //        metadata);

        //    // Assert
        //    result.Should().BeOfType<DomainValidationResult>();

        //    result.Errors.Should().NotBeEmpty();
        //}

        [Fact]
        public async Task WithdrawalAsync_WithEmptyAccountBalance_ShouldBeOfTypeValidationResultWithErrors()
        {
            // Arrange
            var mockAccountRepository = new Mock<IAccountRepository>();

            var account = new Account(new UserId());

            var metadata = new TransactionMetadata
            {
                {"UserId", account.Id.ToString()},
                {"Email", "gabriel@edoxa.gg"}
            };

            var service = new AccountService(mockAccountRepository.Object, TestValidator);

            // Act
            var result = await service.CreateTransactionAsync(
                account,
                Money.Twenty.Amount,
                Currency.Money,
                TransactionType.Withdrawal,
                metadata);

            // Assert
            result.Should().BeOfType<DomainValidationResult>();

            result.Errors.Should().NotBeEmpty();
        }

        //[Fact]
        //public async Task WithdrawalAsync_WithWithdrawlPresent_ShouldBeOfTypeValidationResultWithErrors()
        //{
        //    // Arrange
        //    var mockAccountRepository = new Mock<IAccountRepository>();

        //    var account = new Account(new UserId());
        //    var moneyAccount = new MoneyAccountDecorator(account);
        //    var transaction = moneyAccount.Deposit(Money.OneHundred);

        //    transaction.MarkAsSucceeded();

        //    var service = new AccountService(mockAccountRepository.Object, TestValidator);

        //    // Act
        //    var result = await service.CreateTransactionAsync(
        //        account,
        //        Money.Fifty.Amount,
        //        Currency.Money,
        //        TransactionType.Withdrawal);

        //    // Assert
        //    result.Should().BeOfType<DomainValidationResult>();

        //    result.Errors.Should().NotBeEmpty();
        //}

        [Fact]
        public async Task WithdrawalAsync_WithWrongBundle_ShouldBeOfTypeValidationResultWithErrors()
        {
            // Arrange
            var mockAccountRepository = new Mock<IAccountRepository>();

            var account = new Account(new UserId());

            var metadata = new TransactionMetadata
            {
                {"UserId", account.Id.ToString()},
                {"Email", "gabriel@edoxa.gg"}
            };

            var service = new AccountService(mockAccountRepository.Object, TestValidator);

            // Act
            var result = await service.CreateTransactionAsync(
                account,
                Money.Twenty.Amount,
                Currency.Money,
                TransactionType.Withdrawal,
                metadata);

            // Assert
            result.Should().BeOfType<DomainValidationResult>();

            result.Errors.Should().NotBeEmpty();
        }
    }
}
