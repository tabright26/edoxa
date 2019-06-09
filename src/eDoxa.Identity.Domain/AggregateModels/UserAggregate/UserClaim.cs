﻿// Filename: UserClaim.cs
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

namespace eDoxa.Identity.Domain.AggregateModels.UserAggregate
{
    public sealed class UserClaim : IdentityUserClaim<Guid>
    {
        public UserClaim(Guid userId, string type, string value) : this()
        {
            UserId = userId;
            ClaimType = type;
            ClaimValue = value;
        }

        public UserClaim()
        {
            // Required by EF Core.
        }
    }
}
