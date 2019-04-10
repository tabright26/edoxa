// Filename: ManageNavPages.cs
// Date Created: 2019-03-04
// 
// ============================================================
// Copyright © 2019, Francis Quenneville
// All rights reserved.
// 
// This file is subject to the terms and conditions defined in file 'LICENSE.md', which is part of
// this source code package.

using System;
using System.IO;

using Microsoft.AspNetCore.Mvc.Rendering;

namespace eDoxa.Identity.Areas.Identity.Pages.Account.Manage
{
    public static class ManageNavPages
    {
        public static string Index
        {
            get
            {
                return "ACCOUNT OVERVIEW";
            }
        }

        public static string Details
        {
            get
            {
                return "ACCOUNT DETAILS";
            }
        }

        public static string Subscriptions
        {
            get
            {
                return "SUBSCRIPTIONS";
            }
        }

        public static string ChangePassword
        {
            get
            {
                return "ChangePassword";
            }
        }

        public static string Security
        {
            get
            {
                return "SECURITY";
            }
        }

        public static string Privacy
        {
            get
            {
                return "PRIVACY";
            }
        }

        public static string Connections
        {
            get
            {
                return "CONNECTIONS";
            }
        }

        public static string PersonalData
        {
            get
            {
                return "PersonalData";
            }
        }

        public static string TwoFactorAuthentication
        {
            get
            {
                return "TwoFactorAuthentication";
            }
        }

        public static string IndexNavClass(ViewContext viewContext)
        {
            return PageNavClass(viewContext, Index);
        }

        public static string DetailsNavClass(ViewContext viewContext)
        {
            return PageNavClass(viewContext, Details);
        }

        public static string SubscriptionsNavClass(ViewContext viewContext)
        {
            return PageNavClass(viewContext, Subscriptions);
        }

        public static string ChangePasswordNavClass(ViewContext viewContext)
        {
            return PageNavClass(viewContext, ChangePassword);
        }

        public static string SecurityNavClass(ViewContext viewContext)
        {
            return PageNavClass(viewContext, Security);
        }

        public static string PrivacyNavClass(ViewContext viewContext)
        {
            return PageNavClass(viewContext, Privacy);
        }

        public static string ConnectionsNavClass(ViewContext viewContext)
        {
            return PageNavClass(viewContext, Connections);
        }

        public static string PersonalDataNavClass(ViewContext viewContext)
        {
            return PageNavClass(viewContext, PersonalData);
        }

        public static string TwoFactorAuthenticationNavClass(ViewContext viewContext)
        {
            return PageNavClass(viewContext, TwoFactorAuthentication);
        }

        private static string PageNavClass(ViewContext viewContext, string page)
        {
            var activePage = viewContext.ViewData["ActivePage"] as string ?? Path.GetFileNameWithoutExtension(viewContext.ActionDescriptor.DisplayName);
            return string.Equals(activePage, page, StringComparison.OrdinalIgnoreCase) ? "active" : "bg-dark text-white";
        }
    }
}