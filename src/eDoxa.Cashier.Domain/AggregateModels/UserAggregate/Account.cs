// Filename: Account.cs
// Date Created: 2019-04-21
// 
// ================================================
// Copyright © 2019, eDoxa. All rights reserved.
//  
// This file is subject to the terms and conditions
// defined in file 'LICENSE.md', which is part of
// this source code package.

using eDoxa.Seedwork.Domain.Aggregate;

namespace eDoxa.Cashier.Domain.AggregateModels.UserAggregate
{
    public class Account : Entity<AccountId>
    {
        private MoneyAccount _funds;
        private TokenAccount _tokens;
        private User _user;

        public Account(User user) : this()
        {
            _user = user;
        }

        private Account()
        {
            _funds = new MoneyAccount();
            _tokens = new TokenAccount();
        }

        public MoneyAccount Funds => _funds;

        public TokenAccount Tokens => _tokens;

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

    public class MoneyAccount : IAccount<Money>
    {
        private Money _balance;
        private Money _pending;

        public MoneyAccount()
        {
            _balance = Money.Zero;
            _pending = Money.Zero;
        }

        public Money Balance => _balance;

        public Money Pending => _pending;

        public void AddBalance(Money currency)
        {
            _balance = new Money((long) _balance + (long) currency);
        }

        public void AddPending(Money currency)
        {
            _pending = new Money((long) _pending + (long) currency);
        }

        public void SubtractBalance(Money currency)
        {
            _balance = new Money((long) _balance - (long) currency);
        }

        public void SubtractPending(Money currency)
        {
            _pending = new Money((long) _pending - (long) currency);
        }
    }

    public class TokenAccount : IAccount<Token>
    {
        private Token _balance;
        private Token _pending;

        public TokenAccount()
        {
            _balance = Token.Zero;
            _pending = Token.Zero;
        }

        public Token Balance => _balance;

        public Token Pending => _pending;

        public void AddBalance(Token currency)
        {
            _balance = new Token((long) _balance + (long) currency);
        }

        public void AddPending(Token currency)
        {
            _pending = new Token((long) _pending + (long) currency);
        }

        public void SubtractBalance(Token currency)
        {
            _balance = new Token((long) _balance - (long) currency);
        }

        public void SubtractPending(Token currency)
        {
            _pending = new Token((long) _pending - (long) currency);
        }
    }
}