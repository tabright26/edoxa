// Filename: User.cs
// Date Created: 2019-04-21
// 
// ================================================
// Copyright © 2019, eDoxa. All rights reserved.
//  
// This file is subject to the terms and conditions
// defined in file 'LICENSE.md', which is part of
// this source code package.

using eDoxa.Cashier.Domain.AggregateModels.UserAggregate.DomainEvents;
using eDoxa.Seedwork.Domain;
using eDoxa.Seedwork.Domain.Aggregate;
using eDoxa.Seedwork.Domain.Common;

namespace eDoxa.Cashier.Domain.AggregateModels.UserAggregate
{
    public class User : Entity<UserId>, IAggregateRoot
    {
        private CustomerId _customerId;
        private MoneyAccount _funds;
        private TokenAccount _tokens;

        private User(UserId userId, CustomerId customerId) : this()
        {
            Id = userId;
            _customerId = customerId;
        }

        private User()
        {
            _funds = new MoneyAccount(this);
            _tokens = new TokenAccount(this);
        }

        public CustomerId CustomerId => _customerId;

        public MoneyAccount Funds => _funds;

        public TokenAccount Tokens => _tokens;

        public static User Create(UserId userId, CustomerId customerId)
        {
            var user = new User(userId, customerId);

            user.AddDomainEvent(new UserCreatedDomainEvent(userId, customerId));

            return user;
        }

        public static User Create(UserData data)
        {
            return new User(UserId.FromGuid(data.Id), CustomerId.Parse(data.StripeCustomerId));
        }

        public Money AddFunds(MoneyBundle bundle)
        {
            Funds.AddBalance(bundle.Amount);

            return Funds.Balance;
        }

        public Money Withdraw(Money amount)
        {
            Funds.SubtractBalance(amount);

            return Funds.Balance;
        }

        public Token BuyTokens(TokenBundle bundle)
        {
            Tokens.AddBalance(bundle.Amount);

            return Tokens.Balance;
        }
    }
}