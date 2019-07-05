// Filename: HttpContextAccessorExtensions.cs
// Date Created: 2019-07-03
// 
// ================================================
// Copyright © 2019, eDoxa. All rights reserved.
// 
// This file is subject to the terms and conditions
// defined in file 'LICENSE.md', which is part of
// this source code package.

using System;

using eDoxa.Seedwork.Security.Constants;
using eDoxa.Seedwork.Security.Extensions;

using JetBrains.Annotations;

using Microsoft.AspNetCore.Http;

namespace eDoxa.Cashier.Api.Extensions
{
    public static class HttpContextAccessorExtensions
    {
        [CanBeNull]
        public static string GetCustomerId(this IHttpContextAccessor accessor)
        {
            return accessor.HttpContext?.GetClaimOrDefault(CustomClaimTypes.StripeCustomerId) ?? throw new ArgumentException(nameof(CustomClaimTypes.StripeCustomerId));
        }

        [CanBeNull]
        public static string GetConnectAccountId(this IHttpContextAccessor accessor)
        {
            return accessor.HttpContext?.GetClaimOrDefault(CustomClaimTypes.StripeConnectAccountId) ?? throw new ArgumentException(nameof(CustomClaimTypes.StripeConnectAccountId));
        }
    }
}
