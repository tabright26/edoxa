// Filename: Account.cs
// Date Created: 2019-04-09
// 
// ============================================================
// Copyright © 2019, Francis Quenneville
// All rights reserved.
// 
// This file is subject to the terms and conditions defined in file 'LICENSE.md', which is part of
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

        public Account<Money> Funds
        {
            get
            {
                return _funds;
            }
        }

        public Account<Token> Tokens
        {
            get
            {
                return _tokens;
            }
        }

        public User User
        {
            get
            {
                return _user;
            }
        }

        public Money AddFunds(Money money)
        {
            return Funds.AddBalance(money);
        }

        public Money Withdrawal(Money money)
        {
            return Funds.SubtractBalance(money);
        }

        public Token BuyTokens(Token tokens)
        {
            return Tokens.AddBalance(tokens);
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

        public TCurrency Balance
        {
            get
            {
                return _balance;
            }
        }

        public TCurrency Pending
        {
            get
            {
                return _pending;
            }
        }

        public TCurrency AddBalance(TCurrency currency)
        {
            _balance += currency;

            return _balance;
        }

        public TCurrency AddPending(TCurrency currency)
        {
            _pending += currency;

            return _pending;
        }

        public TCurrency SubtractBalance(TCurrency currency)
        {
            _balance -= currency;

            return _balance;
        }

        public TCurrency SubtractPending(TCurrency currency)
        {
            _pending -= currency;

            return _pending;
        }
    }
}