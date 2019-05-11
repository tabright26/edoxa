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
using eDoxa.Functional;
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

        public Money Balance =>
            new Money(Transactions
                .Where(transaction => transaction.Status.Equals(TransactionStatus.Paid) || 
                                      transaction.Status.Equals(TransactionStatus.Completed))
                .Sum(transaction => transaction.Amount));

        public Money Pending =>
            new Money(Transactions
                .Where(transaction => transaction.Status.Equals(TransactionStatus.Pending))
                .Sum(transaction => transaction.Amount));

        public IReadOnlyCollection<MoneyTransaction> Transactions => _transactions;

        public IMoneyTransaction Deposit(Money amount)
        {
            var transaction = new DepositMoneyTransaction(amount);

            if (_transactions.Add(transaction))
            {
                Log.Information(transaction.ToString());
            }

            return transaction;
        }

        public Option<IMoneyTransaction> TryWithdrawal(Money amount)
        {
            if (Balance < amount)
            {
                return new Option<IMoneyTransaction>();
            }

            var transaction = new WithdrawalMoneyTransaction(amount);

            if (!_transactions.Add(transaction))
            {
                return new Option<IMoneyTransaction>();
            }

            Log.Information(transaction.ToString());

            return new Option<IMoneyTransaction>(transaction);
        }

        public Option<IMoneyTransaction> TryRegister(Money amount)
        {
            if (Balance < amount)
            {
                return new Option<IMoneyTransaction>();
            }

            var transaction = new ServiceMoneyTransaction(-amount);

            if (!_transactions.Add(transaction))
            {
                return new Option<IMoneyTransaction>();
            }

            Log.Information(transaction.ToString());

            return new Option<IMoneyTransaction>(transaction);
        }

        public Option<IMoneyTransaction> TryPayout(Money amount)
        {
            var transaction = new PrizeMoneyTransaction(amount);

            if (!_transactions.Add(transaction))
            {
                return new Option<IMoneyTransaction>();
            }

            Log.Information(transaction.ToString());

            return new Option<IMoneyTransaction>(transaction);
        }
    }
}