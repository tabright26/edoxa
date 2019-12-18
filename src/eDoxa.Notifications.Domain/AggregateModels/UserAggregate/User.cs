// Filename: User.cs
// Date Created: 2019-12-17
// 
// ================================================
// Copyright © 2019, eDoxa. All rights reserved.

using eDoxa.Seedwork.Domain;
using eDoxa.Seedwork.Domain.Misc;

namespace eDoxa.Notifications.Domain.AggregateModels.UserAggregate
{
    public sealed class User : Entity<UserId>
    {
        public User(UserId userId, string email) : this()
        {
            this.SetEntityId(userId);
            Email = email;
        }

        private User()
        {
        }

        public string Email { get; private set; }

        public void Update(string email)
        {
            Email = email;
        }
    }
}
