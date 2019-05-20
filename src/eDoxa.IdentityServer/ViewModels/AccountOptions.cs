// Filename: AccountOptions.cs
// Date Created: 2019-05-20
// 
// ================================================
// Copyright © 2019, eDoxa. All rights reserved.
// 
// This file is subject to the terms and conditions
// defined in file 'LICENSE.md', which is part of
// this source code package.

using System;

using Microsoft.AspNetCore.Server.IISIntegration;

namespace eDoxa.IdentityServer.ViewModels
{
    public static class AccountOptions
    {
        public const bool AllowLocalLogin = true;
        public const bool AllowRememberLogin = true;
        public static TimeSpan RememberMeLoginDuration = TimeSpan.FromDays(30);

        public const bool ShowLogoutPrompt = false;
        public const bool AutomaticRedirectAfterSignOut = true;

        // specify the Windows authentication scheme being used
        public static readonly string WindowsAuthenticationSchemeName = IISDefaults.AuthenticationScheme;

        // if user uses windows auth, should we load the groups from windows
        public const bool IncludeWindowsGroups = false;

        public const string InvalidCredentialsErrorMessage = "Invalid email or password";
    }
}
