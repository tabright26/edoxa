// Filename: MoneyAccount.cs
// Date Created: 2019-04-24
// 
// ================================================
// Copyright © 2019, eDoxa. All rights reserved.
//  
// This file is subject to the terms and conditions
// defined in file 'LICENSE.md', which is part of
// this source code package.

using System.Collections.Generic;
using System.Linq;

using eDoxa.Seedwork.Domain.Aggregate;

namespace eDoxa.Cashier.Domain.AggregateModels.UserAggregate
{
    public class MoneyAccount : Entity<AccountId>, IAccount<Money>
    {
        private Money _pending;
        private HashSet<MoneyTransaction> _transactions;
        private User _user;

        public MoneyAccount(User user) : this()
        {
            _user = user;
        }

        private MoneyAccount()
        {
            _pending = Money.Zero;
            _transactions = new HashSet<MoneyTransaction>();
        }

        public User User => _user;

        public IReadOnlyCollection<MoneyTransaction> Transactions => _transactions;

        public Money Balance => new Money(Transactions.Sum(transaction => transaction.Amount));

        public Money Pending => _pending;

        public void AddBalance(Money amount)
        {
            _transactions.Add(new MoneyTransaction(this, amount));
        }

        public void SubtractBalance(Money amount)
        {
            _transactions.Add(new MoneyTransaction(this, -amount));
        }

        public void AddPending(Money amount)
        {
            _pending = new Money(_pending + amount);
        }

        public void SubtractPending(Money amount)
        {
            _pending = new Money(_pending - amount);
        }
    }
}