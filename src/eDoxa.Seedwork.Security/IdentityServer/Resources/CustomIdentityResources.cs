// Filename: CustomIdentityResources.cs
// Date Created: 2019-08-18
// 
// ================================================
// Copyright © 2019, eDoxa. All rights reserved.

using System.Collections.Generic;

using IdentityModel;

using IdentityServer4.Models;

namespace eDoxa.Seedwork.Security.IdentityServer.Resources
{
    public sealed class CustomIdentityResources
    {
        public static readonly RoleIdentityResource Roles = new RoleIdentityResource();
        public static readonly PermissionIdentityResource Permissions = new PermissionIdentityResource();
        public static readonly GameIdentityResource Games = new GameIdentityResource();
        public static readonly StripeIdentityResource Stripe = new StripeIdentityResource();

        public sealed class GameIdentityResource : IdentityResource
        {
            internal GameIdentityResource() : base(
                "games",
                "Your game(s)",
                new HashSet<string>
                {
                    "games"
                }
            )
            {
            }
        }

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
                    AppClaimTypes.Permission
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
                    AppClaimTypes.StripeConnectAccountId,
                    AppClaimTypes.StripeCustomerId
                }
            )
            {
            }
        }
    }
}
