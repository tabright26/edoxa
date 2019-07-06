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

using eDoxa.Seedwork.Domain.Aggregate;

using JetBrains.Annotations;

namespace eDoxa.Cashier.Domain.AggregateModels.AccountAggregate
{
    public sealed partial class Account : Entity<AccountId>, IAccount
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

        public Balance GetBalanceFor(Currency currency)
        {
            if (currency == Currency.Money)
            {
                var accountMoney = new AccountMoney(this);

                return accountMoney.Balance;
            }

            if (currency == Currency.Token)
            {
                var accountToken = new AccountToken(this);

                return accountToken.Balance;
            }

            throw new ArgumentException(nameof(currency));
        }
    }

    public partial class Account : IEquatable<IAccount>
    {
        public bool Equals([CanBeNull] IAccount account)
        {
            return Id.Equals(account?.Id);
        }

        public override bool Equals([CanBeNull] object obj)
        {
            return this.Equals(obj as IAccount);
        }

        public override int GetHashCode()
        {
            return base.GetHashCode();
        }
    }
}
