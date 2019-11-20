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
        public static readonly ApiResource GamesApi = new GamesResource();
        public static readonly ApiResource ClansApi = new ClansResource();
        public static readonly ApiResource NotificationsApi = new NotificationsResource();
        public static readonly ApiResource PaymentApi = new PaymentResource();
        public static readonly ApiResource ChallengesApi = new ChallengesResource();
        public static readonly ApiResource ChallengesWebAggregator = new ChallengesAggregateResource();

        public sealed class PaymentResource : ApiResource
        {
            internal PaymentResource() : base(
                Security.Scopes.PaymentApi.Name,
                Security.Scopes.PaymentApi.DisplayName,
                IdentityResources.Roles.UserClaims.Union(IdentityResources.Permissions.UserClaims))
            {
                ApiSecrets.Add(new Secret("secret".Sha256()));
            }
        }

        public sealed class NotificationsResource : ApiResource
        {
            internal NotificationsResource() : base(
                Security.Scopes.NotificationsApi.Name,
                Security.Scopes.NotificationsApi.DisplayName,
                IdentityResources.Roles.UserClaims.Union(IdentityResources.Permissions.UserClaims))
            {
                ApiSecrets.Add(new Secret("secret".Sha256()));
            }
        }

        public sealed class ClansResource : ApiResource
        {
            internal ClansResource() : base(
                Security.Scopes.ClansApi.Name,
                Security.Scopes.ClansApi.DisplayName,
                IdentityResources.Roles.UserClaims.Union(IdentityResources.Permissions.UserClaims).Union(IdentityResources.Games.UserClaims))
            {
                ApiSecrets.Add(new Secret("secret".Sha256()));
            }
        }

        public sealed class IdentityResource : ApiResource
        {
            internal IdentityResource() : base(
                Security.Scopes.IdentityApi.Name,
                Security.Scopes.IdentityApi.DisplayName,
                IdentityResources.Roles.UserClaims.Union(IdentityResources.Permissions.UserClaims).Union(IdentityResources.Games.UserClaims))
            {
                ApiSecrets.Add(new Secret("secret".Sha256()));
            }
        }

        public sealed class CashierResource : ApiResource
        {
            internal CashierResource() : base(
                Security.Scopes.CashierApi.Name,
                Security.Scopes.CashierApi.DisplayName,
                IdentityResources.Roles.UserClaims.Union(IdentityResources.Permissions.UserClaims))
            {
                ApiSecrets.Add(new Secret("secret".Sha256()));
            }
        }

        public sealed class ChallengesResource : ApiResource
        {
            internal ChallengesResource() : base(
                Security.Scopes.ChallengesApi.Name,
                Security.Scopes.ChallengesApi.DisplayName,
                IdentityResources.Roles.UserClaims.Union(IdentityResources.Permissions.UserClaims))

            {
                ApiSecrets.Add(new Secret("secret".Sha256()));
            }
        }

        public sealed class GamesResource : ApiResource
        {
            internal GamesResource() : base(
                Security.Scopes.GamesApi.Name,
                Security.Scopes.GamesApi.DisplayName,
                IdentityResources.Roles.UserClaims.Union(IdentityResources.Permissions.UserClaims))
            {
                ApiSecrets.Add(new Secret("secret".Sha256()));
            }
        }

        public sealed class ChallengesAggregateResource : ApiResource
        {
            internal ChallengesAggregateResource() : base(
                Security.Scopes.ChallengesWebAggregator.Name,
                Security.Scopes.ChallengesWebAggregator.DisplayName,
                IdentityResources.Roles.UserClaims.Union(IdentityResources.Permissions.UserClaims))
            {
                ApiSecrets.Add(new Secret("secret".Sha256()));
            }
        }
    }
}
