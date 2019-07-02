// Filename: AccountMoney.cs
// Date Created: 2019-06-01
// 
// ================================================
// Copyright © 2019, eDoxa. All rights reserved.
// 
// This file is subject to the terms and conditions
// defined in file 'LICENSE.md', which is part of
// this source code package.

using System;
using System.Linq;

using eDoxa.Cashier.Domain.AggregateModels.AccountAggregate.Transactions;
using eDoxa.Cashier.Domain.Validators;
using eDoxa.Seedwork.Common.Enumerations;

namespace eDoxa.Cashier.Domain.AggregateModels.AccountAggregate
{
    public class AccountMoney : IAccountMoney
    {
        private readonly IAccount _account;

        public AccountMoney(IAccount account)
        {
            _account = account;
        }

        public Balance Balance => new Balance(_account.Transactions, CurrencyType.Money);

        public DateTime? LastDeposit =>
            _account.Transactions
                .Where(
                    transaction => transaction.Currency.Type == CurrencyType.Money &&
                                   transaction.Type == TransactionType.Deposit &&
                                   transaction.Status == TransactionStatus.Succeded
                )
                .OrderByDescending(transaction => transaction.Timestamp)
                .FirstOrDefault()
                ?.Timestamp;

        public DateTime? LastWithdraw =>
            _account.Transactions
                .Where(
                    transaction => transaction.Currency.Type == CurrencyType.Money &&
                                   transaction.Type == TransactionType.Withdraw &&
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

            var transaction = new MoneyChargeTransaction(-amount);

            _account.CreateTransaction(transaction);

            return transaction;
        }

        public ITransaction Payout(Money amount)
        {
            var transaction = new MoneyPayoutTransaction(amount);

            _account.CreateTransaction(transaction);

            return transaction;
        }

        public ITransaction Withdraw(Money amount)
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
            return new WithdrawMoneyValidator(money).Validate(this).IsValid;
        }
    }
}
