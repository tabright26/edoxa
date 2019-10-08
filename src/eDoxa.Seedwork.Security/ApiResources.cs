// Filename: ApiResources.cs
// Date Created: 2019-09-29
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
        public static readonly ApiResource ArenaGamesLeagueOfLegendsApi = new ArenaGamesLeagueOfLegendsResource();
        public static readonly ApiResource OrganizationsClansApi = new OrganizationsClansResource();
        public static readonly ApiResource NotificationsApi = new NotificationsResource();
        public static readonly ApiResource PaymentApi = new PaymentResource();

        public sealed class PaymentResource : ApiResource
        {
            internal PaymentResource() : base(
                Security.Scopes.PaymentApi,
                "eDoxa Payment API",
                IdentityResources.Roles.UserClaims.Union(IdentityResources.Permissions.UserClaims))
            {
                ApiSecrets = new HashSet<Secret>
                {
                    new Secret("secret".Sha256())
                };
            }
        }

        public sealed class NotificationsResource : ApiResource
        {
            internal NotificationsResource() : base(
                Security.Scopes.NotificationsApi,
                "eDoxa Notifications API",
                IdentityResources.Roles.UserClaims.Union(IdentityResources.Permissions.UserClaims))
            {
                ApiSecrets = new HashSet<Secret>
                {
                    new Secret("secret".Sha256())
                };
            }
        }

        public sealed class OrganizationsClansResource : ApiResource
        {
            internal OrganizationsClansResource() : base(
                Security.Scopes.OrganizationsClans,
                "eDoxa Organizations Clans API",
                IdentityResources.Roles.UserClaims.Union(IdentityResources.Permissions.UserClaims).Union(IdentityResources.Games.UserClaims))
            {
                ApiSecrets = new HashSet<Secret>
                {
                    new Secret("secret".Sha256())
                };
            }
        }

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
                IdentityResources.Roles.UserClaims.Union(IdentityResources.Permissions.UserClaims))
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

        public sealed class ArenaGamesLeagueOfLegendsResource : ApiResource
        {
            internal ArenaGamesLeagueOfLegendsResource() : base(
                Security.Scopes.ArenaGamesLeagueOfLegendsApi,
                "eDoxa Arena Games League Of Legends API",
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
