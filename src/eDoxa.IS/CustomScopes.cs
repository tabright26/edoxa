// Filename: CustomScopes.cs
// Date Created: 2019-04-13
// 
// ============================================================
// Copyright © 2019, Francis Quenneville
// All rights reserved.
// 
// This file is subject to the terms and conditions defined in file 'LICENSE.md', which is part of
// this source code package.

namespace eDoxa.IS
{
    public static class CustomScopes
    {
        public static readonly string IdentityApi = new CustomApiResources.IdentityApi().Name;

        public static readonly string CashierApi = new CustomApiResources.CashierApi().Name;

        public static readonly string ChallengeApi = new CustomApiResources.ChallengeApi().Name;
    }
}