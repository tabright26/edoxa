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
using System.Threading;
using System.Threading.Tasks;

using eDoxa.Cashier.Domain.Abstractions;
using eDoxa.Cashier.Domain.AggregateModels;
using eDoxa.Cashier.Domain.AggregateModels.MoneyAccountAggregate;
using eDoxa.Cashier.Domain.Repositories;
using eDoxa.Cashier.Domain.Services.Abstractions;
using eDoxa.Cashier.Domain.Services.Stripe.Abstractions;
using eDoxa.Cashier.Domain.Services.Stripe.Models;
using eDoxa.Functional;

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

        public async Task<Either<ValidationResult, IMoneyTransaction>> DepositAsync(
            UserId userId,
            CustomerId customerId,
            MoneyBundle bundle,
            string email,
            CancellationToken cancellationToken = default)
        {
            var account = await _moneyAccountRepository.FindUserAccountAsync(userId);

            var moneyTransaction = account.Deposit(bundle.Amount);

            await _moneyAccountRepository.UnitOfWork.CommitAndDispatchDomainEventsAsync(cancellationToken);

            var either = await _stripeService.CreateInvoiceAsync(customerId, email, bundle, moneyTransaction, cancellationToken);

            return either.Match(
                result =>
                {
                    moneyTransaction.Fail();

                    _moneyAccountRepository.UnitOfWork.CommitAndDispatchDomainEventsAsync(cancellationToken).Wait(cancellationToken);

                    return result;
                },
                payout =>
                {
                    moneyTransaction.Pay();

                    _moneyAccountRepository.UnitOfWork.CommitAndDispatchDomainEventsAsync(cancellationToken).Wait(cancellationToken);

                    return new Either<ValidationResult, IMoneyTransaction>(moneyTransaction);
                });
        }

        public Task<Either<ValidationResult, IMoneyTransaction>> TryWithdrawalAsync(
            UserId userId,
            CustomerId customerId,
            MoneyBundle bundle,
            CancellationToken cancellationToken = default)
        {
            throw new NotImplementedException();

            //var account = await _moneyAccountRepository.FindUserAccountAsync(userId);

            //// TODO: InsufficientFundsSpecification (Validation)

            //// TODO: WeeklyWithdrawalSpecification (Validation)

            //var moneyTransaction = account.TryWithdrawal(bundle.Amount);

            //return moneyTransaction.Select(transaction =>
            //{
            //    var either = _stripeService.CreatePayoutAsync(customerId, bundle, transaction, cancellationToken).Result;

            //    return either.Match(
            //        result =>
            //        {
            //            transaction.Fail();

            //            _moneyAccountRepository.UnitOfWork.CommitAndDispatchDomainEventsAsync(cancellationToken).Wait(cancellationToken);

            //            return result;
            //        },
            //        payout =>
            //        {
            //            transaction.Success();

            //            _moneyAccountRepository.UnitOfWork.CommitAndDispatchDomainEventsAsync(cancellationToken).Wait(cancellationToken);

            //            return new Either<ValidationResult, IMoneyTransaction>(transaction);
            //        });
            //}).DefaultIfEmpty(new ValidationResult("Failed to withdraw funds.")).Single();
        }
    }
}