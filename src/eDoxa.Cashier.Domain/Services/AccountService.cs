// Filename: AccountService.cs
// Date Created: 2019-06-08
// 
// ================================================
// Copyright © 2019, eDoxa. All rights reserved.
// 
// This file is subject to the terms and conditions
// defined in file 'LICENSE.md', which is part of
// this source code package.

using System;
using System.Runtime.ExceptionServices;
using System.Threading;
using System.Threading.Tasks;

using eDoxa.Cashier.Domain.Abstractions;
using eDoxa.Cashier.Domain.Abstractions.Services;
using eDoxa.Cashier.Domain.AggregateModels.AccountAggregate;
using eDoxa.Cashier.Domain.AggregateModels.UserAggregate;
using eDoxa.Cashier.Domain.Extensions;
using eDoxa.Cashier.Domain.Repositories;
using eDoxa.Seedwork.Common;
using eDoxa.Seedwork.Common.Abstactions;
using eDoxa.Stripe.Abstractions;

namespace eDoxa.Cashier.Domain.Services
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

        public async Task<ITransaction> WithdrawAsync(UserId userId, Money money, CancellationToken cancellationToken = default)
        {
            var account = await _accountRepository.GetAccountAsync(userId);

            var accountMoney = new AccountMoney(account);

            var transaction = accountMoney.Withdraw(money);

            await _accountRepository.UnitOfWork.CommitAndDispatchDomainEventsAsync(cancellationToken);

            try
            {
                await _stripeService.CreateTransferAsync(
                    account.User.GetConnectAccountId(),
                    new Price(money).ToCents(),
                    transaction.Id,
                    transaction.Description.ToString(),
                    cancellationToken
                );

                transaction = accountMoney.CompleteTransaction(transaction);

                await _accountRepository.UnitOfWork.CommitAndDispatchDomainEventsAsync(cancellationToken);

                return transaction as Transaction;
            }
            catch (Exception exception)
            {
                transaction = accountMoney.FailureTransaction(transaction, exception.Message);

                await _accountRepository.UnitOfWork.CommitAndDispatchDomainEventsAsync(cancellationToken);

                ExceptionDispatchInfo.Capture(exception.InnerException).Throw();

                throw new InvalidOperationException(transaction.Failure.ToString(), exception);
            }
        }

        public async Task<ITransaction> DepositAsync(UserId userId, ICurrency currency, CancellationToken cancellationToken = default)
        {
            var account = await _accountRepository.GetAccountAsync(userId);

            switch (currency)
            {
                case Money money:
                {
                    var moneyAccount = new AccountMoney(account);

                    return await this.DepositAsync(account.User, moneyAccount, money, cancellationToken);
                }

                case Token token:
                {
                    var tokenAccount = new AccountToken(account);

                    return await this.DepositAsync(account.User, tokenAccount, token, cancellationToken);
                }

                default:
                {
                    throw new InvalidOperationException();
                }
            }
        }

        private async Task<ITransaction> DepositAsync<TCurrency>(
            User user,
            IAccount<TCurrency> account,
            TCurrency currency,
            CancellationToken cancellationToken = default
        )
        where TCurrency : Currency
        {
            var transaction = account.Deposit(currency);

            await _accountRepository.UnitOfWork.CommitAndDispatchDomainEventsAsync(cancellationToken);

            try
            {
                await _stripeService.CreateInvoiceAsync(
                    user.GetCustomerId(),
                    new Price(currency).ToCents(),
                    transaction.Id,
                    transaction.Description.ToString(),
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

                ExceptionDispatchInfo.Capture(exception.InnerException).Throw();

                throw new InvalidOperationException(transaction.Failure.ToString());
            }
        }
    }
}
