// Filename: TokenAccount.cs
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
    public sealed class TokenAccount : ITokenAccount
    {
        private readonly IAccount _account;

        public TokenAccount(IAccount account)
        {
            _account = account;
        }

        public Balance Balance => new Balance(_account.Transactions, Currency.Token);

        public DateTime? LastDeposit =>
            _account.Transactions
                .Where(
                    transaction => transaction.Currency.Type == Currency.Token &&
                                   transaction.Type == TransactionType.Deposit &&
                                   transaction.Status == TransactionStatus.Succeded
                )
                .OrderByDescending(transaction => transaction)
                .FirstOrDefault()
                ?.Timestamp;

        public ITransaction Deposit(Token amount, IImmutableSet<Bundle> bundles)
        {
            if (!this.CanDeposit())
            {
                throw new InvalidOperationException();
            }

            var transaction = new TokenDepositTransaction(amount);

            _account.CreateTransaction(transaction);

            return transaction;
        }

        public ITransaction Charge(TransactionId transactionId, Token amount, TransactionMetadata? metadata = null)
        {
            if (!this.CanCharge(amount))
            {
                throw new InvalidOperationException();
            }

            var transaction = new TokenChargeTransaction(amount, metadata);

            transaction.SetEntityId(transactionId);

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
