// Filename: MoneyAccount.cs
// Date Created: 2019-06-25
// 
// ================================================
// Copyright © 2019, eDoxa. All rights reserved.

using System;
using System.Linq;

using eDoxa.Cashier.Domain.AggregateModels.TransactionAggregate;
using eDoxa.Cashier.Domain.Validators;

namespace eDoxa.Cashier.Domain.AggregateModels.AccountAggregate
{
    public class MoneyAccount : IMoneyAccount
    {
        private readonly IAccount _account;

        public MoneyAccount(IAccount account)
        {
            _account = account;
        }

        public Balance Balance => new Balance(_account.Transactions, Currency.Money);

        public DateTime? LastDeposit =>
            _account.Transactions
                .Where(
                    transaction => transaction.Currency.Type == Currency.Money &&
                                   transaction.Type == TransactionType.Deposit &&
                                   transaction.Status == TransactionStatus.Succeded
                )
                .OrderByDescending(transaction => transaction.Timestamp)
                .FirstOrDefault()
                ?.Timestamp;

        public DateTime? LastWithdraw =>
            _account.Transactions
                .Where(
                    transaction => transaction.Currency.Type == Currency.Money &&
                                   transaction.Type == TransactionType.Withdrawal &&
                                   transaction.Status == TransactionStatus.Succeded
                )
                .OrderByDescending(transaction => transaction.Timestamp)
                .FirstOrDefault()
                ?.Timestamp;

        public ITransaction Deposit(Money amount)
        {
            if (!this.CanDeposit())
            {
                throw new InvalidOperationException();
            }

            var transaction = new MoneyDepositTransaction(amount);

            _account.CreateTransaction(transaction);

            return transaction;
        }

        public ITransaction Charge(Money amount)
        {
            if (!this.CanCharge(amount))
            {
                throw new InvalidOperationException();
            }

            var transaction = new MoneyChargeTransaction(amount);

            _account.CreateTransaction(transaction);

            return transaction;
        }

        public ITransaction Payout(Money amount)
        {
            var transaction = new MoneyPayoutTransaction(amount);

            _account.CreateTransaction(transaction);

            return transaction;
        }

        public ITransaction Withdrawal(Money amount)
        {
            if (!this.CanWithdraw(amount))
            {
                throw new InvalidOperationException();
            }

            var transaction = new MoneyWithdrawTransaction(amount);

            _account.CreateTransaction(transaction);

            return transaction;
        }

        private bool CanDeposit()
        {
            return new DepositMoneyValidator().Validate(this).IsValid;
        }

        private bool CanCharge(Money money)
        {
            return new ChargeMoneyValidator(money).Validate(this).IsValid;
        }

        private bool CanWithdraw(Money money)
        {
            return new WithdrawalMoneyValidator(money).Validate(this).IsValid;
        }
    }
}
