// Filename: TokenAccount.cs
// Date Created: 2019-04-24
// 
// ================================================
// Copyright © 2019, eDoxa. All rights reserved.
//  
// This file is subject to the terms and conditions
// defined in file 'LICENSE.md', which is part of
// this source code package.

using System.Collections.Generic;
using System.Linq;

using eDoxa.Seedwork.Domain.Aggregate;

namespace eDoxa.Cashier.Domain.AggregateModels.UserAggregate
{
    public class TokenAccount : Entity<AccountId>, IAccount<Token>
    {
        private User _user;        
        private Token _pending;
        private HashSet<TokenTransaction> _transactions;

        public TokenAccount(User user) : this()
        {
            _user = user;
        }

        private TokenAccount()
        {            
            _pending = Token.Zero;
            _transactions = new HashSet<TokenTransaction>();
        }

        public User User => _user;


        public IReadOnlyCollection<TokenTransaction> Transactions => _transactions;

        public Token Balance => new Token(Transactions.Sum(transaction => transaction.Amount));

        public Token Pending => _pending;

        public void AddBalance(Token amount)
        {
            _transactions.Add(new TokenTransaction(this, amount));
        }

        public void SubtractBalance(Token amount)
        {
            _transactions.Add(new TokenTransaction(this, -amount));
        }

        public void AddPending(Token amount)
        {
            _pending = new Token(_pending + amount);
        }
        

        public void SubtractPending(Token amount)
        {
            _pending = new Token(_pending - amount);
        }
    }
}