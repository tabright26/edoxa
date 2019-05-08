// Filename: CustomIdentityResources.cs
// Date Created: 2019-05-03
// 
// ================================================
// Copyright © 2019, eDoxa. All rights reserved.
// 
// This file is subject to the terms and conditions
// defined in file 'LICENSE.md', which is part of
// this source code package.

using System.Collections.Generic;

using eDoxa.Security.Extensions;

using IdentityModel;

using IdentityServer4.Models;

namespace eDoxa.Security.Resources
{
    public sealed class CustomIdentityResources
    {
        public sealed class Role : IdentityResource
        {
            public Role() : base(
                "roles",
                "Your role(s)",
                new HashSet<string> {JwtClaimTypes.Role})
            {
            }
        }

        public sealed class Permission : IdentityResource
        {
            public Permission() : base(
                "permissions",
                "Your permission(s)",
                new HashSet<string> {CustomClaimTypes.Permission})
            {
            }
        }

        public sealed class Game : IdentityResource
        {
            public Game() : base(
                "games",
                "Your game(s)",
                new HashSet<string>
                {
                    Seedwork.Domain.Enumerations.Game.LeagueOfLegends.GetClaimType()
                })
            {
            }
        }

        public sealed class Stripe : IdentityResource
        {
            public Stripe() : base(
                "stripe",
                "Your Stripe account",
                new HashSet<string> {CustomClaimTypes.CustomerId})
            {
            }
        }
    }
}