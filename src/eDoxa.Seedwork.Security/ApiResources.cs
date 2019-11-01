// Filename: ApiResources.cs
// Date Created: 2019-10-06
// 
// ================================================
// Copyright © 2019, eDoxa. All rights reserved.

using System.Linq;

using IdentityServer4.Models;

namespace eDoxa.Seedwork.Security
{
    public sealed class ApiResources
    {
        public static readonly ApiResource IdentityApi = new IdentityResource();
        public static readonly ApiResource CashierApi = new CashierResource();
        public static readonly ApiResource ArenaChallengesApi = new ArenaChallengesResource();
        public static readonly ApiResource ArenaGamesApi = new ArenaGamesResource();
        public static readonly ApiResource OrganizationsClansApi = new OrganizationsClansResource();
        public static readonly ApiResource NotificationsApi = new NotificationsResource();
        public static readonly ApiResource PaymentApi = new PaymentResource();

        public sealed class PaymentResource : ApiResource
        {
            internal PaymentResource() : base(
                Security.Scopes.PaymentApi.Name,
                "eDoxa Payment API",
                IdentityResources.Roles.UserClaims.Union(IdentityResources.Permissions.UserClaims))
            {
                ApiSecrets.Add(new Secret("secret".Sha256()));
            }
        }

        public sealed class NotificationsResource : ApiResource
        {
            internal NotificationsResource() : base(
                Security.Scopes.NotificationsApi.Name,
                "eDoxa Notifications API",
                IdentityResources.Roles.UserClaims.Union(IdentityResources.Permissions.UserClaims))
            {
                ApiSecrets.Add(new Secret("secret".Sha256()));
            }
        }

        public sealed class OrganizationsClansResource : ApiResource
        {
            internal OrganizationsClansResource() : base(
                Security.Scopes.OrganizationsClansApi.Name,
                "eDoxa Organizations Clans API",
                IdentityResources.Roles.UserClaims.Union(IdentityResources.Permissions.UserClaims).Union(IdentityResources.Games.UserClaims))
            {
                ApiSecrets.Add(new Secret("secret".Sha256()));
            }
        }

        public sealed class IdentityResource : ApiResource
        {
            internal IdentityResource() : base(
                Security.Scopes.IdentityApi.Name,
                "eDoxa Identity API",
                IdentityResources.Roles.UserClaims.Union(IdentityResources.Permissions.UserClaims).Union(IdentityResources.Games.UserClaims))
            {
                ApiSecrets.Add(new Secret("secret".Sha256()));
            }
        }

        public sealed class CashierResource : ApiResource
        {
            internal CashierResource() : base(
                Security.Scopes.CashierApi.Name,
                "eDoxa Cashier API",
                IdentityResources.Roles.UserClaims.Union(IdentityResources.Permissions.UserClaims))
            {
                ApiSecrets.Add(new Secret("secret".Sha256()));
            }
        }

        public sealed class ArenaChallengesResource : ApiResource
        {
            internal ArenaChallengesResource() : base(
                Security.Scopes.ArenaChallengesApi.Name,
                "eDoxa Arena Challenges API",
                IdentityResources.Roles.UserClaims.Union(IdentityResources.Permissions.UserClaims))

            {
                ApiSecrets.Add(new Secret("secret".Sha256()));
            }
        }

        public sealed class ArenaGamesResource : ApiResource
        {
            internal ArenaGamesResource() : base(
                Security.Scopes.ArenaGamesApi.Name,
                "eDoxa Arena Games API",
                IdentityResources.Roles.UserClaims.Union(IdentityResources.Permissions.UserClaims))
            {
                ApiSecrets.Add(new Secret("secret".Sha256()));
            }
        }
    }
}
