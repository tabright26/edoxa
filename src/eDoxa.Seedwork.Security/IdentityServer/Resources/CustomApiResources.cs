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
        public static readonly ApiResource Aggregator = new WebAggregator();

        public sealed class IdentityApi : ApiResource
        {
            internal IdentityApi() : base(
                "identity.api",
                "eDoxa Identity API",
                new CustomIdentityResources.Role().UserClaims.Union(new CustomIdentityResources.Permission().UserClaims)
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
                new IdentityResources.Profile().UserClaims.Union(
                    new CustomIdentityResources.Role().UserClaims.Union(new CustomIdentityResources.Permission().UserClaims)
                )
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
                new IdentityResources.Profile().UserClaims.Union(
                    new CustomIdentityResources.Role().UserClaims.Union(new CustomIdentityResources.Permission().UserClaims)
                )
            )

            {
                ApiSecrets = new HashSet<Secret>
                {
                    new Secret("secret".Sha256())
                };
            }
        }

        public sealed class WebAggregator : ApiResource
        {
            internal WebAggregator() : base("web.aggregator", "eDoxa Web Aggregator")
            {
                ApiSecrets = new HashSet<Secret>
                {
                    new Secret("secret".Sha256())
                };
            }
        }
    }
}
