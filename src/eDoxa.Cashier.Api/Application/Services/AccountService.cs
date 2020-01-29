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
using eDoxa.Cashier.Domain.AggregateModels.ChallengeAggregate;
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

        public async Task<IDomainValidationResult> CreateTransactionAsync(
            IAccount account,
            int transactionBundleId,
            TransactionMetadata? transactionMetadata = null,
            CancellationToken cancellationToken = default
        )
        {
            var result = new DomainValidationResult();

            if (!await this.TransactionBundleExistsAsync(transactionBundleId))
            {
                result.AddFailedPreconditionError($"Transaction bundle with id of '{transactionBundleId}' wasn't found.");
            }

            if (result.IsValid)
            {
                var transactionBundle = await this.FindTransactionBundleAsync(transactionBundleId);

                return await this.CreateTransactionAsync(
                    account,
                    new decimal(transactionBundle.Currency.Amount),
                    transactionBundle.Currency.Type.ToEnumeration<Currency>(),
                    transactionBundle.Type.ToEnumeration<TransactionType>(),
                    transactionMetadata,
                    cancellationToken);
            }

            return result;
        }

        // TODO: Need to be refactored.
        public async Task<IDomainValidationResult> ProcessChallengePayoutAsync(Scoreboard scoreboard, CancellationToken cancellationToken = default)
        {
            var result = new DomainValidationResult();

            var payoutPrizes = new PayoutPrizes();

            if (result.IsValid)
            {
                foreach (var ladderGroup in scoreboard.Ladders)
                {
                    for (var index = 0; index < ladderGroup.Size; index++)
                    {
                        var user = scoreboard.Winners.Dequeue();

                        var score = scoreboard[user];

                        if (score == null)
                        {
                            // TODO: Need to be refactored.
                            await this.ProcessChallengePayoutAsync(user, scoreboard.PayoutCurrency, 0);

                            payoutPrizes.Add(user, new Prize(0, scoreboard.PayoutCurrency));
                        }
                        else
                        {
                            // TODO: Need to be refactored.
                            await this.ProcessChallengePayoutAsync(user, scoreboard.PayoutCurrency, ladderGroup.Prize);

                            payoutPrizes.Add(user, new Prize(ladderGroup.Prize.Amount, scoreboard.PayoutCurrency));
                        }
                    }
                }

                foreach (var user in scoreboard.Losers)
                {
                    var score = scoreboard[user];

                    if (score == null)
                    {
                        // TODO: Need to be refactored.
                        await this.ProcessChallengePayoutAsync(user, scoreboard.PayoutCurrency, 0);

                        payoutPrizes.Add(user, new Prize(0, scoreboard.PayoutCurrency));
                    }
                    else
                    {
                        // TODO: Need to be refactored.
                        await this.ProcessChallengePayoutAsync(user, Currency.Token, Token.MinValue);

                        payoutPrizes.Add(user, new Prize(Token.MinValue.Amount, Currency.Token));
                    }
                }

                await _accountRepository.CommitAsync(true, cancellationToken);

                result.AddEntityToMetadata(payoutPrizes);
            }

            return result;
        }

        public async Task<IDomainValidationResult> CreateAccountAsync(UserId userId)
        {
            var result = new DomainValidationResult();

            if (result.IsValid)
            {
                var account = new Account(userId);

                _accountRepository.Create(account);

                await _accountRepository.CommitAsync();

                result.AddEntityToMetadata(account);
            }

            return result;
        }

        public async Task<IDomainValidationResult> CreateTransactionAsync(
            IAccount account,
            decimal amount,
            Currency currency,
            TransactionType transactionType,
            TransactionMetadata? transactionMetadata = null,
            CancellationToken cancellationToken = default
        )
        {
            var result = new DomainValidationResult();

            if (result.IsValid)
            {
                var value = currency.From(amount);

                if (value is Money money)
                {
                    return await this.CreateTransactionAsync(
                        new MoneyAccountDecorator(account),
                        money,
                        transactionType,
                        transactionMetadata,
                        cancellationToken);
                }

                if (value is Token token)
                {
                    return await this.CreateTransactionAsync(
                        new TokenAccountDecorator(account),
                        token,
                        transactionType,
                        transactionMetadata,
                        cancellationToken);
                }
            }

            return result;
        }

        public async Task<ITransaction?> FindAccountTransactionAsync(IAccount account, TransactionId transactionId)
        {
            return await Task.FromResult(account.TransactionExists(transactionId) ? account.FindTransaction(transactionId) : null);
        }

        public async Task<IDomainValidationResult> MarkAccountTransactionAsSucceededAsync(
            IAccount account,
            TransactionId transactionId,
            CancellationToken cancellationToken = default
        )
        {
            var result = new DomainValidationResult();

            if (!account.TransactionExists(transactionId))
            {
                result.AddFailedPreconditionError("Transaction does not exists.");
            }

            if (result.IsValid)
            {
                var transaction = account.FindTransaction(transactionId);

                transaction.MarkAsSucceeded();

                await _accountRepository.CommitAsync(true, cancellationToken);

                result.AddEntityToMetadata(transaction);
            }

            return result;
        }

        public async Task<IDomainValidationResult> MarkAccountTransactionAsFailedAsync(
            IAccount account,
            TransactionId transactionId,
            CancellationToken cancellationToken = default
        )
        {
            var result = new DomainValidationResult();

            if (!account.TransactionExists(transactionId))
            {
                result.AddFailedPreconditionError("Transaction does not exists.");
            }

            if (result.IsValid)
            {
                var transaction = account.FindTransaction(transactionId);

                transaction.MarkAsFailed();

                await _accountRepository.CommitAsync(true, cancellationToken);

                result.AddEntityToMetadata(transaction);
            }

            return result;
        }

        public async Task<IDomainValidationResult> MarkAccountTransactionAsCanceledAsync(
            IAccount account,
            TransactionId transactionId,
            CancellationToken cancellationToken = default
        )
        {
            var result = new DomainValidationResult();

            if (!account.TransactionExists(transactionId))
            {
                result.AddFailedPreconditionError("Transaction does not exists.");
            }

            if (result.IsValid)
            {
                var transaction = account.FindTransaction(transactionId);

                transaction.MarkAsCanceled();

                await _accountRepository.CommitAsync(true, cancellationToken);

                result.AddEntityToMetadata(transaction);
            }

            return result;
        }

        public async Task<IDomainValidationResult> MarkAccountTransactionAsSucceededAsync(
            IAccount account,
            TransactionMetadata metadata,
            CancellationToken cancellationToken = default
        )
        {
            var result = new DomainValidationResult();

            if (!account.TransactionExists(metadata))
            {
                result.AddFailedPreconditionError("Transaction does not exists.");
            }

            if (result.IsValid)
            {
                var transaction = account.FindTransaction(metadata);

                transaction.MarkAsSucceeded();

                await _accountRepository.CommitAsync(true, cancellationToken);

                result.AddEntityToMetadata(transaction);
            }

            return result;
        }

        public async Task<IDomainValidationResult> MarkAccountTransactionAsFailedAsync(
            IAccount account,
            TransactionMetadata metadata,
            CancellationToken cancellationToken = default
        )
        {
            var result = new DomainValidationResult();

            if (!account.TransactionExists(metadata))
            {
                result.AddFailedPreconditionError("Transaction does not exists.");
            }

            if (result.IsValid)
            {
                var transaction = account.FindTransaction(metadata);

                transaction.MarkAsFailed();

                await _accountRepository.CommitAsync(true, cancellationToken);

                result.AddEntityToMetadata(transaction);
            }

            return result;
        }

        public async Task<IDomainValidationResult> MarkAccountTransactionAsCanceledAsync(
            IAccount account,
            TransactionMetadata metadata,
            CancellationToken cancellationToken = default
        )
        {
            var result = new DomainValidationResult();

            if (!account.TransactionExists(metadata))
            {
                result.AddFailedPreconditionError("Transaction does not exists.");
            }

            if (result.IsValid)
            {
                var transaction = account.FindTransaction(metadata);

                transaction.MarkAsCanceled();

                await _accountRepository.CommitAsync(true, cancellationToken);

                result.AddEntityToMetadata(transaction);
            }

            return result;
        }

        public async Task<IReadOnlyCollection<TransactionBundleDto>> FetchTransactionBundlesAsync(
            EnumTransactionType transactionType = EnumTransactionType.All,
            EnumCurrency currency = EnumCurrency.All,
            bool includeDisabled = false
        )
        {
            var transactionBundles = Options.Static.Transaction.Bundles.Where(
                transactionBundle => (transactionType == EnumTransactionType.All || transactionType == transactionBundle.Type) &&
                                     (currency == EnumCurrency.All || currency == transactionBundle.Currency.Type) &&
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

        private async Task ProcessChallengePayoutAsync(UserId userId, Currency currency, decimal amount)
        {
            var account = await _accountRepository.FindAccountOrNullAsync(userId);

            if (currency == Currency.Money)
            {
                var moneyAccount = new MoneyAccountDecorator(account!);

                moneyAccount.Payout(new Money(amount)).MarkAsSucceeded();
            }

            if (currency == Currency.Token)
            {
                var tokenAccount = new TokenAccountDecorator(account!);

                tokenAccount.Payout(new Token(amount)).MarkAsSucceeded();
            }
        }

        private async Task<IDomainValidationResult> CreateTransactionAsync(
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

            if (type == TransactionType.Withdrawal)
            {
                return await this.CreateWithdrawalTransactionAsync(account, money, cancellationToken);
            }

            if (type == TransactionType.Charge)
            {
                return await this.CreateChargeTransactionAsync(
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

            return DomainValidationResult.Failure("Unsupported transaction type for money currency.");
        }

        private async Task<IDomainValidationResult> CreateDepositTransactionAsync(
            IMoneyAccount account,
            Money money,
            CancellationToken cancellationToken = default
        )
        {
            var result = new DomainValidationResult();

            var transactionBundles = await this.FetchTransactionBundlesAsync(EnumTransactionType.Deposit, EnumCurrency.Money);

            if (transactionBundles.All(deposit => new decimal(deposit.Currency.Amount) != money.Amount))
            {
                result.AddFailedPreconditionError(
                    $"The amount of {nameof(Money)} is invalid. These are valid amounts: [{string.Join(", ", transactionBundles.Select(deposit => deposit.Currency.Amount))}].");
            }

            if (!account.IsDepositAvailable())
            {
                result.AddFailedPreconditionError($"Deposit is unavailable until {account.LastDeposit?.Add(MoneyAccountDecorator.DepositInterval)}. For security reason we limit the number of financial transaction that can be done in {MoneyAccountDecorator.DepositInterval.TotalHours} hours.");
            }

            if (result.IsValid)
            {
                var transaction = account.Deposit(money);

                await _accountRepository.CommitAsync(true, cancellationToken);

                result.AddEntityToMetadata(transaction);
            }

            return result;
        }

        private async Task<IDomainValidationResult> CreateWithdrawalTransactionAsync(
            IMoneyAccount account,
            Money money,
            CancellationToken cancellationToken = default
        )
        {
            var result = new DomainValidationResult();

            var transactionBundles = await this.FetchTransactionBundlesAsync(EnumTransactionType.Withdrawal, EnumCurrency.Money);

            if (transactionBundles.All(withdrawal => new decimal(withdrawal.Currency.Amount) != money.Amount))
            {
                result.AddFailedPreconditionError(
                    $"The amount of {nameof(Money)} is invalid. These are valid amounts: [{string.Join(", ", transactionBundles.Select(deposit => deposit.Currency.Amount))}].");
            }

            if (!account.HaveSufficientMoney(money))
            {
                result.AddFailedPreconditionError("Insufficient funds.");
            }

            if (!account.IsWithdrawalAvailable())
            {
                result.AddFailedPreconditionError($"Withdrawal is unavailable until {account.LastWithdraw?.Add(MoneyAccountDecorator.WithdrawalInterval)}. For security reason we limit the number of financial transaction that can be done in {MoneyAccountDecorator.WithdrawalInterval.TotalHours} hours.");
            }

            if (result.IsValid)
            {
                var transaction = account.Withdrawal(money);

                await _accountRepository.CommitAsync(true, cancellationToken);

                result.AddEntityToMetadata(transaction);
            }

            return result;
        }

        private async Task<IDomainValidationResult> CreateChargeTransactionAsync(
            IMoneyAccount account,
            Money money,
            TransactionMetadata? metadata = null,
            CancellationToken cancellationToken = default
        )
        {
            var result = new DomainValidationResult();

            if (!account.HaveSufficientMoney(money))
            {
                result.AddFailedPreconditionError("Insufficient funds.");
            }

            if (result.IsValid)
            {
                var transaction = account.Charge(money, metadata);

                await _accountRepository.CommitAsync(true, cancellationToken);

                result.AddEntityToMetadata(transaction);
            }

            return result;
        }

        private async Task<IDomainValidationResult> CreateDepositTransactionAsync(
            ITokenAccount account,
            Token token,
            CancellationToken cancellationToken = default
        )
        {
            var result = new DomainValidationResult();

            var transactionBundles = await this.FetchTransactionBundlesAsync(EnumTransactionType.Deposit, EnumCurrency.Token);

            if (transactionBundles.All(deposit => new decimal(deposit.Currency.Amount) != token.Amount))
            {
                result.AddFailedPreconditionError(
                    $"The amount of {nameof(Token)} is invalid. These are valid amounts: [{string.Join(", ", transactionBundles.Select(deposit => deposit.Currency.Amount))}].");
            }

            if (!account.IsDepositAvailable())
            {
                result.AddFailedPreconditionError($"Buying tokens is unavailable until {account.LastDeposit?.Add(TokenAccountDecorator.DepositInterval)}. For security reason we limit the number of financial transaction that can be done in {TokenAccountDecorator.DepositInterval.TotalHours} hours.");
            }

            if (result.IsValid)
            {
                var transaction = account.Deposit(token);

                await _accountRepository.CommitAsync(true, cancellationToken);

                result.AddEntityToMetadata(transaction);
            }

            return result;
        }

        private async Task<IDomainValidationResult> CreateChargeTransactionAsync(
            ITokenAccount account,
            Token token,
            TransactionMetadata? metadata = null,
            CancellationToken cancellationToken = default
        )
        {
            var result = new DomainValidationResult();

            if (!account.HaveSufficientMoney(token))
            {
                result.AddFailedPreconditionError("Insufficient funds.");
            }

            if (result.IsValid)
            {
                var transaction = account.Charge(token, metadata);

                await _accountRepository.CommitAsync(true, cancellationToken);

                result.AddEntityToMetadata(transaction);
            }

            return result;
        }

        private async Task<IDomainValidationResult> CreateTransactionAsync(
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

            if (type == TransactionType.Promotion)
            {
                return await this.CreatePromotionTransactionAsync(
                    account,
                    token,
                    metadata,
                    cancellationToken);
            }

            return DomainValidationResult.Failure("Unsupported transaction type for token currency.");
        }

        private async Task<IDomainValidationResult> CreatePromotionTransactionAsync(
            ITokenAccount account,
            Token token,
            TransactionMetadata? metadata = null,
            CancellationToken cancellationToken = default
        )
        {
            var result = new DomainValidationResult();

            if (result.IsValid)
            {
                var transaction = account.Promotion(token, metadata);

                transaction.MarkAsSucceeded();

                await _accountRepository.CommitAsync(true, cancellationToken);

                result.AddEntityToMetadata(transaction);
            }

            return result;
        }

        private async Task<IDomainValidationResult> CreatePromotionTransactionAsync(
            IMoneyAccount account,
            Money money,
            TransactionMetadata? metadata = null,
            CancellationToken cancellationToken = default
        )
        {
            var result = new DomainValidationResult();

            if (result.IsValid)
            {
                var transaction = account.Promotion(money, metadata);

                transaction.MarkAsSucceeded();

                await _accountRepository.CommitAsync(true, cancellationToken);

                result.AddEntityToMetadata(transaction);
            }

            return result;
        }
    }
}
