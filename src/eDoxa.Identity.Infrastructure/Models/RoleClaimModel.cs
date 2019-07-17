// Filename: RoleClaimModel.cs
// Date Created: 2019-07-16
// 
// ================================================
// Copyright © 2019, eDoxa. All rights reserved.

using System;

using Microsoft.AspNetCore.Identity;

namespace eDoxa.Identity.Infrastructure.Models
{
    public class RoleClaimModel : IdentityRoleClaim<Guid>
    {
    }
}
