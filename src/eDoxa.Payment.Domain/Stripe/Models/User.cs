// Filename: User.cs
// Date Created: 2019-10-07
// 
// ================================================
// Copyright © 2019, eDoxa. All rights reserved.

using eDoxa.Seedwork.Domain;

namespace eDoxa.Payment.Domain.Stripe.Models
{
    public sealed class User : Entity<UserId>
    {
        public User(UserId userId)
        {
            this.SetEntityId(userId);
        }
    }
}
