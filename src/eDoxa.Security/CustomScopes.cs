﻿// Filename: CustomScopes.cs
// Date Created: 2019-05-02
// 
// ================================================
// Copyright © 2019, eDoxa. All rights reserved.
// 
// This file is subject to the terms and conditions
// defined in file 'LICENSE.md', which is part of
// this source code package.

using eDoxa.Security.Resources;

namespace eDoxa.Security
{
    public static class CustomScopes
    {
        public static readonly string Roles = new CustomIdentityResources.Role().Name;

        public static readonly string Permissions = new CustomIdentityResources.Permission().Name;

        public static readonly string IdentityApi = new CustomApiResources.IdentityApi().Name;

        public static readonly string CashierApi = new CustomApiResources.CashierApi().Name;

        public static readonly string ChallengeApi = new CustomApiResources.ChallengeApi().Name;
    }
}