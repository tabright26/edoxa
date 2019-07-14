// Filename: HashedPassword.cs
// Date Created: --
// 
// ================================================
// Copyright © 2019, eDoxa. All rights reserved.

using Microsoft.AspNetCore.Identity;

namespace eDoxa.Identity.Domain.AggregateModels.UserAggregate
{
    public sealed class HashPassword : Password
    {
        private static readonly PasswordHasher<User> Hasher = new PasswordHasher<User>();

        public HashPassword(User user, string password) : base(Hasher.HashPassword(user, password))
        {
        }
    }
}
