// Filename: User.cs
// Date Created: 2019-07-21
// 
// ================================================
// Copyright © 2019, eDoxa. All rights reserved.

using System;
using System.Collections.Generic;

using Microsoft.AspNetCore.Identity;

namespace eDoxa.Identity.Api.Infrastructure.Models
{
    public class User : IdentityUser<Guid>
    {
        public User()
        {
            AddressBook = new List<UserAddress>();
        }

        public PersonalInfo? PersonalInfo { get; set; }

        public DoxaTag? DoxaTag { get; set; }

        public IList<UserAddress> AddressBook { get; set; }
    }
}
