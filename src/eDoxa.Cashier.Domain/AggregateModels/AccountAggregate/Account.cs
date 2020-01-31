﻿// Filename: Account.cs
// Date Created: 2019-11-25
// 
// ================================================
// Copyright © 2019, eDoxa. All rights reserved.

using System;
using System.Collections.Generic;
using System.Linq;

using eDoxa.Seedwork.Domain;
using eDoxa.Seedwork.Domain.Misc;

namespace eDoxa.Cashier.Domain.AggregateModels.AccountAggregate
{
    public partial class Account : Entity<UserId>, IAccount
    {
        private readonly HashSet<ITransaction> _transactions;

        public Account(UserId userId, IEnumerable<ITransaction>? transactions = null)
        {
            this.SetEntityId(userId);
            _transactions = transactions?.ToHashSet() ?? new HashSet<ITransaction>();
        }

        public IReadOnlyCollection<ITransaction> Transactions => _transactions;

        public Balance GetBalanceFor(CurrencyType currencyType)
        {
            if (currencyType == CurrencyType.Money)
            {
                var accountMoney = new MoneyAccountDecorator(this);

                return accountMoney.Balance;
            }

            if (currencyType == CurrencyType.Token)
            {
                var accountToken = new TokenAccountDecorator(this);

                return accountToken.Balance;
            }

            throw new ArgumentException(nameof(currencyType));
        }

        public void CreateTransaction(ITransaction transaction)
        {
            _transactions.Add(transaction);
        }

        public bool TransactionExists(TransactionId transactionId)
        {
            return Transactions.Any(transaction => transaction.Id == transactionId);
        }

        public bool TransactionExists(TransactionMetadata metadata)
        {
            return Transactions.Any(TransactionMetadataFilter(metadata));
        }

        public ITransaction FindTransaction(TransactionId transactionId)
        {
            if (!this.TransactionExists(transactionId))
            {
                throw new InvalidOperationException(nameof(FindTransaction));
            }

            return Transactions.Single(transaction => transaction.Id == transactionId);
        }

        public ITransaction FindTransaction(TransactionMetadata metadata)
        {
            if (!this.TransactionExists(metadata))
            {
                throw new InvalidOperationException(nameof(FindTransaction));
            }

            return Transactions.Single(TransactionMetadataFilter(metadata));
        }

        private static Func<ITransaction, bool> TransactionMetadataFilter(TransactionMetadata metadata)
        {
            return transaction => transaction.Metadata.Any() &&
                                  metadata.Any() &&
                                  metadata.All(
                                      transactionMetadata =>
                                          transaction.Metadata.Contains(new KeyValuePair<string, string>(transactionMetadata.Key, transactionMetadata.Value)));
        }
    }

    public partial class Account : IEquatable<IAccount?>
    {
        public bool Equals(IAccount? account)
        {
            return Id.Equals(account?.Id);
        }

        public override bool Equals(object? obj)
        {
            return this.Equals(obj as IAccount);
        }

        public override int GetHashCode()
        {
            return base.GetHashCode();
        }
    }
}
