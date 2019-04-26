// Filename: MoneyAccount.cs
// Date Created: 2019-04-26
// 
// ================================================
// Copyright © 2019, eDoxa. All rights reserved.
//  
// This file is subject to the terms and conditions
// defined in file 'LICENSE.md', which is part of
// this source code package.

using System.Collections.Generic;
using System.Linq;

using eDoxa.Functional.Maybe;
using eDoxa.Seedwork.Domain.Aggregate;

using Serilog;

namespace eDoxa.Cashier.Domain.AggregateModels.UserAggregate
{
    public class MoneyAccount : Entity<AccountId>, IMoneyAccount
    {
        private HashSet<MoneyTransaction> _transactions;
        private User _user;

        public MoneyAccount(User user) : this()
        {
            _user = user;
        }

        private MoneyAccount()
        {
            _transactions = new HashSet<MoneyTransaction>();
        }

        public User User => _user;

        public IReadOnlyCollection<MoneyTransaction> Transactions => _transactions;

        public Money Balance => new Money(Transactions.Sum(transaction => transaction.Amount));

        public Money Pending => new Money(Transactions.Where(transaction => transaction.Pending).Sum(transaction => -transaction.Amount));

        public IMoneyTransaction Deposit(Money amount)
        {
            var transaction = new MoneyTransaction(amount);

            if (_transactions.Add(transaction))
            {
                Log.Information($"{User} deposit amount {amount} - balance {Balance}");
            }

            return transaction;
        }

        public Maybe<IMoneyTransaction> TryRegister(Money amount, ActivityId activityId)
        {
            if (Balance < amount)
            {
                return new Maybe<IMoneyTransaction>();
            }

            var transaction = new MoneyPendingTransaction(-amount, activityId);

            if (!_transactions.Add(transaction))
            {
                return new Maybe<IMoneyTransaction>();
            }

            Log.Information($"{User} register to {activityId} amount {amount} - balance {Balance}");

            return new Maybe<IMoneyTransaction>(transaction);
        }

        public Maybe<IMoneyTransaction> TryPayoff(Money amount, ActivityId activityId)
        {
            return Transactions.Where(transaction => transaction.Pending && transaction.ActivityId == activityId)
                .Select(transaction => this.TryPayoff(amount, transaction))
                .DefaultIfEmpty(new Maybe<IMoneyTransaction>())
                .Single();
        }

        public Maybe<IMoneyTransaction> TryWithdraw(Money amount)
        {
            if (Balance < amount)
            {
                return new Maybe<IMoneyTransaction>();
            }

            var transaction = new MoneyTransaction(-amount);

            if (!_transactions.Add(transaction))
            {
                return new Maybe<IMoneyTransaction>();
            }

            Log.Information($"{User} withdrew amount {amount} - balance {Balance}");

            return new Maybe<IMoneyTransaction>(transaction);
        }

        private Maybe<IMoneyTransaction> TryPayoff(Money amount, IMoneyTransaction transaction)
        {
            return transaction.TryPayoff(amount).Select(payoff =>
            {
                if (!_transactions.Add(payoff))
                {
                    return new Maybe<IMoneyTransaction>();
                }

                Log.Information($"{User} deposit amount {amount} - balance {Balance}");

                return new Maybe<IMoneyTransaction>(payoff);
            }).Single();
        }
    }
}