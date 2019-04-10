// Filename: ProcessConsentResult.cs
// Date Created: 2019-03-04
// 
// ============================================================
// Copyright © 2019, Francis Quenneville
// All rights reserved.
// 
// This file is subject to the terms and conditions defined in file 'LICENSE.md', which is part of
// this source code package.

namespace eDoxa.Identity.Areas.Identity.ViewModels.Consent
{
    public class ProcessConsentResult
    {
        public string RedirectUri { get; set; }
        public string ClientId { get; set; }

        public ConsentViewModel ViewModel { get; set; }

        public string ValidationError { get; set; }

        public bool IsRedirect
        {
            get
            {
                return RedirectUri != null;
            }
        }

        public bool ShowView
        {
            get
            {
                return ViewModel != null;
            }
        }

        public bool HasValidationError
        {
            get
            {
                return ValidationError != null;
            }
        }
    }
}