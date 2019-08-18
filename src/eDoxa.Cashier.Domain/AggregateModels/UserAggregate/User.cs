// Filename: User.cs
// Date Created: 2019-07-12
// 
// ================================================
// Copyright © 2019, eDoxa. All rights reserved.

using eDoxa.Cashier.Domain.AggregateModels.AccountAggregate;
using eDoxa.Seedwork.Domain;

namespace eDoxa.Cashier.Domain.AggregateModels.UserAggregate
{
    public sealed class User : Entity<UserId>
    {
        public User(UserId userId)
        {
            this.SetEntityId(userId);
        }
    }
}
