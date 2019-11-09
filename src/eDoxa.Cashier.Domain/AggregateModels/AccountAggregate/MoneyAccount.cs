// Filename: MoneyAccount.cs
// Date Created: 2019-06-25
// 
// ================================================
// Copyright © 2019, eDoxa. All rights reserved.

using System;
using System.Collections.Immutable;
using System.Linq;

using eDoxa.Cashier.Domain.AggregateModels.TransactionAggregate;
using eDoxa.Seedwork.Domain.Miscs;

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

        public ITransaction Deposit(TransactionId transactionId, Money amount, IImmutableSet<Bundle> bundles)
        {
            if (!this.CanDeposit())
            {
                throw new InvalidOperationException();
            }

            var transaction = new MoneyDepositTransaction(transactionId, amount);

            _account.CreateTransaction(transaction);

            return transaction;
        }

        public ITransaction Charge(TransactionId transactionId, Money amount)
        {
            if (!this.CanCharge(amount))
            {
                throw new InvalidOperationException();
            }

            var transaction = new MoneyChargeTransaction(transactionId,amount);

            _account.CreateTransaction(transaction);

            return transaction;
        }

        public ITransaction Payout(TransactionId transactionId, Money amount)
        {
            var transaction = new MoneyPayoutTransaction(transactionId, amount);

            _account.CreateTransaction(transaction);

            return transaction;
        }

        public ITransaction Withdrawal(TransactionId transactionId, Money amount, IImmutableSet<Bundle> bundles)
        {
            if (!this.CanWithdraw(amount))
            {
                throw new InvalidOperationException();
            }

            var transaction = new MoneyWithdrawTransaction(transactionId, amount);

            _account.CreateTransaction(transaction);

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
