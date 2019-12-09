// Filename: TokenAccountDecorator.cs
// Date Created: 2019-11-25
// 
// ================================================
// Copyright © 2019, eDoxa. All rights reserved.

using System;
using System.Linq;

using eDoxa.Seedwork.Domain.Misc;

namespace eDoxa.Cashier.Domain.AggregateModels.AccountAggregate
{
    public sealed class TokenAccountDecorator : AccountDecorator, ITokenAccount
    {
        public TokenAccountDecorator(IAccount account) : base(account)
        {
        }

        public Balance Balance => new Balance(Transactions, Currency.Token);

        public DateTime? LastDeposit =>
            Transactions.Where(
                    transaction => transaction.Currency.Type == Currency.Token &&
                                   transaction.Type == TransactionType.Deposit &&
                                   transaction.Status == TransactionStatus.Succeded)
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

            var builder = new TransactionBuilder(TransactionType.Charge, amount);

            var transaction = builder.Build();
            
            this.CreateTransaction(transaction);

            return transaction;
        }

        public ITransaction Payout(Token amount, TransactionMetadata? metadata = null)
        {
            var builder = new TransactionBuilder(TransactionType.Payout, amount);

            var transaction = builder.Build();

            this.CreateTransaction(transaction);

            return transaction;
        }

        public ITransaction Reward(Token amount, TransactionMetadata? metadata = null)
        {
            var builder = new TransactionBuilder(TransactionType.Reward, amount);

            var transaction = builder.Build();

            this.CreateTransaction(transaction);

            return transaction;
        }

        public bool IsDepositAvailable()
        {
            return !(LastDeposit.HasValue && LastDeposit.Value.AddDays(1) >= DateTime.UtcNow);
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
