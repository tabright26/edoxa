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
}