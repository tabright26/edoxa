// Filename: AccountService.cs
// Date Created: 2019-12-03
// 
// ================================================
// Copyright © 2019, eDoxa. All rights reserved.

using System.Collections.Generic;
using System.Linq;
using System.Threading;
using System.Threading.Tasks;

using eDoxa.Cashier.Domain.AggregateModels;
using eDoxa.Cashier.Domain.AggregateModels.AccountAggregate;
using eDoxa.Cashier.Domain.AggregateModels.ChallengeAggregate;
using eDoxa.Cashier.Domain.Repositories;
using eDoxa.Cashier.Domain.Services;
using eDoxa.Seedwork.Domain;
using eDoxa.Seedwork.Domain.Misc;

namespace eDoxa.Cashier.Api.Services
{
    public sealed class AccountService : IAccountService
    {
        private readonly IAccountRepository _accountRepository;
        private readonly IBundleService _bundleService;

        public AccountService(IAccountRepository accountRepository, IBundleService bundleService)
        {
            _accountRepository = accountRepository;
            _bundleService = bundleService;
        }

        public async Task<IAccount?> FindAccountAsync(UserId userId)
        {
            return await _accountRepository.FindAccountAsync(userId);
        }

        // TODO: Need to be refactored.
        public async Task PayoutChallengeAsync(
            IChallenge challenge, /*TODO: Create object Scoreboard*/
            IDictionary<UserId, decimal?> scoreboard,
            CancellationToken cancellationToken = default
        )
        {
            var payout = challenge.Payout;

            var winners = new Queue<UserId>(scoreboard.OrderByDescending(item => item.Value).Select(item => item.Key).Take(payout.Entries));

            var losers = new List<UserId>(scoreboard.OrderByDescending(item => item.Value).Select(item => item.Key).Skip(payout.Entries));

            foreach (var bucket in payout.Buckets)
            {
                for (var index = 0; index < bucket.Size; index++)
                {
                    var user = winners.Dequeue();

                    var score = scoreboard[user];

                    if (score == null)
                    {
                        // TODO: Need to be refactored.
                        await this.PayoutChallengeAsync(user, payout.PrizePool.Currency, 0);
                    }
                    else
                    {
                        // TODO: Need to be refactored.
                        await this.PayoutChallengeAsync(user, payout.PrizePool.Currency, bucket.Prize);
                    }
                }
            }

            foreach (var user in losers)
            {
                var score = scoreboard[user];

                if (score == null)
                {
                    // TODO: Need to be refactored.
                    await this.PayoutChallengeAsync(user, payout.PrizePool.Currency, 0);
                }
                else
                {
                    // TODO: Need to be refactored.
                    await this.PayoutChallengeAsync(user, payout.PrizePool.Currency, Token.MinValue);
                }
            }

            await _accountRepository.CommitAsync(cancellationToken);
        }

        public async Task CreateAccountAsync(UserId userId)
        {
            var account = new Account(userId);

            _accountRepository.Create(account);

            await _accountRepository.CommitAsync();
        }

        private async Task PayoutChallengeAsync(UserId userId, Currency currency, decimal amount)
        {
            var account = await _accountRepository.FindAccountAsync(userId);

            if (currency == Currency.Money)
            {
                var moneyAccount = new MoneyAccountDecorator(account!);

                moneyAccount.Payout(new Money(amount));
            }

            if (currency == Currency.Token)
            {
                var tokenAccount = new TokenAccountDecorator(account!);

                tokenAccount.Payout(new Token(amount));
            }
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
            var result = TryGetCurrency(currency, amount, out var value);

            if (result.IsValid)
            {
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

            return DomainValidationResult.Failure("Invalid currency.");
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

            return DomainValidationResult.Failure("Unsupported transaction type for money currency.");
        }

        private async Task<IDomainValidationResult> CreateDepositTransactionAsync(
            IMoneyAccount account,
            Money money,
            CancellationToken cancellationToken = default
        )
        {
            var result = new DomainValidationResult();

            var bundles = _bundleService.FetchDepositMoneyBundles();

            if (bundles.All(deposit => deposit.Currency.Amount != money.Amount))
            {
                result.AddDomainValidationError(
                    "_error",
                    $"The amount of {nameof(Money)} is invalid. These are valid amounts: [{string.Join(", ", bundles.Select(deposit => deposit.Currency.Amount))}].");
            }

            if (!account.IsDepositAvailable())
            {
                result.AddDomainValidationError("_error", $"Deposit unavailable until {account.LastDeposit?.AddDays(1)}");
            }

            if (result.IsValid)
            {
                var transaction = account.Deposit(money);

                await _accountRepository.CommitAsync(cancellationToken);

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

            var bundles = _bundleService.FetchWithdrawalMoneyBundles();

            if (bundles.All(withdrawal => withdrawal.Currency.Amount != money.Amount))
            {
                result.AddDomainValidationError(
                    "_error",
                    $"The amount of {nameof(Money)} is invalid. These are valid amounts: [{string.Join(", ", bundles.Select(deposit => deposit.Currency.Amount))}].");
            }

            if (!account.HaveSufficientMoney(money))
            {
                result.AddDomainValidationError("_error", "Insufficient funds.");
            }

            if (!account.IsWithdrawalAvailable())
            {
                result.AddDomainValidationError("_error", $"Withdrawal unavailable until {account.LastWithdraw?.AddDays(7)}");
            }

            if (result.IsValid)
            {
                var transaction = account.Withdrawal(money);

                await _accountRepository.CommitAsync(cancellationToken);

                result.AddEntityToMetadata(transaction);

                //await _serviceBusPublisher.PublishUserAccountWithdrawalIntegrationEventAsync(
                //    UserId.Parse(transactionMetadata["USERID"]),
                //    transactionMetadata["EMAIL"],
                //    transaction.Id,
                //    transaction.Description.Text,
                //    transaction.Price.ToCents()); 
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
                result.AddDomainValidationError("_error", "Insufficient funds.");
            }

            if (result.IsValid)
            {
                var transaction = account.Charge(money, metadata);

                await _accountRepository.CommitAsync(cancellationToken);

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

            var bundles = _bundleService.FetchDepositTokenBundles();

            if (bundles.All(deposit => deposit.Currency.Amount != token.Amount))
            {
                result.AddDomainValidationError(
                    "_error",
                    $"The amount of {nameof(Token)} is invalid. These are valid amounts: [{string.Join(", ", bundles.Select(deposit => deposit.Currency.Amount))}].");
            }

            if (!account.IsDepositAvailable())
            {
                result.AddDomainValidationError("_error", $"Deposit unavailable until {account.LastDeposit?.AddDays(1)}");
            }

            if (result.IsValid)
            {
                var transaction = account.Deposit(token);

                await _accountRepository.CommitAsync(cancellationToken);

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
                result.AddDomainValidationError("_error", "Insufficient funds.");
            }

            if (result.IsValid)
            {
                var transaction = account.Charge(token, metadata);

                await _accountRepository.CommitAsync(cancellationToken);

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

            return DomainValidationResult.Failure("Unsupported transaction type for token currency.");
        }

        // TODO: Need to be refactored.
        private static DomainValidationResult TryGetCurrency(Currency currency, decimal amount, out ICurrency? result)
        {
            result = null;

            if (currency == Currency.Money)
            {
                // TODO: Validation.

                result = new Money(amount);
            }

            if (currency == Currency.Token)
            {
                // TODO: Validation.

                result = new Token(amount);
            }

            return new DomainValidationResult();
        }

        public async Task<ITransaction?> FindAccountTransactionAsync(IAccount account, TransactionId transactionId)
        {
            return await Task.FromResult(account.TransactionExists(transactionId) ? account.FindTransaction(transactionId) : null);
        }

        public async Task<IDomainValidationResult> MarkAccountTransactionAsSuccededAsync(IAccount account, TransactionId transactionId, CancellationToken cancellationToken = default)
        {
            var result = new DomainValidationResult();

            if (!account.TransactionExists(transactionId))
            {
                result.AddDomainValidationError("Transaction does not exists.");
            }

            if (result.IsValid)
            {
                var transaction = account.FindTransaction(transactionId);

                transaction.MarkAsSucceded();

                await _accountRepository.CommitAsync(cancellationToken);

                result.AddEntityToMetadata(transaction);
            }

            return result;
        }

        public async Task<IDomainValidationResult> MarkAccountTransactionAsFailedAsync(IAccount account, TransactionId transactionId, CancellationToken cancellationToken = default)
        {
            var result = new DomainValidationResult();

            if (!account.TransactionExists(transactionId))
            {
                result.AddDomainValidationError("Transaction does not exists.");
            }

            if (result.IsValid)
            {
                var transaction = account.FindTransaction(transactionId);

                transaction.MarkAsFailed();

                await _accountRepository.CommitAsync(cancellationToken);

                result.AddEntityToMetadata(transaction);
            }

            return result;
        }

        public async Task<IDomainValidationResult> MarkAccountTransactionAsCanceledAsync(IAccount account, TransactionId transactionId, CancellationToken cancellationToken = default)
        {
            var result = new DomainValidationResult();

            if (!account.TransactionExists(transactionId))
            {
                result.AddDomainValidationError("Transaction does not exists.");
            }

            if (result.IsValid)
            {
                var transaction = account.FindTransaction(transactionId);

                transaction.MarkAsCanceled();

                await _accountRepository.CommitAsync(cancellationToken);

                result.AddEntityToMetadata(transaction);
            }

            return result;
        }
    }
}
