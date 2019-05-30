// Filename: MoneyAccountService.cs
// Date Created: 2019-05-20
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
using eDoxa.Cashier.Domain.Repositories;
using eDoxa.Cashier.Domain.Validators;
using eDoxa.Cashier.Services.Abstractions;
using eDoxa.Cashier.Services.Stripe.Abstractions;
using eDoxa.Functional;
using eDoxa.Seedwork.Application.Validations.Extensions;
using eDoxa.Seedwork.Domain.Common;

using FluentValidation.Results;

using TransactionStatus = eDoxa.Cashier.Domain.AggregateModels.AccountAggregate.TransactionStatus;

namespace eDoxa.Cashier.Services
{
    public sealed class MoneyAccountService : IMoneyAccountService
    {
        private readonly IAccountRepository _accountRepository;
        private readonly IStripeService _stripeService;

        public MoneyAccountService(IAccountRepository accountRepository, IStripeService stripeService)
        {
            _accountRepository = accountRepository;
            _stripeService = stripeService;
        }

        public async Task<Either<ValidationResult, TransactionStatus>> DepositAsync(
            UserId userId,
            MoneyBundle bundle,
            StripeCustomerId customerId,
            CancellationToken cancellationToken = default
        )
        {
            var account = await _accountRepository.GetAccountAsync(userId);

            var moneyAccount = new MoneyAccount(account);

            var validator = new DepositMoneyValidator();

            var result = validator.Validate(moneyAccount);

            if (!result.IsValid)
            {
                return result;
            }

            var customer = await _stripeService.GetCustomerAsync(customerId, cancellationToken);

            if (customer.DefaultSource == null)
            {
                 return new ValidationFailure(null, "There are no credit cards associated with this account.").ToResult();
            }

            var transaction = moneyAccount.Deposit(bundle.Amount);

            await _accountRepository.UnitOfWork.CommitAndDispatchDomainEventsAsync(cancellationToken);

            try
            {
                await _stripeService.CreateInvoiceAsync(customerId, bundle, transaction, cancellationToken);

                transaction = moneyAccount.CompleteTransaction(transaction);

                await _accountRepository.UnitOfWork.CommitAndDispatchDomainEventsAsync(cancellationToken);

                return transaction.Status;
            }
            catch (Exception exception)
            {
                transaction = moneyAccount.FailureTransaction(transaction, exception.Message);

                await _accountRepository.UnitOfWork.CommitAndDispatchDomainEventsAsync(cancellationToken);

                return new ValidationFailure(null, transaction.Failure.ToString()).ToResult();
            }
        }

        public async Task<Either<ValidationResult, TransactionStatus>> WithdrawAsync(
            UserId userId,
            MoneyBundle bundle,
            StripeAccountId accountId,
            CancellationToken cancellationToken = default
        )
        {
            var account = await _accountRepository.GetAccountAsync(userId);

            var moneyAccount = new MoneyAccount(account);

            var money = bundle.Amount;

            var validator = new WithdrawMoneyValidator(money);

            var result = validator.Validate(moneyAccount);

            if (!result.IsValid)
            {
                return result;
            }

            var transaction = moneyAccount.Withdraw(money);

            await _accountRepository.UnitOfWork.CommitAndDispatchDomainEventsAsync(cancellationToken);

            try
            {
                await _stripeService.CreateTransferAsync(accountId, bundle, transaction, cancellationToken);

                transaction = moneyAccount.CompleteTransaction(transaction);

                await _accountRepository.UnitOfWork.CommitAndDispatchDomainEventsAsync(cancellationToken);

                return transaction.Status;
            }
            catch (Exception exception)
            {
                transaction = moneyAccount.FailureTransaction(transaction, exception.Message);

                await _accountRepository.UnitOfWork.CommitAndDispatchDomainEventsAsync(cancellationToken);

                return new ValidationFailure(null, transaction.Failure.ToString()).ToResult();
            }
        }
    }
}
