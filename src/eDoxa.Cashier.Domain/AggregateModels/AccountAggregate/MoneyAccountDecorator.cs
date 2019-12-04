// Filename: MoneyAccountDecorator.cs
// Date Created: 2019-11-25
// 
// ================================================
// Copyright © 2019, eDoxa. All rights reserved.

using System;
using System.Linq;

using eDoxa.Cashier.Domain.AggregateModels.TransactionAggregate;
using eDoxa.Seedwork.Domain.Misc;

namespace eDoxa.Cashier.Domain.AggregateModels.AccountAggregate
{
    public sealed class MoneyAccountDecorator : AccountDecorator, IMoneyAccount
    {
        public MoneyAccountDecorator(IAccount account) : base(account)
        {
        }

        public Balance Balance => new Balance(Transactions, Currency.Money);

        public DateTime? LastDeposit =>
            Transactions.Where(
                    transaction => transaction.Currency.Type == Currency.Money &&
                                   transaction.Type == TransactionType.Deposit &&
                                   transaction.Status == TransactionStatus.Succeded)
                .OrderByDescending(transaction => transaction.Timestamp)
                .FirstOrDefault()
                ?.Timestamp;

        public DateTime? LastWithdraw =>
            Transactions.Where(
                    transaction => transaction.Currency.Type == Currency.Money &&
                                   transaction.Type == TransactionType.Withdrawal &&
                                   transaction.Status == TransactionStatus.Succeded)
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

            this.CreateTransaction(transaction);

            return transaction;
        }

        public ITransaction Charge(Money amount, TransactionMetadata? metadata = null)
        {
            if (!this.CanCharge(amount))
            {
                throw new InvalidOperationException();
            }

            var transaction = new MoneyChargeTransaction(amount, metadata);

            this.CreateTransaction(transaction);

            return transaction;
        }

        public ITransaction Payout(Money amount, TransactionMetadata? metadata = null)
        {
            var transaction = new MoneyPayoutTransaction(amount);

            this.CreateTransaction(transaction);

            return transaction;
        }

        public ITransaction Withdrawal(Money amount)
        {
            if (!this.CanWithdraw(amount))
            {
                throw new InvalidOperationException();
            }

            var transaction = new MoneyWithdrawTransaction(amount);

            this.CreateTransaction(transaction);

            return transaction;
        }

        public bool IsDepositAvailable()
        {
            return !(LastDeposit.HasValue && LastDeposit.Value.AddDays(1) >= DateTime.UtcNow);
        }

        public bool IsWithdrawalAvailable()
        {
            return !(LastWithdraw.HasValue && LastWithdraw.Value.AddDays(7) >= DateTime.UtcNow);
        }

        public bool HaveSufficientMoney(Money money)
        {
            return Balance.Available >= money;
        }

        private bool CanDeposit()
        {
            return this.IsDepositAvailable();
        }

        private bool CanCharge(Money money)
        {
            return this.HaveSufficientMoney(money);
        }

        private bool CanWithdraw(Money money)
        {
            return this.HaveSufficientMoney(money) && this.IsWithdrawalAvailable();
        }
    }
}
