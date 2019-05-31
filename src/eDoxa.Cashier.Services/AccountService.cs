// Filename: AccountService.cs
// Date Created: 2019-05-29
// 
// ================================================
// Copyright © 2019, eDoxa. All rights reserved.
// 
// This file is subject to the terms and conditions
// defined in file 'LICENSE.md', which is part of
// this source code package.

using System;
using System.Threading;
using System.Threading.Tasks;

using eDoxa.Cashier.Domain.Abstractions;
using eDoxa.Cashier.Domain.AggregateModels.AccountAggregate;
using eDoxa.Cashier.Domain.AggregateModels.UserAggregate;
using eDoxa.Cashier.Domain.Repositories;
using eDoxa.Cashier.Domain.Validators;
using eDoxa.Cashier.Services.Abstractions;
using eDoxa.Cashier.Services.Stripe.Abstractions;
using eDoxa.Functional;
using eDoxa.Seedwork.Application.Validations.Extensions;
using eDoxa.Seedwork.Domain.Common;
using eDoxa.Seedwork.Domain.Common.Abstactions;

using FluentValidation.Results;

namespace eDoxa.Cashier.Services
{
    public sealed class AccountService : IAccountService
    {
        private readonly IAccountRepository _accountRepository;
        private readonly IStripeService _stripeService;

        public AccountService(IAccountRepository accountRepository, IStripeService stripeService)
        {
            _accountRepository = accountRepository;
            _stripeService = stripeService;
        }

        public async Task<Either<ValidationResult, ITransaction>> WithdrawAsync(UserId userId, Money money, CancellationToken cancellationToken = default)
        {
            var account = await _accountRepository.GetAccountAsync(userId);

            var accountMoney = new AccountMoney(account);

            var validator = new WithdrawMoneyValidator(money);

            var result = validator.Validate(accountMoney);

            if (!result.IsValid)
            {
                return result;
            }

            var transaction = accountMoney.Withdraw(money);

            await _accountRepository.UnitOfWork.CommitAndDispatchDomainEventsAsync(cancellationToken);

            try
            {
                await _stripeService.CreateTransferAsync(account.User.AccountId, money, transaction, cancellationToken);

                transaction = accountMoney.CompleteTransaction(transaction);

                await _accountRepository.UnitOfWork.CommitAndDispatchDomainEventsAsync(cancellationToken);

                return transaction as Transaction;
            }
            catch (Exception exception)
            {
                transaction = accountMoney.FailureTransaction(transaction, exception.Message);

                await _accountRepository.UnitOfWork.CommitAndDispatchDomainEventsAsync(cancellationToken);

                return new ValidationFailure(null, transaction.Failure.ToString()).ToResult();
            }
        }

        public async Task<Either<ValidationResult, ITransaction>> DepositAsync(UserId userId, ICurrency currency, CancellationToken cancellationToken = default)
        {
            var account = await _accountRepository.GetAccountAsync(userId);

            switch (currency)
            {
                case Money money:
                {
                    var moneyAccount = new AccountMoney(account);

                    var validator = new DepositMoneyValidator();

                    var result = validator.Validate(moneyAccount);

                    if (!result.IsValid)
                    {
                        return result;
                    }

                    return await this.DepositAsync(account.User, moneyAccount, money, cancellationToken);
                }

                case Token token:
                {
                    var tokenAccount = new AccountToken(account);

                    var validator = new DepositTokenValidator();

                    var result = validator.Validate(tokenAccount);

                    if (!result.IsValid)
                    {
                        return result;
                    }

                    return await this.DepositAsync(account.User, tokenAccount, token, cancellationToken);
                }

                default:
                {
                    return new ValidationFailure(null, "Invalid currency type.").ToResult();
                }
            }
        }

        private async Task<Either<ValidationResult, ITransaction>> DepositAsync<TCurrency>(
            User user,
            IAccount<TCurrency> account,
            TCurrency currency,
            CancellationToken cancellationToken = default
        )
        where TCurrency : Currency
        {
            var customer = await _stripeService.GetCustomerAsync(user.CustomerId, cancellationToken);

            if (customer.DefaultSource == null)
            {
                return new ValidationFailure(null, "There are no credit cards associated with this account.").ToResult();
            }

            var transaction = account.Deposit(currency);

            await _accountRepository.UnitOfWork.CommitAndDispatchDomainEventsAsync(cancellationToken);

            try
            {
                await _stripeService.CreateInvoiceAsync(
                    user.CustomerId,
                    currency,
                    transaction,
                    cancellationToken
                );

                transaction = account.CompleteTransaction(transaction);

                await _accountRepository.UnitOfWork.CommitAndDispatchDomainEventsAsync(cancellationToken);

                return transaction as Transaction;
            }
            catch (Exception exception)
            {
                transaction = account.FailureTransaction(transaction, exception.Message);

                await _accountRepository.UnitOfWork.CommitAndDispatchDomainEventsAsync(cancellationToken);

                return new ValidationFailure(null, transaction.Failure.ToString()).ToResult();
            }
        }
    }
}
