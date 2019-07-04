// Filename: Account.cs
// Date Created: 2019-06-25
// 
// ================================================
// Copyright © 2019, eDoxa. All rights reserved.
// 
// This file is subject to the terms and conditions
// defined in file 'LICENSE.md', which is part of
// this source code package.

using System;
using System.Collections.Generic;

using eDoxa.Seedwork.Common.Enumerations;
using eDoxa.Seedwork.Common.ValueObjects;
using eDoxa.Seedwork.Domain.Aggregate;

namespace eDoxa.Cashier.Domain.AggregateModels.AccountAggregate
{
    public sealed class Account : Entity<AccountId>, IAccount
    {
        private HashSet<ITransaction> _transactions = new HashSet<ITransaction>();

        public Account(UserId userId)
        {
            UserId = userId;
        }

        public UserId UserId { get; }

        public IReadOnlyCollection<ITransaction> Transactions => _transactions;

        public void CreateTransaction(ITransaction transaction)
        {
            _transactions.Add(transaction);
        }

        public Balance GetBalanceFor(CurrencyType currency)
        {
            if (currency == CurrencyType.Money)
            {
                var accountMoney = new AccountMoney(this);

                return accountMoney.Balance;
            }

            if (currency == CurrencyType.Token)
            {
                var accountToken = new AccountToken(this);

                return accountToken.Balance;
            }

            throw new ArgumentException(nameof(currency));
        }
    }
}
