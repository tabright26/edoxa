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
        public static readonly RoleIdentityResource Roles = new RoleIdentityResource();
        public static readonly PermissionIdentityResource Permissions = new PermissionIdentityResource();
        public static readonly StripeIdentityResource Stripe = new StripeIdentityResource();

        public sealed class RoleIdentityResource : IdentityResource
        {
            internal RoleIdentityResource() : base(
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

        public sealed class PermissionIdentityResource : IdentityResource
        {
            internal PermissionIdentityResource() : base(
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

        public sealed class StripeIdentityResource : IdentityResource
        {
            internal StripeIdentityResource() : base(
                "stripe",
                "Stripe",
                new HashSet<string>
                {
                    CustomClaimTypes.StripeConnectAccountId,
                    CustomClaimTypes.StripeCustomerId
                }
            )
            {
            }
        }
    }
}
