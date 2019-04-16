// Filename: Account.cs
// Date Created: 2019-04-14
// 
// ================================================
// Copyright © 2019, eDoxa. All rights reserved.
//  
// This file is subject to the terms and conditions
// defined in file 'LICENSE.md', which is part of
// this source code package.

using System;
using eDoxa.Seedwork.Domain.Aggregate;

namespace eDoxa.Cashier.Domain.AggregateModels.UserAggregate
{
    public class Account : Entity<AccountId>
    {
        private Account<Money> _funds;
        private Account<Token> _tokens;
        private User _user;

        public Account(User user) : this()
        {
            _user = user ?? throw new ArgumentNullException(nameof(user));
        }

        private Account()
        {
            _funds = new Account<Money>();
            _tokens = new Account<Token>();
        }

        public Account<Money> Funds => _funds;

        public Account<Token> Tokens => _tokens;

        public User User => _user;

        public void AddFunds(Money money)
        {
            Funds.AddBalance(money);
        }

        public void Withdrawal(Money money)
        {
            Funds.SubtractBalance(money);
        }

        public void BuyTokens(Token tokens)
        {
            Tokens.AddBalance(tokens);
        }
    }

    public class Account<TCurrency>
        where TCurrency : Currency<TCurrency>, new()
    {
        private TCurrency _balance;
        private TCurrency _pending;

        public Account()
        {
            _balance = Currency<TCurrency>.Empty;
            _pending = Currency<TCurrency>.Empty;
        }

        public TCurrency Balance => _balance;

        public TCurrency Pending => _pending;

        public void AddBalance(TCurrency currency)
        {
            _balance += currency;
        }

        public void AddPending(TCurrency currency)
        {
            _pending += currency;
        }

        public void SubtractBalance(TCurrency currency)
        {
            _balance -= currency;
        }

        public void SubtractPending(TCurrency currency)
        {
            _pending -= currency;
        }
    }
}