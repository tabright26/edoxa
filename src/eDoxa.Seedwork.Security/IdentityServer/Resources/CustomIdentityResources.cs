// Filename: CustomIdentityResources.cs
// Date Created: 2019-06-25
// 
// ================================================
// Copyright © 2019, eDoxa. All rights reserved.
// 
// This file is subject to the terms and conditions
// defined in file 'LICENSE.md', which is part of
// this source code package.

using System.Collections.Generic;

using eDoxa.Seedwork.Security.Constants;

using IdentityModel;

using IdentityServer4.Models;

namespace eDoxa.Seedwork.Security.IdentityServer.Resources
{
    public sealed class CustomIdentityResources
    {
        public static readonly Role Roles = new Role();
        public static readonly Permission Permissions = new Permission();

        public sealed class Role : IdentityResource
        {
            internal Role() : base(
                "roles",
                "Your role(s)",
                new HashSet<string>
                {
                    JwtClaimTypes.Role
                }
            )
            {
            }
        }

        public sealed class Permission : IdentityResource
        {
            internal Permission() : base(
                "permissions",
                "Your permission(s)",
                new HashSet<string>
                {
                    CustomClaimTypes.Permission
                }
            )
            {
            }
        }
    }
}
