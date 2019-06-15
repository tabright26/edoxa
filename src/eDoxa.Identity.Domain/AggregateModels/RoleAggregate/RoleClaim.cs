// Filename: RoleClaim.cs
// Date Created: 2019-06-01
// 
// ================================================
// Copyright © 2019, eDoxa. All rights reserved.
// 
// This file is subject to the terms and conditions
// defined in file 'LICENSE.md', which is part of
// this source code package.

using System;

using Microsoft.AspNetCore.Identity;

namespace eDoxa.Identity.Domain.AggregateModels.RoleAggregate
{
    public sealed class RoleClaim : IdentityRoleClaim<Guid>
    {
        public RoleClaim(Guid roleId, string type, string value) : this()
        {
            RoleId = roleId;
            ClaimType = type;
            ClaimValue = value;
        }

        public RoleClaim()
        {
            // Required by EF core.
        }
    }
}
