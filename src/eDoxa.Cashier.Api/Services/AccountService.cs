// Filename: AccountService.cs
// Date Created: 2019-12-03
// 
// ================================================
// Copyright © 2019, eDoxa. All rights reserved.

using System.Collections.Generic;
using System.Collections.Immutable;
using System.Linq;
using System.Threading;
using System.Threading.Tasks;

using eDoxa.Cashier.Domain.AggregateModels;
using eDoxa.Cashier.Domain.AggregateModels.AccountAggregate;
using eDoxa.Cashier.Domain.AggregateModels.ChallengeAggregate;
using eDoxa.Cashier.Domain.AggregateModels.TransactionAggregate;
using eDoxa.Cashier.Domain.Repositories;
using eDoxa.Cashier.Domain.Services;
using eDoxa.Seedwork.Domain;
using eDoxa.Seedwork.Domain.Misc;
using eDoxa.ServiceBus.Abstractions;

namespace eDoxa.Cashier.Api.Services
{
    public sealed class AccountService : IAccountService
    {
        private readonly IAccountRepository _accountRepository;
        private readonly IBundleService _bundleService;
        private readonly IServiceBusPublisher _serviceBusPublisher;

        public AccountService(IAccountRepository accountRepository, IBundleService bundleService, IServiceBusPublisher serviceBusPublisher)
        {
            _accountRepository = accountRepository;
            _bundleService = bundleService;
            _serviceBusPublisher = serviceBusPublisher;
        }

        public async Task<DomainValidationResult> CreateTransactionAsync(
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
                        new MoneyAccount(account),
                        money,
                        transactionType,
                        transactionMetadata,
                        cancellationToken);
                }

                if (value is Token token)
                {
                    return await this.CreateTransactionAsync(
                        new TokenAccount(account),
                        token,
                        transactionType,
                        transactionMetadata,
                        cancellationToken);
                }
            }

            return DomainValidationResult.Failure("Invalid currency.");
        }

        public async Task<IAccount?> FindUserAccountAsync(UserId userId)
        {
            return await _accountRepository.FindUserAccountAsync(userId);
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
            var account = await _accountRepository.FindUserAccountAsync(userId);

            if (currency == Currency.Money)
            {
                var moneyAccount = new MoneyAccount(account!);

                moneyAccount.Payout(new Money(amount));
            }

            if (currency == Currency.Token)
            {
                var tokenAccount = new TokenAccount(account!);

                tokenAccount.Payout(new Token(amount));
            }
        }

        private async Task<DomainValidationResult> CreateTransactionAsync(
            IMoneyAccount account,
            Money money,
            TransactionType transactionType,
            TransactionMetadata? transactionMetadata = null,
            CancellationToken cancellationToken = default
        )
        {
            if (transactionType == TransactionType.Deposit)
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
                    var transaction = account.Deposit(money, bundles);

                    await _accountRepository.CommitAsync(cancellationToken);

                    result.AddMetadataResponse(transaction);
                }

                return result;
            }

            if (transactionType == TransactionType.Withdrawal)
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
                    var transaction = account.Withdrawal(money, bundles);

                    await _accountRepository.CommitAsync(cancellationToken);

                    result.AddMetadataResponse(transaction);

                    //await _serviceBusPublisher.PublishUserAccountWithdrawalIntegrationEventAsync(
                    //    UserId.Parse(transactionMetadata["USERID"]),
                    //    transactionMetadata["EMAIL"],
                    //    transaction.Id,
                    //    transaction.Description.Text,
                    //    transaction.Price.ToCents()); 
                }

                return result;
            }

            if (transactionType == TransactionType.Charge)
            {
                var result = new DomainValidationResult();

                if (!account.HaveSufficientMoney(money))
                {
                    result.AddDomainValidationError("_error", "Insufficient funds.");
                }

                if (result.IsValid)
                {
                    var transaction = account.Charge(money, transactionMetadata);

                    await _accountRepository.CommitAsync(cancellationToken);

                    result.AddMetadataResponse(transaction);
                }

                return result;
            }

            return DomainValidationResult.Failure("Unsupported transaction type for money currency.");
        }

        private async Task<DomainValidationResult> CreateTransactionAsync(
            ITokenAccount account,
            Token token,
            TransactionType transactionType,
            TransactionMetadata? transactionMetadata = null,
            CancellationToken cancellationToken = default
        )
        {
            if (transactionType == TransactionType.Deposit)
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
                    var transaction = account.Deposit(token, bundles);

                    await _accountRepository.CommitAsync(cancellationToken);

                    result.AddMetadataResponse(transaction);
                }

                return result;
            }

            if (transactionType == TransactionType.Charge)
            {
                var result = new DomainValidationResult();

                if (!account.HaveSufficientMoney(token))
                {
                    result.AddDomainValidationError("_error", "Insufficient funds.");
                }

                if (result.IsValid)
                {
                    var transaction = account.Charge(token, transactionMetadata);

                    await _accountRepository.CommitAsync(cancellationToken);

                    result.AddMetadataResponse(transaction);
                }

                return result;
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

        private async Task DepositAsync<TCurrency>(
            IAccount<TCurrency> account,
            TCurrency currency,
            IImmutableSet<Bundle> bundles,
            CancellationToken cancellationToken = default
        )
        where TCurrency : ICurrency
        {
            var transaction = account.Deposit(currency, bundles);

            await _accountRepository.CommitAsync(cancellationToken);

            //// TODO: Need to be refactored as DomainEvent
            //await _serviceBusPublisher.PublishUserAccountDepositIntegrationEventAsync(
            //    userId,
            //    email,
            //    transaction.Id,
            //    transaction.Description.Text,
            //    transaction.Price.ToCents());
        }
    }
}
