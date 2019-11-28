// Filename: User.cs
// Date Created: 2019-07-21
// 
// ================================================
// Copyright © 2019, eDoxa. All rights reserved.

using System;
using System.Collections.Generic;

using eDoxa.Identity.Domain.AggregateModels.AddressAggregate;
using eDoxa.Identity.Domain.AggregateModels.DoxatagAggregate;
using eDoxa.Seedwork.Domain.Miscs;

using Microsoft.AspNetCore.Identity;

namespace eDoxa.Identity.Domain.AggregateModels.UserAggregate
{
    public class User : IdentityUser<Guid>
    {
#nullable disable
        public User()
        {
            Profile = null;
            DoxatagHistory = new HashSet<Doxatag>();
            AddressBook = new HashSet<Address>();
        }
#nullable restore
        public Country Country { get; set; }

        public UserProfile? Profile { get; set; }

        public ICollection<Doxatag> DoxatagHistory { get; set; }

        public ICollection<Address> AddressBook { get; set; }
    }
}
