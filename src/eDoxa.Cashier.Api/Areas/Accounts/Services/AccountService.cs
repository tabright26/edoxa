// Filename: AccountService.cs
// Date Created: 2019-10-06
// 
// ================================================
// Copyright © 2019, eDoxa. All rights reserved.

using System;
using System.Collections.Immutable;
using System.Linq;
using System.Threading;
using System.Threading.Tasks;

using eDoxa.Cashier.Api.IntegrationEvents.Extensions;
using eDoxa.Cashier.Domain.AggregateModels;
using eDoxa.Cashier.Domain.AggregateModels.AccountAggregate;
using eDoxa.Cashier.Domain.Repositories;
using eDoxa.Cashier.Domain.Services;
using eDoxa.Cashier.Domain.Validators;
using eDoxa.Seedwork.Application.Validations.Extensions;
using eDoxa.Seedwork.Domain.Miscs;
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

                    var validator = new WithdrawalMoneyValidator(money);

                    var result = validator.Validate(moneyAccount);

                    if (result.IsValid)
                    {
                        var transaction = moneyAccount.Withdrawal(money, bundles);

                        await _accountRepository.CommitAsync(cancellationToken);

                        await _serviceBusPublisher.PublishUserAccountWithdrawalIntegrationEventAsync(
                            account.UserId,
                            email,
                            transaction.Id,
                            transaction.Description.Text,
                            transaction.Price.ToCents());
                    }

                    return result;
                }

                case Token _:
                {
                    var result = new ValidationFailure(string.Empty, "The withdrawal of token is not supported.");

                    return result.ToResult();
                }

                default:
                {
                    throw new InvalidOperationException();
                }
            }
        }

        public async Task<IAccount?> FindUserAccountAsync(UserId userId)
        {
            return await _accountRepository.FindUserAccountAsync(userId);
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

                    var validator = new DepositMoneyValidator();

                    var result = validator.Validate(moneyAccount);

                    if (result.IsValid)
                    {
                        await this.DepositAsync(
                            account.UserId,
                            moneyAccount,
                            money,
                            bundles,
                            email,
                            cancellationToken);
                    }

                    return result;
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

                    var validator = new DepositTokenValidator();

                    var result = validator.Validate(tokenAccount);

                    if (result.IsValid)
                    {
                        await this.DepositAsync(
                            account.UserId,
                            tokenAccount,
                            token,
                            bundles,
                            email,
                            cancellationToken);
                    }

                    return result;
                }

                default:
                {
                    throw new InvalidOperationException();
                }
            }
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

            await _serviceBusPublisher.PublishUserAccountDepositIntegrationEventAsync(
                userId,
                email,
                transaction.Id,
                transaction.Description.Text,
                transaction.Price.ToCents());
        }
    }
}
