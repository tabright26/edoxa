// Filename: Account.cs
// Date Created: 2019-06-01
// 
// ================================================
// Copyright © 2019, eDoxa. All rights reserved.
// 
// This file is subject to the terms and conditions
// defined in file 'LICENSE.md', which is part of
// this source code package.

using System.Collections.Generic;

using eDoxa.Cashier.Domain.Abstractions;
using eDoxa.Cashier.Domain.AggregateModels.UserAggregate;
using eDoxa.Seedwork.Domain.Aggregate;

namespace eDoxa.Cashier.Domain.AggregateModels.AccountAggregate
{
    public sealed class Account : Entity<AccountId>, IAccount
    {
        private HashSet<Transaction> _transactions;

        public Account(User user) : this()
        {
            User = user;
        }

        private Account()
        {
            _transactions = new HashSet<Transaction>();
        }

        public User User { get; private set; }

        public IReadOnlyCollection<Transaction> Transactions => _transactions;

        public void CreateTransaction(Transaction transaction)
        {
            _transactions.Add(transaction);
        }
    }
}
