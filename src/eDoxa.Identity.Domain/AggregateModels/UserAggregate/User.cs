// Filename: User.cs
// Date Created: 2019-05-06
// 
// ================================================
// Copyright © 2019, eDoxa. All rights reserved.
// 
// This file is subject to the terms and conditions
// defined in file 'LICENSE.md', which is part of
// this source code package.

using System;
using System.Collections.Generic;

using Microsoft.AspNetCore.Identity;

namespace eDoxa.Identity.Domain.AggregateModels.UserAggregate
{
    public sealed class User : IdentityUser<Guid>
    {
        public User()
        {
            Roles = new HashSet<UserRole>();
            Claims = new HashSet<UserClaim>();
            Logins = new HashSet<UserLogin>();
            Tokens = new HashSet<UserToken>();
        }

        public ICollection<UserRole> Roles { get; set; }

        public ICollection<UserClaim> Claims { get; set; }

        public ICollection<UserLogin> Logins { get; set; }

        public ICollection<UserToken> Tokens { get; set; }

        public void HashPassword(string password)
        {
            this.UpdateSecurityStamp();

            var hasher = new PasswordHasher<User>();

            PasswordHash = hasher.HashPassword(this, password);
        }

        private void UpdateSecurityStamp()
        {
            SecurityStamp = Guid.NewGuid().ToString("D");
        }
    }
}