// Filename: MoneyAccount.cs
// Date Created: 2019-05-06
// 
// ================================================
// Copyright © 2019, eDoxa. All rights reserved.
// 
// This file is subject to the terms and conditions
// defined in file 'LICENSE.md', which is part of
// this source code package.

using System.Collections.Generic;
using System.Linq;

using eDoxa.Cashier.Domain.Abstractions;
using eDoxa.Functional.Maybe;
using eDoxa.Seedwork.Domain;
using eDoxa.Seedwork.Domain.Aggregate;

using Serilog;

namespace eDoxa.Cashier.Domain.AggregateModels.MoneyAccountAggregate
{
    public class MoneyAccount : Entity<AccountId>, IMoneyAccount, IAggregateRoot
    {
        private HashSet<MoneyTransaction> _transactions;
        private UserId _userId;

        public MoneyAccount(UserId userId) : this()
        {
            _userId = userId;
        }

        private MoneyAccount()
        {
            _transactions = new HashSet<MoneyTransaction>();
        }

        public UserId UserId => _userId;

        public IReadOnlyCollection<MoneyTransaction> Transactions => _transactions;

        public Money Balance => new Money(Transactions.Sum(transaction => transaction.Amount));

        public Money Pending => new Money(Transactions.Where(transaction => transaction.Pending).Sum(transaction => -transaction.Amount));

        public IMoneyTransaction Deposit(Money amount)
        {
            var transaction = new MoneyTransaction(amount);

            if (_transactions.Add(transaction))
            {
                Log.Information($"{UserId} deposit amount {amount} - balance {Balance}");
            }

            return transaction;
        }

        public Option<IMoneyTransaction> TryRegister(Money amount, ServiceId serviceId)
        {
            if (Balance < amount)
            {
                return new Option<IMoneyTransaction>();
            }

            var transaction = new MoneyPendingTransaction(-amount, serviceId);

            if (!_transactions.Add(transaction))
            {
                return new Option<IMoneyTransaction>();
            }

            Log.Information($"{UserId} register to {serviceId} amount {amount} - balance {Balance}");

            return new Option<IMoneyTransaction>(transaction);
        }

        public Option<IMoneyTransaction> TryPayoff(Money amount, ServiceId serviceId)
        {
            return Transactions.Where(transaction => transaction.Pending && transaction.ServiceId == serviceId.ToString())
                .Select(transaction => this.TryPayoff(amount, transaction))
                .DefaultIfEmpty(new Option<IMoneyTransaction>())
                .Single();
        }

        public Option<IMoneyTransaction> TryWithdraw(Money amount)
        {
            if (Balance < amount)
            {
                return new Option<IMoneyTransaction>();
            }

            var transaction = new MoneyTransaction(-amount);

            if (!_transactions.Add(transaction))
            {
                return new Option<IMoneyTransaction>();
            }

            Log.Information($"{UserId} withdrew amount {amount} - balance {Balance}");

            return new Option<IMoneyTransaction>(transaction);
        }

        private Option<IMoneyTransaction> TryPayoff(Money amount, IMoneyTransaction transaction)
        {
            return transaction.TryPayoff(amount).Select(payoff =>
            {
                if (!_transactions.Add(payoff))
                {
                    return new Option<IMoneyTransaction>();
                }

                Log.Information($"{UserId} deposit amount {amount} - balance {Balance}");

                return new Option<IMoneyTransaction>(payoff);
            }).Single();
        }
    }
}