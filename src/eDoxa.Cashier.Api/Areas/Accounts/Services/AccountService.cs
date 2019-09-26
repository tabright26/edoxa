// Filename: AccountService.cs
// Date Created: 2019-09-16
// 
// ================================================
// Copyright © 2019, eDoxa. All rights reserved.

using System;
using System.Linq;
using System.Threading;
using System.Threading.Tasks;

using eDoxa.Cashier.Api.IntegrationEvents;
using eDoxa.Cashier.Domain.AggregateModels;
using eDoxa.Cashier.Domain.AggregateModels.AccountAggregate;
using eDoxa.Cashier.Domain.Repositories;
using eDoxa.Cashier.Domain.Services;
using eDoxa.Cashier.Domain.Validators;
using eDoxa.Seedwork.Application.Validations.Extensions;
using eDoxa.ServiceBus.Abstractions;

using FluentValidation.Results;

namespace eDoxa.Cashier.Api.Areas.Accounts.Services
{
    public sealed class AccountService : IAccountService
    {
        private readonly IAccountRepository _accountRepository;
        private readonly IServiceBusPublisher _serviceBusPublisher;

        public AccountService(IAccountRepository accountRepository, IServiceBusPublisher serviceBusPublisher)
        {
            _accountRepository = accountRepository;
            _serviceBusPublisher = serviceBusPublisher;
        }

        public async Task<ValidationResult> WithdrawalAsync(
            IMoneyAccount account,
            ICurrency currency,
            string connectAccountId,
            CancellationToken cancellationToken = default
        )
        {
            switch (currency)
            {
                case Money money:
                {
                    if (Money.WithdrawalAmounts().All(withdrawal => withdrawal.Amount != money.Amount))
                    {
                        var failure = new ValidationFailure(
                            string.Empty,
                            $"The amount of {nameof(Money)} is invalid. These are valid amounts: [{string.Join(", ", Money.WithdrawalAmounts().Select(deposit => deposit.Amount))}].");

                        return failure.ToResult();
                    }

                    var validator = new WithdrawalMoneyValidator(money);

                    var result = validator.Validate(account);

                    if (result.IsValid)
                    {
                        var transaction = account.Withdrawal(money);

                        await _accountRepository.CommitAsync(cancellationToken);

                        await _serviceBusPublisher.PublishAsync(
                            new UserAccountWithdrawalIntegrationEvent(
                                transaction.Id,
                                transaction.Description.Text,
                                connectAccountId,
                                transaction.Price.ToCents()));
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
            string customerId,
            CancellationToken cancellationToken = default
        )
        {
            switch (currency)
            {
                case Money money:
                {
                    if (Money.DepositAmounts().All(deposit => deposit.Amount != money.Amount))
                    {
                        var failure = new ValidationFailure(
                            string.Empty,
                            $"The amount of {nameof(Money)} is invalid. These are valid amounts: [{string.Join(", ", Money.DepositAmounts().Select(deposit => deposit.Amount))}].");

                        return failure.ToResult();
                    }

                    var moneyAccount = new MoneyAccount(account);

                    var validator = new DepositMoneyValidator();

                    var result = validator.Validate(moneyAccount);

                    if (result.IsValid)
                    {
                        await this.DepositAsync(
                            moneyAccount,
                            money,
                            customerId,
                            cancellationToken);
                    }

                    return result;
                }

                case Token token:
                {
                    if (Token.DepositAmounts().All(deposit => deposit.Amount != token.Amount))
                    {
                        var failure = new ValidationFailure(
                            string.Empty,
                            $"The amount of {nameof(Token)} is invalid. These are valid amounts: [{string.Join(", ", Token.DepositAmounts().Select(deposit => deposit.Amount))}].");

                        return failure.ToResult();
                    }

                    var tokenAccount = new TokenAccount(account);

                    var validator = new DepositTokenValidator();

                    var result = validator.Validate(tokenAccount);

                    if (result.IsValid)
                    {
                        await this.DepositAsync(
                            tokenAccount,
                            token,
                            customerId,
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
            IAccount<TCurrency> account,
            TCurrency currency,
            string customerId,
            CancellationToken cancellationToken = default
        )
        where TCurrency : ICurrency
        {
            var transaction = account.Deposit(currency);

            await _accountRepository.CommitAsync(cancellationToken);

            await _serviceBusPublisher.PublishAsync(
                new UserAccountDepositIntegrationEvent(
                    transaction.Id,
                    transaction.Description.Text,
                    customerId,
                    transaction.Price.ToCents()));
        }
    }
}
