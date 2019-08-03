// Filename: ConsentOptions.cs
// Date Created: 2019-06-01
// 
// ================================================
// Copyright © 2019, eDoxa. All rights reserved.
// 
// This file is subject to the terms and conditions
// defined in file 'LICENSE.md', which is part of
// this source code package.

namespace eDoxa.Identity.Api.ViewModels
{
    public static class ConsentOptions
    {
        public static bool EnableOfflineAccess = true;
        public const string OfflineAccessDisplayName = "Offline Access";
        public const string OfflineAccessDescription = "Access to your applications and resources, even when you are offline";

        public const string MustChooseOneErrorMessage = "You must pick at least one permission";
        public const string InvalidSelectionErrorMessage = "Invalid selection";
    }
}
