// Filename: CustomClaimTypes.cs
// Date Created: 2019-03-04
// 
// ============================================================
// Copyright © 2019, Francis Quenneville
// All rights reserved.
// 
// This file is subject to the terms and conditions defined in file 'LICENSE.md', which is part of
// this source code package.

namespace eDoxa.Security
{
    public static class CustomClaimTypes
    {
        public const string RoleClaimType = "https://edoxa.gg/identity/claims/role";

        public const string UserClanIdClaimType = "https://edoxa.gg/identity/claims/clan/identifier";
        public const string UserCustomerIdClaimType = "https://edoxa.gg/identity/claims/customer/identifier";
    }
}