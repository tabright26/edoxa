// Filename: User.cs
// Date Created: 2019-07-21
// 
// ================================================
// Copyright © 2019, eDoxa. All rights reserved.

using System;
using System.Collections.Generic;
using System.Collections.ObjectModel;

using eDoxa.Seedwork.Domain.Miscs;

using Microsoft.AspNetCore.Identity;

namespace eDoxa.Identity.Api.Infrastructure.Models
{
    public class User : IdentityUser<Guid>
    {
#nullable disable
        public User()
        {
            AddressBook = new Collection<UserAddress>();
            DoxaTagHistory = new Collection<UserDoxaTag>();
        }
#nullable restore
        public Country Country { get; set; }

        public UserPersonalInfo? PersonalInfo { get; set; }

        public ICollection<UserDoxaTag> DoxaTagHistory { get; set; }

        public ICollection<UserAddress> AddressBook { get; set; }
    }
}
