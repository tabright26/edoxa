// Filename: CustomIdentityResources.cs
// Date Created: 2019-06-08
// 
// ================================================
// Copyright © 2019, eDoxa. All rights reserved.
// 
// This file is subject to the terms and conditions
// defined in file 'LICENSE.md', which is part of
// this source code package.

using System.Collections.Generic;

using eDoxa.Seedwork.Common.Enumerations;
using eDoxa.Seedwork.Security.Constants;
using eDoxa.Seedwork.Security.Extensions;

using IdentityModel;

using IdentityServer4.Models;

namespace eDoxa.Seedwork.Security.IdentityServer.Resources
{
    public sealed class CustomIdentityResources
    {
        public sealed class Role : IdentityResource
        {
            public Role() : base(
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
            public Permission() : base(
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

        public sealed class UserGameReference : IdentityResource
        {
            public UserGameReference() : base(
                "games",
                "Your game(s)",
                new HashSet<string>
                {
                    Game.LeagueOfLegends.GetClaimType()
                }
            )
            {
            }
        }
    }
}
