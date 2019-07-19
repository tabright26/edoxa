// Filename: CustomApiResources.cs
// Date Created: 2019-06-25
// 
// ================================================
// Copyright © 2019, eDoxa. All rights reserved.
// 
// This file is subject to the terms and conditions
// defined in file 'LICENSE.md', which is part of
// this source code package.

using System.Collections.Generic;
using System.Linq;

using IdentityServer4.Models;

namespace eDoxa.Seedwork.Security.IdentityServer.Resources
{
    public sealed class CustomApiResources
    {
        public static readonly ApiResource Identity = new IdentityApi();
        public static readonly ApiResource Cashier = new CashierApi();
        public static readonly ApiResource ArenaChallenges = new ChallengeApi();

        public sealed class IdentityApi : ApiResource
        {
            internal IdentityApi() : base(
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

        public sealed class CashierApi : ApiResource
        {
            internal CashierApi() : base(
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

        public sealed class ChallengeApi : ApiResource
        {
            internal ChallengeApi() : base(
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
