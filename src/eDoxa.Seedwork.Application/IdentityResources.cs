﻿// Filename: IdentityResources.cs
// Date Created: 2019-09-02
// 
// ================================================
// Copyright © 2019, eDoxa. All rights reserved.

using System.Collections.Generic;

using IdentityModel;

using IdentityServer4.Models;

namespace eDoxa.Seedwork.Application
{
    public sealed class IdentityResources
    {
        public static readonly RoleIdentityResource Roles = new RoleIdentityResource();
        public static readonly PermissionIdentityResource Permissions = new PermissionIdentityResource();
        public static readonly GameIdentityResource Games = new GameIdentityResource();
        public static readonly CountryIdentityResource Country = new CountryIdentityResource();
        public static readonly StripeIdentityResource Stripe = new StripeIdentityResource();

        public sealed class StripeIdentityResource : IdentityResource
        {
            internal StripeIdentityResource() : base(
                Scopes.Stripe.Name,
                "Stripe.js",
                new HashSet<string>
                {
                    CustomClaimTypes.StripeCustomer
                })
            {
            }
        }

        public sealed class CountryIdentityResource : IdentityResource
        {
            internal CountryIdentityResource() : base(
                Scopes.Country.Name,
                "Your legal country",
                new HashSet<string>
                {
                    CustomClaimTypes.Country
                })
            {
            }
        }

        public sealed class GameIdentityResource : IdentityResource
        {
            internal GameIdentityResource() : base(
                Scopes.Games.Name,
                "Your game(s)",
                new HashSet<string>
                {
                    "games"
                })
            {
            }
        }

        public sealed class RoleIdentityResource : IdentityResource
        {
            internal RoleIdentityResource() : base(
                Scopes.Roles.Name,
                "Your role(s)",
                new HashSet<string>
                {
                    JwtClaimTypes.Role
                })
            {
            }
        }

        public sealed class PermissionIdentityResource : IdentityResource
        {
            internal PermissionIdentityResource() : base(
                Scopes.Permissions.Name,
                "Your permission(s)",
                new HashSet<string>
                {
                    CustomClaimTypes.Permission
                })
            {
            }
        }
    }
}
