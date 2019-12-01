// Filename: AccountService.cs
// Date Created: 2019-10-06
// 
// ================================================
// Copyright © 2019, eDoxa. All rights reserved.

using System;
using System.Collections.Generic;
using System.Collections.Immutable;
using System.Linq;
using System.Threading;
using System.Threading.Tasks;

using eDoxa.Cashier.Api.Areas.Accounts.Services.Abstractions;
using eDoxa.Cashier.Api.IntegrationEvents.Extensions;
using eDoxa.Cashier.Domain.AggregateModels;
using eDoxa.Cashier.Domain.AggregateModels.AccountAggregate;
using eDoxa.Cashier.Domain.AggregateModels.ChallengeAggregate;
using eDoxa.Cashier.Domain.AggregateModels.TransactionAggregate;
using eDoxa.Cashier.Domain.Repositories;
using eDoxa.Seedwork.Application.FluentValidation.Extensions;
using eDoxa.Seedwork.Domain.Misc;
using eDoxa.ServiceBus.Abstractions;

using FluentValidation.Results;

namespace eDoxa.Cashier.Api.Areas.Accounts.Services
{
    public sealed class AccountService : IAccountService
    {
        private readonly IAccountRepository _accountRepository;
        private readonly IBundlesService _bundlesService;
        private readonly IServiceBusPublisher _serviceBusPublisher;

        public AccountService(IAccountRepository accountRepository, IBundlesService bundlesService, IServiceBusPublisher serviceBusPublisher)
        {
            _accountRepository = accountRepository;
            _bundlesService = bundlesService;
            _serviceBusPublisher = serviceBusPublisher;
        }

        public async Task<ValidationResult> WithdrawalAsync(
            IAccount account,
            ICurrency currency,
            string email,
            CancellationToken cancellationToken = default
        )
        {
            switch (currency)
            {
                case Money money:
                {
                    var bundles = _bundlesService.FetchWithdrawalMoneyBundles();

                    if (bundles.All(withdrawal => withdrawal.Currency.Amount != money.Amount))
                    {
                        return new ValidationFailure(
                                string.Empty,
                                $"The amount of {nameof(Money)} is invalid. These are valid amounts: [{string.Join(", ", bundles.Select(deposit => deposit.Currency.Amount))}].")
                            .ToResult();
                    }

                    var moneyAccount = new MoneyAccount(account);

                    if (!moneyAccount.HaveSufficientMoney(money))
                    {
                        return new ValidationFailure("_error", "Insufficient funds.").ToResult();
                    }

                    if (!moneyAccount.IsWithdrawalAvailable())
                    {
                        return new ValidationFailure("_error", $"Withdrawal unavailable until {moneyAccount.LastWithdraw?.AddDays(7)}").ToResult();
                    }

                    var transaction = moneyAccount.Withdrawal(money, bundles);

                    await _accountRepository.CommitAsync(cancellationToken);

                    await _serviceBusPublisher.PublishUserAccountWithdrawalIntegrationEventAsync(
                        account.UserId,
                        email,
                        transaction.Id,
                        transaction.Description.Text,
                        transaction.Price.ToCents());

                    return new ValidationResult();
                }

                default:
                {
                    return new ValidationFailure(string.Empty, "The withdrawal of token is not supported.").ToResult();
                }
            }
        }

        public async Task<ValidationResult> CreateTransactionAsync(
            IAccount account,
            decimal amount,
            Currency currency,
            TransactionId transactionId,
            TransactionType transactionType,
            TransactionMetadata? transactionMetadata = null,
            CancellationToken cancellationToken = default
        )
        {
            var result = TryGetCurrency(currency, amount, out var value);

            if (result.IsValid)
            {
                switch (value)
                {
                    case Money money:
                    {
                        return await this.CreateTransactionAsync(
                            new MoneyAccount(account),
                            money,
                            transactionId,
                            transactionType,
                            transactionMetadata,
                            cancellationToken);
                    }

                    case Token token:
                    {
                        return await this.CreateTransactionAsync(
                            new TokenAccount(account),
                            token,
                            transactionId,
                            transactionType,
                            transactionMetadata,
                            cancellationToken);
                    }

                    case null:
                    {
                        return new ValidationFailure("_error", "Invalid currency.").ToResult();
                    }
                }
            }

            return result;
        }

        public async Task<IAccount?> FindUserAccountAsync(UserId userId)
        {
            return await _accountRepository.FindUserAccountAsync(userId);
        }

        // TODO: Need to be refactored.
        public async Task PayoutChallengeAsync(IChallenge challenge, /*TODO: Create object Scoreboard*/ IDictionary<UserId, decimal?> scoreboard, CancellationToken cancellationToken = default)
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

        public async Task<ValidationResult> DepositAsync(
            IAccount account,
            ICurrency currency,
            string email,
            CancellationToken cancellationToken = default
        )
        {
            switch (currency)
            {
                case Money money:
                {
                    var bundles = _bundlesService.FetchDepositMoneyBundles();

                    if (bundles.All(deposit => deposit.Currency.Amount != money.Amount))
                    {
                        return new ValidationFailure(
                                string.Empty,
                                $"The amount of {nameof(Money)} is invalid. These are valid amounts: [{string.Join(", ", bundles.Select(deposit => deposit.Currency.Amount))}].")
                            .ToResult();
                    }

                    var moneyAccount = new MoneyAccount(account);

                    if (!moneyAccount.IsDepositAvailable())
                    {
                        return new ValidationFailure("_error", $"Deposit unavailable until {moneyAccount.LastDeposit?.AddDays(1)}").ToResult();
                    }

                    await this.DepositAsync(
                        account.UserId,
                        moneyAccount,
                        money,
                        bundles,
                        email,
                        cancellationToken);

                    return new ValidationResult();
                }

                case Token token:
                {
                    var bundles = _bundlesService.FetchDepositTokenBundles();

                    if (bundles.All(deposit => deposit.Currency.Amount != token.Amount))
                    {
                        return new ValidationFailure(
                                "_error",
                                $"The amount of {nameof(Token)} is invalid. These are valid amounts: [{string.Join(", ", bundles.Select(deposit => deposit.Currency.Amount))}].")
                            .ToResult();
                    }

                    var tokenAccount = new TokenAccount(account);

                    if (!tokenAccount.IsDepositAvailable())
                    {
                        return new ValidationFailure("_error", $"Deposit unavailable until {tokenAccount.LastDeposit?.AddDays(1)}").ToResult();
                    }

                    await this.DepositAsync(
                        account.UserId,
                        tokenAccount,
                        token,
                        bundles,
                        email,
                        cancellationToken);

                    return new ValidationResult();
                }

                default:
                {
                    throw new InvalidOperationException();
                }
            }
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

        public async Task<ValidationResult> CreateTransactionAsync(
            IMoneyAccount account,
            Money money,
            TransactionId transactionId,
            TransactionType transactionType,
            TransactionMetadata? transactionMetadata = null,
            CancellationToken cancellationToken = default
        )
        {
            if (transactionType == TransactionType.Charge)
            {
                if (!account.HaveSufficientMoney(money))
                {
                    return new ValidationFailure("_error", "Insufficient funds.").ToResult();
                }

                account.Charge(transactionId, money, transactionMetadata);

                await _accountRepository.CommitAsync(cancellationToken);

                return new ValidationResult();
            }

            return new ValidationFailure("_error", "Unsupported transaction type for money.").ToResult();
        }

        public async Task<ValidationResult> CreateTransactionAsync(
            ITokenAccount account,
            Token token,
            TransactionId transactionId,
            TransactionType transactionType,
            TransactionMetadata? transactionMetadata = null,
            CancellationToken cancellationToken = default
        )
        {
            if (transactionType == TransactionType.Charge)
            {
                if (!account.HaveSufficientMoney(token))
                {
                    return new ValidationFailure("_error", "Insufficient funds.").ToResult();
                }

                account.Charge(transactionId, token, transactionMetadata);

                await _accountRepository.CommitAsync(cancellationToken);

                return new ValidationResult();
            }

            return new ValidationFailure("_error", "Unsupported transaction type for money.").ToResult();
        }

        // TODO: Need to be refactored.
        private static ValidationResult TryGetCurrency(Currency currency, decimal amount, out ICurrency? result)
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

            return new ValidationResult();
        }

        private async Task DepositAsync<TCurrency>(
            UserId userId,
            IAccount<TCurrency> account,
            TCurrency currency,
            IImmutableSet<Bundle> bundles,
            string email,
            CancellationToken cancellationToken = default
        )
        where TCurrency : ICurrency
        {
            var transaction = account.Deposit(currency, bundles);

            await _accountRepository.CommitAsync(cancellationToken);

            // TODO: Need to be refactored as DomainEvent
            await _serviceBusPublisher.PublishUserAccountDepositIntegrationEventAsync(
                userId,
                email,
                transaction.Id,
                transaction.Description.Text,
                transaction.Price.ToCents());
        }
    }
}
