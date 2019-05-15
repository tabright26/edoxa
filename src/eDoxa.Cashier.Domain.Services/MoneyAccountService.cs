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
using System.ComponentModel.DataAnnotations;
using System.Linq;
using System.Runtime.ExceptionServices;
using System.Threading;
using System.Threading.Tasks;

using eDoxa.Cashier.Domain.Abstractions;
using eDoxa.Cashier.Domain.AggregateModels;
using eDoxa.Cashier.Domain.AggregateModels.MoneyAccountAggregate;
using eDoxa.Cashier.Domain.AggregateModels.MoneyAccountAggregate.Specifications;
using eDoxa.Cashier.Domain.Repositories;
using eDoxa.Cashier.Domain.Services.Abstractions;
using eDoxa.Cashier.Domain.Services.Stripe.Abstractions;
using eDoxa.Cashier.Domain.Services.Stripe.Models;
using eDoxa.Functional;

using Stripe;

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

        public async Task<Either<ValidationResult, IMoneyTransaction>> DepositAsync(StripeCustomerId customerId, UserId userId,  MoneyBundle bundle, CancellationToken cancellationToken = default)
        {
            var account = await _moneyAccountRepository.FindUserAccountAsync(userId);

            var moneyTransaction = account.Deposit(bundle.Amount);

            await _moneyAccountRepository.UnitOfWork.CommitAndDispatchDomainEventsAsync(cancellationToken);

            try
            {
                await _stripeService.CreateInvoiceAsync(customerId, bundle, moneyTransaction, cancellationToken);

                moneyTransaction.Pay();

                await _moneyAccountRepository.UnitOfWork.CommitAndDispatchDomainEventsAsync(cancellationToken);
            }
            catch (Exception exception)
            {
                moneyTransaction.Fail();

                await _moneyAccountRepository.UnitOfWork.CommitAndDispatchDomainEventsAsync(cancellationToken);

                ExceptionDispatchInfo.Capture(exception).Throw();

                throw;
            }

            return new Either<ValidationResult, IMoneyTransaction>(moneyTransaction);
        }

        public async Task<Either<ValidationResult, IMoneyTransaction>> TryWithdrawalAsync(StripeAccountId accountId, UserId userId, MoneyBundle bundle, CancellationToken cancellationToken = default)
        {
            var account = await _moneyAccountRepository.FindUserAccountAsync(userId);

            if (new InsufficientFundsSpecification(bundle.Amount).IsSatisfiedBy(account))
            {
                return new ValidationResult("Insufficient funds.");
            }

            if (new WeeklyWithdrawalUnavailableSpecification().IsSatisfiedBy(account))
            {
                return new ValidationResult($"Withdrawal unavailable until {account.LastWithdrawal?.AddDays(7)}");
            }

            var moneyTransaction = account.TryWithdrawal(bundle.Amount);

            return await moneyTransaction.Select(Selector).DefaultIfEmpty(Task.FromResult<Either<ValidationResult, IMoneyTransaction>>(new ValidationResult("Failed to withdrawal funds."))).Single();

            async Task<Either<ValidationResult, IMoneyTransaction>> Selector(IMoneyTransaction transaction)
            {
                try
                {
                    await _stripeService.CreateTransferAsync(accountId, bundle, transaction, cancellationToken);

                    transaction.Success();

                    await _moneyAccountRepository.UnitOfWork.CommitAndDispatchDomainEventsAsync(cancellationToken);
                }
                catch (StripeException exception)
                {
                    transaction.Fail();

                    await _moneyAccountRepository.UnitOfWork.CommitAndDispatchDomainEventsAsync(cancellationToken);

                    ExceptionDispatchInfo.Capture(exception).Throw();

                    throw;
                }

                return new Either<ValidationResult, IMoneyTransaction>(transaction);
            }
        }
    }
}