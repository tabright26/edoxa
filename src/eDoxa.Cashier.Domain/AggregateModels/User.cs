// Filename: User.cs
// Date Created: 2020-01-20
// 
// ================================================
// Copyright © 2020, eDoxa. All rights reserved.

using eDoxa.Seedwork.Domain;
using eDoxa.Seedwork.Domain.Misc;

namespace eDoxa.Cashier.Domain.AggregateModels
{
    public sealed class User : Entity<UserId>
    {
        public User(UserId userId)
        {
            this.SetEntityId(userId);
        }
    }
}
