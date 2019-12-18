// Filename: AccountService.cs
// Date Created: 2019-12-08
// 
// ================================================
// Copyright © 2019, eDoxa. All rights reserved.

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

namespace eDoxa.Cashier.Api.Application.Services
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

        // TODO: Need to be refactored.
        public async Task<IDomainValidationResult> PayoutChallengeAsync(Scoreboard scoreboard, CancellationToken cancellationToken = default)
        {
            var result = new DomainValidationResult();

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
                            await this.PayoutChallengeAsync(user, scoreboard.PayoutCurrency, 0);
                        }
                        else
                        {
                            // TODO: Need to be refactored.
                            await this.PayoutChallengeAsync(user, scoreboard.PayoutCurrency, ladderGroup.Prize);
                        }
                    }
                }

                foreach (var user in scoreboard.Losers)
                {
                    var score = scoreboard[user];

                    if (score == null)
                    {
                        // TODO: Need to be refactored.
                        await this.PayoutChallengeAsync(user, scoreboard.PayoutCurrency, 0);
                    }
                    else
                    {
                        // TODO: Need to be refactored.
                        await this.PayoutChallengeAsync(user, scoreboard.PayoutCurrency, Token.MinValue);
                    }
                }

                await _accountRepository.CommitAsync(cancellationToken);
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

        public async Task<ITransaction?> FindAccountTransactionAsync(IAccount account, TransactionId transactionId)
        {
            return await Task.FromResult(account.TransactionExists(transactionId) ? account.FindTransaction(transactionId) : null);
        }

        public async Task<IDomainValidationResult> MarkAccountTransactionAsSuccededAsync(
            IAccount account,
            TransactionId transactionId,
            CancellationToken cancellationToken = default
        )
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

        public async Task<IDomainValidationResult> MarkAccountTransactionAsFailedAsync(
            IAccount account,
            TransactionId transactionId,
            CancellationToken cancellationToken = default
        )
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

        public async Task<IDomainValidationResult> MarkAccountTransactionAsCanceledAsync(
            IAccount account,
            TransactionId transactionId,
            CancellationToken cancellationToken = default
        )
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

        public async Task<IDomainValidationResult> MarkAccountTransactionAsSuccededAsync(
            IAccount account,
            TransactionMetadata metadata,
            CancellationToken cancellationToken = default
        )
        {
            var result = new DomainValidationResult();

            if (!account.TransactionExists(metadata))
            {
                result.AddDomainValidationError("Transaction does not exists.");
            }

            if (result.IsValid)
            {
                var transaction = account.FindTransaction(metadata);

                transaction.MarkAsSucceded();

                await _accountRepository.CommitAsync(cancellationToken);

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
                result.AddDomainValidationError("Transaction does not exists.");
            }

            if (result.IsValid)
            {
                var transaction = account.FindTransaction(metadata);

                transaction.MarkAsFailed();

                await _accountRepository.CommitAsync(cancellationToken);

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
                result.AddDomainValidationError("Transaction does not exists.");
            }

            if (result.IsValid)
            {
                var transaction = account.FindTransaction(metadata);

                transaction.MarkAsCanceled();

                await _accountRepository.CommitAsync(cancellationToken);

                result.AddEntityToMetadata(transaction);
            }

            return result;
        }

        private async Task PayoutChallengeAsync(UserId userId, Currency currency, decimal amount)
        {
            var account = await _accountRepository.FindAccountOrNullAsync(userId);

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
    }
}
