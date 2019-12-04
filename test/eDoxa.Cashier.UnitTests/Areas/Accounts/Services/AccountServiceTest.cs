// Filename: AccountServiceTest.cs
// Date Created: 2019-11-25
// 
// ================================================
// Copyright © 2019, eDoxa. All rights reserved.

using System.Collections.Generic;
using System.Collections.Immutable;
using System.Threading;
using System.Threading.Tasks;

using eDoxa.Cashier.Api.Services;
using eDoxa.Cashier.Domain.AggregateModels;
using eDoxa.Cashier.Domain.AggregateModels.AccountAggregate;
using eDoxa.Cashier.Domain.AggregateModels.TransactionAggregate;
using eDoxa.Cashier.Domain.Repositories;
using eDoxa.Cashier.Domain.Services;
using eDoxa.Cashier.TestHelper;
using eDoxa.Cashier.TestHelper.Fixtures;
using eDoxa.Seedwork.Domain;
using eDoxa.Seedwork.Domain.Misc;

using FluentAssertions;

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
            var mockBundlesService = new Mock<IBundleService>();
            

            var userId = new UserId();

            mockAccountRepository.Setup(accountRepository => accountRepository.Create(It.IsAny<Account>())).Verifiable();

            mockAccountRepository.Setup(accountRepository => accountRepository.CommitAsync(It.IsAny<CancellationToken>()))
                .Returns(Task.CompletedTask)
                .Verifiable();

            var service = new AccountService(mockAccountRepository.Object, mockBundlesService.Object);

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
            var mockBundlesService = new Mock<IBundleService>();
            
            var account = new Account(new UserId());
            var moneyAccount = new MoneyAccountDecorator(account);
            var transaction = moneyAccount.Deposit(Money.OneHundred);

            transaction.MarkAsSucceded();

            mockAccountRepository.Setup(accountRepository => accountRepository.CommitAsync(It.IsAny<CancellationToken>()))
                .Returns(Task.CompletedTask)
                .Verifiable();

            var service = new AccountService(mockAccountRepository.Object, mockBundlesService.Object);

            // Act
            var result = await service.CreateTransactionAsync(
                moneyAccount,
                Money.Twenty.Amount,
                Currency.Money,
                TransactionType.Charge);

            // Assert
            result.Should().BeOfType<DomainValidationResult>();

            mockAccountRepository.Verify(accountRepository => accountRepository.CommitAsync(It.IsAny<CancellationToken>()), Times.Once);
        }

        [Fact]
        public async Task CreateMoneyTransactionAsync_WithTypeChargeWithEmptyFunds_ShouldBeOfTypeValidationResultWithErrors()
        {
            // Arrange
            var mockAccountRepository = new Mock<IAccountRepository>();
            var mockBundlesService = new Mock<IBundleService>();
            

            var account = new Account(new UserId());

            var service = new AccountService(mockAccountRepository.Object, mockBundlesService.Object);

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
            var mockBundlesService = new Mock<IBundleService>();

            var account = new Account(new UserId());
            var tokenAccount = new TokenAccountDecorator(account);
            var transaction = tokenAccount.Deposit(Token.TwoHundredFiftyThousand);

            transaction.MarkAsSucceded();

            mockAccountRepository.Setup(accountRepository => accountRepository.CommitAsync(It.IsAny<CancellationToken>()))
                .Returns(Task.CompletedTask)
                .Verifiable();

            var service = new AccountService(mockAccountRepository.Object, mockBundlesService.Object);

            // Act
            var result = await service.CreateTransactionAsync(
                tokenAccount,
                Token.FiftyThousand.Amount,
                Currency.Token,
                TransactionType.Charge);

            // Assert
            result.Should().BeOfType<DomainValidationResult>();

            mockAccountRepository.Verify(accountRepository => accountRepository.CommitAsync(It.IsAny<CancellationToken>()), Times.Once);
        }

        [Fact]
        public async Task CreateTokenTransactionAsync_WithTypeChargeWithEmptyFunds_ShouldBeOfTypeValidationResultWithErrors()
        {
            // Arrange
            var mockAccountRepository = new Mock<IAccountRepository>();
            var mockBundlesService = new Mock<IBundleService>();
            

            var account = new Account(new UserId());

            var service = new AccountService(mockAccountRepository.Object, mockBundlesService.Object);

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
            var mockBundlesService = new Mock<IBundleService>();
            

            var account = new Account(new UserId());

            var service = new AccountService(mockAccountRepository.Object, mockBundlesService.Object);

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
            var mockBundlesService = new Mock<IBundleService>();
            

            mockBundlesService.Setup(x => x.FetchDepositMoneyBundles()).Returns(new List<Bundle>().ToImmutableHashSet()).Verifiable();

            var account = new Account(new UserId());

            var service = new AccountService(mockAccountRepository.Object, mockBundlesService.Object);

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
            var mockBundlesService = new Mock<IBundleService>();
            

            mockBundlesService.Setup(x => x.FetchDepositTokenBundles()).Returns(new List<Bundle>().ToImmutableHashSet()).Verifiable();

            var account = new Account(new UserId());

            var service = new AccountService(mockAccountRepository.Object, mockBundlesService.Object);

            // Act
            var result = await service.CreateTransactionAsync(
                account,
                20,
                Currency.Token,
                TransactionType.Deposit);

            // Assert
            result.Should().BeOfType<DomainValidationResult>();
        }

        [Fact] //TODO: Validation with TryGetValue
        public async Task CreateTransactionAsync_WithWrongAmount_ShouldBeOfTypeValidationResultWithErrors()
        {
            // Arrange
            var mockAccountRepository = new Mock<IAccountRepository>();
            var mockBundlesService = new Mock<IBundleService>();
            

            var account = new Account(new UserId());
            var service = new AccountService(mockAccountRepository.Object, mockBundlesService.Object);

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
            var mockBundlesService = new Mock<IBundleService>();
            

            var account = new Account(new UserId());

            mockAccountRepository.Setup(accountRepository => accountRepository.CommitAsync(It.IsAny<CancellationToken>()))
                .Returns(Task.CompletedTask)
                .Verifiable();

            //mockServiceBusPublisher.Setup(serviceBus => serviceBus.PublishAsync(It.IsAny<UserAccountDepositIntegrationEvent>()))
            //    .Returns(Task.CompletedTask)
            //    .Verifiable();

            var bundle = new List<Bundle>
            {
                new Bundle(new Money(100), new Price(new Money(100)))
            };

            mockBundlesService.Setup(bundleService => bundleService.FetchDepositMoneyBundles()).Returns(bundle.ToImmutableHashSet()).Verifiable();

            var service = new AccountService(mockAccountRepository.Object, mockBundlesService.Object);

            // Act
            var result = await service.CreateTransactionAsync(account, Money.OneHundred.Amount, Currency.Money, TransactionType.Deposit);

            // Assert
            result.Should().BeOfType<DomainValidationResult>();

            mockBundlesService.Verify(bundleService => bundleService.FetchDepositMoneyBundles(), Times.Once());

            mockAccountRepository.Verify(accountRepository => accountRepository.CommitAsync(It.IsAny<CancellationToken>()), Times.Once());

            //mockServiceBusPublisher.Verify(serviceBus => serviceBus.PublishAsync(It.IsAny<UserAccountDepositIntegrationEvent>()), Times.Once());
        }

        [Fact]
        public async Task DepositAsync_WithCurrencyMoneyWithDepositPresent_ShouldBeOfTypeValidationResultWithErrors()
        {
            // Arrange
            var mockAccountRepository = new Mock<IAccountRepository>();
            var mockBundlesService = new Mock<IBundleService>();
            
            var account = new Account(new UserId());
            var moneyAccount = new MoneyAccountDecorator(account);
            var transaction = moneyAccount.Deposit(Money.OneHundred);

            transaction.MarkAsSucceded();

            var bundle = new List<Bundle>
            {
                new Bundle(new Money(100), new Price(new Money(100)))
            };

            mockBundlesService.Setup(bundleService => bundleService.FetchDepositMoneyBundles()).Returns(bundle.ToImmutableHashSet()).Verifiable();

            var service = new AccountService(mockAccountRepository.Object, mockBundlesService.Object);

            // Act
            var result = await service.CreateTransactionAsync(moneyAccount, Money.OneHundred.Amount, Currency.Money, TransactionType.Deposit);

            // Assert
            result.Should().BeOfType<DomainValidationResult>();
            result.Errors.Should().NotBeEmpty();

            mockBundlesService.Verify(bundleService => bundleService.FetchDepositMoneyBundles(), Times.Once());
        }

        [Fact]
        public async Task DepositAsync_WithCurrencyMoneyWithWrongBundle_ShouldBeOfTypeValidationResultWithErrors()
        {
            // Arrange
            var mockAccountRepository = new Mock<IAccountRepository>();
            var mockBundlesService = new Mock<IBundleService>();
            

            var account = new Account(new UserId());

            var bundle = new List<Bundle>
            {
                new Bundle(new Money(20), new Price(new Money(100)))
            };

            mockBundlesService.Setup(bundleService => bundleService.FetchDepositMoneyBundles()).Returns(bundle.ToImmutableHashSet()).Verifiable();
            var service = new AccountService(mockAccountRepository.Object, mockBundlesService.Object);

            // Act
            var result = await service.CreateTransactionAsync(account, Money.OneHundred.Amount, Currency.Money, TransactionType.Deposit);

            // Assert
            result.Should().BeOfType<DomainValidationResult>();

            result.Errors.Should().NotBeEmpty();

            mockBundlesService.Verify(bundleService => bundleService.FetchDepositMoneyBundles(), Times.Once());
        }

        //[Fact]
        //public void DepositAsync_WithCurrencyNull_ShouldThrowInvalidOperationException()
        //{
        //    // Arrange
        //    var mockAccountRepository = new Mock<IAccountRepository>();
        //    var mockBundlesService = new Mock<IBundlesService>();
        //    

        //    var account = new Account(new UserId());

        //    var service = new AccountService(mockAccountRepository.Object, mockBundlesService.Object);

        //    // Act
        //    var action = new Func<Task<DomainValidationResult>>(async () => await service.CreateTransactionAsync(account, 0, null, TransactionType.Deposit));

        //    // Assert
        //    action.Should().Throw<InvalidOperationException>();
        //}

        [Fact]
        public async Task DepositAsync_WithCurrencyToken_ShouldBeOfTypeValidationResult()
        {
            // Arrange
            var mockAccountRepository = new Mock<IAccountRepository>();
            var mockBundlesService = new Mock<IBundleService>();
            

            var account = new Account(new UserId());

            mockAccountRepository.Setup(accountRepository => accountRepository.CommitAsync(It.IsAny<CancellationToken>()))
                .Returns(Task.CompletedTask)
                .Verifiable();

            //mockServiceBusPublisher.Setup(serviceBus => serviceBus.PublishAsync(It.IsAny<UserAccountDepositIntegrationEvent>()))
            //    .Returns(Task.CompletedTask)
            //    .Verifiable();

            var bundle = new List<Bundle>
            {
                new Bundle(new Token(50000), new Price(new Money(100)))
            };

            mockBundlesService.Setup(bundleService => bundleService.FetchDepositTokenBundles()).Returns(bundle.ToImmutableHashSet()).Verifiable();

            var service = new AccountService(mockAccountRepository.Object, mockBundlesService.Object);

            // Act
            var result = await service.CreateTransactionAsync(account, Token.FiftyThousand.Amount, Currency.Token, TransactionType.Deposit);

            // Assert
            result.Should().BeOfType<DomainValidationResult>();
            mockBundlesService.Verify(bundleService => bundleService.FetchDepositTokenBundles(), Times.Once());
            mockAccountRepository.Verify(accountRepository => accountRepository.CommitAsync(It.IsAny<CancellationToken>()), Times.Once());
            //mockServiceBusPublisher.Verify(serviceBus => serviceBus.PublishAsync(It.IsAny<UserAccountDepositIntegrationEvent>()), Times.Once());
        }

        [Fact]
        public async Task DepositAsync_WithCurrencyTokenWithDepositPresent_ShouldBeOfTypeValidationResultWithErrors()
        {
            // Arrange
            var mockAccountRepository = new Mock<IAccountRepository>();
            var mockBundlesService = new Mock<IBundleService>();
            
            var account = new Account(new UserId());
            var tokenAccount = new TokenAccountDecorator(account);
            var transaction = tokenAccount.Deposit(Token.FiftyThousand);

            transaction.MarkAsSucceded();

            var bundle = new List<Bundle>
            {
                new Bundle(new Token(50000), new Price(new Money(100)))
            };

            mockBundlesService.Setup(bundleService => bundleService.FetchDepositTokenBundles()).Returns(bundle.ToImmutableHashSet()).Verifiable();

            var service = new AccountService(mockAccountRepository.Object, mockBundlesService.Object);

            // Act
            var result = await service.CreateTransactionAsync(tokenAccount, Token.FiftyThousand.Amount, Currency.Token, TransactionType.Deposit);

            // Assert
            result.Should().BeOfType<DomainValidationResult>();
            result.Errors.Should().NotBeEmpty();

            mockBundlesService.Verify(bundleService => bundleService.FetchDepositTokenBundles(), Times.Once());
        }

        [Fact]
        public async Task DepositAsync_WithCurrencyTokenWithWrongBundle_ShouldBeOfTypeValidationResultWithErrors()
        {
            // Arrange
            var mockAccountRepository = new Mock<IAccountRepository>();
            var mockBundlesService = new Mock<IBundleService>();
            

            var account = new Account(new UserId());

            var bundle = new List<Bundle>
            {
                new Bundle(new Token(1000), new Price(new Money(100)))
            };

            mockBundlesService.Setup(bundleService => bundleService.FetchDepositTokenBundles()).Returns(bundle.ToImmutableHashSet()).Verifiable();

            var service = new AccountService(mockAccountRepository.Object, mockBundlesService.Object);

            // Act
            var result = await service.CreateTransactionAsync(account, Token.FiftyThousand.Amount, Currency.Token, TransactionType.Deposit);

            // Assert
            result.Should().BeOfType<DomainValidationResult>();
            result.Errors.Should().NotBeEmpty();

            mockBundlesService.Verify(bundleService => bundleService.FetchDepositTokenBundles(), Times.Once());
        }

        [Fact]
        public async Task FindUserAccountAsync_ShouldBeOfTypeValidationResult()
        {
            // Arrange
            var mockAccountRepository = new Mock<IAccountRepository>();
            var mockBundlesService = new Mock<IBundleService>();
            

            var userId = new UserId();
            var account = new Account(userId);

            mockAccountRepository.Setup(accountRepository => accountRepository.FindUserAccountAsync(It.IsAny<UserId>())).ReturnsAsync(account).Verifiable();

            var service = new AccountService(mockAccountRepository.Object, mockBundlesService.Object);

            // Act
            var result = await service.FindAccountAsync(userId);

            // Assert
            result.Should().BeOfType<Account>();

            mockAccountRepository.Verify(accountRepository => accountRepository.FindUserAccountAsync(It.IsAny<UserId>()), Times.Once);
        }

        [Fact]
        public async Task WithdrawalAsync_WithCurrencyMoney_ShouldBeOfTypeValidationResult()
        {
            // Arrange
            var mockAccountRepository = new Mock<IAccountRepository>();
            var mockBundlesService = new Mock<IBundleService>();
            
            var account = new Account(new UserId());
            var moneyAccount = new MoneyAccountDecorator(account);
            var transaction = moneyAccount.Deposit(Money.OneHundred);

            transaction.MarkAsSucceded();

            mockAccountRepository.Setup(accountRepository => accountRepository.CommitAsync(It.IsAny<CancellationToken>()))
                .Returns(Task.CompletedTask)
                .Verifiable();

            //mockServiceBusPublisher.Setup(serviceBus => serviceBus.PublishAsync(It.IsAny<UserAccountWithdrawalIntegrationEvent>()))
            //    .Returns(Task.CompletedTask)
            //    .Verifiable();

            var bundle = new List<Bundle>
            {
                new Bundle(new Money(20), new Price(new Money(100)))
            };

            mockBundlesService.Setup(bundleService => bundleService.FetchWithdrawalMoneyBundles()).Returns(bundle.ToImmutableHashSet()).Verifiable();

            var metadata = new TransactionMetadata
            {
                {"UserId", account.Id.ToString()},
                {"Email", "gabriel@edoxa.gg"}
            };

            var service = new AccountService(mockAccountRepository.Object, mockBundlesService.Object);

            // Act
            var result = await service.CreateTransactionAsync(
                moneyAccount,
                Money.Twenty.Amount,
                Currency.Money,
                TransactionType.Withdrawal,
                metadata);

            // Assert
            result.Should().BeOfType<DomainValidationResult>();

            mockBundlesService.Verify(bundleService => bundleService.FetchWithdrawalMoneyBundles(), Times.Once());

            mockAccountRepository.Verify(accountRepository => accountRepository.CommitAsync(It.IsAny<CancellationToken>()), Times.Once());

            //mockServiceBusPublisher.Verify(serviceBus => serviceBus.PublishAsync(It.IsAny<UserAccountWithdrawalIntegrationEvent>()), Times.Once());
        }

        [Fact]
        public async Task WithdrawalAsync_WithCurrencyToken_ShouldBeOfTypeValidationResultWithErrors()
        {
            // Arrange
            var mockAccountRepository = new Mock<IAccountRepository>();

            var mockBundlesService = new Mock<IBundleService>();

            mockBundlesService.Setup(x => x.FetchWithdrawalMoneyBundles()).Returns(new List<Bundle>().ToImmutableHashSet).Verifiable();

            

            var account = new Account(new UserId());

            var metadata = new TransactionMetadata
            {
                {"UserId", account.Id.ToString()},
                {"Email", "gabriel@edoxa.gg"}
            };

            var service = new AccountService(mockAccountRepository.Object, mockBundlesService.Object);

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

        [Fact]
        public async Task WithdrawalAsync_WithEmptyAccountBalance_ShouldBeOfTypeValidationResultWithErrors()
        {
            // Arrange
            var mockAccountRepository = new Mock<IAccountRepository>();

            var mockBundlesService = new Mock<IBundleService>();

            

            var account = new Account(new UserId());

            var bundle = new List<Bundle>
            {
                new Bundle(new Money(20), new Price(new Money(100)))
            };

            mockBundlesService.Setup(bundleService => bundleService.FetchWithdrawalMoneyBundles()).Returns(bundle.ToImmutableHashSet()).Verifiable();

            var metadata = new TransactionMetadata
            {
                {"UserId", account.Id.ToString()},
                {"Email", "gabriel@edoxa.gg"}
            };

            var service = new AccountService(mockAccountRepository.Object, mockBundlesService.Object);

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

            mockBundlesService.Verify(bundleService => bundleService.FetchWithdrawalMoneyBundles(), Times.Once());
        }

        [Fact]
        public async Task WithdrawalAsync_WithWithdrawlPresent_ShouldBeOfTypeValidationResultWithErrors()
        {
            // Arrange
            var mockAccountRepository = new Mock<IAccountRepository>();
            var mockBundlesService = new Mock<IBundleService>();
            
            var account = new Account(new UserId());
            var moneyAccount = new MoneyAccountDecorator(account);
            var transaction = moneyAccount.Deposit(Money.OneHundred);

            transaction.MarkAsSucceded();

            var bundle = new List<Bundle>
            {
                new Bundle(new Money(20), new Price(new Money(100)))
            };

            mockBundlesService.Setup(bundleService => bundleService.FetchWithdrawalMoneyBundles()).Returns(bundle.ToImmutableHashSet()).Verifiable();
            
            var service = new AccountService(mockAccountRepository.Object, mockBundlesService.Object);

            // Act
            var result = await service.CreateTransactionAsync(
                account,
                Money.Fifty.Amount,
                Currency.Money,
                TransactionType.Withdrawal);

            // Assert
            result.Should().BeOfType<DomainValidationResult>();
            result.Errors.Should().NotBeEmpty();

            mockBundlesService.Verify(bundleService => bundleService.FetchWithdrawalMoneyBundles(), Times.Once());
        }

        [Fact]
        public async Task WithdrawalAsync_WithWrongBundle_ShouldBeOfTypeValidationResultWithErrors()
        {
            // Arrange
            var mockAccountRepository = new Mock<IAccountRepository>();
            var mockBundlesService = new Mock<IBundleService>();
            

            var account = new Account(new UserId());

            var bundle = new List<Bundle>
            {
                new Bundle(new Money(100), new Price(new Money(100)))
            };

            mockBundlesService.Setup(bundleService => bundleService.FetchWithdrawalMoneyBundles()).Returns(bundle.ToImmutableHashSet()).Verifiable();

            var metadata = new TransactionMetadata
            {
                {"UserId", account.Id.ToString()},
                {"Email", "gabriel@edoxa.gg"}
            };

            var service = new AccountService(mockAccountRepository.Object, mockBundlesService.Object);

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

            mockBundlesService.Verify(bundleService => bundleService.FetchWithdrawalMoneyBundles(), Times.Once());
        }
    }
}
