// Filename: AccountService.cs
// Date Created: 2019-12-26
// 
// ================================================
// Copyright © 2020, eDoxa. All rights reserved.

using System.Collections.Generic;
using System.Linq;
using System.Threading;
using System.Threading.Tasks;

using eDoxa.Cashier.Domain.AggregateModels;
using eDoxa.Cashier.Domain.AggregateModels.AccountAggregate;
using eDoxa.Cashier.Domain.Repositories;
using eDoxa.Cashier.Domain.Services;
using eDoxa.Grpc.Protos.Cashier.Dtos;
using eDoxa.Grpc.Protos.Cashier.Enums;
using eDoxa.Grpc.Protos.Cashier.Options;
using eDoxa.Seedwork.Domain;
using eDoxa.Seedwork.Domain.Extensions;
using eDoxa.Seedwork.Domain.Misc;

using Microsoft.Extensions.Options;

namespace eDoxa.Cashier.Api.Application.Services
{
    public sealed class AccountService : IAccountService
    {
        private readonly IAccountRepository _accountRepository;
        private readonly IOptions<CashierApiOptions> _optionsSnapshot;

        public AccountService(IAccountRepository accountRepository, IOptionsSnapshot<CashierApiOptions> optionsSnapshot)
        {
            _accountRepository = accountRepository;
            _optionsSnapshot = optionsSnapshot;
        }

        private CashierApiOptions Options => _optionsSnapshot.Value;

        public async Task<IAccount> FindAccountAsync(UserId userId)
        {
            return await _accountRepository.FindAccountAsync(userId);
        }

        public async Task<IAccount?> FindAccountOrNullAsync(UserId userId)
        {
            return await _accountRepository.FindAccountOrNullAsync(userId);
        }

        public async Task<bool> AccountExistsAsync(UserId userId)
        {
            return await _accountRepository.AccountExistsAsync(userId);
        }

        public async Task<DomainValidationResult<ITransaction>> CreateTransactionAsync(
            IAccount account,
            int bundleId,
            TransactionMetadata? metadata = null,
            CancellationToken cancellationToken = default
        )
        {
            var result = new DomainValidationResult<ITransaction>();

            if (!await this.TransactionBundleExistsAsync(bundleId))
            {
                result.AddFailedPreconditionError($"Transaction bundle with id of '{bundleId}' wasn't found.");
            }

            if (result.IsValid)
            {
                var transactionBundle = await this.FindTransactionBundleAsync(bundleId);

                return await this.CreateTransactionAsync(
                    account,
                    transactionBundle.Currency.Amount,
                    transactionBundle.Currency.Type.ToEnumeration<CurrencyType>(),
                    transactionBundle.Type.ToEnumeration<TransactionType>(),
                    metadata,
                    cancellationToken);
            }

            return result;
        }

        public async Task<DomainValidationResult<ITransaction>> CreateTransactionAsync(
            IAccount account,
            Currency currency,
            TransactionType type,
            TransactionMetadata? metadata = null,
            CancellationToken cancellationToken = default
        )
        {
            return await this.CreateTransactionAsync(
                account,
                currency.Amount,
                currency.Type,
                type,
                metadata,
                cancellationToken);
        }

        public async Task<DomainValidationResult<IAccount>> CreateAccountAsync(UserId userId)
        {
            var result = new DomainValidationResult<IAccount>();

            if (result.IsValid)
            {
                var account = new Account(userId);

                _accountRepository.Create(account);

                await _accountRepository.CommitAsync();

                return account;
            }

            return result;
        }

        public async Task<DomainValidationResult<ITransaction>> CreateTransactionAsync(
            IAccount account,
            decimal amount,
            CurrencyType currencyType,
            TransactionType type,
            TransactionMetadata? metadata = null,
            CancellationToken cancellationToken = default
        )
        {
            var result = new DomainValidationResult<ITransaction>();

            if (result.IsValid)
            {
                var value = currencyType.ToCurrency(amount);

                if (value is Money money)
                {
                    return await this.CreateTransactionAsync(
                        new MoneyAccountDecorator(account),
                        money,
                        type,
                        metadata,
                        cancellationToken);
                }

                if (value is Token token)
                {
                    return await this.CreateTransactionAsync(
                        new TokenAccountDecorator(account),
                        token,
                        type,
                        metadata,
                        cancellationToken);
                }
            }

            return result;
        }

        public async Task<ITransaction?> FindAccountTransactionAsync(IAccount account, TransactionId transactionId)
        {
            return await Task.FromResult(account.TransactionExists(transactionId) ? account.FindTransaction(transactionId) : null);
        }

        public async Task<DomainValidationResult<ITransaction>> MarkAccountTransactionAsSucceededAsync(
            IAccount account,
            TransactionId transactionId,
            CancellationToken cancellationToken = default
        )
        {
            var result = new DomainValidationResult<ITransaction>();

            if (!account.TransactionExists(transactionId))
            {
                result.AddFailedPreconditionError("Transaction does not exists.");
            }

            if (result.IsValid)
            {
                var transaction = account.FindTransaction(transactionId);

                transaction.MarkAsSucceeded();

                await _accountRepository.CommitAsync(true, cancellationToken);

                return transaction.Cast<Transaction>();
            }

            return result;
        }

        public async Task<DomainValidationResult<ITransaction>> MarkAccountTransactionAsFailedAsync(
            IAccount account,
            TransactionId transactionId,
            CancellationToken cancellationToken = default
        )
        {
            var result = new DomainValidationResult<ITransaction>();

            if (!account.TransactionExists(transactionId))
            {
                result.AddFailedPreconditionError("Transaction does not exists.");
            }

            if (result.IsValid)
            {
                var transaction = account.FindTransaction(transactionId);

                transaction.MarkAsFailed();

                await _accountRepository.CommitAsync(true, cancellationToken);

                return transaction.Cast<Transaction>();
            }

            return result;
        }

        public async Task<DomainValidationResult<ITransaction>> MarkAccountTransactionAsCanceledAsync(
            IAccount account,
            TransactionId transactionId,
            CancellationToken cancellationToken = default
        )
        {
            var result = new DomainValidationResult<ITransaction>();

            if (!account.TransactionExists(transactionId))
            {
                result.AddFailedPreconditionError("Transaction does not exists.");
            }

            if (result.IsValid)
            {
                var transaction = account.FindTransaction(transactionId);

                transaction.MarkAsCanceled();

                await _accountRepository.CommitAsync(true, cancellationToken);

                return transaction.Cast<Transaction>();
            }

            return result;
        }

        public async Task<DomainValidationResult<ITransaction>> MarkAccountTransactionAsSucceededAsync(
            IAccount account,
            TransactionMetadata metadata,
            CancellationToken cancellationToken = default
        )
        {
            var result = new DomainValidationResult<ITransaction>();

            if (!account.TransactionExists(metadata))
            {
                result.AddFailedPreconditionError("Transaction does not exists.");
            }

            if (result.IsValid)
            {
                var transaction = account.FindTransaction(metadata);

                transaction.MarkAsSucceeded();

                await _accountRepository.CommitAsync(true, cancellationToken);

                return transaction.Cast<Transaction>();
            }

            return result;
        }

        public async Task<DomainValidationResult<ITransaction>> MarkAccountTransactionAsFailedAsync(
            IAccount account,
            TransactionMetadata metadata,
            CancellationToken cancellationToken = default
        )
        {
            var result = new DomainValidationResult<ITransaction>();

            if (!account.TransactionExists(metadata))
            {
                result.AddFailedPreconditionError("Transaction does not exists.");
            }

            if (result.IsValid)
            {
                var transaction = account.FindTransaction(metadata);

                transaction.MarkAsFailed();

                await _accountRepository.CommitAsync(true, cancellationToken);

                return transaction.Cast<Transaction>();
            }

            return result;
        }

        public async Task<DomainValidationResult<ITransaction>> MarkAccountTransactionAsCanceledAsync(
            IAccount account,
            TransactionMetadata metadata,
            CancellationToken cancellationToken = default
        )
        {
            var result = new DomainValidationResult<ITransaction>();

            if (!account.TransactionExists(metadata))
            {
                result.AddFailedPreconditionError("Transaction does not exists.");
            }

            if (result.IsValid)
            {
                var transaction = account.FindTransaction(metadata);

                transaction.MarkAsCanceled();

                await _accountRepository.CommitAsync(true, cancellationToken);

                return transaction.Cast<Transaction>();
            }

            return result;
        }

        public async Task<DomainValidationResult<ITransaction>> DeleteTransactionAsync(IAccount account, TransactionId transactionId, CancellationToken cancellationToken = default)
        {
            var result = new DomainValidationResult<ITransaction>();

            if (!account.TransactionExists(transactionId))
            {
                result.AddFailedPreconditionError("Transaction does not exists.");
            }

            if (result.IsValid)
            {
                var transaction = account.FindTransaction(transactionId);

                transaction.Delete();

                await _accountRepository.CommitAsync(true, cancellationToken);

                return transaction.Cast<Transaction>();
            }

            return result;
        }

        public async Task<IReadOnlyCollection<TransactionBundleDto>> FetchTransactionBundlesAsync(
            EnumTransactionType type = EnumTransactionType.All,
            EnumCurrencyType currencyType = EnumCurrencyType.All,
            bool includeDisabled = false
        )
        {
            var transactionBundles = Options.Static.Transaction.Bundles.Where(
                transactionBundle => (type == EnumTransactionType.All || type == transactionBundle.Type) &&
                                     (currencyType == EnumCurrencyType.All || currencyType == transactionBundle.Currency.Type) &&
                                     !transactionBundle.Deprecated);

            if (!includeDisabled)
            {
                transactionBundles = transactionBundles.Where(transactionBundle => !transactionBundle.Disabled);
            }

            return await Task.FromResult(transactionBundles.OrderBy(transactionBundle => transactionBundle.Id).ToList());
        }

        private async Task<bool> TransactionBundleExistsAsync(int transactionBundleId)
        {
            var transactionBundles = await this.FetchTransactionBundlesAsync();

            return transactionBundles.Any(transactionBundle => transactionBundle.Id == transactionBundleId);
        }

        private async Task<TransactionBundleDto> FindTransactionBundleAsync(int transactionBundleId)
        {
            var transactionBundles = await this.FetchTransactionBundlesAsync();

            return transactionBundles.Single(transactionBundle => transactionBundle.Id == transactionBundleId);
        }

        private async Task<DomainValidationResult<ITransaction>> CreateTransactionAsync(
            IMoneyAccount account,
            Money money,
            TransactionType type,
            TransactionMetadata? metadata = null,
            CancellationToken cancellationToken = default
        )
        {
            if (type == TransactionType.Deposit)
            {
                return await this.CreateDepositTransactionAsync(account, money, cancellationToken);
            }

            if (type == TransactionType.Withdraw)
            {
                return await this.CreateWithdrawTransactionAsync(account, money, cancellationToken);
            }

            if (type == TransactionType.Charge)
            {
                return await this.CreateChargeTransactionAsync(
                    account,
                    money,
                    metadata,
                    cancellationToken);
            }

            if (type == TransactionType.Payout)
            {
                return await this.CreatePayoutTransactionAsync(
                    account,
                    money,
                    metadata,
                    cancellationToken);
            }

            if (type == TransactionType.Promotion)
            {
                return await this.CreatePromotionTransactionAsync(
                    account,
                    money,
                    metadata,
                    cancellationToken);
            }

            return DomainValidationResult<ITransaction>.Failure("Unsupported transaction type for money currency.");
        }

        private async Task<DomainValidationResult<ITransaction>> CreateDepositTransactionAsync(
            IMoneyAccount account,
            Money money,
            CancellationToken cancellationToken = default
        )
        {
            var result = new DomainValidationResult<ITransaction>();

            var transactionBundles = await this.FetchTransactionBundlesAsync(EnumTransactionType.Deposit, EnumCurrencyType.Money);

            if (transactionBundles.All(deposit => deposit.Currency.Amount != money.Amount))
            {
                result.AddFailedPreconditionError(
                    $"The amount of {nameof(Money)} is invalid. These are valid amounts: [{string.Join(", ", transactionBundles.Select(deposit => deposit.Currency.Amount))}].");
            }

            if (!account.IsDepositAvailable())
            {
                result.AddFailedPreconditionError(
                    $"Deposit is unavailable until {account.LastDeposit?.Add(MoneyAccountDecorator.DepositInterval)}. For security reason we limit the number of financial transaction that can be done in {MoneyAccountDecorator.DepositInterval.TotalHours} hours.");
            }

            if (result.IsValid)
            {
                var transaction = account.Deposit(money);

                await _accountRepository.CommitAsync(true, cancellationToken);

                return transaction.Cast<Transaction>();
            }

            return result;
        }

        private async Task<DomainValidationResult<ITransaction>> CreateWithdrawTransactionAsync(
            IMoneyAccount account,
            Money money,
            CancellationToken cancellationToken = default
        )
        {
            var result = new DomainValidationResult<ITransaction>();

            var transactionBundles = await this.FetchTransactionBundlesAsync(EnumTransactionType.Withdraw, EnumCurrencyType.Money);

            if (transactionBundles.All(withdraw => withdraw.Currency.Amount != money.Amount))
            {
                result.AddFailedPreconditionError(
                    $"The amount of {nameof(Money)} is invalid. These are valid amounts: [{string.Join(", ", transactionBundles.Select(deposit => deposit.Currency.Amount))}].");
            }

            if (!account.HaveSufficientMoney(money))
            {
                result.AddFailedPreconditionError("Insufficient funds.");
            }

            if (!account.IsWithdrawAvailable())
            {
                result.AddFailedPreconditionError(
                    $"Withdraw is unavailable until {account.LastWithdraw?.Add(MoneyAccountDecorator.WithdrawInterval)}. For security reason we limit the number of financial transaction that can be done in {MoneyAccountDecorator.WithdrawInterval.TotalHours} hours.");
            }

            if (result.IsValid)
            {
                var transaction = account.Withdraw(money);

                await _accountRepository.CommitAsync(true, cancellationToken);

                return transaction.Cast<Transaction>();
            }

            return result;
        }

        private async Task<DomainValidationResult<ITransaction>> CreatePayoutTransactionAsync(
            IMoneyAccount account,
            Money money,
            TransactionMetadata? metadata = null,
            CancellationToken cancellationToken = default
        )
        {
            var result = new DomainValidationResult<ITransaction>();

            if (result.IsValid)
            {
                var transaction = account.Payout(money, metadata);

                transaction.MarkAsSucceeded();

                await _accountRepository.CommitAsync(true, cancellationToken);

                return transaction.Cast<Transaction>();
            }

            return result;
        }

        private async Task<DomainValidationResult<ITransaction>> CreatePayoutTransactionAsync(
            ITokenAccount account,
            Token token,
            TransactionMetadata? metadata = null,
            CancellationToken cancellationToken = default
        )
        {
            var result = new DomainValidationResult<ITransaction>();

            if (result.IsValid)
            {
                var transaction = account.Payout(token, metadata);

                transaction.MarkAsSucceeded();

                await _accountRepository.CommitAsync(true, cancellationToken);

                return transaction.Cast<Transaction>();
            }

            return result;
        }

        private async Task<DomainValidationResult<ITransaction>> CreateChargeTransactionAsync(
            IMoneyAccount account,
            Money money,
            TransactionMetadata? metadata = null,
            CancellationToken cancellationToken = default
        )
        {
            var result = new DomainValidationResult<ITransaction>();

            if (!account.HaveSufficientMoney(money))
            {
                result.AddFailedPreconditionError("Insufficient funds.");
            }

            if (result.IsValid)
            {
                var transaction = account.Charge(money, metadata);

                await _accountRepository.CommitAsync(true, cancellationToken);

                return transaction.Cast<Transaction>();
            }

            return result;
        }

        private async Task<DomainValidationResult<ITransaction>> CreateDepositTransactionAsync(
            ITokenAccount account,
            Token token,
            CancellationToken cancellationToken = default
        )
        {
            var result = new DomainValidationResult<ITransaction>();

            var transactionBundles = await this.FetchTransactionBundlesAsync(EnumTransactionType.Deposit, EnumCurrencyType.Token);

            if (transactionBundles.All(deposit => deposit.Currency.Amount != token.Amount))
            {
                result.AddFailedPreconditionError(
                    $"The amount of {nameof(Token)} is invalid. These are valid amounts: [{string.Join(", ", transactionBundles.Select(deposit => deposit.Currency.Amount))}].");
            }

            if (!account.IsDepositAvailable())
            {
                result.AddFailedPreconditionError(
                    $"Buying tokens is unavailable until {account.LastDeposit?.Add(TokenAccountDecorator.DepositInterval)}. For security reason we limit the number of financial transaction that can be done in {TokenAccountDecorator.DepositInterval.TotalHours} hours.");
            }

            if (result.IsValid)
            {
                var transaction = account.Deposit(token);

                await _accountRepository.CommitAsync(true, cancellationToken);

                return transaction.Cast<Transaction>();
            }

            return result;
        }

        private async Task<DomainValidationResult<ITransaction>> CreateChargeTransactionAsync(
            ITokenAccount account,
            Token token,
            TransactionMetadata? metadata = null,
            CancellationToken cancellationToken = default
        )
        {
            var result = new DomainValidationResult<ITransaction>();

            if (!account.HaveSufficientMoney(token))
            {
                result.AddFailedPreconditionError("Insufficient funds.");
            }

            if (result.IsValid)
            {
                var transaction = account.Charge(token, metadata);

                await _accountRepository.CommitAsync(true, cancellationToken);

                return transaction.Cast<Transaction>();
            }

            return result;
        }

        private async Task<DomainValidationResult<ITransaction>> CreateTransactionAsync(
            ITokenAccount account,
            Token token,
            TransactionType type,
            TransactionMetadata? metadata = null,
            CancellationToken cancellationToken = default
        )
        {
            if (type == TransactionType.Deposit)
            {
                return await this.CreateDepositTransactionAsync(account, token, cancellationToken);
            }

            if (type == TransactionType.Charge)
            {
                return await this.CreateChargeTransactionAsync(
                    account,
                    token,
                    metadata,
                    cancellationToken);
            }

            if (type == TransactionType.Payout)
            {
                return await this.CreatePayoutTransactionAsync(
                    account,
                    token,
                    metadata,
                    cancellationToken);
            }

            if (type == TransactionType.Promotion)
            {
                return await this.CreatePromotionTransactionAsync(
                    account,
                    token,
                    metadata,
                    cancellationToken);
            }

            return DomainValidationResult<ITransaction>.Failure("Unsupported transaction type for token currency.");
        }

        private async Task<DomainValidationResult<ITransaction>> CreatePromotionTransactionAsync(
            ITokenAccount account,
            Token token,
            TransactionMetadata? metadata = null,
            CancellationToken cancellationToken = default
        )
        {
            var result = new DomainValidationResult<ITransaction>();

            if (result.IsValid)
            {
                var transaction = account.Promotion(token, metadata);

                transaction.MarkAsSucceeded();

                await _accountRepository.CommitAsync(true, cancellationToken);

                return transaction.Cast<Transaction>();
            }

            return result;
        }

        private async Task<DomainValidationResult<ITransaction>> CreatePromotionTransactionAsync(
            IMoneyAccount account,
            Money money,
            TransactionMetadata? metadata = null,
            CancellationToken cancellationToken = default
        )
        {
            var result = new DomainValidationResult<ITransaction>();

            if (result.IsValid)
            {
                var transaction = account.Promotion(money, metadata);

                transaction.MarkAsSucceeded();

                await _accountRepository.CommitAsync(true, cancellationToken);

                return transaction.Cast<Transaction>();
            }

            return result;
        }
    }
}
