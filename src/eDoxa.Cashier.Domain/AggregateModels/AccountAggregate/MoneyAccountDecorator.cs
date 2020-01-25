// Filename: MoneyAccountDecorator.cs
// Date Created: 2019-11-25
// 
// ================================================
// Copyright © 2019, eDoxa. All rights reserved.

using System;
using System.Linq;

namespace eDoxa.Cashier.Domain.AggregateModels.AccountAggregate
{
    public sealed class MoneyAccountDecorator : AccountDecorator, IMoneyAccount
    {
        public static readonly TimeSpan DepositInterval = TimeSpan.FromDays(1);
        public static readonly TimeSpan WithdrawalInterval = TimeSpan.FromDays(7);

        public MoneyAccountDecorator(IAccount account) : base(account)
        {
        }

        public Balance Balance => new Balance(Transactions, Currency.Money);

        public DateTime? LastDeposit =>
            Transactions.Where(
                    transaction => transaction.Currency.Type == Currency.Money &&
                                   transaction.Type == TransactionType.Deposit &&
                                   transaction.Status == TransactionStatus.Succeeded)
                .OrderByDescending(transaction => transaction.Timestamp)
                .FirstOrDefault()
                ?.Timestamp;

        public DateTime? LastWithdraw =>
            Transactions.Where(
                    transaction => transaction.Currency.Type == Currency.Money &&
                                   transaction.Type == TransactionType.Withdrawal &&
                                   transaction.Status == TransactionStatus.Succeeded)
                .OrderByDescending(transaction => transaction.Timestamp)
                .FirstOrDefault()
                ?.Timestamp;

        public ITransaction Deposit(Money amount)
        {
            if (!this.CanDeposit())
            {
                throw new InvalidOperationException();
            }

            var builder = new TransactionBuilder(TransactionType.Deposit, amount);

            var transaction = builder.Build();

            this.CreateTransaction(transaction);

            return transaction;
        }

        public ITransaction Charge(Money amount, TransactionMetadata? metadata = null)
        {
            if (!this.CanCharge(amount))
            {
                throw new InvalidOperationException();
            }

            var builder = new TransactionBuilder(TransactionType.Charge, amount).WithMetadata(metadata);

            var transaction = builder.Build();

            this.CreateTransaction(transaction);

            return transaction;
        }

        public ITransaction Payout(Money amount, TransactionMetadata? metadata = null)
        {
            var builder = new TransactionBuilder(TransactionType.Payout, amount).WithMetadata(metadata);

            var transaction = builder.Build();

            this.CreateTransaction(transaction);

            return transaction;
        }

        public ITransaction Promotion(Money amount, TransactionMetadata? metadata = null)
        {
            var builder = new TransactionBuilder(TransactionType.Promotion, amount).WithMetadata(metadata);

            var transaction = builder.Build();

            this.CreateTransaction(transaction);

            return transaction;
        }

        public ITransaction Withdrawal(Money amount)
        {
            if (!this.CanWithdraw(amount))
            {
                throw new InvalidOperationException();
            }

            var builder = new TransactionBuilder(TransactionType.Withdrawal, amount);

            var transaction = builder.Build();

            this.CreateTransaction(transaction);

            return transaction;
        }

        public bool IsDepositAvailable()
        {
            return !(LastDeposit.HasValue && LastDeposit.Value.Add(DepositInterval) >= DateTime.UtcNow);
        }

        public bool IsWithdrawalAvailable()
        {
            return !(LastWithdraw.HasValue && LastWithdraw.Value.Add(WithdrawalInterval) >= DateTime.UtcNow);
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
