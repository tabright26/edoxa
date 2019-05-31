// Filename: TokenAccount.cs
// Date Created: 2019-05-29
// 
// ================================================
// Copyright © 2019, eDoxa. All rights reserved.
// 
// This file is subject to the terms and conditions
// defined in file 'LICENSE.md', which is part of
// this source code package.

using System;
using System.Linq;

using eDoxa.Cashier.Domain.Abstractions;
using eDoxa.Cashier.Domain.AggregateModels.AccountAggregate.Transactions;
using eDoxa.Cashier.Domain.Validators;
using eDoxa.Seedwork.Domain.Common.Enumerations;

namespace eDoxa.Cashier.Domain.AggregateModels.AccountAggregate
{
    public sealed class AccountToken : IAccountToken
    {
        private readonly IAccount _account;

        public AccountToken(IAccount account)
        {
            _account = account;
        }

        public Balance Balance => new Balance(_account.Transactions, CurrencyType.Token);

        public DateTime? LastDeposit =>
            _account.Transactions
                .Where(
                    transaction => transaction.Currency.Type == CurrencyType.Token &&
                                   transaction.Type == TransactionType.Deposit &&
                                   transaction.Status == TransactionStatus.Completed
                )
                .OrderByDescending(transaction => transaction)
                .FirstOrDefault()
                ?.Timestamp;

        public ITransaction Deposit(Token amount)
        {
            if (!this.CanDeposit())
            {
                throw new InvalidOperationException();
            }

            var transaction = new TokenDepositTransaction(amount);

            _account.CreateTransaction(transaction);

            return transaction;
        }

        public ITransaction Charge(Token amount)
        {
            if (!this.CanCharge(amount))
            {
                throw new InvalidOperationException();
            }

            var transaction = new TokenChargeTransaction(amount);

            _account.CreateTransaction(transaction);

            return transaction;
        }

        public ITransaction Payout(Token amount)
        {
            var transaction = new TokenPayoutTransaction(amount);

            _account.CreateTransaction(transaction);

            return transaction;
        }

        public ITransaction Reward(Token amount)
        {
            var transaction = new TokenRewardTransaction(amount);

            _account.CreateTransaction(transaction);

            return transaction;
        }

        public ITransaction CompleteTransaction(ITransaction transaction)
        {
            transaction.Complete();

            return transaction;
        }

        public ITransaction FailureTransaction(ITransaction transaction, string message)
        {
            transaction.Fail(message);

            return transaction;
        }

        public bool CanDeposit()
        {
            return new DepositTokenValidator().Validate(this).IsValid;
        }

        public bool CanCharge(Token token)
        {
            return new ChargeTokenValidator(token).Validate(this).IsValid;
        }
    }
}
