// Filename: User.cs
// Date Created: 2019-07-21
// 
// ================================================
// Copyright © 2019, eDoxa. All rights reserved.

using System;
using System.Collections.Generic;
using System.Collections.ObjectModel;

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
            Informations = null;
            DoxatagHistory = new Collection<UserDoxatag>();
            AddressBook = new Collection<UserAddress>();
        }
#nullable restore
        public Country Country { get; set; }

        public UserInformations? Informations { get; set; }

        public ICollection<UserDoxatag> DoxatagHistory { get; set; }

        public ICollection<UserAddress> AddressBook { get; set; }
    }
}
