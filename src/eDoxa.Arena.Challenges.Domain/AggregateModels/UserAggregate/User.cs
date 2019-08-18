// Filename: User.cs
// Date Created: 2019-07-12
// 
// ================================================
// Copyright © 2019, eDoxa. All rights reserved.

using eDoxa.Seedwork.Domain;

namespace eDoxa.Arena.Challenges.Domain.AggregateModels.UserAggregate
{
    public sealed class User : Entity<UserId>
    {
        public User(UserId userId)
        {
            this.SetEntityId(userId);
        }
    }
}
