// Filename: MoneyAccount.cs
// Date Created: 2019-05-20
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
using eDoxa.Cashier.Domain.AggregateModels.UserAggregate;
using eDoxa.Seedwork.Domain;
using eDoxa.Seedwork.Domain.Aggregate;
using eDoxa.Specifications.Factories;

namespace eDoxa.Cashier.Domain.AggregateModels.MoneyAccountAggregate
{
    public class MoneyAccount : Entity<AccountId>, IMoneyAccount, IAggregateRoot
    {
        private HashSet<MoneyTransaction> _transactions;

        public MoneyAccount(User user) : this()
        {
            User = user;
        }

        private MoneyAccount()
        {
            LastDeposit = null;
            LastWithdraw = null;
            _transactions = new HashSet<MoneyTransaction>();
        }

        public User User { get; private set; }

        public Money Balance =>
            new Money(Transactions.Where(transaction => transaction.Status.Equals(TransactionStatus.Completed)).Sum(transaction => transaction.Amount));

        public Money Pending =>
            new Money(Transactions.Where(transaction => transaction.Status.Equals(TransactionStatus.Pending)).Sum(transaction => transaction.Amount));

        public DateTime? LastDeposit { get; private set; }

        public DateTime? LastWithdraw { get; private set; }

        public IReadOnlyCollection<MoneyTransaction> Transactions => _transactions;

        public IMoneyTransaction Deposit(Money amount)
        {
            if (!this.CanDeposit())
            {
                throw new InvalidOperationException();
            }

            var transaction = new MoneyDepositTransaction(amount);

            _transactions.Add(transaction);

            return transaction;
        }

        public IMoneyTransaction Charge(Money amount)
        {
            if (!this.CanCharge(amount))
            {
                throw new InvalidOperationException();
            }

            var transaction = new MoneyChargeTransaction(-amount);

            _transactions.Add(transaction);

            return transaction;
        }

        public IMoneyTransaction Payout(Money amount)
        {
            var transaction = new MoneyPayoutTransaction(amount);

            _transactions.Add(transaction);

            return transaction;
        }

        public IMoneyTransaction Withdraw(Money amount)
        {
            if (!this.CanWithdraw(amount))
            {
                throw new InvalidOperationException();
            }

            var transaction = new MoneyWithdrawTransaction(amount);

            _transactions.Add(transaction);

            return transaction;
        }

        public IMoneyTransaction CompleteTransaction(IMoneyTransaction transaction)
        {
            transaction.Complete();

            if (transaction.Type.Equals(TransactionType.Deposit))
            {
                LastDeposit = DateTime.UtcNow;
            }

            if (transaction.Type.Equals(TransactionType.Withdraw))
            {
                LastWithdraw = DateTime.UtcNow;
            }

            return transaction;
        }

        public IMoneyTransaction FailureTransaction(IMoneyTransaction transaction, string message)
        {
            transaction.Fail(message);

            return transaction;
        }

        public bool CanDeposit()
        {
            var specification = SpecificationFactory.Instance.CreateSpecification<MoneyAccount>().And(new DailyMoneyDepositUnavailableSpecification().Not());

            return specification.IsSatisfiedBy(this);
        }

        public bool CanCharge(Money money)
        {
            var specification = SpecificationFactory.Instance.CreateSpecification<MoneyAccount>().And(new InsufficientMoneySpecification(money).Not());

            return specification.IsSatisfiedBy(this);
        }

        public bool CanWithdraw(Money money)
        {
            var specification = SpecificationFactory.Instance.CreateSpecification<MoneyAccount>()
                .And(new HasBankAccountSpecification())
                .And(new InsufficientMoneySpecification(money).Not())
                .And(new WeeklyMoneyWithdrawUnavailableSpecification().Not());

            return specification.IsSatisfiedBy(this);
        }
    }
}
