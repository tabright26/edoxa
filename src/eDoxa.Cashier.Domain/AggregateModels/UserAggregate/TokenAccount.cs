// Filename: TokenAccount.cs
// Date Created: 2019-04-26
// 
// ================================================
// Copyright © 2019, eDoxa. All rights reserved.
//  
// This file is subject to the terms and conditions
// defined in file 'LICENSE.md', which is part of
// this source code package.

using System;
using System.Collections.Generic;
using System.Linq;

using eDoxa.Seedwork.Domain.Aggregate;

namespace eDoxa.Cashier.Domain.AggregateModels.UserAggregate
{
    public class TokenAccount : Entity<AccountId>, ITokenAccount
    {
        private HashSet<TokenTransaction> _transactions;
        private User _user;

        public TokenAccount(User user) : this()
        {
            _user = user;
        }

        private TokenAccount()
        {
            _transactions = new HashSet<TokenTransaction>();
        }

        public User User => _user;

        public IReadOnlyCollection<TokenTransaction> Transactions => _transactions;

        public Token Balance => new Token(Transactions.Sum(transaction => transaction.Amount));

        public Token Pending => new Token(Transactions.Where(transaction => transaction.Pending).Sum(transaction => transaction.Amount));

        public ITransaction<Token> Deposit(Token amount)
        {
            var transaction = new TokenTransaction(amount);

            _transactions.Add(transaction);

            return transaction;
        }

        public ITransaction<Token> Register(Token amount, ActivityId activityId)
        {
            var transaction = new TokenPendingTransaction(-amount, activityId);

            _transactions.Add(transaction);

            return transaction;
        }

        public ITransaction<Token> Payoff(Token amount, ActivityId activityId)
        {
            throw new NotImplementedException();
        }
    }
}