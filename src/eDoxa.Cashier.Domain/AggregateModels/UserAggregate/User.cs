// Filename: User.cs
// Date Created: 2019-04-14
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
        private Account _account;
        private CustomerId _customerId;

        private User(UserId userId, CustomerId customerId) : this()
        {
            Id = userId;
            _customerId = customerId;
        }

        private User()
        {
            _account = new Account(this);
        }

        public CustomerId CustomerId => _customerId;

        public Account Account => _account;

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
            Account.AddFunds(bundle.Amount);

            return Account.Funds.Balance;
        }

        public Money Withdrawal(Money amount)
        {
            Account.Withdrawal(amount);

            return Account.Funds.Balance;
        }

        public Token BuyTokens(TokenBundle bundle)
        {
            Account.BuyTokens(bundle.Amount);

            return Account.Tokens.Balance;
        }
    }
}