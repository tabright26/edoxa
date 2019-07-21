// Filename: CustomClaimTypes.cs
// Date Created: 2019-06-08
// 
// ================================================
// Copyright © 2019, eDoxa. All rights reserved.
// 
// This file is subject to the terms and conditions
// defined in file 'LICENSE.md', which is part of
// this source code package.

namespace eDoxa.Seedwork.Security.Constants
{
    public static class CustomClaimTypes
    {
        public const string SecurityStamp = "security_stamp";
        public const string Permission = "permission";
        public const string Games = "games";
        public const string StripeCustomerId = "stripe:customerId";
        public const string StripeConnectAccountId = "stripe:connectAccountId";
    }
}
