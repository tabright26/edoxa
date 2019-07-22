// Filename: CustomScopes.cs
// Date Created: 2019-06-25
// 
// ================================================
// Copyright © 2019, eDoxa. All rights reserved.
// 
// This file is subject to the terms and conditions
// defined in file 'LICENSE.md', which is part of
// this source code package.

using eDoxa.Seedwork.Security.IdentityServer.Resources;

namespace eDoxa.Seedwork.Security.Constants
{
    public static class CustomScopes
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
