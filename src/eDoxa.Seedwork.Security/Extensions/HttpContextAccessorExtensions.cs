﻿// Filename: HttpContextAccessorExtensions.cs
// Date Created: 2019-06-08
// 
// ================================================
// Copyright © 2019, eDoxa. All rights reserved.
// 
// This file is subject to the terms and conditions
// defined in file 'LICENSE.md', which is part of
// this source code package.

using JetBrains.Annotations;

using Microsoft.AspNetCore.Http;

namespace eDoxa.Seedwork.Security.Extensions
{
    public static class HttpContextAccessorExtensions
    {
        [CanBeNull]
        public static string GetClaimOrDefault(this IHttpContextAccessor accessor, string claimType)
        {
            return accessor.HttpContext?.GetClaimOrDefault(claimType);
        }
    }
}