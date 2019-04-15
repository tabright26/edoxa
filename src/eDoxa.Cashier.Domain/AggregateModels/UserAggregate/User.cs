// Filename: User.cs
// Date Created: 2019-04-14
// 
// ============================================================
// Copyright © 2019, Francis Quenneville
// All rights reserved.
// 
// This file is subject to the terms and conditions defined in file 'LICENSE.md', which is part of
// this source code package.

using System;

using eDoxa.Cashier.Domain.AggregateModels.UserAggregate.DomainEvents;
using eDoxa.Seedwork.Domain;
using eDoxa.Seedwork.Domain.Aggregate;
using eDoxa.Seedwork.Domain.Common;

namespace eDoxa.Cashier.Domain.AggregateModels.UserAggregate
{
    public class User : Entity<UserId>, IAggregateRoot
    {
        private CustomerId _customerId;
        private Account _account;

        public User(UserId userId, CustomerId customerId) : this()
        {
            Id = userId ?? throw new ArgumentNullException(nameof(userId));
            _customerId = customerId ?? throw new ArgumentNullException(nameof(customerId));
        }

        private User()
        {
            _account = new Account(this);
        }

        public CustomerId CustomerId
        {
            get
            {
                return _customerId;
            }
        }

        public Account Account
        {
            get
            {
                return _account;
            }
        }

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
            return Account.AddFunds(bundle.Amount);
        }

        public Money Withdrawal(Money amount)
        {
            return Account.Withdrawal(amount);
        }

        public Token BuyTokens(TokenBundle bundle)
        {
            return Account.BuyTokens(bundle.Amount);
        }
    }
}