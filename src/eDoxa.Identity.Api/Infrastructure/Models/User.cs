// Filename: User.cs
// Date Created: 2019-07-21
// 
// ================================================
// Copyright © 2019, eDoxa. All rights reserved.

using System;
using System.Collections.Generic;
using System.Collections.ObjectModel;

using Microsoft.AspNetCore.Identity;

namespace eDoxa.Identity.Api.Infrastructure.Models
{
    public class User : IdentityUser<Guid>
    {
        public User()
        {
            AddressBook = new Collection<UserAddress>();
            DoxaTagHistory = new Collection<UserDoxaTag>();
        }

        public UserPersonalInfo? PersonalInfo { get; set; }

        public ICollection<UserDoxaTag> DoxaTagHistory { get; set; }

        public ICollection<UserAddress> AddressBook { get; set; }
    }
}
