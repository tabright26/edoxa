// Filename: AppScopes.cs
// Date Created: 2019-08-18
// 
// ================================================
// Copyright © 2019, eDoxa. All rights reserved.

using eDoxa.Seedwork.Security.IdentityServer.Resources;

namespace eDoxa.Seedwork.Security
{
    public static class AppScopes
    {
        public static readonly string Roles = CustomIdentityResources.Roles.Name;
        public static readonly string Permissions = CustomIdentityResources.Permissions.Name;
        public static readonly string Stripe = CustomIdentityResources.Stripe.Name;
        public static readonly string Games = CustomIdentityResources.Games.Name;
        public static readonly string IdentityApi = CustomApiResources.IdentityApi.Name;
        public static readonly string CashierApi = CustomApiResources.CashierApi.Name;
        public static readonly string ArenaChallengesApi = CustomApiResources.ArenaChallengesApi.Name;
    }
}
