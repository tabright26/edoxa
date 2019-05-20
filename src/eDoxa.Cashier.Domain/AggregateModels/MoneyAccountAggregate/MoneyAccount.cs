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
using eDoxa.Cashier.Domain.AggregateModels.UserAggregate;
using eDoxa.Cashier.Domain.AggregateModels.UserAggregate.Specifications;
using eDoxa.Seedwork.Domain;
using eDoxa.Seedwork.Domain.Aggregate;
using eDoxa.Seedwork.Domain.Validations;

namespace eDoxa.Cashier.Domain.AggregateModels.MoneyAccountAggregate
{
    public class MoneyAccount : Entity<AccountId>, IMoneyAccount, IAggregateRoot
    {
        private DateTime? _lastDeposit;
        private DateTime? _lastWithdraw;
        private HashSet<MoneyTransaction> _transactions;
        private User _user;

        public MoneyAccount(User user) : this()
        {
            _user = user;
        }

        private MoneyAccount()
        {
            _lastDeposit = null;
            _lastWithdraw = null;
            _transactions = new HashSet<MoneyTransaction>();
        }

        public User User => _user;

        public Money Balance =>
            new Money(Transactions.Where(transaction => transaction.Status.Equals(TransactionStatus.Completed)).Sum(transaction => transaction.Amount));

        public Money Pending =>
            new Money(Transactions.Where(transaction => transaction.Status.Equals(TransactionStatus.Pending)).Sum(transaction => transaction.Amount));

        public DateTime? LastDeposit => _lastDeposit;

        public DateTime? LastWithdraw => _lastWithdraw;

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
                _lastDeposit = DateTime.UtcNow;
            }

            if (transaction.Type.Equals(TransactionType.Withdraw))
            {
                _lastWithdraw = DateTime.UtcNow;
            }

            return transaction;
        }

        public IMoneyTransaction FailureTransaction(IMoneyTransaction transaction, string message)
        {
            transaction.Fail(message);

            return transaction;
        }

        public ValidationResult CanDeposit()
        {
            var result = new ValidationResult();

            if (new DailyMoneyDepositUnavailableSpecification().IsSatisfiedBy(this))
            {
                result.AddError($"Deposit unavailable until {LastDeposit?.AddDays(1)}");
            }

            return result;
        }

        public ValidationResult CanCharge(Money money)
        {
            var result = new ValidationResult();

            if (new InsufficientMoneySpecification(money).IsSatisfiedBy(this))
            {
                result.AddError("Insufficient funds.");
            }

            return result;
        }

        public ValidationResult CanWithdraw(Money money)
        {
            var result = new ValidationResult();

            if (new HasBankAccountSpecification().Not().IsSatisfiedBy(User))
            {
                result.AddError("A bank account is required to withdrawal.");
            }

            if (new InsufficientMoneySpecification(money).IsSatisfiedBy(this))
            {
                result.AddError("Insufficient funds.");
            }

            if (new WeeklyMoneyWithdrawUnavailableSpecification().IsSatisfiedBy(this))
            {
                result.AddError($"Withdrawal unavailable until {LastWithdraw?.AddDays(7)}");
            }

            return result;
        }
    }
}
