// Filename: User.cs
// Date Created: 2019-04-21
// 
// ================================================
// Copyright © 2019, eDoxa. All rights reserved.
//  
// This file is subject to the terms and conditions
// defined in file 'LICENSE.md', which is part of
// this source code package.

using eDoxa.Seedwork.Domain;
using eDoxa.Seedwork.Domain.Aggregate;
using eDoxa.Seedwork.Domain.Common;

namespace eDoxa.Cashier.Domain.AggregateModels.UserAggregate
{
    public class User : Entity<UserId>, IAggregateRoot
    {
        private CustomerId _customerId;
        private MoneyAccount _moneyAccount;
        private TokenAccount _tokenAccount;

        public User(UserId userId, CustomerId customerId) : this()
        {
            Id = userId;
            _customerId = customerId;
        }

        public User(UserData data) : this()
        {
            Id = UserId.FromGuid(data.Id);
            _customerId = CustomerId.Parse(data.StripeCustomerId);
        }

        private User()
        {
            _moneyAccount = new MoneyAccount(this);
            _tokenAccount = new TokenAccount(this);
        }

        public CustomerId CustomerId => _customerId;

        public MoneyAccount MoneyAccount => _moneyAccount;

        public TokenAccount TokenAccount => _tokenAccount;

        public Money AddFunds(MoneyBundle bundle)
        {
            MoneyAccount.Deposit(bundle.Amount);

            return MoneyAccount.Balance;
        }

        public Money Withdraw(Money amount)
        {
            MoneyAccount.Withdraw(amount);

            return MoneyAccount.Balance;
        }

        public Token BuyTokens(TokenBundle bundle)
        {
            TokenAccount.Deposit(bundle.Amount);

            return TokenAccount.Balance;
        }
    }
}