// Filename: MoneyAccountService.cs
// Date Created: 2019-05-11
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
using eDoxa.Cashier.Domain.AggregateModels;
using eDoxa.Cashier.Domain.AggregateModels.MoneyAccountAggregate;
using eDoxa.Cashier.Domain.Repositories;
using eDoxa.Cashier.Domain.Services.Abstractions;
using eDoxa.Cashier.Domain.Services.Stripe.Abstractions;
using eDoxa.Cashier.Domain.Services.Stripe.Models;
using eDoxa.Functional;
using eDoxa.Functional.Extensions;

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

        public async Task<IMoneyTransaction> DepositAsync(
            UserId userId,
            CustomerId customerId,
            MoneyBundle bundle,
            CancellationToken cancellationToken = default)
        {
            var account = await _moneyAccountRepository.FindUserAccountAsync(userId);

            var moneyTransaction = account.Deposit(bundle.Amount);

            await _moneyAccountRepository.UnitOfWork.CommitAndDispatchDomainEventsAsync(cancellationToken);

            try
            {
                await _stripeService.CreateInvoiceAsync(customerId, bundle, moneyTransaction, cancellationToken);
            }
            catch (Exception)
            {
                moneyTransaction.Fail();

                await _moneyAccountRepository.UnitOfWork.CommitAndDispatchDomainEventsAsync(cancellationToken);

                return moneyTransaction;
            }

            moneyTransaction.Pay();

            await _moneyAccountRepository.UnitOfWork.CommitAndDispatchDomainEventsAsync(cancellationToken);

            return moneyTransaction;
        }

        public async Task<Option<IMoneyTransaction>> TryWithdrawalAsync(
            UserId userId,
            CustomerId customerId,
            MoneyBundle bundle,
            CancellationToken cancellationToken = default)
        {
            var account = await _moneyAccountRepository.FindUserAccountAsync(userId);

            var moneyTransaction = account.TryWithdrawal(bundle.Amount);

            try
            {
                moneyTransaction.ForEach(async transaction => await _stripeService.CreatePayoutAsync(customerId, bundle, transaction, cancellationToken));
            }
            catch (Exception)
            {
                moneyTransaction.ForEach(async transaction =>
                {
                    transaction.Fail();

                    await _moneyAccountRepository.UnitOfWork.CommitAndDispatchDomainEventsAsync(cancellationToken);
                });
            }

            moneyTransaction.ForEach(async transaction =>
            {
                transaction.Complete();

                await _moneyAccountRepository.UnitOfWork.CommitAndDispatchDomainEventsAsync(cancellationToken);
            });

            return moneyTransaction;
        }
    }
}