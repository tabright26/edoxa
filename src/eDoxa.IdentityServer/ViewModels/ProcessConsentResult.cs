// Filename: ProcessConsentResult.cs
// Date Created: 2019-06-01
// 
// ================================================
// Copyright © 2019, eDoxa. All rights reserved.
// 
// This file is subject to the terms and conditions
// defined in file 'LICENSE.md', which is part of
// this source code package.

namespace eDoxa.IdentityServer.ViewModels
{
    public class ProcessConsentResult
    {
        public bool IsRedirect => RedirectUri != null;

        public string RedirectUri { get; set; }

        public string ClientId { get; set; }

        public bool ShowView => ViewModel != null;

        public ConsentViewModel ViewModel { get; set; }

        public bool HasValidationError => ValidationError != null;

        public string ValidationError { get; set; }
    }
}
