// Filename: User.cs
// Date Created: 2019-07-21
// 
// ================================================
// Copyright © 2019, eDoxa. All rights reserved.

using System;

using eDoxa.Seedwork.Domain.Misc;

using Microsoft.AspNetCore.Identity;

namespace eDoxa.Identity.Domain.AggregateModels.UserAggregate
{
    public class User : IdentityUser<Guid>
    {
#nullable disable
        public User()
        {
            Profile = null;
        }
#nullable restore
        public Country Country { get; set; }

        public UserProfile? Profile { get; set; }
    }
}
