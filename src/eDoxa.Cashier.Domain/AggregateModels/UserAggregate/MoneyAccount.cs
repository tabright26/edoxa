// Filename: MoneyAccount.cs
// Date Created: 2019-04-26
// 
// ================================================
// Copyright © 2019, eDoxa. All rights reserved.
//  
// This file is subject to the terms and conditions
// defined in file 'LICENSE.md', which is part of
// this source code package.

using System;
using System.Collections.Generic;
using System.Linq;

using eDoxa.Seedwork.Domain.Aggregate;

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

        public ITransaction<Money> Deposit(Money amount)
        {
            var transaction = new MoneyTransaction(amount);

            _transactions.Add(transaction);

            return transaction;
        }

        public ITransaction<Money> Register(Money amount, ActivityId activityId)
        {
            var transaction = new MoneyPendingTransaction(-amount, activityId);

            _transactions.Add(transaction);

            return transaction;
        }

        public ITransaction<Money> Payoff(Money amount, ActivityId activityId)
        {
            throw new NotImplementedException();
        }

        public IMoneyTransaction Withdraw(Money amount)
        {
            var transaction = new MoneyTransaction(-amount);

            _transactions.Add(transaction);

            return transaction;
        }
    }
}