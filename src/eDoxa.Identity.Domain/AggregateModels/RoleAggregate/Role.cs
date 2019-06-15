// Filename: Role.cs
// Date Created: 2019-06-01
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

namespace eDoxa.Identity.Domain.AggregateModels.RoleAggregate
{
    public sealed class Role : IdentityRole<Guid>
    {
        public Role(string roleName) : this()
        {
            Name = roleName;
            NormalizedName = roleName.ToUpperInvariant();
        }

        private Role()
        {
            Claims = new HashSet<RoleClaim>();
        }

        public ICollection<RoleClaim> Claims { get; set; }
    }
}
