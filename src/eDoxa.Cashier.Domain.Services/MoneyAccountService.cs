// Filename: MoneyAccountService.cs
// Date Created: 2019-05-13
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

using eDoxa.Cashier.Domain.AggregateModels;
using eDoxa.Cashier.Domain.AggregateModels.MoneyAccountAggregate;
using eDoxa.Cashier.Domain.Repositories;
using eDoxa.Cashier.Domain.Services.Abstractions;
using eDoxa.Cashier.Domain.Services.Stripe.Abstractions;
using eDoxa.Cashier.Domain.Services.Stripe.Models;
using eDoxa.Functional;
using eDoxa.Seedwork.Domain.Validations;

namespace eDoxa.Cashier.Domain.Services
{
    public sealed class MoneyAccountService : IMoneyAccountService
    {
        private readonly IMoneyAccountRepository _moneyAccountRepository;
        private readonly IStripeService _stripeService;

        public MoneyAccountService(IMoneyAccountRepository moneyAccountRepository, IStripeService stripeService)
        {
            _moneyAccountRepository = moneyAccountRepository;
            _stripeService = stripeService;
        }

        public async Task CreateAccount(UserId userId)
        {
            _moneyAccountRepository.Create(new MoneyAccount(userId));

            await _moneyAccountRepository.UnitOfWork.CommitAndDispatchDomainEventsAsync();
        }

        public async Task<Either<ValidationError, TransactionStatus>> DepositAsync(
            UserId userId,
            MoneyBundle bundle,
            StripeCustomerId customerId,
            CancellationToken cancellationToken = default)
        {
            var account = await _moneyAccountRepository.FindUserAccountAsync(userId);

            var result = account.CanDeposit();

            if (result.Failure)
            {
                return result.ValidationError;
            }

            var customer = await _stripeService.GetCustomerAsync(customerId, cancellationToken);

            if (customer.DefaultSource == null)
            {
                return new ValidationError("There are no credit cards associated with this account.");
            }

            var transaction = account.Deposit(bundle.Amount);

            await _moneyAccountRepository.UnitOfWork.CommitAndDispatchDomainEventsAsync(cancellationToken);

            try
            {
                await _stripeService.CreateInvoiceAsync(customerId, bundle, transaction, cancellationToken);

                transaction = account.CompleteTransaction(transaction);

                await _moneyAccountRepository.UnitOfWork.CommitAndDispatchDomainEventsAsync(cancellationToken);

                return transaction.Status;
            }
            catch (Exception exception)
            {
                transaction = account.FailureTransaction(transaction, exception.Message);

                await _moneyAccountRepository.UnitOfWork.CommitAndDispatchDomainEventsAsync(cancellationToken);

                return new ValidationError(transaction.Failure.ToString());
            }
        }

        public async Task<Either<ValidationError, TransactionStatus>> WithdrawAsync(
            UserId userId,
            MoneyBundle bundle,
            StripeAccountId accountId,
            CancellationToken cancellationToken = default)
        {
            var account = await _moneyAccountRepository.FindUserAccountAsync(userId);

            var money = bundle.Amount;

            var result = account.CanWithdraw(money);

            if (result.Failure)
            {
                return result.ValidationError;
            }

            var transaction = account.Withdraw(money);

            await _moneyAccountRepository.UnitOfWork.CommitAndDispatchDomainEventsAsync(cancellationToken);

            try
            {
                await _stripeService.CreateTransferAsync(accountId, bundle, transaction, cancellationToken);

                transaction = account.CompleteTransaction(transaction);

                await _moneyAccountRepository.UnitOfWork.CommitAndDispatchDomainEventsAsync(cancellationToken);

                return transaction.Status;
            }
            catch (Exception exception)
            {
                transaction = account.FailureTransaction(transaction, exception.Message);

                await _moneyAccountRepository.UnitOfWork.CommitAndDispatchDomainEventsAsync(cancellationToken);

                return new ValidationError(transaction.Failure.ToString());
            }
        }
    }
}