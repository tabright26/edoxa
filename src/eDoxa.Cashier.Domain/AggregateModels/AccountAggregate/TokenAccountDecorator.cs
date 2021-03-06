﻿// Filename: TokenAccountDecorator.cs
// Date Created: 2019-11-25
// 
// ================================================
// Copyright © 2019, eDoxa. All rights reserved.

using System;
using System.Linq;

namespace eDoxa.Cashier.Domain.AggregateModels.AccountAggregate
{
    public sealed class TokenAccountDecorator : AccountDecorator, ITokenAccount
    {
        public static readonly TimeSpan DepositInterval = TimeSpan.FromDays(1);

        public TokenAccountDecorator(IAccount account) : base(account)
        {
        }

        public Balance Balance => new Balance(Transactions, CurrencyType.Token);

        public DateTime? LastDeposit =>
            Transactions.Where(
                    transaction => transaction.Currency.Type == CurrencyType.Token &&
                                   transaction.Type == TransactionType.Deposit &&
                                   transaction.Status == TransactionStatus.Succeeded)
                .OrderByDescending(transaction => transaction)
                .FirstOrDefault()
                ?.Timestamp;

        public ITransaction Deposit(Token amount)
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

        public ITransaction Charge(Token amount, TransactionMetadata? metadata = null)
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

        public ITransaction Payout(Token amount, TransactionMetadata? metadata = null)
        {
            var builder = new TransactionBuilder(TransactionType.Payout, amount).WithMetadata(metadata);

            var transaction = builder.Build();

            this.CreateTransaction(transaction);

            return transaction;
        }

        public ITransaction Reward(Token amount, TransactionMetadata? metadata = null)
        {
            var builder = new TransactionBuilder(TransactionType.Reward, amount).WithMetadata(metadata);

            var transaction = builder.Build();

            this.CreateTransaction(transaction);

            return transaction;
        }

        public ITransaction Promotion(Token amount, TransactionMetadata? metadata = null)
        {
            var builder = new TransactionBuilder(TransactionType.Promotion, amount).WithMetadata(metadata);

            var transaction = builder.Build();

            this.CreateTransaction(transaction);

            return transaction;
        }

        public bool IsDepositAvailable()
        {
            return !(LastDeposit.HasValue && LastDeposit.Value.Add(DepositInterval) >= DateTime.UtcNow);
        }

        public bool HaveSufficientMoney(Token token)
        {
            return Balance.Available >= token;
        }

        public bool CanDeposit()
        {
            return this.IsDepositAvailable();
        }

        public bool CanCharge(Token token)
        {
            return this.HaveSufficientMoney(token);
        }
    }
}
