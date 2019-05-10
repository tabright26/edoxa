// Filename: MoneyAccountService.cs
// Date Created: 2019-05-06
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
using eDoxa.Cashier.Domain.Services;
using eDoxa.Cashier.Domain.Services.Stripe.Abstractions;
using eDoxa.Cashier.Domain.Services.Stripe.Models;
using eDoxa.Functional.Maybe;

namespace eDoxa.Cashier.Application.Services
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

        public async Task<IMoneyTransaction> TransactionAsync(
            UserId userId,
            CustomerId customerId,
            MoneyBundle bundle,
            CancellationToken cancellationToken = default)
        {
            var account = await _moneyAccountRepository.FindUserAccountAsync(userId);

            var transaction = account.Deposit(bundle.Amount);

            await _moneyAccountRepository.UnitOfWork.CommitAndDispatchDomainEventsAsync(cancellationToken);

            try
            {
                await _stripeService.CreateInvoiceAsync(customerId, bundle, transaction, cancellationToken);
            }
            catch (Exception)
            {
                transaction.Fail();

                await _moneyAccountRepository.UnitOfWork.CommitAndDispatchDomainEventsAsync(cancellationToken);

                return transaction;
            }

            transaction.Pay();

            await _moneyAccountRepository.UnitOfWork.CommitAndDispatchDomainEventsAsync(cancellationToken);

            return transaction;
        }

        public async Task<Option<IMoneyTransaction>> TryWithdrawalAsync(UserId userId, decimal amount, CancellationToken cancellationToken = default)
        {
            var account = await _moneyAccountRepository.FindUserAccountAsync(userId);

            var money = new Money(amount);

            var transaction = account.TryWithdrawal(money);

            await _moneyAccountRepository.UnitOfWork.CommitAndDispatchDomainEventsAsync(cancellationToken);

            return transaction;
        }
    }
}