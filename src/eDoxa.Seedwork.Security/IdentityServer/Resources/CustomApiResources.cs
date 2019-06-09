// Filename: CustomApiResources.cs
// Date Created: 2019-06-08
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
        public sealed class IdentityApi : ApiResource
        {
            public IdentityApi() : base(
                "edoxa.identity.api",
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
            public CashierApi() : base(
                "edoxa.cashier.api",
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
            public ChallengeApi() : base(
                "edoxa.challenge.api",
                "eDoxa Challenge API",
                new IdentityResources.Profile().UserClaims.Union(
                    new CustomIdentityResources.Role().UserClaims.Union(
                        new CustomIdentityResources.Permission().UserClaims.Union(new CustomIdentityResources.ExternalAccount().UserClaims)
                    )
                )
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
