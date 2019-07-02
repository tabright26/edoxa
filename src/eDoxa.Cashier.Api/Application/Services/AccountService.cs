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

using eDoxa.Cashier.Api.Extensions;
using eDoxa.Cashier.Domain.AggregateModels;
using eDoxa.Cashier.Domain.AggregateModels.AccountAggregate;
using eDoxa.Cashier.Domain.AggregateModels.UserAggregate;
using eDoxa.Cashier.Domain.Queries;
using eDoxa.Cashier.Domain.Repositories;
using eDoxa.Cashier.Domain.Services;
using eDoxa.Seedwork.Common.Abstactions;
using eDoxa.Seedwork.Common.ValueObjects;
using eDoxa.Stripe.Abstractions;

namespace eDoxa.Cashier.Api.Application.Services
{
    public sealed class AccountService : IAccountService
    {
        private readonly IAccountRepository _accountRepository;
        private readonly IUserQuery _userQuery;
        private readonly IStripeService _stripeService;

        public AccountService(IAccountRepository accountRepository, IUserQuery userQuery, IStripeService stripeService)
        {
            _accountRepository = accountRepository;
            _userQuery = userQuery;
            _stripeService = stripeService;
        }

        public async Task<ITransaction> WithdrawAsync(UserId userId, Money money, CancellationToken cancellationToken = default)
        {
            var user = await _userQuery.FindUserAsync(userId);

            var account = await _accountRepository.FindUserAccountAsync(userId);

            var accountMoney = new AccountMoney(account);

            var transaction = accountMoney.Withdraw(money);

            await _accountRepository.CommitAsync(cancellationToken);

            try
            {
                await _stripeService.CreateTransferAsync(
                    user.GetConnectAccountId(),
                    new Price(money).ToCents(),
                    transaction.Id,
                    transaction.Description.ToString(),
                    cancellationToken
                );

                transaction.MarkAsSucceded();

                await _accountRepository.CommitAsync(cancellationToken);

                return transaction as Transaction;
            }
            catch (Exception exception)
            {
                transaction.MarkAsFailed();

                await _accountRepository.CommitAsync(cancellationToken);

                ExceptionDispatchInfo.Capture(exception.InnerException).Throw();

                throw new InvalidOperationException(transaction.ToString(), exception);
            }
        }

        public async Task<ITransaction> DepositAsync(UserId userId, ICurrency currency, CancellationToken cancellationToken = default)
        {
            var user = await _userQuery.FindUserAsync(userId);

            var account = await _accountRepository.FindUserAccountAsync(userId);

            switch (currency)
            {
                case Money money:
                {
                    var moneyAccount = new AccountMoney(account);

                    return await this.DepositAsync(user, moneyAccount, money, cancellationToken);
                }

                case Token token:
                {
                    var tokenAccount = new AccountToken(account);

                    return await this.DepositAsync(user, tokenAccount, token, cancellationToken);
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

            await _accountRepository.CommitAsync(cancellationToken);

            try
            {
                await _stripeService.CreateInvoiceAsync(
                    user.GetCustomerId(),
                    new Price(currency).ToCents(),
                    transaction.Id,
                    transaction.Description.ToString(),
                    cancellationToken
                );

                transaction.MarkAsSucceded();

                await _accountRepository.CommitAsync(cancellationToken);

                return transaction as Transaction;
            }
            catch (Exception exception)
            {
                transaction.MarkAsFailed();

                await _accountRepository.CommitAsync(cancellationToken);

                ExceptionDispatchInfo.Capture(exception.InnerException).Throw();

                throw new InvalidOperationException(transaction.ToString());
            }
        }
    }
}
