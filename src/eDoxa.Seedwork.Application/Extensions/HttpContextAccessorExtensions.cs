// Filename: HttpContextAccessorExtensions.cs
// Date Created: 2019-09-02
// 
// ================================================
// Copyright © 2019, eDoxa. All rights reserved.

using Microsoft.AspNetCore.Http;

namespace eDoxa.Seedwork.Application.Extensions
{
    public static class HttpContextAccessorExtensions
    {
        public static string? GetClaimOrDefault(this IHttpContextAccessor accessor, string claimType)
        {
            return accessor.HttpContext?.GetClaimOrDefault(claimType);
        }
    }
}
