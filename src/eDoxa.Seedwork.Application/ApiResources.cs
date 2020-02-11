// Filename: ApiResources.cs
// Date Created: 2019-10-06
// 
// ================================================
// Copyright © 2019, eDoxa. All rights reserved.

using System.Linq;

using IdentityServer4.Models;

namespace eDoxa.Seedwork.Application
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
        public static readonly ApiResource ChallengesWebAggregator = new ChallengesWebAggregatorResource();
        public static readonly ApiResource CashierWebAggregator = new CashierWebAggregatorResource();

        public sealed class PaymentResource : ApiResource
        {
            internal PaymentResource() : base(
                Application.Scopes.PaymentApi.Name,
                Application.Scopes.PaymentApi.DisplayName,
                IdentityResources.Roles.UserClaims.Union(IdentityResources.Permissions.UserClaims.Union(IdentityResources.Stripe.UserClaims)))
            {
                ApiSecrets.Add(new Secret("secret".Sha256()));
            }
        }

        public sealed class NotificationsResource : ApiResource
        {
            internal NotificationsResource() : base(
                Application.Scopes.NotificationsApi.Name,
                Application.Scopes.NotificationsApi.DisplayName,
                IdentityResources.Roles.UserClaims.Union(IdentityResources.Permissions.UserClaims))
            {
                ApiSecrets.Add(new Secret("secret".Sha256()));
            }
        }

        public sealed class ClansResource : ApiResource
        {
            internal ClansResource() : base(
                Application.Scopes.ClansApi.Name,
                Application.Scopes.ClansApi.DisplayName,
                IdentityResources.Roles.UserClaims.Union(IdentityResources.Permissions.UserClaims).Union(IdentityResources.Games.UserClaims))
            {
                ApiSecrets.Add(new Secret("secret".Sha256()));
            }
        }

        public sealed class IdentityResource : ApiResource
        {
            internal IdentityResource() : base(
                Application.Scopes.IdentityApi.Name,
                Application.Scopes.IdentityApi.DisplayName,
                IdentityResources.Roles.UserClaims.Union(IdentityResources.Permissions.UserClaims).Union(IdentityResources.Games.UserClaims))
            {
                ApiSecrets.Add(new Secret("secret".Sha256()));
            }
        }

        public sealed class CashierResource : ApiResource
        {
            internal CashierResource() : base(
                Application.Scopes.CashierApi.Name,
                Application.Scopes.CashierApi.DisplayName,
                IdentityResources.Roles.UserClaims.Union(IdentityResources.Permissions.UserClaims))
            {
                ApiSecrets.Add(new Secret("secret".Sha256()));
            }
        }

        public sealed class ChallengesResource : ApiResource
        {
            internal ChallengesResource() : base(
                Application.Scopes.ChallengesApi.Name,
                Application.Scopes.ChallengesApi.DisplayName,
                IdentityResources.Roles.UserClaims.Union(IdentityResources.Permissions.UserClaims))

            {
                ApiSecrets.Add(new Secret("secret".Sha256()));
            }
        }

        public sealed class GamesResource : ApiResource
        {
            internal GamesResource() : base(
                Application.Scopes.GamesApi.Name,
                Application.Scopes.GamesApi.DisplayName,
                IdentityResources.Roles.UserClaims.Union(IdentityResources.Permissions.UserClaims))
            {
                ApiSecrets.Add(new Secret("secret".Sha256()));
            }
        }

        public sealed class ChallengesWebAggregatorResource : ApiResource
        {
            internal ChallengesWebAggregatorResource() : base(
                Application.Scopes.ChallengesWebAggregator.Name,
                Application.Scopes.ChallengesWebAggregator.DisplayName,
                IdentityResources.Roles.UserClaims.Union(IdentityResources.Permissions.UserClaims))
            {
                ApiSecrets.Add(new Secret("secret".Sha256()));
            }
        }

        public sealed class CashierWebAggregatorResource : ApiResource
        {
            internal CashierWebAggregatorResource() : base(
                Application.Scopes.CashierWebAggregator.Name,
                Application.Scopes.CashierWebAggregator.DisplayName,
                IdentityResources.Roles.UserClaims.Union(IdentityResources.Permissions.UserClaims))
            {
                ApiSecrets.Add(new Secret("secret".Sha256()));
            }
        }
    }
}
