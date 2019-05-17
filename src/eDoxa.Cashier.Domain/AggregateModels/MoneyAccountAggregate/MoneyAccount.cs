// Filename: MoneyAccount.cs
// Date Created: 2019-05-06
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

using eDoxa.Cashier.Domain.Abstractions;
using eDoxa.Cashier.Domain.AggregateModels.MoneyAccountAggregate.Specifications;
using eDoxa.Functional;
using eDoxa.Seedwork.Domain;
using eDoxa.Seedwork.Domain.Aggregate;
using eDoxa.Specifications.Factories;

namespace eDoxa.Cashier.Domain.AggregateModels.MoneyAccountAggregate
{
    public class MoneyAccount : Entity<AccountId>, IMoneyAccount, IAggregateRoot
    {
        private DateTime? _lastDeposit;
        private DateTime? _lastWithdrawal;
        private HashSet<MoneyTransaction> _transactions;
        private UserId _userId;

        public MoneyAccount(UserId userId) : this()
        {
            _userId = userId;
        }

        private MoneyAccount()
        {
            _lastDeposit = null;
            _lastWithdrawal = null;
            _transactions = new HashSet<MoneyTransaction>();
        }

        public UserId UserId => _userId;

        public Money Balance =>
            new Money(Transactions
                .Where(transaction => transaction.Status.Equals(TransactionStatus.Paid) ||
                                      transaction.Status.Equals(TransactionStatus.Succeeded))
                .Sum(transaction => transaction.Amount));

        public Money Pending =>
            new Money(Transactions
                .Where(transaction => transaction.Status.Equals(TransactionStatus.Pending))
                .Sum(transaction => transaction.Amount));

        public DateTime? LastDeposit => _lastDeposit;

        public DateTime? LastWithdrawal => _lastWithdrawal;

        public IReadOnlyCollection<MoneyTransaction> Transactions => _transactions;

        public IMoneyTransaction Deposit(Money amount)
        {
            var transaction = new MoneyDepositTransaction(amount);

            if (_transactions.Add(transaction))
            {
                this.Deposit();
            }

            return transaction;
        }

        public Option<IMoneyTransaction> TryWithdrawal(Money amount)
        {
            if (!this.CanWithdrawal(amount))
            {
                return new Option<IMoneyTransaction>();
            }

            var transaction = new MoneyWithdrawalTransaction(amount);

            if (_transactions.Add(transaction))
            {
                this.Withdrawal();
            }

            return new Option<IMoneyTransaction>(transaction);
        }

        public Option<IMoneyTransaction> TryRegister(Money amount)
        {
            if (Balance < amount)
            {
                return new Option<IMoneyTransaction>();
            }

            var transaction = new MoneyServiceTransaction(-amount);

            return _transactions.Add(transaction) ? new Option<IMoneyTransaction>(transaction) : new Option<IMoneyTransaction>();
        }

        public Option<IMoneyTransaction> TryPayout(Money amount)
        {
            var transaction = new MoneyPrizeTransaction(amount);

            return _transactions.Add(transaction) ? new Option<IMoneyTransaction>(transaction) : new Option<IMoneyTransaction>();
        }

        private void Deposit()
        {
            _lastDeposit = DateTime.UtcNow;
        }

        private void Withdrawal()
        {
            _lastWithdrawal = DateTime.UtcNow;
        }

        private bool CanWithdrawal(Money money)
        {
            var specification = SpecificationFactory.Instance.CreateSpecification<MoneyAccount>()
                .And(new InsufficientFundsSpecification(money).Not())
                .And(new WeeklyWithdrawalUnavailableSpecification().Not());

            return specification.IsSatisfiedBy(this);
        }
    }
}