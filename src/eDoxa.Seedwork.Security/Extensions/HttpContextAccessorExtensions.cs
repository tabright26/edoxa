// Filename: HttpContextAccessorExtensions.cs
// Date Created: 2019-06-01
// 
// ================================================
// Copyright © 2019, eDoxa. All rights reserved.
// 
// This file is subject to the terms and conditions
// defined in file 'LICENSE.md', which is part of
// this source code package.

using System;

using eDoxa.Seedwork.Domain.Common;

using IdentityModel;

using JetBrains.Annotations;

using Microsoft.AspNetCore.Http;

namespace eDoxa.Seedwork.Security.Extensions
{
    public static class HttpContextAccessorExtensions
    {
        public static UserId GetUserId(this IHttpContextAccessor accessor)
        {
            return UserId.Parse(accessor.GetClaimOrDefault(JwtClaimTypes.Subject) ?? throw new NullReferenceException(JwtClaimTypes.Subject));
        }

        [CanBeNull]
        public static string GetClaimOrDefault(this IHttpContextAccessor accessor, string claimType)
        {
            return accessor.HttpContext?.GetClaimOrDefault(claimType);
        }
    }
}
