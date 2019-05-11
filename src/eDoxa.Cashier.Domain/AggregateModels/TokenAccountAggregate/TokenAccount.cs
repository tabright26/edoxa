// Filename: TokenAccount.cs
// Date Created: 2019-05-06
// 
// ================================================
// Copyright © 2019, eDoxa. All rights reserved.
// 
// This file is subject to the terms and conditions
// defined in file 'LICENSE.md', which is part of
// this source code package.

using System.Collections.Generic;
using System.Linq;

using eDoxa.Cashier.Domain.Abstractions;
using eDoxa.Functional;
using eDoxa.Seedwork.Domain;
using eDoxa.Seedwork.Domain.Aggregate;

using Serilog;

namespace eDoxa.Cashier.Domain.AggregateModels.TokenAccountAggregate
{
    public class TokenAccount : Entity<AccountId>, ITokenAccount, IAggregateRoot
    {
        private HashSet<TokenTransaction> _transactions;
        private UserId _userId;

        public TokenAccount(UserId userId) : this()
        {
            _userId = userId;
        }

        private TokenAccount()
        {
            _transactions = new HashSet<TokenTransaction>();
        }

        public UserId UserId => _userId;

        public Token Balance =>
            new Token(Transactions
                .Where(transaction => transaction.Status.Equals(TransactionStatus.Paid))
                .Sum(transaction => transaction.Amount));

        public Token Pending =>
            new Token(Transactions
                .Where(transaction => transaction.Status.Equals(TransactionStatus.Pending))
                .Sum(transaction => transaction.Amount));

        public IReadOnlyCollection<TokenTransaction> Transactions => _transactions;

        public ITokenTransaction Deposit(Token amount)
        {
            var transaction = new DepositTokenTransaction(amount);

            if (_transactions.Add(transaction))
            {
                Log.Information(transaction.ToString());
            }

            return transaction;
        }

        public Option<ITokenTransaction> TryRegister(Token amount)
        {
            if (Balance < amount)
            {
                return new Option<ITokenTransaction>();
            }

            var transaction = new ServiceTokenTransaction(amount);

            if (!_transactions.Add(transaction))
            {
                return new Option<ITokenTransaction>();
            }

            Log.Information(transaction.ToString());

            return new Option<ITokenTransaction>(transaction);
        }

        public Option<ITokenTransaction> TryPayout(Token amount)
        {
            var transaction = new PrizeTokenTransaction(amount);

            if (!_transactions.Add(transaction))
            {
                return new Option<ITokenTransaction>();
            }

            Log.Information(transaction.ToString());

            return new Option<ITokenTransaction>(transaction);
        }

        public Option<ITokenTransaction> TryReward(Token amount)
        {
            var transaction = new RewardTokenTransaction(amount);

            if (!_transactions.Add(transaction))
            {
                return new Option<ITokenTransaction>();
            }

            Log.Information(transaction.ToString());

            return new Option<ITokenTransaction>(transaction);
        }
    }
}