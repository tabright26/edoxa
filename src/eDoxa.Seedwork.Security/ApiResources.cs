// Filename: ApiResources.cs
// Date Created: 2019-09-02
// 
// ================================================
// Copyright © 2019, eDoxa. All rights reserved.

using System.Collections.Generic;
using System.Linq;

using IdentityServer4.Models;

namespace eDoxa.Seedwork.Security
{
    public sealed class ApiResources
    {
        public static readonly ApiResource IdentityApi = new IdentityResource();
        public static readonly ApiResource CashierApi = new CashierResource();
        public static readonly ApiResource ArenaChallengesApi = new ArenaChallengesResource();

        public sealed class IdentityResource : ApiResource
        {
            internal IdentityResource() : base(
                Security.Scopes.IdentityApi,
                "eDoxa Identity API",
                IdentityResources.Roles.UserClaims.Union(IdentityResources.Permissions.UserClaims).Union(IdentityResources.Games.UserClaims))
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
                Security.Scopes.CashierApi,
                "eDoxa Cashier API",
                IdentityResources.Roles.UserClaims.Union(IdentityResources.Permissions.UserClaims.Union(IdentityResources.Stripe.UserClaims)))
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
                Security.Scopes.ArenaChallengesApi,
                "eDoxa Arena Challenges API",
                IdentityResources.Roles.UserClaims.Union(IdentityResources.Permissions.UserClaims))

            {
                ApiSecrets = new HashSet<Secret>
                {
                    new Secret("secret".Sha256())
                };
            }
        }
    }
}
