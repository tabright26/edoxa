// Filename: CustomApiResources.cs
// Date Created: 2019-08-18
// 
// ================================================
// Copyright © 2019, eDoxa. All rights reserved.

using System.Collections.Generic;
using System.Linq;

using IdentityServer4.Models;

namespace eDoxa.Seedwork.Security.IdentityServer.Resources
{
    public sealed class CustomApiResources
    {
        public static readonly ApiResource IdentityApi = new IdentityResource();
        public static readonly ApiResource CashierApi = new CashierResource();
        public static readonly ApiResource ArenaChallengesApi = new ArenaChallengesResource();

        public sealed class IdentityResource : ApiResource
        {
            internal IdentityResource() : base(
                "identity.api",
                "eDoxa Identity API",
                CustomIdentityResources.Roles.UserClaims.Union(CustomIdentityResources.Permissions.UserClaims).Union(CustomIdentityResources.Games.UserClaims)
            )
            {
                ApiSecrets = new HashSet<Secret>
                {
                    new Secret("secret".Sha256())
                };
            }
        }

        public sealed class CashierResource : ApiResource
        {
            internal CashierResource() : base(
                "cashier.api",
                "eDoxa Cashier API",
                CustomIdentityResources.Roles.UserClaims.Union(CustomIdentityResources.Permissions.UserClaims.Union(CustomIdentityResources.Stripe.UserClaims))
            )
            {
                ApiSecrets = new HashSet<Secret>
                {
                    new Secret("secret".Sha256())
                };
            }
        }

        public sealed class ArenaChallengesResource : ApiResource
        {
            internal ArenaChallengesResource() : base(
                "arena.challenges.api",
                "eDoxa Arena Challenges API",
                CustomIdentityResources.Roles.UserClaims.Union(CustomIdentityResources.Permissions.UserClaims)
            )

            {
                ApiSecrets = new HashSet<Secret>
                {
                    new Secret("secret".Sha256())
                };
            }
        }
    }
}
